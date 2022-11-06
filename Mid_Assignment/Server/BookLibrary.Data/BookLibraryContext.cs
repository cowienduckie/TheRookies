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
    public DbSet<BorrowRequest> BorrowRequests { get; set; } = null!;
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
            .ToTable("Books");

        builder.Entity<Category>()
            .ToTable("Categories");

        builder.Entity<BorrowRequest>()
            .ToTable("BorrowRequests");

        builder.Entity<User>()
            .ToTable("Users");
    }

    private static void ConfigureRelationships(ModelBuilder builder)
    {
        builder.Entity<Book>()
            .HasMany(b => b.Categories)
            .WithMany(c => c.Books)
            .UsingEntity(b => b.ToTable("BookCategories"));

        builder.Entity<Book>()
            .HasMany(b => b.BorrowRequests)
            .WithMany(bc => bc.Books)
            .UsingEntity(b => b.ToTable("BorrowRequestDetails"));

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

        builder.Entity<Book>()
            .HasMany(b => b.Categories)
            .WithMany(c => c.Books)
            .UsingEntity(b => b.HasData(
                new {BooksId = 1, CategoriesId = 1},
                new {BooksId = 2, CategoriesId = 2},
                new {BooksId = 3, CategoriesId = 2},
                new {BooksId = 4, CategoriesId = 3},
                new {BooksId = 5, CategoriesId = 3},
                new {BooksId = 6, CategoriesId = 3},
                new {BooksId = 3, CategoriesId = 1},
                new {BooksId = 4, CategoriesId = 2}
            ));

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

        builder.Entity<Book>()
            .HasMany(b => b.BorrowRequests)
            .WithMany(br => br.Books)
            .UsingEntity(b => b.HasData(
                new {BooksId = 1, BorrowRequestsId = 1},
                new {BooksId = 2, BorrowRequestsId = 1},
                new {BooksId = 3, BorrowRequestsId = 1},
                new {BooksId = 4, BorrowRequestsId = 2},
                new {BooksId = 5, BorrowRequestsId = 2},
                new {BooksId = 6, BorrowRequestsId = 2},
                new {BooksId = 1, BorrowRequestsId = 2},
                new {BooksId = 2, BorrowRequestsId = 2},
                new {BooksId = 3, BorrowRequestsId = 3},
                new {BooksId = 4, BorrowRequestsId = 3}
            ));
    }
}