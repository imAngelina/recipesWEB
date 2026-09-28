using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipesDATA;
using recipesDATA.Entities;
using recipesWEB.ViewModels.Categories;

namespace recipesWEB.Controllers
{
    public class CategoryController : Controller
    {
        private readonly RecipesDbContext context;

        public CategoryController(RecipesDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await context.Categories
                .ToListAsync();

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
            var category = await context.Categories
                .Include(c => c.Recipes)
                .FirstOrDefaultAsync(c => c.Id == id);

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
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name,
                    Description = model.Description
                };

                context.Categories.Add(category);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

       

        public async Task<IActionResult> Edit(int id)
        {
            var category = await context.Categories
                .FindAsync(id);

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
                var category = await context.Categories
                    .FindAsync(id);

                if (category == null)
                {
                    return NotFound();
                }

                category.Name = model.Name;
                category.Description = model.Description;

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

      

        public async Task<IActionResult> Delete(int id)
        {
            var category = await context.Categories
                .FindAsync(id);

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
            var category = await context.Categories
                .FindAsync(id);

            if (category != null)
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
