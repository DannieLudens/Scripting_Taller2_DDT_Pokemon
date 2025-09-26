using NUnit.Framework;
using TestProject_DDT_OOP_Pokemon.Source;

namespace TestProject_DDT_OOP_Pokemon;

[TestFixture]
public class CombatTests
{
    [TestFixture]
    public class TestPokemonAndMove
    {
        [Test]
        public void TestPokemon_DefaultValues()
        {
            Pokemon pikachu = new Pokemon();

            Assert.That(pikachu.Name, Is.EqualTo(""));  // por defecto vacío
            Assert.That(pikachu.Level, Is.EqualTo(1));  // mínimo 1
            Assert.That(pikachu.Atk, Is.EqualTo(0));
            Assert.That(pikachu.Def, Is.EqualTo(0));
            Assert.That(pikachu.SpAtk, Is.EqualTo(0));
            Assert.That(pikachu.SpDef, Is.EqualTo(0));
            Assert.That(pikachu.Types.Count, Is.EqualTo(0)); // sin tipo al inicio
            Assert.That(pikachu.Moves.Count, Is.EqualTo(0)); // sin movimientos al inicio
        }

        [Test]
        public void TestPokemon_CustomValues()
        {
            Pokemon charmander = new Pokemon(
                name: "Charmander",
                level: 5,
                atk: 52,
                def: 43,
                spAtk: 60,
                spDef: 50,
                types: new List<PokemonType> { PokemonType.Fire }
            );

            Assert.That(charmander.Name, Is.EqualTo("Charmander"));
            Assert.That(charmander.Level, Is.EqualTo(5));
            Assert.That(charmander.Atk, Is.EqualTo(52));
            Assert.That(charmander.Def, Is.EqualTo(43));
            Assert.That(charmander.SpAtk, Is.EqualTo(60));
            Assert.That(charmander.SpDef, Is.EqualTo(50));
            Assert.That(charmander.Types.Contains(PokemonType.Fire));
        }

        [Test]
        public void TestMove_DefaultValues()
        {
            Move tackle = new Move();

            Assert.That(tackle.Name, Is.EqualTo(""));
            Assert.That(tackle.Power, Is.EqualTo(0));
            Assert.That(tackle.Speed, Is.EqualTo(0));
            Assert.That(tackle.Type, Is.EqualTo(PokemonType.Normal));
            Assert.That(tackle.MoveType, Is.EqualTo(MoveType.Physical));
        }

        [Test]
        public void TestMove_CustomValues()
        {
            Move flamethrower = new Move(
                name: "Flamethrower",
                power: 90,
                speed: 100,
                type: PokemonType.Fire,
                moveType: MoveType.Special
            );

            Assert.That(flamethrower.Name, Is.EqualTo("Flamethrower"));
            Assert.That(flamethrower.Power, Is.EqualTo(90));
            Assert.That(flamethrower.Speed, Is.EqualTo(100));
            Assert.That(flamethrower.Type, Is.EqualTo(PokemonType.Fire));
            Assert.That(flamethrower.MoveType, Is.EqualTo(MoveType.Special));
        }
    }
}
