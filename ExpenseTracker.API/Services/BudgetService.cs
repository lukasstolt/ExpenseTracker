using ExpenseTracker.API.Data;
using ExpenseTracker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.API.Services
{
	public class BudgetService : IBudgetService
	{
		private DatabaseContext _db;

		public BudgetService(DatabaseContext db)
		{
			_db = db;
			EnsureUpToDate();
		}

		/// <summary>
		/// Gets a budget by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Budget GetBudgetById(int id)
		{
			return _db.Budgets
				.Include(b => b.BudgetItems)
				.ThenInclude(e => e.Category)
				.SingleOrDefault(b => b.Id == id);
		}

		/// <summary>
		/// Gets a budget by year and month
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public Budget GetBudgetByDate(DateTime dateTime)
		{
			return _db.Budgets
				.Include(b => b.BudgetItems)
				.ThenInclude(e => e.Category)
				.SingleOrDefault(b => b.DateTime.Year == dateTime.Year && b.DateTime.Month == dateTime.Month);
		}

		/// <summary>
		/// Adds a budget item to the specified budget
		/// </summary>
		/// <param name="id"></param>
		/// <param name="budgetItem"></param>
		public void AddBudgetItem(int id, BudgetItem budgetItem)
		{
			var budget = _db.Budgets.SingleOrDefault(b => b.Id == id);

			if (budget == null)
				throw new Exception("Invalid id");

			if (budget.BudgetItems == null)
				budget.BudgetItems = new List<BudgetItem>();

			budgetItem.Budget = budget;

			budget.BudgetItems.Add(budgetItem);
			_db.SaveChanges();
		}

		/// <summary>
		/// Adds a category to the database
		/// </summary>
		/// <param name="category"></param>
		public void AddCategory(Category category)
		{
			_db.Categories.Add(category);
			_db.SaveChanges();
		}

		/// <summary>
		/// Gets all the categories stored in the database
		/// </summary>
		/// <returns></returns>
		public List<Category> GetCategories()
		{
			return _db.Categories.ToList();
		}

		#region Private Helpers

		/// <summary>
		/// Ensures that the budget is up to date
		/// </summary>
		private void EnsureUpToDate()
		{
			var latestBudget = _db.Budgets.OrderByDescending(b => b.Id).FirstOrDefault();

			if(latestBudget == default || !SameMonth(latestBudget.DateTime, DateTime.Now))
			{
				var newBudget = new Budget
				{
					DateTime = DateTime.Now,
					BudgetItems = new List<BudgetItem>()
				};

				_db.Add(newBudget);
				_db.SaveChanges();
			}
		}

		/// <summary>
		/// Checks if two datetimes have the same year and month
		/// </summary>
		/// <param name="d1"></param>
		/// <param name="d2"></param>
		/// <returns></returns>
		private bool SameMonth(DateTime d1, DateTime d2)
		{
			return d1.Year == d2.Year && d1.Month == d2.Month;
		}

		
		#endregion
	}
}
