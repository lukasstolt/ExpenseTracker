using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.API.Models;
using ExpenseTracker.API.Services;

namespace ExpenseTracker.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BudgetsController : ControllerBase
	{
		IBudgetService _budgetService;

		public BudgetsController(IBudgetService budgetService)
		{
			_budgetService = budgetService;
		}

		// GET api/Budgets/Base
		[HttpGet("base")]
		public IActionResult GetBaseBudget()
		{
			return Ok(_budgetService.GetBudgetById(1));
		}

		// GET api/Budgets/3
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var budget = _budgetService.GetBudgetById(id);

			if (budget == null)
				return NotFound();
			else
				return Ok(budget);
		}

		// GET api/Budgets/Current
		[HttpGet("Current")]
		public IActionResult GetCurrent()
		{
			var budget = _budgetService.GetBudgetByDate(DateTime.Now);

			if (budget == null)
				return BadRequest();
			else
				return Ok(budget);
		}

		// POST api/Budgets/3/AddIncome
		[HttpPost("{id}/AddIncome")]
		public IActionResult AddIncome(int id, [FromBody] Income income)
		{
			try
			{
				_budgetService.AddIncome(id, income);
			}
			catch
			{
				return BadRequest();
			}

			return Created($"/{id}", income);
		}

		// POST api/Budgets/3/AddExpense
		[HttpPost("{id}/AddExpense")]
		public IActionResult AddExpense(int id, [FromBody] Expense expense)
		{
			try
			{
				_budgetService.AddExpense(id, expense);
			}
			catch
			{
				return BadRequest();
			}

			return Created($"/{id}", expense);
		}

		// POST api/Budgets/AddCategory
		[HttpPost("AddCategory")]
		public IActionResult AddCategory(Category category)
		{
			try
			{
				_budgetService.AddCategory(category);
			}
			catch
			{
				return BadRequest();
			}

			return CreatedAtRoute("Categories", category);
		}

		[HttpGet("Categories")]
		public IActionResult GetCategories()
		{
			return Ok(_budgetService.GetCategories());
		}
	}
}
