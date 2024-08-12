using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Resources.Crafting {
    public class RecipeManager {
        
        private List<Recipe> _recipes = new List<Recipe>();

        public RecipeManager(List<Recipe> recipes) {
            _recipes = recipes;
        }

        // public bool TryCraft(List<RecipeComponent> resource, out RecipeComponent res) {
        //     if(_recipes.Any(x => x.RecipeRequirements.ToHashSet() == resource.ToHashSet())) {

        //     }
        // }
    }
}