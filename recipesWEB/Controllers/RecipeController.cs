using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using recipesDATA;
using recipesDATA.Entities;
using recipesWEB.ViewModels.Recipes;

namespace recipesWEB.Controllers
{
    public class RecipeController : Controller
    {
            private readonly RecipesDbContext context;

            public RecipeController(RecipesDbContext context)
            {
                this.context = context;
            }

            public async Task<IActionResult> Index()
            {
                var recipes = await context.Recipes
                    .Include(r => r.Category)
                    .ToListAsync();

                var model = new List<RecipeIndexViewModel>();

                foreach (var recipe in recipes)
                {
                    model.Add(new RecipeIndexViewModel
                    {
                        Id = recipe.Id,
                        Name = recipe.Name,
                        Ingredients = recipe.Ingredients,
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
                var recipe = await context.Recipes
                    .Include(r => r.Category)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (recipe == null)
                {
                    return NotFound();
                }

                var model = new RecipeDetailsViewModel
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Ingredients = recipe.Ingredients,
                    Instructions = recipe.Instructions,
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
                    await context.Categories.ToListAsync(),
                    "Id",
                    "Name"
                );

                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(RecipeCreateViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var recipe = new Recipe
                    {
                        Name = model.Name,
                        Ingredients = model.Ingredients,
                        Instructions = model.Instructions,
                        PreparationTime = model.PreparationTime,
                        Servings = model.Servings,
                        CategoryId = model.CategoryId
                    };

                    context.Recipes.Add(recipe);

                    await context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Categories = new SelectList(
                    await context.Categories.ToListAsync(),
                    "Id",
                    "Name",
                    model.CategoryId
                );

                return View(model);
            }

            public async Task<IActionResult> Edit(int id)
            {
                var recipe = await context.Recipes
                    .FindAsync(id);

                if (recipe == null)
                {
                    return NotFound();
                }

                var model = new RecipeEditViewModel
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Ingredients = recipe.Ingredients,
                    Instructions = recipe.Instructions,
                    PreparationTime = recipe.PreparationTime,
                    Servings = recipe.Servings,
                    CategoryId = recipe.CategoryId
                };

                ViewBag.Categories = new SelectList(
                    await context.Categories.ToListAsync(),
                    "Id",
                    "Name",
                    recipe.CategoryId
                );

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
                    var recipe = await context.Recipes
                        .FindAsync(id);

                    if (recipe == null)
                    {
                        return NotFound();
                    }

                    recipe.Name = model.Name;
                    recipe.Ingredients = model.Ingredients;
                    recipe.Instructions = model.Instructions;
                    recipe.PreparationTime = model.PreparationTime;
                    recipe.Servings = model.Servings;
                    recipe.CategoryId = model.CategoryId;

                    await context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Categories = new SelectList(
                    await context.Categories.ToListAsync(),
                    "Id",
                    "Name",
                    model.CategoryId
                );

                return View(model);
            }

            public async Task<IActionResult> Delete(int id)
            {
                var recipe = await context.Recipes
                    .Include(r => r.Category)
                    .FirstOrDefaultAsync(r => r.Id == id);

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
                var recipe = await context.Recipes
                    .FindAsync(id);

                if (recipe != null)
                {
                    context.Recipes.Remove(recipe);

                    await context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
        }
    }

