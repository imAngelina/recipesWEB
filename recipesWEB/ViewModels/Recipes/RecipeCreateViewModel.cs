using System.ComponentModel.DataAnnotations;

namespace recipesWEB.ViewModels.Recipes
{
    public class RecipeCreateViewModel
    {
        [Required(ErrorMessage = "Моля, въведете име на рецептата.")]
        [StringLength(100, ErrorMessage = "Името на рецептата не може да бъде повече от 100 символа.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Моля, въведете съставките.")]
        [StringLength(1000, ErrorMessage = "Съставките не могат да бъдат повече от 1000 символа.")]
        public string Ingredients { get; set; }

        [Required(ErrorMessage = "Моля, въведете инструкции за приготвяне.")]
        [StringLength(2000, ErrorMessage = "Инструкциите не могат да бъдат повече от 2000 символа.")]
        public string Instructions { get; set; }

        [Range(1, 600, ErrorMessage = "Времето за приготвяне трябва да бъде между 1 и 600 минути.")]
        public int PreparationTime { get; set; }

        [Range(1, 20, ErrorMessage = "Броят порции трябва да бъде между 1 и 20.")]
        public int Servings { get; set; }

        [Required(ErrorMessage = "Моля, изберете категория.")]
        public int CategoryId { get; set; }
    }
}
