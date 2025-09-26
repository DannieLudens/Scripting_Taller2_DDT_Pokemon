using NUnit.Framework;
using TestProject_DDT_OOP_Pokemon.Source;
using TestProject_DDT_OOP_Pokemon.Source.Species;

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
            Assert.That(pikachu.Atk, Is.EqualTo(10));
            Assert.That(pikachu.Def, Is.EqualTo(10));
            Assert.That(pikachu.SpAtk, Is.EqualTo(10));
            Assert.That(pikachu.SpDef, Is.EqualTo(10));
            Assert.That(pikachu.Types.Count, Is.EqualTo(0)); // sin tipo al inicio
            Assert.That(pikachu.Moves.Count, Is.EqualTo(1));
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
            Assert.That(tackle.Power, Is.EqualTo(100));
            Assert.That(tackle.Speed, Is.EqualTo(1));
            Assert.That(tackle.Type, Is.EqualTo(PokemonType.Normal));
            Assert.That(tackle.MoveType, Is.EqualTo(MoveType.Physical));
        }

        [Test]
        public void TestMove_CustomValues()
        {
            Move flamethrower = new Move(
                name: "Flamethrower",
                power: 90,
                speed: 2,
                type: PokemonType.Fire,
                moveType: MoveType.Special
            );

            Assert.That(flamethrower.Name, Is.EqualTo("Flamethrower"));
            Assert.That(flamethrower.Power, Is.EqualTo(90));
            Assert.That(flamethrower.Speed, Is.EqualTo(2));
            Assert.That(flamethrower.Type, Is.EqualTo(PokemonType.Fire));
            Assert.That(flamethrower.MoveType, Is.EqualTo(MoveType.Special));
        }
    }

    [TestFixture]
    public class TypeModifierTests
    {
        [TestCase(PokemonType.Fire, PokemonType.Water, 0.5)]      // Fire vs Water = No muy efectivo
        [TestCase(PokemonType.Water, PokemonType.Fire, 2.0)]      // Water vs Fire = Super efectivo  
        [TestCase(PokemonType.Electric, PokemonType.Ground, 0.0)] // Electric vs Ground = Inmune
        [TestCase(PokemonType.Normal, PokemonType.Rock, 1.0)]     // Normal vs cualquiera = Normal
        [TestCase(PokemonType.Rock, PokemonType.Fire, 2.0)]       // Rock vs Fire = Super efectivo
        [TestCase(PokemonType.Fire, PokemonType.Rock, 0.5)]       // Fire vs Rock = No muy efectivo
        public void TestSingleTypeModifiers(PokemonType attackType, PokemonType defendType, double expectedMod)
        {
            var defendingTypes = new List<PokemonType> { defendType };
            double result = CombatCalculator.CalculateTypeModifier(attackType, defendingTypes);
            Assert.That(result, Is.EqualTo(expectedMod).Within(0.01));
        }

        [Test] 
        public void TestDualTypeModifiers_GeodudeBareExamples()
        {
            // Geodude es Rock/Ground
            var geodude = new Geodude();
            
            // Water vs Rock/Ground = 2.0 × 2.0 = 4.0 (cuádruple daño)
            double waterMod = CombatCalculator.CalculateTypeModifier(PokemonType.Water, geodude.Types);
            Assert.That(waterMod, Is.EqualTo(4.0).Within(0.01));
            
            // Electric vs Rock/Ground = 1.0 × 0.0 = 0.0 (inmune)
            double electricMod = CombatCalculator.CalculateTypeModifier(PokemonType.Electric, geodude.Types);
            Assert.That(electricMod, Is.EqualTo(0.0).Within(0.01));
        }
        
        [Test]
        public void TestDualTypeModifiers_GastlyExamples()
        {
            // Gastly es Ghost/Poison  
            var gastly = new Gastly();
            
            // Psychic vs Ghost/Poison = 2.0 × 1.0 = 2.0
            double psychicMod = CombatCalculator.CalculateTypeModifier(PokemonType.Psychic, gastly.Types);
            Assert.That(psychicMod, Is.EqualTo(2.0).Within(0.01));
        }
    }

} 
