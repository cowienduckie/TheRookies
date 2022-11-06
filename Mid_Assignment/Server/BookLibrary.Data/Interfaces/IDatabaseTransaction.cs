namespace BookLibrary.Data.Interfaces;

public interface IDatabaseTransaction : IDisposable
{
    Task CommitAsync();
    Task RollbackAsync();
}