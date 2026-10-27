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
    public class RecipeIngredientService:IRecipeIngredientService
    {
        private readonly RecipesDbContext new_context;

        public RecipeIngredientService(RecipesDbContext context)
        {
            new_context = context;
        }

        public async Task<List<RecipeIngredient>> GetAllRecipeIngredientsAsync()
        {
            return await new_context.RecipeIngredients
                .Include(ri => ri.Recipe)
                .Include(ri => ri.Ingredient)
                .OrderBy(ri => ri.Recipe.Name)
                .ThenBy(ri => ri.Ingredient.Name)
                .ToListAsync();
        }

        public async Task<RecipeIngredient?> GetRecipeIngredientByIdAsync(int recipeId, int ingredientId)
        {
            return await new_context.RecipeIngredients
                .Include(ri => ri.Recipe)
                .Include(ri => ri.Ingredient)
                .FirstOrDefaultAsync(ri =>
                    ri.RecipeId == recipeId &&
                    ri.IngredientId == ingredientId);
        }

        public async Task AddRecipeIngredientAsync(RecipeIngredient recipeIngredient)
        {
            new_context.RecipeIngredients.Add(recipeIngredient);
            await new_context.SaveChangesAsync();
        }

        public async Task UpdateRecipeIngredientAsync(int recipeId,int ingredientId,decimal quantity,string unit)
        {
            var item = await new_context.RecipeIngredients
                .FirstOrDefaultAsync(ri =>
                    ri.RecipeId == recipeId &&
                    ri.IngredientId == ingredientId);

            if (item != null)
            {
                item.Quantity = quantity;
                item.Unit = unit.Trim();

                await new_context.SaveChangesAsync();
            }
        }

        public async Task DeleteRecipeIngredientAsync(int recipeId, int ingredientId)
        {
            var item = await new_context.RecipeIngredients
                .FirstOrDefaultAsync(ri =>
                    ri.RecipeId == recipeId &&
                    ri.IngredientId == ingredientId);

            if (item != null)
            {
                new_context.RecipeIngredients.Remove(item);
                await new_context.SaveChangesAsync();
            }
        }

        public async Task<bool> RecipeExistsAsync(int recipeId)
        {
            return await new_context.Recipes
                .AnyAsync(r => r.Id == recipeId);
        }

        public async Task<bool> IngredientExistsAsync(int ingredientId)
        {
            return await new_context.Ingredients
                .AnyAsync(i => i.Id == ingredientId);
        }

        public async Task<bool> RecipeIngredientExistsAsync( int recipeId, int ingredientId)
        {
            return await new_context.RecipeIngredients
                .AnyAsync(ri =>
                    ri.RecipeId == recipeId &&
                    ri.IngredientId == ingredientId);
        }
    }
}
