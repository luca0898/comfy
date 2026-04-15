namespace CrossCutting.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync(CancellationToken cancellationToken);
}