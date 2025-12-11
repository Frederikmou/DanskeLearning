using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Core.Models;

namespace Server.Controllers;

[ApiController]
[Route("api/myteam")]
public class MyTeamController : ControllerBase
{
    private readonly string _connectionString;

    public MyTeamController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    [HttpGet]
    public async Task<List<Team>> GetMyTeams()
    {
        var teams = new List<Team>();

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        const string sql = @"SELECT teamname FROM teams;";

        await using var command = new NpgsqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var team = new Team
            {
                TeamName = reader.GetString(0)
            };

            teams.Add(team);
        }

        return teams;
    }
}