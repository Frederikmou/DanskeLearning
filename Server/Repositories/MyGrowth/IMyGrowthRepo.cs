using System.Threading.Tasks;
using Core.Models;

namespace Server.Repositories.MyGrowth
{
    public interface IMyGrowthRepo
    {
        Task CreateAsync(Core.Models.MyGrowth growth);
    }
}