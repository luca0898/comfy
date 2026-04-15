namespace CrossCutting.Interfaces;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create();
}