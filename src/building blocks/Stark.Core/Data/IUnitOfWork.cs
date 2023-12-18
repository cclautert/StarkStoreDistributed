namespace Stark.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
