using System;
using System.Collections.Generic;
using Assets.Scripts.Resources.Data;

namespace Assets.Scripts.Resources.Crafting {
    public class Recipe {
        public List<RecipeComponent> RecipeRequirements = new List<RecipeComponent>();
        public List<RecipeComponent> Result = new List<RecipeComponent>();
    }
    [Serializable]
    public class RecipeComponent {
        public Resource resource;
        public int count = 1;
    }
}