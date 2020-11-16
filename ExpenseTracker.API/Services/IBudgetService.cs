using ExpenseTracker.API.Models;
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
		void AddIncome(int id, BudgetItem budgetItem);
		void AddExpense(int id, BudgetItem budgetItem);
		void AddCategory(Category category);
		List<Category> GetCategories();
	}
}
