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
    public class IngredientService:IIngredientService
    {
        private readonly RecipesDbContext new_context;

        public IngredientService(RecipesDbContext context)
        {
            new_context = context;
        }

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            return await new_context.Ingredients
                .Include(i => i.RecipeIngredients)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        public async Task<Ingredient?> GetIngredientByIdAsync(int id)
        {
            return await new_context.Ingredients
                .FindAsync(id);
        }

        public async Task<Ingredient?> GetIngredientDetailsAsync(int id)
        {
            return await new_context.Ingredients
                .Include(i => i.RecipeIngredients)
                    .ThenInclude(ri => ri.Recipe)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddIngredientAsync(Ingredient ingredient)
        {
            new_context.Ingredients.Add(ingredient);
            await new_context.SaveChangesAsync();
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            await new_context.SaveChangesAsync();
        }

        public async Task DeleteIngredientAsync(int id)
        {
            var ingredient = await new_context.Ingredients
                .Include(i => i.RecipeIngredients)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (ingredient != null)
            {
                new_context.RecipeIngredients.RemoveRange(
                    ingredient.RecipeIngredients);

                new_context.Ingredients.Remove(ingredient);

                await new_context.SaveChangesAsync();
            }
        }
    }
}
