﻿using ExpenseTracker.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.API.Data
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

		public DbSet<Budget> Budgets { get; set; }
		public DbSet<Category> Categories { get; set; }
	}
}