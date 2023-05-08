namespace RecipeLibrary
{
    public class Recipe
    {
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public string Description { get; set; }
        
        public Recipe() {}

        public Recipe(string name, List<string> ingredients, string description)
        {
            Name = name;
            Ingredients = ingredients;
            Description = description;
        }

        public bool IsIngredientPresent(string ingredient) { 
            return Ingredients.Contains(ingredient);
        }

        public bool IsIngredientPresent(List<string> ingredientsList) {
            foreach (string ingredient in ingredientsList)
            {
                if (!IsIngredientPresent(ingredient))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class RecipeShort
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public RecipeShort(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return "Name - " + Name + " ; Description - " + Description;
        }
    }

    public class RecipeBook
    {
        public static List<Recipe> LoadRecipes()
        {
            List<Recipe> recipes = new List<Recipe>()
            {
                new Recipe("Салат Весняний", new List<string>(){"капуста", "сіль", "цибуля", "огірок", "помідор", "олія" }, "Рецепт салату"),
                new Recipe("Піца Домашня", new List<string>(){"тісто", "томатний соус", "цибуля", "майонез", "сир", "маслини" }, "Рецепт піци Домашньої"),
                new Recipe("Тісто для Піци", new List<string>(){"вода", "яйце", "борошно", "сода", "цукор", "сіль", "маргарин" }, "Рецепт тіста для піци"),
                new Recipe("Хліб білий", new List<string>(){"вода", "олія", "борошно", "цукор", "сіль", "дріжджі" }, "Рецепт білого хліба"),
                new Recipe("Смажена картопля", new List<string>(){"картопля", "олія", "сіль", "перець" }, "Рецепт смаженої картоплі")
            };
            return recipes;
        }

        public static List<Recipe> GetRecipesByIngredients(List<string> ingredientsList)
        {
            List<Recipe> recipes = LoadRecipes().Where(recipe => recipe.IsIngredientPresent(ingredientsList)).ToList();

            return recipes;
        }

        public static List<RecipeShort> GetRecipeShortsFromRecipe(List<Recipe> recipeList)
        {
            List<RecipeShort> recipeShoprtList = new List<RecipeShort>();
            foreach (Recipe recipe in recipeList)
            {
                recipeShoprtList.Add(new RecipeShort(recipe.Name, recipe.Description));
            }
            return recipeShoprtList;
        }
    }
}