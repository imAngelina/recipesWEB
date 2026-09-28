using Microsoft.EntityFrameworkCore;
using recipesDATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace recipesDATA
{
    public class RecipesDbContext:DbContext
    {
        public RecipesDbContext(DbContextOptions<RecipesDbContext> options)
    : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
