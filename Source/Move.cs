namespace TestProject_DDT_OOP_Pokemon.Source
{
    public class Move
    {
        public string Name { get; }
        public int Power { get; }    // 1..255, default 100
        public int Speed { get; }    // 1..5, default 1
        public PokemonType Type { get; }
        public MoveType MoveType { get; }

        // Constructor por defecto (GREEN mínimo)
        public Move()
            : this(string.Empty, 100, 1, PokemonType.Normal, MoveType.Physical) { }

        // Constructor con parámetros (acepta named args como en los tests)
        public Move(string name = "", int power = 100, int speed = 1, PokemonType type = PokemonType.Normal, MoveType moveType = MoveType.Physical)
        {
            Name = name ?? string.Empty;
            Power = Clamp(power, 1, 255, 100);
            Speed = Clamp(speed, 1, 5, 1);
            Type = type;
            MoveType = moveType;
        }

        private static int Clamp(int value, int min, int max, int defaultIfInvalid)
        {
            if (value < min) return defaultIfInvalid;
            if (value > max) return max;
            return value;
        }
    }
}
