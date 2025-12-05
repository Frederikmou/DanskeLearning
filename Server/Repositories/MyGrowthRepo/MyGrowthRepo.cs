using System;
using System.Threading.Tasks;
using System.Collections.Generic;
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
        (userid, fagligudfordring, nykompetence, motivation, trivsel, answertext)
        VALUES
        (@userid,@fagligudfordring, @nykompetence, @motivation, @trivsel, @answertext);";


        var userParam = command.CreateParameter();
        userParam.ParameterName = "userid";
        userParam.Value = growth.userId;
        command.Parameters.Add(userParam);


        var fagligParam = command.CreateParameter();
        fagligParam.ParameterName = "fagligudfordring";
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

    public async Task<List<MyGrowthAnswers>> GetByUserAsync(Guid userId)
    {
        var result = new List<MyGrowthAnswers>();

        await using var dbConnection = new NpgsqlConnection(connectionString);
        await dbConnection.OpenAsync();

        await using var command = dbConnection.CreateCommand();
        command.CommandText = @"
    SELECT userid,
       fagligudfordring,
       nykompetence,
       motivation,
       trivsel,
       answertext
       answerdate
    FROM mygrowth
    WHERE userid = @userid
    ORDER BY answerdate DESC;";

        var userParam = command.CreateParameter();
        userParam.ParameterName = "userid";
        userParam.Value = userId;
        command.Parameters.Add(userParam);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result.Add(new MyGrowthAnswers()
            {
                UserId = reader.GetGuid(0),
                FagligUdfordring = reader.IsDBNull(1) ? null : reader.GetString(1),
                NyKompetence = reader.IsDBNull(2) ? null : reader.GetString(2),
                Motivation = reader.IsDBNull(3) ? null : reader.GetString(3),
                Trivsel = reader.IsDBNull(4) ? null : reader.GetString(4),
                AnswerText = reader.IsDBNull(5) ? null : reader.GetString(5),
                AnswerDate = reader.GetDateTime(6)
            });
        }

        return result;
    }
}




















