using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.API.Models
{
	public class BudgetItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Amount { get; set; }
		public DateTime DateTime { get; set; }
		public Category Category { get; set; }
	}
}
