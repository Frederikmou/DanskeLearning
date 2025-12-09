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
        (userid, answertext, answerdate,
         fagligudfordring, nykompetence, motivation, trivsel)
        VALUES
        (@userid, @answertext, @answerdate,
         @fagligudfordring, @nykompetence, @motivation, @trivsel);";

        
       /* var checkinParam = command.CreateParameter(); -> til gruppe. Dette er en serial i vores database
        checkinParam.ParameterName = "checkinid";           hvilket betyder at den genererer et Id selv.
        checkinParam.Value = growth.checkinId;              Ergo ingen "insert into". Ellers kommer der fejl. 
        command.Parameters.Add(checkinParam);
        */ 
       
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

    public async Task<List<MyGrowth>> GetPreviousAsync(Guid userId)
    {
        var result = new List<MyGrowth>();

        using (var dbConnection = new NpgsqlConnection(connectionString))
        {
            await dbConnection.OpenAsync();

            var command = dbConnection.CreateCommand();
            command.CommandText = @"SELECT checkinid, userid, answertext, answerdate, 
            fagligudfordring, nykompetence, motivation, 
            trivsel from mygrowth where userid = @userId";
            command.Parameters.AddWithValue("@userId", userId);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var checkinid = reader.GetInt32(0);
                    var Id =  reader.GetGuid(1);
                    var answerText =  reader.IsDBNull(2)? null: reader.GetString(2);
                    var answerDate = reader.GetDateTime(3);
                    var fagligUdfordring = reader.IsDBNull(4)? null: reader.GetString(4);
                    var nykompetence = reader.IsDBNull(5)? null: reader.GetString(5);
                    var motivation = reader.IsDBNull(6)? null: reader.GetString(6);
                    var trivsel = reader.IsDBNull(7)? null: reader.GetString(7);

                    MyGrowth growth = new MyGrowth()
                    {
                        checkinId = checkinid,
                        userId = Id,
                        answerText = answerText,
                        answerDate = answerDate,
                        FagligUdfordring = fagligUdfordring,
                        NyKompetence = nykompetence,
                        Motivation = motivation,
                        Trivsel = trivsel
                    };
                    result.Add(growth);
                }

                return result;
            }
        }
    }
}
