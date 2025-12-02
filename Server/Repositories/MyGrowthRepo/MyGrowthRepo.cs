using System;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Connections;
using Npgsql;

namespace Server.Repositories.MyGrowthRepo;

public class MyGrowthRepo : IMyGrowthRepo
{
    private const string connectionString =
        @"Server=ep-long-sea-agyfr4ak-pooler.c-2.eu-central-1.aws.neon.tech;
          User Id=neondb_owner;Password=npg_rwHcEK1Li0Bs;Database=neondb";

    public async Task CreateAsync(MyGrowth growth)
    {
        await using var dbConnection = new NpgsqlConnection(connectionString);
        await dbConnection.OpenAsync();
        var command = dbConnection.CreateCommand();

        command.CommandText =
            "INSERT INTO mygrowth (checkinid, userid, month, answertext, answerdate, answerrid, questionid, fagligudfordring, nykompetence, motivation, trivsel) " +
            "VALUES (@checkinid, @userid, @month, @answertext, @answerdate, @answerid, @questionid, @fagligudfordring, @nykompetence, @motivation, @trivsel)";

        Console.WriteLine(command.CommandText);
        var paramCheckinid = command.CreateParameter();
        paramCheckinid.ParameterName = "@checkinid";
        command.Parameters.Add(paramCheckinid);
        paramCheckinid.Value = growth.checkinId;

        var paramUserid = command.CreateParameter();
        paramUserid.ParameterName = "@userid";
        command.Parameters.Add(paramUserid);
        paramUserid.Value = growth.userId;

        var paramMonth = command.CreateParameter();
        paramMonth.ParameterName = "@month";
        command.Parameters.Add(paramMonth);
        paramMonth.Value = growth.month;

        var paramAnswertext = command.CreateParameter();
        paramAnswertext.ParameterName = "@answertext";
        command.Parameters.Add(paramAnswertext);
        paramAnswertext.Value = growth.answerText;

        var paramAnswerdate = command.CreateParameter();
        paramAnswerdate.ParameterName = "@answerdate";
        command.Parameters.Add(paramAnswerdate);
        paramAnswerdate.Value = growth.answerDate;

        var paramAnswerid = command.CreateParameter();
        paramAnswerid.ParameterName = "@answrid";
        command.Parameters.Add(paramAnswerid);
        paramAnswerid.Value = growth.answerId;

        var paramQuestionid = command.CreateParameter();
        paramQuestionid.ParameterName = "@questionid";
        command.Parameters.Add(paramQuestionid);
        paramQuestionid.Value = growth.questionId;

        var paramFagligudfording = command.CreateParameter();
        paramFagligudfording.ParameterName = "@fagligudfording";
        command.Parameters.Add(paramFagligudfording);
        paramFagligudfording.Value = true;

        var paramNykompetence = command.CreateParameter();
        paramNykompetence.ParameterName = "@nykompetence";
        command.Parameters.Add(paramNykompetence);
        paramNykompetence.Value = true;

        var paramMotivation = command.CreateParameter();
        paramMotivation.ParameterName = "@motivation";
        command.Parameters.Add(paramMotivation);
        paramMotivation.Value = true;

        var paramTrivsel = command.CreateParameter();
        paramTrivsel.ParameterName = "@trivsel";
        command.Parameters.Add(paramTrivsel);
        paramTrivsel.Value = true;

        command.ExecuteNonQuery();

    }
}


















/* command.Commandtext = @"
INSERT INTO mygrowth
(userid, month, answertext, answerdate, answerid, questionid,
fagligudfordring, nykompetence, motivation, trivsel)
VALUES
(@userid, @month, @answertext, @answerdate, @answerid, @questionid,
@faglig, @nykomp, @motivation, @trivsel);";

Console.WriteLine(command.Commandtext);

const string sql */





             /*await using var command = dbConnection.CreateCommand();
             command.CommandText = sql;

             command.Parameters.AddWithValue("userid", growth.userId);
             command.Parameters.AddWithValue("month", growth.month);
             command.Parameters.AddWithValue("answertext", (object?)growth.answerText ?? DBNull.Value);
             command.Parameters.AddWithValue("answerdate", growth.answerDate);
             command.Parameters.AddWithValue("answerid", growth.answerId);
             command.Parameters.AddWithValue("questionid", growth.questionId);
             command.Parameters.AddWithValue("faglig", (object?)growth.FagligUdfordring ?? DBNull.Value);
             command.Parameters.AddWithValue("nykomp", (object?)growth.NyKompetence ?? DBNull.Value);
             command.Parameters.AddWithValue("motivation", (object?)growth.Motivation ?? DBNull.Value);
             command.Parameters.AddWithValue("trivsel", (object?)growth.Trivsel ?? DBNull.Value);

             await command.ExecuteNonQueryAsync(); */