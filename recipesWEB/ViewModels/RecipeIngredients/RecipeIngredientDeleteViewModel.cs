namespace recipesWEB.ViewModels.RecipeIngredients
{
    public class RecipeIngredientDeleteViewModel
    {
        public int RecipeId { get; set; }

        public int IngredientId { get; set; }

        public string RecipeName { get; set; } = string.Empty;

        public string IngredientName { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public string Unit { get; set; } = string.Empty;
    }
}
