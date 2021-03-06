﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpenseTracker.Shared.Models
{
	public class BudgetItem
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public decimal Amount { get; set; }
		[Required]
		public DateTime DateTime { get; set; }
		[Required]
		public Category Category { get; set; }
		[Required]
		public BudgetItemType BudgetItemType { get; set; }

		[JsonIgnore]
		public Budget Budget { get; set; }
	}
}
