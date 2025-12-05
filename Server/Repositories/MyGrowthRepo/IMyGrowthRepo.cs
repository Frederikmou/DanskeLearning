using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Server.Repositories.MyGrowthRepo
{
    public interface IMyGrowthRepo
    {
        Task CreateAsync(MyGrowth growth);
        Task<List<MyGrowthAnswers>> GetByUserAsync(Guid userId);
    }
}