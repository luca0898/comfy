using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Comfy.Product.Contracts.Repositories.Shared;
using Comfy.PRODUCT.Entities;

namespace Comfy.PRODUCT.Contracts.Repositories
{
    public interface IScheduleRepository : IGenericRepository<Schedule>
    {
    }
}
