using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Specifications;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Areas.Dashboard.Controllers;

[Authorize(AuthenticationSchemes = "user")]
public class ExpensesController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    public ExpensesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult> Index([FromQuery]ExpenseSpecParams expenseParams)
    {
        var spec = new ExpenseSpecification(expenseParams);
        var expenses = await _unitOfWork.Repository<Expense>().GetItemsWithSpecificAsync(spec);

        var categories = await _unitOfWork.Repository<Category>().GetItemsAsync();
        
        var model = new ExpenseViewModel(categories, expenses);
        return View(model);
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create(Expense expense)
    {
        if (expense.ExpenseDate > DateTime.Today)
            ModelState.AddModelError("ExpenseDate","Date must not be a future date");

        if(ModelState.IsValid)
        {
            await _unitOfWork.Repository<Expense>().AddAsync(expense);
            await _unitOfWork.SaveChangesAsync();

            TempData["success"] = true;
            return NoContent();
        }

        return BadRequest(ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage));
    }

    [HttpPost("edit")]
    public async Task<ActionResult> Edit(Expense expense)
    {
        if (expense.ExpenseDate > DateTime.Today)
            ModelState.AddModelError("ExpenseDate","Date must not be a future date");

        if(ModelState.IsValid)
        {
            var row = await _unitOfWork.Repository<Expense>().GetItemByIdAsync(expense.Id);
            row.Name = expense.Name;
            row.CategoryId = expense.CategoryId;
            row.Amount = expense.Amount;
            row.ExpenseDate = expense.ExpenseDate;

            await _unitOfWork.SaveChangesAsync();

            TempData["success"] = true;
            return NoContent();
        }

        return BadRequest(ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage));
    }

    [HttpPost("delete")]
    public async Task<ActionResult> Delete(string id)
    {
        var row = await _unitOfWork.Repository<Expense>().GetItemByIdAsync(id);
        _unitOfWork.Repository<Expense>().Remove(row);

        await _unitOfWork.SaveChangesAsync();
        
        TempData["success"] = true;
        return RedirectToAction(nameof(Index));
    }
}