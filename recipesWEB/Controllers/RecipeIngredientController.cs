using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using recipesDATA.Entities;
using recipesDATA;
using recipesWEB.ViewModels.RecipeIngredients;
using Microsoft.EntityFrameworkCore;

namespace recipesWEB.Controllers
{
    public class RecipeIngredientController : Controller
    {
        private readonly RecipesDbContext context;

        public RecipeIngredientController(RecipesDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var recipeIngredients = await context.RecipeIngredients
                .Include(ri => ri.Recipe)
                .Include(ri => ri.Ingredient)
                .OrderBy(ri => ri.Recipe.Name)
                .ThenBy(ri => ri.Ingredient.Name)
                .ToListAsync();

            var model = new List<RecipeIngredientIndexViewModel>();

            foreach (var item in recipeIngredients)
            {
                model.Add(new RecipeIngredientIndexViewModel
                {
                    RecipeId = item.RecipeId,
                    RecipeName = item.Recipe.Name,
                    IngredientId = item.IngredientId,
                    IngredientName = item.Ingredient.Name,
                    Quantity = item.Quantity,
                    Unit = item.Unit
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Create(
            int? recipeId,
            int? ingredientId)
        {
            await LoadDropdowns(recipeId, ingredientId);

            var model = new RecipeIngredientCreateViewModel
            {
                RecipeId = recipeId ?? 0,
                IngredientId = ingredientId ?? 0,
                Quantity = 1,
                Unit = "g"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            RecipeIngredientCreateViewModel model)
        {
            if (!await context.Recipes
                .AnyAsync(r => r.Id == model.RecipeId))
            {
                ModelState.AddModelError(
                    nameof(model.RecipeId),
                    "Избраната рецепта не съществува.");
            }

            if (!await context.Ingredients
                .AnyAsync(i => i.Id == model.IngredientId))
            {
                ModelState.AddModelError(
                    nameof(model.IngredientId),
                    "Избраният продукт не съществува.");
            }

            if (await context.RecipeIngredients.AnyAsync(ri =>
                ri.RecipeId == model.RecipeId &&
                ri.IngredientId == model.IngredientId))
            {
                ModelState.AddModelError(
                    nameof(model.IngredientId),
                    "Този продукт вече е добавен към рецептата.");
            }

            if (ModelState.IsValid)
            {
                var recipeIngredient = new RecipeIngredient
                {
                    RecipeId = model.RecipeId,
                    IngredientId = model.IngredientId,
                    Quantity = model.Quantity,
                    Unit = model.Unit.Trim()
                };

                context.RecipeIngredients.Add(recipeIngredient);

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await LoadDropdowns(model.RecipeId, model.IngredientId);

            return View(model);
        }

        public async Task<IActionResult> Edit(
            int recipeId,
            int ingredientId)
        {
            var item = await context.RecipeIngredients
                .Include(ri => ri.Recipe)
                .Include(ri => ri.Ingredient)
                .FirstOrDefaultAsync(ri =>
                    ri.RecipeId == recipeId &&
                    ri.IngredientId == ingredientId);

            if (item == null)
            {
                return NotFound();
            }

            var model = new RecipeIngredientEditViewModel
            {
                RecipeId = item.RecipeId,
                IngredientId = item.IngredientId,
                Quantity = item.Quantity,
                Unit = item.Unit,
                RecipeName = item.Recipe.Name,
                IngredientName = item.Ingredient.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int recipeId,
            int ingredientId,
            RecipeIngredientEditViewModel model)
        {
            if (recipeId != model.RecipeId ||
                ingredientId != model.IngredientId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var item = await context.RecipeIngredients
                .FirstOrDefaultAsync(ri =>
                    ri.RecipeId == recipeId &&
                    ri.IngredientId == ingredientId);

            if (item == null)
            {
                return NotFound();
            }

            item.Quantity = model.Quantity;
            item.Unit = model.Unit.Trim();

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(
            int recipeId,
            int ingredientId)
        {
            var item = await context.RecipeIngredients
                .Include(ri => ri.Recipe)
                .Include(ri => ri.Ingredient)
                .FirstOrDefaultAsync(ri =>
                    ri.RecipeId == recipeId &&
                    ri.IngredientId == ingredientId);

            if (item == null)
            {
                return NotFound();
            }

            var model = new RecipeIngredientDeleteViewModel
            {
                RecipeId = item.RecipeId,
                IngredientId = item.IngredientId,
                RecipeName = item.Recipe.Name,
                IngredientName = item.Ingredient.Name,
                Quantity = item.Quantity,
                Unit = item.Unit
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(
            int recipeId,
            int ingredientId)
        {
            var item = await context.RecipeIngredients
                .FirstOrDefaultAsync(ri =>
                    ri.RecipeId == recipeId &&
                    ri.IngredientId == ingredientId);

            if (item == null)
            {
                return NotFound();
            }

            context.RecipeIngredients.Remove(item);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropdowns(
            int? selectedRecipeId = null,
            int? selectedIngredientId = null)
        {
            ViewBag.Recipes = new SelectList(
                await context.Recipes
                    .OrderBy(r => r.Name)
                    .ToListAsync(),
                "Id",
                "Name",
                selectedRecipeId);

            ViewBag.Ingredients = new SelectList(
                await context.Ingredients
                    .OrderBy(i => i.Name)
                    .ToListAsync(),
                "Id",
                "Name",
                selectedIngredientId);
        }
    }
}
