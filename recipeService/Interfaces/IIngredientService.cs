using recipesDATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipesService.Interfaces
{
    public interface IIngredientService
    {
        Task<List<Ingredient>> GetAllIngredientsAsync();
        Task<Ingredient?> GetIngredientByIdAsync(int id);
        Task<Ingredient?> GetIngredientDetailsAsync(int id);
        Task AddIngredientAsync(Ingredient ingredient);
        Task UpdateIngredientAsync(Ingredient ingredient);
        Task DeleteIngredientAsync(int id);
    }
}
