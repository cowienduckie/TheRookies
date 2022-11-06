using BookLibrary.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookLibrary.Data;

public class EntityDatabaseTransaction : IDatabaseTransaction
{
    private readonly IDbContextTransaction _transaction;

    public EntityDatabaseTransaction(DbContext context)
    {
        _transaction = context.Database.BeginTransaction();
    }

    public Task CommitAsync()
    {
        return _transaction.CommitAsync();
    }

    public Task RollbackAsync()
    {
        return _transaction.RollbackAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}