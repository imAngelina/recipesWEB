using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipesDATA.Entities
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(1, 600)]
        public int PreparationTime { get; set; }

        [Range(1, 20)]
        public int Servings { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    = new List<RecipeIngredient>();
    }
}
