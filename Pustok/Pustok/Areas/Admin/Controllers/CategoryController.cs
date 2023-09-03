using Microsoft.AspNetCore.Mvc;
using Pustok.Areas.Admin.ViewModels.Category;
using Pustok.Database;
using Pustok.Database.Models;
using Pustok.Services.Abstracts;

namespace Pustok.Areas.Admin.Controllers;

[Route("admin/categories")]
[Area("admin")]
public class CategoryController : Controller
{
    private readonly PustokDbContext _dbContext;
    private readonly IFileService _fileService;

    public CategoryController(PustokDbContext dbContext, IFileService fileService)
    {
        _dbContext = dbContext;
        _fileService = fileService;
    }

    #region Index

    [HttpGet]
    public IActionResult Index()
    {
        var categories = _dbContext.Categories.ToList();
        var categoryViewModels = categories
            .Select(c => new CategoryListItemViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

        return View(categoryViewModels);
    }

    #endregion

    #region Add

    [HttpGet("add")]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost("/category/add")]
    public async Task<IActionResult> AddCategoryAsync(CategoryAddViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Validation failed!Please enter valid parametrs..." });

            var category = new Category
            {
                Name = model.Name,
                Description = model.CategoryDescription
               
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();


            var response = new
            {
                success = true,
                message = "Category successfully added to table...",
                id = category.Id,
                name = category.Name,
                description = category.Description
            };
           
            return Created("api/categories/" + category.Id, response);

        }
        catch (Exception ex)
        {
          
            return Json(new { success = false, message = "An error occurred while adding the category. Mistake:" + ex.Message });
        }
        
    }


    #endregion

    #region Update

    [HttpGet("{id}/update")]
    public IActionResult Update(int id)
    {
        var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null) return NotFound();

        var model = new CategoryUpdateViewModel
        {
            Id = category.Id,
            Name = category.Name
        };

        return View(model);
    }

    [HttpPost("{id}/update")]
    public IActionResult Update(CategoryUpdateViewModel model)
    {
        if (!ModelState.IsValid)
            return View();

        var category = _dbContext.Categories.FirstOrDefault(c => c.Id == model.Id);
        if (category == null)
        {
            ModelState.AddModelError("Name", "Category not found");
            return View();
        }

        category.Name = model.Name;

        _dbContext.Categories.Update(category);
        _dbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Delete

    [HttpPost("{id}/delete")]
    public IActionResult Delete(int id)
    {
        var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null) return NotFound();

        _dbContext.Categories.Remove(category);
        _dbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    #endregion
}
