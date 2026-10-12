namespace recipesWEB.ViewModels.Ingredients
{
    public class IngredientIndexViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int RecipesCount { get; set; }
    }
}
