namespace Results.Domain.Models
{
    public class Result
    {
        public Result(string exercise, float weightKg, int numberOfRepetitions)
        {
            Exercise = exercise;
            WeightKg = weightKg;
            NumberOfRepetitions = numberOfRepetitions;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public string Exercise { get; }
        public float WeightKg { get; }
        public int NumberOfRepetitions { get; }
    }
}
