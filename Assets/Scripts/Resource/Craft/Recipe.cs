using System.Collections.Generic;
using Assets.Scripts.Resources.Data;

namespace Assets.Scripts.Resources.Crafting {
    public class Recipe {
        public Dictionary<Resource, int> Requirements = new Dictionary<Resource, int>();
        public Dictionary<Resource, int> Results = new Dictionary<Resource, int>();
    }
}