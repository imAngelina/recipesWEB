namespace recipesWEB.ViewModels.RecipeIngredients
{
    public class RecipeIngredientIndexViewModel
    {
        public int RecipeId { get; set; }

        public string RecipeName { get; set; } = string.Empty;

        public int IngredientId { get; set; }

        public string IngredientName { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public string Unit { get; set; } = string.Empty;
    }
}
