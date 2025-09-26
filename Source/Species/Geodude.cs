using TestProject_DDT_OOP_Pokemon.Source;

namespace TestProject_DDT_OOP_Pokemon.Source.Species
{
    public class Geodude : Pokemon
    {
        public Geodude() : base(
            name: "Geodude",
            types: new List<PokemonType> { PokemonType.Rock, PokemonType.Ground }
        )
        { }
    }
}