using System.Collections;

namespace Assets.Scripts.World.Generators.GenerationStages
{
    internal interface IGenerator
    {
        int CostGeneration { get; } // The cost of generating this stage
        string NameGeneration {get;}
        IEnumerator Generate();
    }
}
