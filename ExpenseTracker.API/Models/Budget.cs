﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.API.Models
{
	public class Budget
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public DateTime DateTime { get; set; }
		[Required]
		public List<Expense> Expenses { get; set; }
		[Required]
		public List<Income> Income { get; set; }

		public decimal AmountLeft()
		{
			return Income.Sum(i => i.Amount) - Expenses.Sum(e => e.Amount);
		}
	}
}
