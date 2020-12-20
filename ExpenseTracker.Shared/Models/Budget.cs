using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Shared.Models
{
	public class Budget
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public DateTime DateTime { get; set; }
		[Required]
		public ICollection<BudgetItem> BudgetItems { get; set; }

		public decimal AmountLeft()
		{
			return BudgetItems.Where(b => b.BudgetItemType == BudgetItemType.Income).Sum(i => i.Amount) - 
				BudgetItems.Where(b => b.BudgetItemType == BudgetItemType.Expense).Sum(e => e.Amount);
		}
	}
}
