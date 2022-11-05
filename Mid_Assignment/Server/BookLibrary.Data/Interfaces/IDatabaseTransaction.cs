namespace BookLibrary.Data.Interfaces;

public interface IDatabaseTransaction : IDisposable
{
    void Commit();
    void Rollback();
}