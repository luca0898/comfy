using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Comfy.SystemObjects.Interfaces
{
    public interface IUnitOfWorkFactory<TUnitOfWork> where TUnitOfWork : IUnitOfWork
    {
        IUnitOfWork Create();
        IUnitOfWork Create(IsolationLevel isolationLevel);
    }
}
