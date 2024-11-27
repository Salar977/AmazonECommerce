﻿using AmazonECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AmazonECommerce.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
}