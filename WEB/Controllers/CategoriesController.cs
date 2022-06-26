using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Areas.Dashboard.Controllers;

[Authorize(AuthenticationSchemes = "user")]
public class CategoriesController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    public CategoriesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var rows = await _unitOfWork.Repository<Category>().GetItemsAsync();
        return View(rows);
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create(Category category)
    {
        var spec = new CategorySpecification(category.Name);
        var existingCategory = await _unitOfWork.Repository<Category>().GetItemWithSpecificAsync(spec);
        
        if (existingCategory != null) ModelState.AddModelError("Name", "Category already exists");

        if(ModelState.IsValid)
        {
            await _unitOfWork.Repository<Category>().AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            TempData["success"] = true;
            return NoContent();
        }

        return BadRequest(ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage));
    }

    [HttpPost("edit")]
    public async Task<ActionResult> Edit(Category category)
    {
        var spec = new CategorySpecification(category.Name);
        var existingCategory = await _unitOfWork.Repository<Category>().GetItemWithSpecificAsync(spec);
        
        if (existingCategory != null && existingCategory.Id != category.Id) 
            ModelState.AddModelError("Name", "Category already exists");

        if(ModelState.IsValid)
        {
            var row = await _unitOfWork.Repository<Category>().GetItemByIdAsync(category.Id);
            row.Name = category.Name;

            await _unitOfWork.SaveChangesAsync();

            TempData["success"] = true;
            return NoContent();
        }

        return BadRequest(ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage));
    }

    [HttpPost("delete")]
    public async Task<ActionResult> Delete(string id)
    {
        var row = await _unitOfWork.Repository<Category>().GetItemByIdAsync(id);
        _unitOfWork.Repository<Category>().Remove(row);

        await _unitOfWork.SaveChangesAsync();
        
        TempData["success"] = true;
        return RedirectToAction(nameof(Index));
    }
}