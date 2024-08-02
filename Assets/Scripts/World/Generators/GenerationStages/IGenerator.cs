using System.Collections;

namespace Assets.Scripts.World.Generators.GenerationStages
{
    public interface IGenerator
    {
        int CostGeneration { get; } // The cost of generating this stage
        int Order { get; }
        string NameGeneration {get;}
        IEnumerator Generate();
    }
}
