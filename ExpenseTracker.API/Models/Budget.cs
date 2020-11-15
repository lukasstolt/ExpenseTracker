using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.API.Models
{
	public class Budget
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public DateTime Month { get; set; }
		[Required]
		public List<BudgetItem> Expenses { get; set; }
		[Required]
		public List<BudgetItem> Income { get; set; }

		public decimal AmountLeft()
		{
			return Income.Sum(i => i.Amount) - Expenses.Sum(e => e.Amount);
		}
	}
}
