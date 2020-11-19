using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpenseTracker.API.Models
{
	public class Income
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public decimal Amount { get; set; }
		[Required]
		public DateTime DateTime { get; set; }
		[Required]
		public Category Category { get; set; }

		[JsonIgnore]
		[ForeignKey("BudgetId")]
		public Budget Budget { get; set; }
	}
}
