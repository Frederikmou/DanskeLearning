using Core.Models;
namespace DanskeLearning.Services.DashboardService;

public interface IDashboardSerivce
{
   Task<List<Subject>>  GetSubjectsAsync();
}