namespace TestProject_DDT_OOP_Pokemon.Source
{
    public static class CombatCalculator
    {
        // Tabla de efectividad
        private static readonly float[,] TypeEffectiveness = new float[,]
        {
            // attacking   Defending
            //             Rock  Ground Water Electric Fire  Grass Ghost Poison Psychic Bug 
            /* Rock     */ { 1,    0.5f,  1,     1,      2,    0.5f,  1,    1,     1,     2 },
            /* Ground   */ { 2,    1,    1,     2,      2,    0.5f,  1,    2,     1,     0.5f },
            /* Water    */ { 2,    2,    0.5f,   1,      2,    0.5f,  1,    1,     1,     1 },
            /* Electric */ { 1,    0,    2,     0.5f,    1,    0.5f,  1,    1,     1,     1 },
            /* Fire     */ { 0.5f,  1,    0.5f,   1,      0.5f,  2,    1,    1,     1,     2 },
            /* Grass    */ { 2,    2,    2,     1,      0.5f,  0.5f,  1,    0.5f,   1,     0.5f },
            /* Ghost    */ { 1,    1,    1,     1,      1,    1,    2,    1,     2,     1 },
            /* Poison   */ { 0.5f,  0.5f,  1,     1,      1,    2,    0.5f,  0.5f,   1,     1 },
            /* Psychic  */ { 1,    1,    1,     1,      1,    1,    1,    2,     0.5f,   0.5f },
            /* Bug      */ { 1,    1,    1,     1,      0.5f,  2,    1,    1,     2,     1 }
        };        // esto con un if anidado y un switch y sale mas mejor segun el profesor



        /// <summary>
        /// Calcula el modificador de tipo (MOD) para un ataque
        /// </summary>
        /// <param name="attackingType">Tipo del movimiento atacante</param>
        /// <param name="defendingTypes">Lista de tipos del Pokémon defensor</param>
        /// <returns>Modificador final (producto de todos los modificadores)</returns>
        public static float CalculateTypeModifier(PokemonType attackingType, List<PokemonType> defendingTypes)
        {
            float totalModifier = 1;

            // Si el tipo atacante es Normal o el defensor no tiene tipos, modificador = 1
            if (attackingType == PokemonType.Normal || defendingTypes.Count == 0)
            {
                return totalModifier;
            }

            foreach (var defendingType in defendingTypes)
            {
                // Si el tipo defensor es Normal, no afecta el modificador
                if (defendingType == PokemonType.Normal)
                {
                    continue;
                }

                // Obtener índices en la tabla (Normal está al final, no en la tabla)
                int attackIndex = (int)attackingType;
                int defendIndex = (int)defendingType;

                // Solo aplicar si ambos tipos están en la tabla (índices 0-9)
                if (attackIndex < 10 && defendIndex < 10)
                {
                    totalModifier *= TypeEffectiveness[attackIndex, defendIndex];
                }
            }
            return totalModifier;
        }

        /// <summary>
        /// Calcula el daño total de un ataque usando las fórmulas del enunciado
        /// </summary>
        /// <param name="attacker">Pokémon atacante</param>
        /// <param name="move">Movimiento usado</param>
        /// <param name="defender">Pokémon defensor</param>
        /// <returns>Daño final (entero, redondeado hacia abajo)</returns>
        public static float CalculateDamage(Pokemon attacker, Move move, Pokemon defender)
        {
            // Calcular modificador de tipo
            float modifier = CalculateTypeModifier(move.Type, defender.Types);

            // Si el modificador es 0, no hay daño
            if (modifier == 0)
            {
                return 0;
            }

            float baseDamage;

            // Usar la fórmula apropiada según el tipo de movimiento
            if (move.MoveType == MoveType.Physical)
            {
                // Fórmula para ataques físicos
                baseDamage = ((2 * (attacker.Level / 5) + 2) * (move.Power * ((float)attacker.Atk / defender.Def) + 2)) / 50;
            }
            else // MoveType.Special
            {
                // Fórmula para ataques especiales  
                baseDamage = ((2 * (attacker.Level / 5) + 2) * (move.Power * ((float)attacker.SpAtk / defender.SpDef) + 2)) / 50;
            }

            // Aplicar modificador de tipo
            float finalDamage = baseDamage * modifier;

            // Redondear hacia abajo y retornar como entero
            //int floored = (int)Math.Floor(finalDamage);
            //if (modifier > 0 && floored < 1) return 1; // daño mínimo
            //return floored;
            return Math.Max(1,finalDamage); // daño mínimo 1 si el modificador es > 0
        }
    }
}