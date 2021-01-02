using ExpenseTracker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.API.Services
{
	public interface IBudgetService
	{
		Budget GetBudgetById(int id);
		Budget GetBudgetByDate(DateTime dateTime);
		void AddBudgetItem(int id, BudgetItem budgetItem);
		void RemoveBudgetItem(int budgetId, int budgetItemId);

		void AddCategory(Category category);
		void RemoveCategory(int id);
		List<Category> GetCategories();
	}
}
