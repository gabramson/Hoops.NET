namespace Hoops.Service { 
    public class Team
    {
        public int Id { get; }
        public string Name { get; }
        public int Seed { get; }

        public Team(int id, string name, int seed)
        {
            if (id < 1 || id > 64)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be between 1 and 64.");

            if (seed < 1 || seed > 16)
                throw new ArgumentOutOfRangeException(nameof(seed), "Seed must be between 1 and 16.");

            Id = id;
            Name = name;
            Seed = seed;
        }
    }
}