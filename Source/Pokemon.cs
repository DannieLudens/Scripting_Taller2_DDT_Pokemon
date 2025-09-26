namespace TestProject_DDT_OOP_Pokemon.Source
{
    public class Pokemon
    {
        public string Name { get; }
        public int Level { get; }    // 1..99 (default 1)
        public int Atk { get; }      // 1..255 (default 10)
        public int Def { get; }      // 1..255 (default 10)
        public int SpAtk { get; }    // 1..255 (default 10)
        public int SpDef { get; }    // 1..255 (default 10)

        // Una especie puede tener 0, 1 o 2 tipos. (lista para facilidad)
        public List<PokemonType> Types { get; }

        // Movimientos: mínimo 1, máximo 4.
        public List<Move> Moves { get; }

        // Constructor por defecto: valores por defecto según enunciado,
        // y al menos un movimiento por defecto.
        public Pokemon()
            : this(string.Empty, 
                  1, 
                  10, 
                  10, 
                  10, 
                  10, 
                  null, 
                  null)
        { }

        // Constructor completo (acepta named params como en tests)
        public Pokemon(string name = "", 
                         int level = 1, 
                           int atk = 10, 
                           int def = 10, 
                         int spAtk = 10, 
                         int spDef = 10, 
          List<PokemonType>? types = null, 
                 List<Move>? moves = null)
        {
            Name = name ?? string.Empty;
            Level = Clamp(level, 1, 99, 1);

            // Para stats, permitir 0:
            Atk = atk < 0 ? 0 : (atk > 255 ? 255 : atk);
            Def = def < 0 ? 0 : (def > 255 ? 255 : def);
            SpAtk = spAtk < 0 ? 0 : (spAtk > 255 ? 255 : spAtk);
            SpDef = spDef < 0 ? 0 : (spDef > 255 ? 255 : spDef);

            Types = types ?? new List<PokemonType>();
            if (Types.Count > 2)
            {
                Types = Types.GetRange(0, 2);
            }

            Moves = moves ?? new List<Move> { new Move() }; // asegurar al menos 1
            if (Moves.Count < 1)
            {
                Moves.Add(new Move());
            }
            else if (Moves.Count > 4)
            {
                Moves = Moves.GetRange(0, 4);
            }
        }

        private static int Clamp(int value, int min, int max, int defaultValue)
        {
            if (value < min) return defaultValue;
            if (value > max) return max;
            return value;
        }
    }
}