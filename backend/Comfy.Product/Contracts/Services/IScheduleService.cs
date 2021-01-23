using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Comfy.PRODUCT.Entities;

namespace Comfy.PRODUCT.Contracts.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> FindAll(CancellationToken cancellationToken, int skip = 0, int take = 20);
        Task<Schedule> GetOne(int id);
        Task<Schedule> Create(Schedule entity);
        Task<Schedule> Update(Schedule entity);
        Task Delete(int id);
    }
}
