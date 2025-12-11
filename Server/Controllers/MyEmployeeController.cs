using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Core.Models;

namespace Server.Controllers;

[ApiController]
[Route("api/myemployee")]
public class MyEmployeeController : ControllerBase
{
    private readonly string _connectionString;

    public MyEmployeeController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    
    [HttpGet]
    public async Task<List<MyEmployee>> GetMyEmployees()
    {
        var employees = new List<MyEmployee>();

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        const string sql = @"SELECT name, email FROM users;";

        await using var command = new NpgsqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var employee = new MyEmployee
            {
                Name = reader.GetString(0),
                Email = reader.GetString(1)
            };

            employees.Add(employee);
        }

        return employees;
    }
}