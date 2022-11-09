namespace BookLibrary.Data.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}