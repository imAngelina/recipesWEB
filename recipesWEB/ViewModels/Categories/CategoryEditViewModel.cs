using System.ComponentModel.DataAnnotations;

namespace recipesWEB.ViewModels.Categories
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на категорията е задължително.")]
        [StringLength(50, ErrorMessage = "Името не може да бъде по-дълго от 50 символа.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Описанието е задължително.")]
        [StringLength(200, ErrorMessage = "Описанието не може да бъде по-дълго от 200 символа.")]
        public string Description { get; set; }
    }
}
