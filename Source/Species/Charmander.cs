using TestProject_DDT_OOP_Pokemon.Source;

namespace TestProject_DDT_OOP_Pokemon.Source.Species
{
    public class Charmander : Pokemon
    {
        public Charmander() : base(
            name: "Charmander",
            types: new List<PokemonType> { PokemonType.Fire }
        )
        { }
    }
}