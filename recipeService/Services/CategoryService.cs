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
    public class CategoryService:ICategoryService
    {
        private readonly RecipesDbContext new_context;

        public CategoryService(RecipesDbContext context)
        {
            new_context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await new_context.Categories.Include(c => c.Recipes)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await new_context.Categories
         .Include(c => c.Recipes)
         .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> GetCategoryDetailsAsync(int id)
        {
            return await new_context.Categories
                .Include(c => c.Recipes)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            new_context.Categories.Add(category);
            await new_context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await new_context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await new_context.Categories
        .Include(c => c.Recipes)
        .FirstOrDefaultAsync(c => c.Id == id);

            if (category != null)
            {
                // Ако има свързани рецепти, не позволявай да се изтрие директно от базата
                if (category.Recipes != null && category.Recipes.Any())
                {
                    throw new InvalidOperationException("Category contains recipes and cannot be deleted.");
                }

                new_context.Categories.Remove(category);
                await new_context.SaveChangesAsync();
            }
        }
    }

}
