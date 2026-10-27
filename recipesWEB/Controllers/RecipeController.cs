using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using recipesDATA;
using recipesDATA.Entities;
using recipesService.Interfaces;
using recipesWEB.ViewModels.Recipes;

namespace recipesWEB.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService rs;
        private readonly ICategoryService cs;

        public RecipeController(
            IRecipeService rs,
            ICategoryService cs)
        {
            this.rs = rs;
            this.cs = cs;
        }

        public async Task<IActionResult> Index()
        {
            var recipes = await rs.GetAllRecipesAsync();

            var model = new List<RecipeIndexViewModel>();

            foreach (var recipe in recipes)
            {
                model.Add(new RecipeIndexViewModel
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    PreparationTime = recipe.PreparationTime,
                    Servings = recipe.Servings,
                    CategoryId = recipe.CategoryId,
                    CategoryName = recipe.Category.Name
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var recipe = await rs.GetRecipeDetailsAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            var model = new RecipeDetailsViewModel
            {
                Id = recipe.Id,
                Name = recipe.Name,
                PreparationTime = recipe.PreparationTime,
                Servings = recipe.Servings,
                CategoryId = recipe.CategoryId,
                CategoryName = recipe.Category.Name
            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(
                await cs.GetAllCategoriesAsync(),
                "Id",
                "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            RecipeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var recipe = new Recipe
                {
                    Name = model.Name,
                    PreparationTime = model.PreparationTime,
                    Servings = model.Servings,
                    CategoryId = model.CategoryId
                };

                await rs.AddRecipeAsync(recipe);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(
                await cs.GetAllCategoriesAsync(),
                "Id",
                "Name",
                model.CategoryId);

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await rs.GetRecipeByIdAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            var model = new RecipeEditViewModel
            {
                Id = recipe.Id,
                Name = recipe.Name,
                PreparationTime = recipe.PreparationTime,
                Servings = recipe.Servings,
                CategoryId = recipe.CategoryId
            };

            ViewBag.Categories = new SelectList(
                await cs.GetAllCategoriesAsync(),
                "Id",
                "Name",
                recipe.CategoryId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            RecipeEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var recipe = await rs.GetRecipeByIdAsync(id);

                if (recipe == null)
                {
                    return NotFound();
                }

                recipe.Name = model.Name;
                recipe.PreparationTime = model.PreparationTime;
                recipe.Servings = model.Servings;
                recipe.CategoryId = model.CategoryId;

                await rs.UpdateRecipeAsync(recipe);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(
                await cs.GetAllCategoriesAsync(),
                "Id",
                "Name",
                model.CategoryId);

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var recipe = await rs.GetRecipeDetailsAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            var model = new RecipeDeleteViewModel
            {
                Id = recipe.Id,
                Name = recipe.Name,
                CategoryName = recipe.Category.Name
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await rs.GetRecipeByIdAsync(id);

            if (recipe != null)
            {
                await rs.DeleteRecipeAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
    }

