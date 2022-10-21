namespace ProductStore.Data;

public interface IDatabaseTransaction : IDisposable
{
    void Commit();
    void Rollback();
}