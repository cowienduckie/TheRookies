﻿namespace BookLibrary.Data.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<BookCategory> BookCategories { get; set; } = null!;
}