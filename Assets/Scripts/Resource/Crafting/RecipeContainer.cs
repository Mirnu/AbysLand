using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Inventory.Crafting;
using Zenject;

namespace Assets.Scripts.Resources.Crafting {
    public class RecipeContainer {
        public List<Recipe> AllRecipes = new List<Recipe>();

        public RecipeContainer(List<Recipe> recipes) {
            AllRecipes = recipes;
        }

        public void TryFindCraft(List<RecipeComponent> l, out RecipeComponent res) {
            if(AllRecipes.Any(x => x.RecipeRequirements.OrderBy(y => y).SequenceEqual(l.OrderBy(x => x)))) {
                res = AllRecipes.Find(x => x.RecipeRequirements.OrderBy(y => y).SequenceEqual(l.OrderBy(x => x))).Result;
            }
            res = null;
        }
    }
}