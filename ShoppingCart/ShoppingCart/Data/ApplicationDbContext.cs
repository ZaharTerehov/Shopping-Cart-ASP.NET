﻿using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using System.Security.Cryptography.X509Certificates;

namespace ShoppingCart.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		public DbSet<Product> Products { get; set; }

		public DbSet<Category> Categories { get; set; }
		
	}
}
