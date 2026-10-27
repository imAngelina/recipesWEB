using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using recipesDATA.Entities;
using recipesDATA;
using recipesWEB.ViewModels.RecipeIngredients;
using Microsoft.EntityFrameworkCore;
using recipesService.Interfaces;

namespace recipesWEB.Controllers
{
    public class RecipeIngredientController : Controller
    {
        private readonly IRecipeIngredientService ris;
        private readonly IRecipeService rs;
        private readonly IIngredientService ins;

        public RecipeIngredientController(
            IRecipeIngredientService ris,
            IRecipeService rs,
            IIngredientService ins)
        {
            this.ris = ris;
            this.rs = rs;
            this.ins = ins;
        }

        public async Task<IActionResult> Index()
        {
            var recipeIngredients =
                await ris.GetAllRecipeIngredientsAsync();

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
            if (!await ris.RecipeExistsAsync(model.RecipeId))
            {
                ModelState.AddModelError(
                    nameof(model.RecipeId),
                    "Избраната рецепта не съществува.");
            }

            if (!await ris.IngredientExistsAsync(model.IngredientId))
            {
                ModelState.AddModelError(
                    nameof(model.IngredientId),
                    "Избраният продукт не съществува.");
            }

            if (await ris.RecipeIngredientExistsAsync(
                model.RecipeId, model.IngredientId))
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

                await ris.AddRecipeIngredientAsync(recipeIngredient);

                return RedirectToAction(nameof(Index));
            }

            await LoadDropdowns(model.RecipeId, model.IngredientId);

            return View(model);
        }

        public async Task<IActionResult> Edit(
            int recipeId,
            int ingredientId)
        {
            var item = await ris.GetRecipeIngredientByIdAsync(
                recipeId, ingredientId);

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

            if (ModelState.IsValid)
            {
                var item = await ris.GetRecipeIngredientByIdAsync(
                    recipeId, ingredientId);

                if (item == null)
                {
                    return NotFound();
                }

                await ris.UpdateRecipeIngredientAsync(
                    recipeId,
                    ingredientId,
                    model.Quantity,
                    model.Unit.Trim());

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(
            int recipeId,
            int ingredientId)
        {
            var item = await ris.GetRecipeIngredientByIdAsync(
                recipeId, ingredientId);

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
            var item = await ris.GetRecipeIngredientByIdAsync(
                recipeId, ingredientId);

            if (item == null)
            {
                return NotFound();
            }

            await ris.DeleteRecipeIngredientAsync(
                recipeId, ingredientId);

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropdowns(
            int? selectedRecipeId = null,
            int? selectedIngredientId = null)
        {
            var recipes = await rs.GetAllRecipesAsync();
            var ingredients = await ins.GetAllIngredientsAsync();

            ViewBag.Recipes = new SelectList(
                recipes,
                "Id",
                "Name",
                selectedRecipeId);

            ViewBag.Ingredients = new SelectList(
                ingredients,
                "Id",
                "Name",
                selectedIngredientId);
        }
    }
}
