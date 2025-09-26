using TestProject_DDT_OOP_Pokemon.Source;

namespace TestProject_DDT_OOP_Pokemon.Source.Species
{
    internal class Pikachu : Pokemon
    {
        public Pikachu() : base(
            name: "Pikachu",
            types: new List<PokemonType> { PokemonType.Electric }
        )
        { }
    }
}