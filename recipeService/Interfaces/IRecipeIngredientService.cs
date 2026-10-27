using recipesDATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipesService.Interfaces
{
    public interface IRecipeIngredientService
    {
        Task<List<RecipeIngredient>> GetAllRecipeIngredientsAsync();
        Task<RecipeIngredient?> GetRecipeIngredientByIdAsync(
            int recipeId, int ingredientId);
        Task AddRecipeIngredientAsync(RecipeIngredient recipeIngredient);
        Task UpdateRecipeIngredientAsync(
            int recipeId, int ingredientId,
            decimal quantity, string unit);
        Task DeleteRecipeIngredientAsync(
            int recipeId, int ingredientId);
        Task<bool> RecipeExistsAsync(int recipeId);
        Task<bool> IngredientExistsAsync(int ingredientId);
        Task<bool> RecipeIngredientExistsAsync(
            int recipeId, int ingredientId);
    }
}
