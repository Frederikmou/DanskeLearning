using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace DanskeLearning.Services.MyEmployeeService;

public interface IMyEmployeeService
{
    Task<List<MyEmployee>> GetMyEmployeesAsync();
}