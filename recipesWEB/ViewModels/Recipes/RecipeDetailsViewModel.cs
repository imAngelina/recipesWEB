namespace recipesWEB.ViewModels.Recipes
{
    public class RecipeDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Ingredients { get; set; }

        public string Instructions { get; set; }

        public int PreparationTime { get; set; }

        public int Servings { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
