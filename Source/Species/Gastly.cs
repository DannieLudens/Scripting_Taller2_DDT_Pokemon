using TestProject_DDT_OOP_Pokemon.Source;

namespace TestProject_DDT_OOP_Pokemon.Source.Species
{
    public class Gastly : Pokemon
    {
        public Gastly() : base(
            name: "Gastly",
            types: new List<PokemonType> { PokemonType.Ghost, PokemonType.Poison }
        )
        { }
    }
}