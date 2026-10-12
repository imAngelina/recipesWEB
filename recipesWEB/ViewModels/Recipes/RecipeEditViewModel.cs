using System.ComponentModel.DataAnnotations;

namespace recipesWEB.ViewModels.Recipes
{
    public class RecipeEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете име на рецептата.")]
        [StringLength(100, ErrorMessage = "Името на рецептата не може да бъде повече от 100 символа.")]
        public string Name { get; set; }

        

        [Range(1, 600, ErrorMessage = "Времето за приготвяне трябва да бъде между 1 и 600 минути.")]
        public int PreparationTime { get; set; }

        [Range(1, 20, ErrorMessage = "Броят порции трябва да бъде между 1 и 20.")]
        public int Servings { get; set; }

        [Required(ErrorMessage = "Моля, изберете категория.")]
        public int CategoryId { get; set; }
    }
}
