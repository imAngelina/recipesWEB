using Microsoft.AspNetCore.Mvc;
using recipesDATA.Entities;
using recipesDATA;
using recipesWEB.ViewModels.Ingredients;
using Microsoft.EntityFrameworkCore;

namespace recipesWEB.Controllers
{
    public class IngredientController : Controller
    {
        private readonly RecipesDbContext context;

        public IngredientController(RecipesDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ingredients = await context.Ingredients
                .Include(i => i.RecipeIngredients)
                .OrderBy(i => i.Name)
                .ToListAsync();

            var model = new List<IngredientIndexViewModel>();

            foreach (var ingredient in ingredients)
            {
                model.Add(new IngredientIndexViewModel
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    Description = ingredient.Description,
                    RecipesCount = ingredient.RecipeIngredients.Count
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ingredient = await context.Ingredients
                .Include(i => i.RecipeIngredients)
                    .ThenInclude(ri => ri.Recipe)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (ingredient == null)
            {
                return NotFound();
            }

            var model = new IngredientDetailsViewModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Description = ingredient.Description,
                Recipes = ingredient.RecipeIngredients
                    .Select(ri => ri.Recipe.Name)
                    .Distinct()
                    .OrderBy(name => name)
                    .ToList()
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            IngredientCreateViewModel model)
        {
            if (await context.Ingredients
                .AnyAsync(i => i.Name == model.Name.Trim()))
            {
                ModelState.AddModelError(
                    nameof(model.Name),
                    "Вече съществува продукт с това име.");
            }

            if (ModelState.IsValid)
            {
                var ingredient = new Ingredient
                {
                    Name = model.Name.Trim(),
                    Description = model.Description?.Trim()
                };

                context.Ingredients.Add(ingredient);

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ingredient = await context.Ingredients
                .FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            var model = new IngredientEditViewModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Description = ingredient.Description
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            IngredientEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (await context.Ingredients.AnyAsync(i =>
                i.Id != id &&
                i.Name == model.Name.Trim()))
            {
                ModelState.AddModelError(
                    nameof(model.Name),
                    "Вече съществува продукт с това име.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var ingredient = await context.Ingredients
                .FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            ingredient.Name = model.Name.Trim();
            ingredient.Description = model.Description?.Trim();

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ingredient = await context.Ingredients
                .Include(i => i.RecipeIngredients)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (ingredient == null)
            {
                return NotFound();
            }

            var model = new IngredientDeleteViewModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Description = ingredient.Description,
                RecipesCount = ingredient.RecipeIngredients.Count
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredient = await context.Ingredients
                .Include(i => i.RecipeIngredients)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (ingredient == null)
            {
                return NotFound();
            }

            if (ingredient.RecipeIngredients.Any())
            {
                TempData["ErrorMessage"] =
                    "Продуктът участва в рецепти. Премахни връзките му с тях, преди да го изтриеш.";

                return RedirectToAction(nameof(Delete), new { id });
            }

            context.Ingredients.Remove(ingredient);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }

}
