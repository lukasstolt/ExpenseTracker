﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Shared.Models;
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

		// POST api/Budgets/3/AddBudgetItem
		[HttpPost("{id}/AddBudgetItem")]
		public IActionResult AddBudgetItem(int id, [FromBody] BudgetItem budgetItem)
		{
			try
			{
				_budgetService.AddBudgetItem(id, budgetItem);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return BadRequest();
			}

			return Created($"/{id}", budgetItem);
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

			return Created($"/Categories", category);
		}

		[HttpGet("Categories")]
		public IActionResult GetCategories()
		{
			return Ok(_budgetService.GetCategories());
		}
	}
}
