using Core.DTOs;
using Npgsql;

namespace Server.Repositories.TestReposi;

public class TestRepo : ITestRepo
{
    private readonly string _cs = @"Server=ep-long-sea-agyfr4ak-pooler.c-2.eu-central-1.aws.neon.tech;
                                    User Id=neondb_owner;
                                    Password=npg_rwHcEK1Li0Bs;
                                    Database=neondb";

    public async Task<TestDto?> GetTestBySubjectAsync(int subjectId)
    {
        await using var db = new NpgsqlConnection(_cs);
        await db.OpenAsync();

        // ---------- HENT TEST ----------
        var cmdTest = new NpgsqlCommand(
            @"SELECT testid, subjectid, title, descriptiontext
              FROM tests
              WHERE subjectid = @sid",
            db);

        cmdTest.Parameters.AddWithValue("sid", subjectId);

        await using var r = await cmdTest.ExecuteReaderAsync();
        if (!await r.ReadAsync())
            return null; // ingen test i databasen

        var testDto = new TestDto
        {
            TestId = r.GetInt32(0),
            SubjectId = r.GetInt32(1),
            Title = r.GetString(2),
            DescriptionText = r.GetString(3)
        };

        r.Close();

        // ---------- HENT SPØRGSMÅL ----------
        var cmdQ = new NpgsqlCommand(
            @"SELECT questionid, questiontext
              FROM testquestions
              WHERE testid = @tid
              ORDER BY ""order"" ASC;",
            db);

        cmdQ.Parameters.AddWithValue("tid", testDto.TestId);

        var questions = new List<TestQuestionDto>();

        await using var qr = await cmdQ.ExecuteReaderAsync();
        while (await qr.ReadAsync())
        {
            questions.Add(new TestQuestionDto
            {
                QuestionId = qr.GetInt32(0),
                QuestionText = qr.GetString(1)
            });
        }
        qr.Close();

        // ---------- HENT OPTIONS TIL HVER SPØRGSMÅL ----------
        foreach (var q in questions)
        {
            var cmdOpt = new NpgsqlCommand(
                @"SELECT optionid, optiontext, iscorrect
                  FROM testquestionoptions
                  WHERE questionid = @qid
                  ORDER BY optionid ASC;",
                db);

            cmdOpt.Parameters.AddWithValue("qid", q.QuestionId);

            await using var or = await cmdOpt.ExecuteReaderAsync();
            while (await or.ReadAsync())
            {
                q.Options.Add(new TestQuestionOptionDto
                {
                    OptionId = or.GetInt32(0),
                    OptionText = or.GetString(1)
                });
            }
        }

        testDto.Questions = questions;

        return testDto;
    }


    // ---------- SUBMIT TEST ----------
    public async Task<TestSubmitResult> SubmitTestAsync(TestSubmitRequest req)
    {
        await using var db = new NpgsqlConnection(_cs);
        await db.OpenAsync();

        // Hent korrekte svar
        var correct = new Dictionary<int, int>();

        var cmd = new NpgsqlCommand(
            @"SELECT q.questionid, o.optionid
              FROM testquestions q
              JOIN testquestionoptions o ON q.questionid = o.questionid
              WHERE q.testid = @tid AND o.iscorrect = TRUE",
            db);

        cmd.Parameters.AddWithValue("tid", req.TestId);

        await using (var rd = await cmd.ExecuteReaderAsync())
        {
            while (await rd.ReadAsync())
            {
                correct[rd.GetInt32(0)] = rd.GetInt32(1);
            }
        }

        // Score
        int correctCount = 0;
        foreach (var ans in req.Answers)
        {
            if (correct.TryGetValue(ans.QuestionId, out var rightOption)
                && rightOption == ans.OptionId)
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

        // Gem submission
        var insert = new NpgsqlCommand(
            @"INSERT INTO testsubmissions (userid, testid, correctanswers, passed)
              VALUES (@uid, @tid, @corr, @pass)
              RETURNING submissionid;",
            db);

        insert.Parameters.AddWithValue("uid", req.UserId);
        insert.Parameters.AddWithValue("tid", req.TestId);
        insert.Parameters.AddWithValue("corr", correctCount);
        insert.Parameters.AddWithValue("pass", result.Passed);

        int submissionId = Convert.ToInt32(await insert.ExecuteScalarAsync());

        // Gem individuelle svar
        foreach (var ans in req.Answers)
        {
            var ansCmd = new NpgsqlCommand(
                @"INSERT INTO testsubmissionanswers (submissionid, questionid, optionid)
                  VALUES (@sid, @qid, @oid);",
                db);

            ansCmd.Parameters.AddWithValue("sid", submissionId);
            ansCmd.Parameters.AddWithValue("qid", ans.QuestionId);
            ansCmd.Parameters.AddWithValue("oid", ans.OptionId);

            await ansCmd.ExecuteNonQueryAsync();
        }

        return result;
    }
}



