namespace Khanakhajana.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Src { get; set; }
        public string Category { get; set; }
        public float Star { get; set; } // Change the data type to float or double
        public string Text { get; set; }
        public string PrepTime { get; set; }
        public string CookingTime { get; set; }
        public string Serving { get; set; }
        public List<string> Ingredients { get; set; }
        public List<List<string>> NutritionFacts { get; set; }
        public List<string> Instructions { get; set; }
    }

}
