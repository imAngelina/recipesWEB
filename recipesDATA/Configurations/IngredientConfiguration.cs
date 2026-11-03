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
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasData(
                 new Ingredient
                 {
                     Id = 1,
                     Name = "Chicken",
                     Description = "Fresh chicken meat"
                 },
                 new Ingredient
                 {
                     Id = 2,
                     Name = "Rice",
                     Description = "White rice"
                 },
                 new Ingredient
                 {
                     Id = 3,
                     Name = "Chocolate",
                     Description = "Dark chocolate"
                 },
                 new Ingredient
                 {
                     Id = 4,
                     Name = "Tomatoes",
                     Description = "Fresh tomatoes"
                 },
                 new Ingredient
                 {
                     Id = 5,
                     Name = "Cucumber",
                     Description = "Fresh cucumber"
                 },
                 new Ingredient
                 {
                     Id = 6,
                     Name = "Carrots",
                     Description = "Fresh carrots"
                 },
                 new Ingredient
                 {
                     Id = 7,
                     Name = "Onion",
                     Description = "Fresh onion"
                 }
             );
        }
    }
}
