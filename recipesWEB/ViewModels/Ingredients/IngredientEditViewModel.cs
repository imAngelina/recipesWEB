using System.ComponentModel.DataAnnotations;

namespace recipesWEB.ViewModels.Ingredients
{
    public class IngredientEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете име на продукта.")]
        [StringLength(100, ErrorMessage = "Името не може да бъде повече от 100 символа.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Описанието не може да бъде повече от 50 символа.")]
        public string? Description { get; set; }
    }
}
