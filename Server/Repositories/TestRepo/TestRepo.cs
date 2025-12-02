using Core.DTOs;
using Npgsql;

namespace Server.Repositories.TestRepo;

public class TestRepo : ITestRepo
{
    private readonly string _cs = @"Server=…;User Id=…;Password=…;Database=…";

    public async Task<TestDto?> GetTestBySubjectAsync(int subjectId)
    {
        using var db = new NpgsqlConnection(_cs);
        await db.OpenAsync();

        // Hent selve testen
        var cmd = new NpgsqlCommand(
            "SELECT * FROM tests WHERE subjectid=@sid", db);
        cmd.Parameters.AddWithValue("@sid", subjectId);

        using var reader = await cmd.ExecuteReaderAsync();
        if (!reader.Read()) return null;

        var testDto = new TestDto
        {
            TestId = reader.GetInt32(reader.GetOrdinal("testid")),
            SubjectId = subjectId,
            Title = reader.GetString(reader.GetOrdinal("title")),
            DescriptionText = reader.GetString(reader.GetOrdinal("descriptiontext"))
        };
        reader.Close();

        // hent spørgsmål
        cmd = new NpgsqlCommand(
            "SELECT * FROM testquestions WHERE testid=@tid ORDER BY \"order\" ASC", db);
        cmd.Parameters.AddWithValue("@tid", testDto.TestId);

        var questions = new List<TestQuestionDto>();
        using var qReader = await cmd.ExecuteReaderAsync();
        while (await qReader.ReadAsync())
        {
            questions.Add(new TestQuestionDto
            {
                QuestionId = qReader.GetInt32(0),
                QuestionText = qReader.GetString(qReader.GetOrdinal("questiontext"))
            });
        }
        qReader.Close();

        // hent options
        foreach (var q in questions)
        {
            cmd = new NpgsqlCommand(
                "SELECT optionid, optiontext FROM testquestionoptions WHERE questionid=@qid",
                db
            );
            cmd.Parameters.AddWithValue("@qid", q.QuestionId);

            using var oReader = await cmd.ExecuteReaderAsync();
            while (await oReader.ReadAsync())
            {
                q.Options.Add(new TestQuestionOptionDto
                {
                    OptionId = oReader.GetInt32(0),
                    OptionText = oReader.GetString(1)
                });
            }
        }

        testDto.Questions = questions;
        return testDto;
    }




    public async Task<TestSubmitResult> SubmitTestAsync(TestSubmitRequest req)
    {
        using var db = new NpgsqlConnection(_cs);
        await db.OpenAsync();

        // hent korrekte svar
        var correct = new Dictionary<int, int>();

        var cmd = new NpgsqlCommand(@"SELECT q.questionid, o.optionid FROM testquestions q JOIN testquestionoptions o ON q.questionid = o.questionid WHERE o.iscorrect = TRUE AND q.testid = @tid", db);

        cmd.Parameters.AddWithValue("@tid", req.TestId);

        using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync())
        {
            correct[rd.GetInt32(0)] = rd.GetInt32(1);
        }
        rd.Close();

        int correctCount = 0;

        foreach (var ans in req.Answers)
        {
            if (correct.TryGetValue(ans.QuestionId, out var correctOption) &&
                correctOption == ans.OptionId)
            {
                correctCount++;
            }
        }

        var result = new TestSubmitResult
        {
            CorrectAnswers = correctCount,
            TotalQuestions = correct.Count,
            Passed = correctCount >= 4
        };

        // gem submission
        cmd = new NpgsqlCommand(@"INSERT INTO testsubmissions (userid, testid, correctanswers, passed) VALUES (@uid, @tid, @corr, @pass) RETURNING submissionid", db);

        cmd.Parameters.AddWithValue("@uid", req.UserId);
        cmd.Parameters.AddWithValue("@tid", req.TestId);
        cmd.Parameters.AddWithValue("@corr", correctCount);
        cmd.Parameters.AddWithValue("@pass", result.Passed);

        int submissionId = Convert.ToInt32(await cmd.ExecuteScalarAsync());

        // gem answers
        foreach (var ans in req.Answers)
        {
            var aCmd = new NpgsqlCommand(@"INSERT INTO testsubmissionanswers (submissionid, questionid, optionid) VALUES (@sid, @qid, @oid)", db);

            aCmd.Parameters.AddWithValue("@sid", submissionId);
            aCmd.Parameters.AddWithValue("@qid", ans.QuestionId);
            aCmd.Parameters.AddWithValue("@oid", ans.OptionId);

            await aCmd.ExecuteNonQueryAsync();
        }

        // hvis bestået → afslut learningProgress
        if (result.Passed)
        {
            var finish = new NpgsqlCommand(@"UPDATE learningprogress SET status = TRUE, progresspercent = 100, completeddate = NOW() WHERE assignmentid = (SELECT assignmentid FROM learningassignment WHERE userid=@uid AND subjectid = (SELECT subjectid FROM tests WHERE testid=@tid))", db);

            finish.Parameters.AddWithValue("@uid", req.UserId);
            finish.Parameters.AddWithValue("@tid", req.TestId);
            await finish.ExecuteNonQueryAsync();
        }

        return result;
    }
}
