using Core.Models;

namespace Server.Repositories.MyGrowthRepo
{
    public interface IMyGrowthRepo
    {
        Task CreateAsync(MyGrowth growth);

       Task<List<MyGrowth>> GetPreviousAsync(Guid userId);
       
       Task<List<MyGrowth>> GetEntryByIdAsync(int chekinid);
    }
}