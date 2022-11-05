using BookLibrary.Data.Entities;
using Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data;

public class BookLibraryContext : DbContext
{
    public BookLibraryContext(DbContextOptions<BookLibraryContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<BookCategory> BookCategories { get; set; } = null!;
    public DbSet<BorrowRequest> BorrowRequests { get; set; } = null!;
    public DbSet<BorrowRequestDetail> BorrowRequestDetails { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        ConfigureTables(builder);

        ConfigureRelationships(builder);

        SeedData(builder);
    }

    private static void ConfigureTables(ModelBuilder builder)
    {
        builder.Entity<Book>()
            .ToTable("Book");

        builder.Entity<Category>()
            .ToTable("Category");

        builder.Entity<BookCategory>()
            .ToTable("BookCategory");

        builder.Entity<BorrowRequest>()
            .ToTable("BorrowRequest");

        builder.Entity<BorrowRequestDetail>()
            .ToTable("BorrowRequestDetail");

        builder.Entity<User>()
            .ToTable("User");
    }

    private static void ConfigureRelationships(ModelBuilder builder)
    {
        builder.Entity<Book>()
            .HasMany(b => b.BookCategories)
            .WithOne(bc => bc.Book)
            .HasForeignKey(bc => bc.BookId);

        builder.Entity<Category>()
            .HasMany(c => c.BookCategories)
            .WithOne(bc => bc.Category)
            .HasForeignKey(bc => bc.CategoryId);

        builder.Entity<Book>()
            .HasMany(b => b.BoorBorrowRequestDetails)
            .WithOne(brd => brd.Book)
            .HasForeignKey(brd => brd.BookId);

        builder.Entity<BorrowRequest>()
            .HasMany(br => br.BorrowRequestDetails)
            .WithOne(brd => brd.BorrowRequest)
            .HasForeignKey(brd => brd.BorrowRequestId);

        builder.Entity<User>()
            .HasMany(u => u.RequestedBorrowRequests)
            .WithOne(br => br.Requester)
            .HasForeignKey(br => br.RequestedBy);

        builder.Entity<User>()
            .HasMany(u => u.ApprovedBorrowRequests)
            .WithOne(br => br.Approver)
            .HasForeignKey(br => br.ApprovedBy);
    }

    private static void SeedData(ModelBuilder builder)
    {
        builder.Entity<Category>().HasData(
            new Category {Id = 1, Name = "Fiction"},
            new Category {Id = 2, Name = "Science"},
            new Category {Id = 3, Name = "Technology"}
        );

        builder.Entity<Book>().HasData(
            new Book {Id = 1, Name = "Harry Potter"},
            new Book {Id = 2, Name = "Homo Sapiens"},
            new Book {Id = 3, Name = "Homo Deus"},
            new Book {Id = 4, Name = "Algorithm"},
            new Book {Id = 5, Name = "Clean Code"},
            new Book {Id = 6, Name = "Head First: Design Pattern"}
        );

        builder.Entity<BookCategory>().HasData(
            new BookCategory {Id = 1, BookId = 1, CategoryId = 1},
            new BookCategory {Id = 2, BookId = 2, CategoryId = 2},
            new BookCategory {Id = 3, BookId = 3, CategoryId = 2},
            new BookCategory {Id = 4, BookId = 4, CategoryId = 3},
            new BookCategory {Id = 5, BookId = 5, CategoryId = 3},
            new BookCategory {Id = 6, BookId = 6, CategoryId = 3},
            new BookCategory {Id = 7, BookId = 3, CategoryId = 1},
            new BookCategory {Id = 8, BookId = 4, CategoryId = 2}
        );

        builder.Entity<User>().HasData(
            new User {Id = 1, Username = "normie1", Password = "normie1", Name = "Normal 1", Role = Roles.NormalUser},
            new User {Id = 2, Username = "normie2", Password = "normie2", Name = "Normal 2", Role = Roles.NormalUser},
            new User {Id = 3, Username = "supreme1", Password = "supreme2", Name = "Super 1", Role = Roles.SuperUser},
            new User {Id = 4, Username = "supreme1", Password = "supreme2", Name = "Super 2", Role = Roles.SuperUser}
        );

        builder.Entity<BorrowRequest>().HasData(
            new BorrowRequest {Id = 1, Status = RequestStatuses.Waiting, RequestedBy = 1, RequestedAt = DateTime.Now},
            new BorrowRequest
            {
                Id = 2, Status = RequestStatuses.Approved, RequestedBy = 1, RequestedAt = DateTime.Now, ApprovedBy = 3,
                ApprovedAt = DateTime.Now
            },
            new BorrowRequest
            {
                Id = 3, Status = RequestStatuses.Rejected, RequestedBy = 2, RequestedAt = DateTime.Now, ApprovedBy = 4,
                ApprovedAt = DateTime.Now
            }
        );

        builder.Entity<BorrowRequestDetail>().HasData(
            new BorrowRequestDetail {Id = 1, BookId = 1, BorrowRequestId = 1},
            new BorrowRequestDetail {Id = 2, BookId = 2, BorrowRequestId = 1},
            new BorrowRequestDetail {Id = 3, BookId = 3, BorrowRequestId = 1},
            new BorrowRequestDetail {Id = 4, BookId = 4, BorrowRequestId = 2},
            new BorrowRequestDetail {Id = 5, BookId = 5, BorrowRequestId = 2},
            new BorrowRequestDetail {Id = 6, BookId = 6, BorrowRequestId = 2},
            new BorrowRequestDetail {Id = 7, BookId = 1, BorrowRequestId = 2},
            new BorrowRequestDetail {Id = 8, BookId = 2, BorrowRequestId = 2},
            new BorrowRequestDetail {Id = 9, BookId = 3, BorrowRequestId = 3},
            new BorrowRequestDetail {Id = 10, BookId = 4, BorrowRequestId = 3}
        );
    }
}