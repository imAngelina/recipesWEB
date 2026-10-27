using recipesDATA.Entities;
using recipesDATA;
using recipesService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace recipesService.Services
{
    public class RecipeService:IRecipeService
    {
        private readonly RecipesDbContext new_context;

        public RecipeService(RecipesDbContext context)
        {
            new_context = context;
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            return await new_context.Recipes
                .Include(r => r.Category)
                .OrderBy(r => r.Name)
                .ToListAsync();
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            return await new_context.Recipes
                .FindAsync(id);
        }

        public async Task<Recipe?> GetRecipeDetailsAsync(int id)
        {
            return await new_context.Recipes
                .Include(r => r.Category)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            new_context.Recipes.Add(recipe);
            await new_context.SaveChangesAsync();
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            await new_context.SaveChangesAsync();
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await new_context.Recipes
                .Include(r => r.RecipeIngredients)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe != null)
            {
                new_context.RecipeIngredients.RemoveRange(
                    recipe.RecipeIngredients);

                new_context.Recipes.Remove(recipe);

                await new_context.SaveChangesAsync();
            }
        }
    }
}
