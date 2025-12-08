using System.Threading.Tasks;
using Core.Models;

namespace DanskeLearning.Services.MyGrowthService;

public interface IMyGrowthService
{
    
    Task CreateAsync(MyGrowth growth);
    
    Task<List<MyGrowth>> GetPreviousAsync(Guid userId);
}