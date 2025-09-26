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

    [TestFixture]
    public class DamageCalculationTests
    {
        // 40 casos en líneas individuales - mucho más compacto
        [TestCase( 1,  1,   1,   1,   1,   0, 0,    true,  TestName = "Case01_Damage_0_Special")]
        [TestCase( 2,  1,   1,   1,   1,   1, 1,    false, TestName = "Case02_Damage_1_Physical")]
        [TestCase( 3,  5,   50,  100, 50,  2, 16,   true,  TestName = "Case03_Damage_16_Special")]
        [TestCase( 4,  5,   50,  100, 50,  1, 5,    false, TestName = "Case04_Damage_5_Physical")]
        [TestCase( 5,  10,  20,  30,  15,  1, 5,    true,  TestName = "Case05_Damage_5_Special")]
        [TestCase( 6,  12,  40,  60,  80,  2, 9,    false, TestName = "Case06_Damage_9_Physical")]
        [TestCase( 7,  25,  80,  120, 60,  1, 40,   true,  TestName = "Case07_Damage_40_Special")]
        [TestCase( 8,  30,  100, 50,  100, 4, 58,   false, TestName = "Case08_Damage_58_Physical")]
        [TestCase( 9,  40,  150, 200, 150, 1, 37,   true,  TestName = "Case09_Damage_37_Special")]
        [TestCase(10, 50,  128, 200, 100, 1, 58,   false, TestName = "Case10_Damage_58_Physical")]
        [TestCase(11, 50,  128, 200, 100, 4, 455,  true,  TestName = "Case11_Damage_455_Special")]
        [TestCase(12, 60,  200, 250, 200, 1, 132,  false, TestName = "Case12_Damage_132_Physical")]
        [TestCase(13, 70,  180, 200, 100, 2, 435,  true,  TestName = "Case13_Damage_435_Special")]
        [TestCase(14, 80,  90,  45,  90,  1, 33,   false, TestName = "Case14_Damage_33_Physical")]
        [TestCase(15, 90,  255, 200, 50,  2, 1554, true,  TestName = "Case15_Damage_1554_Special")]
        [TestCase(16, 99,  255, 255, 1,   2, 108206, false, TestName = "Case16_Damage_108206_Physical")]
        [TestCase(17, 99,  255, 255, 255, 4, 856,  true,  TestName = "Case17_Damage_856_Special")]
        [TestCase(18, 99,  255, 255, 255, 0, 0,    false, TestName = "Case18_Damage_0_Physical")]
        [TestCase(19, 99,  255, 1,   255, 1, 2,    true,  TestName = "Case19_Damage_2_Special")]
        [TestCase(20, 45,  60,  10,  200, 1, 2,    false, TestName = "Case20_Damage_2_Physical")]
        [TestCase(21, 20,  30,  5,   250, 1, 1,    true,  TestName = "Case21_Damage_1_Special")]
        [TestCase(22, 2,   10,  1,   255, 1, 1,    false, TestName = "Case22_Damage_1_Physical")]
        [TestCase(23, 3,   5,   2,   3,   1, 1,    true,  TestName = "Case23_Damage_1_Special")]
        [TestCase(24, 15,  200, 255, 255, 1, 33,   false, TestName = "Case24_Damage_33_Physical")]
        [TestCase(25, 16,  200, 255, 254, 1, 34,   true,  TestName = "Case25_Damage_34_Special")]
        [TestCase(26, 17,  200, 255, 128, 1, 36,   false, TestName = "Case26_Damage_36_Physical")]
        [TestCase(27, 33,  77,  77,  77,  1, 25,   true,  TestName = "Case27_Damage_25_Special")]
        [TestCase(28, 48,  33,  99,  11,  4, 508,  false, TestName = "Case28_Damage_508_Physical")]
        [TestCase(29, 55,  44,  88,  22,  1, 44,   true,  TestName = "Case29_Damage_44_Special")]
        [TestCase(30, 66,  11,  11,  11,  1, 8,    false, TestName = "Case30_Damage_8_Physical")]
        [TestCase(31, 77,  123, 200, 100, 2, 326,  true,  TestName = "Case31_Damage_326_Special")]
        [TestCase(32, 88,  200, 100, 50,  4, 1197, false, TestName = "Case32_Damage_1197_Physical")]
        [TestCase(33, 10,  200, 200, 200, 0, 0,    true,  TestName = "Case33_Damage_0_Special")]
        [TestCase(34, 50,  255, 100, 50,  0, 0,    false, TestName = "Case34_Damage_0_Physical")]
        [TestCase(35, 75,  180, 255, 180, 0, 0,    true,  TestName = "Case35_Damage_0_Special")]
        [TestCase(36, 99,  255, 255, 1,   0, 0,    false, TestName = "Case36_Damage_0_Physical")]
        [TestCase(37, 25,  60,  40,  20,  0, 0,    true,  TestName = "Case37_Damage_0_Special")]
        [TestCase(38, 60,  100, 255, 128, 1, 40,   false, TestName = "Case38_Damage_40_Physical")]
        [TestCase(39, 80,  90,  45,  90,  1, 17,   true,  TestName = "Case39_Damage_17_Special")]
        [TestCase(40, 99,  200, 150, 150, 1, 84,   false, TestName = "Case40_Damage_84_Physical")]
        public void TestDamageCalculation(int caseNum, int level, int power, int attackStat, int defenseStat, double modifier, int expected, bool isSpecial)
        {
            // Obtener tipos según el modificador
            var (attackType, defenseTypes) = GetTypesForModifier(modifier);
            
            // Crear atacante (usar atacanteStat en la posición correcta)
            Pokemon attacker;
            Pokemon defender;
            if (isSpecial)
            {
                attacker = new Pokemon($"TestAttacker{caseNum}", level, 10, 10, attackStat, 10, new List<PokemonType> { attackType });
                defender = new Pokemon($"TestDefender{caseNum}", 1, 10, 10, 10, defenseStat, defenseTypes);
            }
            else
            {
                attacker = new Pokemon($"TestAttacker{caseNum}", level, attackStat, 10, 10, 10, new List<PokemonType> { attackType });
                defender = new Pokemon($"TestDefender{caseNum}", 1, 10, defenseStat, 10, 10, defenseTypes);
            }
            
            // Crear movimiento
            var moveType = isSpecial ? MoveType.Special : MoveType.Physical;
            var move = new Move($"TestMove{caseNum}", power, 1, attackType, moveType);
            
            // Ejecutar y verificar
            int damage = CombatCalculator.CalculateDamage(attacker, move, defender);
            Assert.That(damage, Is.EqualTo(expected), $"Case {caseNum} failed");
        }

        // Método helper para obtener tipos según el modificador
        private static (PokemonType attackType, List<PokemonType> defenseTypes) GetTypesForModifier(double modifier)
        {
            return modifier switch
            {
                0 => (PokemonType.Electric, new List<PokemonType> { PokemonType.Ground }),        // Inmune
                1 => (PokemonType.Normal, new List<PokemonType> { PokemonType.Normal }),          // Normal
                2 => (PokemonType.Water, new List<PokemonType> { PokemonType.Fire }),            // Super efectivo
                4 => (PokemonType.Water, new List<PokemonType> { PokemonType.Rock, PokemonType.Ground }), // Cuádruple
                _ => (PokemonType.Normal, new List<PokemonType> { PokemonType.Normal })
            };
        }
    }
}
