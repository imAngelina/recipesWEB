namespace recipesWEB.ViewModels.Ingredients
{
    public class IngredientDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public List<string> Recipes { get; set; } = new List<string>();
    }
}
