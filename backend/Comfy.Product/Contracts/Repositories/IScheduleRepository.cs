using Comfy.PRODUCT.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.PRODUCT.Contracts.Repositories
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> FindAll(CancellationToken cancellationToken, int skip = 0, int take = 20);
        Task<Schedule> FindOne(int id);

        Task<Schedule> Create(Schedule entity);
        Task<Schedule> Update(Schedule entity);
        Task SoftDelete(Schedule entity);
        Task HardDelete(Schedule entity);
    }
}
