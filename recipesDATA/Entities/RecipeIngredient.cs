using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipesDATA.Entities
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }

        public int IngredientId { get; set; }

        [Range(0.01, 100000)]
        public decimal Quantity { get; set; }

        [Required]
        [StringLength(30)]
        public string Unit { get; set; } = string.Empty;

        [ForeignKey(nameof(RecipeId))]
        public Recipe Recipe { get; set; } = null!;

        [ForeignKey(nameof(IngredientId))]
        public Ingredient Ingredient { get; set; } = null!;
    }
}
