using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.API.Data;
using ExpenseTracker.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<DatabaseContext>(options => options.UseSqlite(Configuration["ConnectionStrings:DefaultConnection"]));
			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext db)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			DbInit(db);

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private void DbInit(DatabaseContext db)
		{
			db.Database.EnsureCreated();

			// Create standard categories
			if(db.Categories.FirstOrDefault() == null)
			{
				// TODO - flytta till konfigurationsfil
				db.Categories.Add(new Category { Name = "Boende" });
				db.Categories.Add(new Category { Name = "Hem" });
				db.Categories.Add(new Category { Name = "Mat" });
				db.Categories.Add(new Category { Name = "Hälsa / skönhet" });

				db.SaveChanges();
			}

			if(db.Budgets.FirstOrDefault() == null)
			{
				db.Budgets.Add(new Budget
				{
					Id = 1,
					Income = new List<BudgetItem>(),
					Expenses = new List<BudgetItem>(),
					DateTime = new DateTime(0)
				});

				db.SaveChanges();
			}
		}
	}
}
