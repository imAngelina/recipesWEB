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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
               new Category
               {
                   Id = 1,
                   Name = "Main Dishes",
                   Description = "Main meals and dishes"
               },
               new Category
               {
                   Id = 2,
                   Name = "Desserts",
                   Description = "Sweet dishes and desserts"
               },
               new Category
               {
                   Id = 3,
                   Name = "Salads",
                   Description = "Fresh and healthy salads"
               },
               new Category
               {
                   Id = 4,
                   Name = "Soups",
                   Description = "Different types of soups"
               }
           );
        }
    }
}
