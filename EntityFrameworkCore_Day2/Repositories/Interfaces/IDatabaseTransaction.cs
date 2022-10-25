namespace ProductStore.Repositories;

public interface IDatabaseTransaction : IDisposable
{
    void Commit();
    void Rollback();
}