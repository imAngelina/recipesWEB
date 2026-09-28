namespace recipesWEB.ViewModels.Categories
{
    public class CategoryDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> Recipes { get; set; } = new List<string>();
    }
}
