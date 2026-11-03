using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using recipesDATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipesDATA.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasData(
                 new Recipe
                 {
                     Id = 1,
                     Name = "Chicken with Rice",
                     PreparationTime = 45,
                     Servings = 4,
                     CategoryId = 1
                 },
                 new Recipe
                 {
                     Id = 2,
                     Name = "Chocolate Cake",
                     PreparationTime = 60,
                     Servings = 8,
                     CategoryId = 2
                 },
                 new Recipe
                 {
                     Id = 3,
                     Name = "Shopska Salad",
                     PreparationTime = 15,
                     Servings = 2,
                     CategoryId = 3
                 },
                 new Recipe
                 {
                     Id = 4,
                     Name = "Chicken Soup",
                     PreparationTime = 50,
                     Servings = 4,
                     CategoryId = 4
                 }
             );

        }
    }
}
