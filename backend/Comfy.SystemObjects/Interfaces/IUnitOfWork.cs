using System;
using System.Threading.Tasks;

namespace Comfy.SystemObjects.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync();
    }
}
