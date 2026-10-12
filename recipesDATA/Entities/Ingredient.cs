using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipesDATA.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Description { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
            = new List<RecipeIngredient>();
    }
}
