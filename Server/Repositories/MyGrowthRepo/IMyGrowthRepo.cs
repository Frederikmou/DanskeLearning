using System.Threading.Tasks;
using Core.Models;

namespace Server.Repositories.MyGrowthRepo
{
    public interface IMyGrowthRepo
    {
        Task CreateAsync(Core.Models.MyGrowth growth);
    }
}