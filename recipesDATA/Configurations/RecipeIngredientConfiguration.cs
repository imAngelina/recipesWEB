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
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            builder.HasData(
                new RecipeIngredient
                {
                    RecipeId = 1,
                    IngredientId = 1,
                    Quantity = 500,
                    Unit = "g"
                },
                new RecipeIngredient
                {
                    RecipeId = 1,
                    IngredientId = 2,
                    Quantity = 300,
                    Unit = "g"
                },
                new RecipeIngredient
                {
                    RecipeId = 2,
                    IngredientId = 3,
                    Quantity = 200,
                    Unit = "g"
                },
                new RecipeIngredient
                {
                    RecipeId = 3,
                    IngredientId = 4,
                    Quantity = 2,
                    Unit = "pcs"
                },
                new RecipeIngredient
                {
                    RecipeId = 3,
                    IngredientId = 5,
                    Quantity = 1,
                    Unit = "pcs"
                },
                new RecipeIngredient
                {
                    RecipeId = 4,
                    IngredientId = 1,
                    Quantity = 300,
                    Unit = "g"
                },
                new RecipeIngredient
                {
                    RecipeId = 4,
                    IngredientId = 6,
                    Quantity = 2,
                    Unit = "pcs"
                },
                new RecipeIngredient
                {
                    RecipeId = 4,
                    IngredientId = 7,
                    Quantity = 1,
                    Unit = "pcs"
                }
            );
        }
    }
}
