using UnityEngine;

namespace Resource
{
    public class Resource : ScriptableObject
    {
        public Sprite SpriteInInventary;
        public string Name;
        public int Count;
    }

    public class ResourceInTheWorld : Resource
    {

    }
}
