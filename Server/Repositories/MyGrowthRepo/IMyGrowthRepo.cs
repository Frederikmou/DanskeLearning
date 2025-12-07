using Core.Models;

namespace Server.Repositories.MyGrowthRepo
{
    public interface IMyGrowthRepo
    {
        Task CreateAsync(MyGrowth growth);

       Task<List<MyGrowth>> GetPreviousAsync(MyGrowth userId);
    }
}