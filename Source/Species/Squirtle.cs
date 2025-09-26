using TestProject_DDT_OOP_Pokemon.Source;

namespace TestProject_DDT_OOP_Pokemon.Source.Species
{
    internal class Squirtle : Pokemon
    {
        public Squirtle() : base(
            name: "Squirtle",
            types: new List<PokemonType> { PokemonType.Water }
        )
        { }
    }
}