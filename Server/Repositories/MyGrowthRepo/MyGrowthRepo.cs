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
        (checkinid, userid, answertext, answerdate,
         fagligudfordring, nykompetence, motivation, trivsel)
        VALUES
        (@checkinid, @userid, @answertext, @answerdate,
         @fagligudfordring, @nykompetence, @motivation, @trivsel);";

        
        
        var checkinParam = command.CreateParameter();
        checkinParam.ParameterName = "checkinid";
        checkinParam.Value = growth.checkinId;
        command.Parameters.Add(checkinParam);
        
        var userParam = command.CreateParameter();
        userParam.ParameterName = "userid"; 
        userParam.Value = growth.userId;
        command.Parameters.Add(userParam);
        
        var answerTextParam = command.CreateParameter();
        answerTextParam.ParameterName = "answertext";
        answerTextParam.Value = (object?)growth.answerText ?? DBNull.Value;
        command.Parameters.Add(answerTextParam);
        
        
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

        await command.ExecuteNonQueryAsync();
    }

    public Task<List<MyGrowth>> GetPreviousAsync(MyGrowth userId)
    {
        throw new NotImplementedException();
    }
}
