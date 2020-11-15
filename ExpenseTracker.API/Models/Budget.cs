using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.API.Models
{
	public class Budget
	{
		public int Id { get; set; }
		public DateTime Month { get; set; }
		public List<BudgetItem> Expenses { get; set; }
		public List<BudgetItem> Income { get; set; }

		public decimal AmountLeft()
		{
			return Income.Sum(i => i.Amount) - Expenses.Sum(e => e.Amount);
		}
	}
}
