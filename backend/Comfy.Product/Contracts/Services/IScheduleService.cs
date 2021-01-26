using Comfy.PRODUCT.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.PRODUCT.Contracts.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> FindAll(CancellationToken cancellationToken, int skip = 0, int take = 20);
        Task<Schedule> GetOne(int id, CancellationToken cancellationToken);
        Task<Schedule> Create(Schedule entity, CancellationToken cancellationToken);
        Task<Schedule> Update(Schedule entity, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
