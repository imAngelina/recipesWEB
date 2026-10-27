using Microsoft.AspNetCore.Mvc;
using recipesDATA.Entities;
using recipesDATA;
using recipesWEB.ViewModels.Ingredients;
using Microsoft.EntityFrameworkCore;
using recipesService.Interfaces;

namespace recipesWEB.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IIngredientService ins;

        public IngredientController(IIngredientService ins)
        {
            this.ins = ins;
        }

        public async Task<IActionResult> Index()
        {
            var ingredients = await ins.GetAllIngredientsAsync();

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
            var ingredient = await ins.GetIngredientDetailsAsync(id);

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
            var ingredients = await ins.GetAllIngredientsAsync();

            foreach (var ingredient in ingredients)
            {
                if (ingredient.Name == model.Name.Trim())
                {
                    ModelState.AddModelError(
                        nameof(model.Name),
                        "Вече съществува продукт с това име.");

                    break;
                }
            }

            if (ModelState.IsValid)
            {
                var ingredient = new Ingredient
                {
                    Name = model.Name.Trim(),
                    Description = model.Description?.Trim()
                };

                await ins.AddIngredientAsync(ingredient);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ingredient = await ins.GetIngredientByIdAsync(id);

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

            var ingredients = await ins.GetAllIngredientsAsync();

            foreach (var ingredient in ingredients)
            {
                if (ingredient.Id != id &&
                    ingredient.Name == model.Name.Trim())
                {
                    ModelState.AddModelError(
                        nameof(model.Name),
                        "Вече съществува продукт с това име.");

                    break;
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentIngredient = await ins.GetIngredientByIdAsync(id);

            if (currentIngredient == null)
            {
                return NotFound();
            }

            currentIngredient.Name = model.Name.Trim();
            currentIngredient.Description = model.Description?.Trim();

            await ins.UpdateIngredientAsync(currentIngredient);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ingredient = await ins.GetIngredientDetailsAsync(id);

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
            var ingredient = await ins.GetIngredientDetailsAsync(id);

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

            await ins.DeleteIngredientAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }

}
