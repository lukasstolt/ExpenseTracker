using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.API.Data;
using ExpenseTracker.Shared.Models;
using ExpenseTracker.API.Services;
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
			services.AddScoped<IBudgetService, BudgetService>();

			// Swagger API docs
			services.AddSwaggerGen();

			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder =>
					{
						builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader();
					});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext db)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); } );
			}

			DbInit(db);

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors("CorsPolicy");

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private void DbInit(DatabaseContext db)
		{
			db.Database.EnsureCreated();

			if(db.Budgets.FirstOrDefault() == null)
			{
				db.Budgets.Add(new Budget
				{
					Id = 1,
					DateTime = new DateTime(0)
				});

				db.SaveChanges();
			}
		}
	}
}
