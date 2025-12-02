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

        await using var command = dbConnection.CreateCommand();

        command.CommandText = @"
        INSERT INTO mygrowth
        (userid, month, answerdate,
         fagligudfordring, nykompetence, motivation, trivsel, answertext)
        VALUES
        (@userid, @month, @answerdate,
         @fagligudfordring, @nykompetence, @motivation, @trivsel, @answertext);";

        
        var userParam = command.CreateParameter();
        userParam.ParameterName = "userid"; 
        userParam.Value = growth.userId;
        command.Parameters.Add(userParam);

      
        var monthParam = command.CreateParameter();
        monthParam.ParameterName = "month";
        monthParam.Value = growth.month;
        command.Parameters.Add(monthParam);

        
        var dateParam = command.CreateParameter();
        dateParam.ParameterName = "answerdate";
        dateParam.Value = growth.answerDate;
        command.Parameters.Add(dateParam);

        
        var fagligParam = command.CreateParameter();
        fagligParam.ParameterName = "fagligudfordring"; // stavet som kolonnen
        fagligParam.Value = (object?)growth.FagligUdfordring ?? DBNull.Value;
        command.Parameters.Add(fagligParam);

        
        var nyParam = command.CreateParameter();
        nyParam.ParameterName = "nykompetence";
        nyParam.Value = (object?)growth.NyKompetence ?? DBNull.Value;
        command.Parameters.Add(nyParam);

        
        var motParam = command.CreateParameter();
        motParam.ParameterName = "motivation";
        motParam.Value = (object?)growth.Motivation ?? DBNull.Value;
        command.Parameters.Add(motParam);

        
        var trivselParam = command.CreateParameter();
        trivselParam.ParameterName = "trivsel";
        trivselParam.Value = (object?)growth.Trivsel ?? DBNull.Value;
        command.Parameters.Add(trivselParam);

        
        var answerTextParam = command.CreateParameter();
        answerTextParam.ParameterName = "answertext";
        answerTextParam.Value = (object?)growth.answerText ?? DBNull.Value;
        command.Parameters.Add(answerTextParam);

        await command.ExecuteNonQueryAsync();
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