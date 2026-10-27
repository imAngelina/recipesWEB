using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipesDATA;
using recipesDATA.Entities;
using recipesService.Interfaces;
using recipesWEB.ViewModels.Categories;

namespace recipesWEB.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService cs;

        public CategoryController(ICategoryService cs)
        {
            this.cs = cs;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await cs.GetAllCategoriesAsync();

            var model = new List<CategoryIndexViewModel>();

            foreach (var category in categories)
            {
                model.Add(new CategoryIndexViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await cs.GetCategoryDetailsAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var model = new CategoryDetailsViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Recipes = category.Recipes
                    .Select(r => r.Name)
                    .ToList()
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name,
                    Description = model.Description
                };

                await cs.AddCategoryAsync(category);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await cs.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var model = new CategoryEditViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            int id,
            CategoryEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var category = await cs.GetCategoryByIdAsync(id);

                if (category == null)
                {
                    return NotFound();
                }

                category.Name = model.Name;
                category.Description = model.Description;

                await cs.UpdateCategoryAsync(category);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await cs.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var model = new CategoryDeleteViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await cs.GetCategoryByIdAsync(id);

            if (category != null)
            {
                await cs.DeleteCategoryAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
