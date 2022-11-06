using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data.Repositories;

public class BorrowRequestRepository : BaseRepository<BorrowRequest>, IBorrowRequestRepository
{
    public BorrowRequestRepository(BookLibraryContext context)
        : base(context)
    {
    }

    public override async Task<IEnumerable<BorrowRequest>> GetAllAsync(Expression<Func<BorrowRequest, bool>>? predicate = null)
    {
        var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

        return await dbSet
            .Include(br => br.Requester)
            .Include(br => br.Approver)
            .Include(br => br.Books)
            .ToListAsync();
    }

    public override async Task<BorrowRequest?> GetAsync(Expression<Func<BorrowRequest, bool>>? predicate = null)
    {
        var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

        return await dbSet
            .Include(br => br.Requester)
            .Include(br => br.Approver)
            .Include(br => br.Books)
            .FirstOrDefaultAsync();
    }
}