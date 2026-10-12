using System.ComponentModel.DataAnnotations;

namespace recipesWEB.ViewModels.RecipeIngredients
{
    public class RecipeIngredientEditViewModel
    {
        public int RecipeId { get; set; }

        public int IngredientId { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Количеството трябва да е положително.")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Моля, въведете мерна единица.")]
        [StringLength(30, ErrorMessage = "Мерната единица не може да е повече от 30 символа.")]
        public string Unit { get; set; } = string.Empty;

        public string RecipeName { get; set; } = string.Empty;

        public string IngredientName { get; set; } = string.Empty;
    }
}
