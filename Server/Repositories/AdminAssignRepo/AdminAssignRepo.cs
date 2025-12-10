using System;
using System.Collections.Generic;
using Npgsql;

namespace Server.Repositories.AdminAssignRepo;

public class AdminAssignRepo : IAdminAssignRepo
{
    private const string connectionString =
        @"Server=ep-long-sea-agyfr4ak-pooler.c-2.eu-central-1.aws.neon.tech;
    User Id=neondb_owner;Password=npg_rwHcEK1Li0Bs;Database=neondb";

    public async Task<bool> AssignSubjectToUser(Guid userId, Guid subjectId)
    {
        await using var dbConnection = new NpgsqlConnection(connectionString);
        await dbConnection.OpenAsync();

        await using var command = dbConnection.CreateCommand();

        command.CommandText = @"INSERT INTO adminassign(userid, subjectid, assigneddate)
                            VALUES(@userId, @subjectId);"

        var userParam = command.CreateParameter();
        userParam.ParameterName = "userid";
        userParam.Value = userId;
        command.Parameters.Add(userParam);

        var subjectidParam = command.CreateParameter();
        subjectidParam.ParameterName = "subjectid";
        subjectidParam.Value = subjectId;
        command.Parameters.Add(subjectidParam);

        await command.ExecuteNonQueryAsync();

        return true;
    }
}
