namespace Results.Domain.Models
{
    public class Result
    {
        public Result(Guid id, string exercise, float weightKg, int numberOfRepetitions)
        {
            Id = id;
            Exercise = exercise;
            WeightKg = weightKg;
            NumberOfRepetitions = numberOfRepetitions;
        }

        public Guid Id { get; }
        public string Exercise { get; }
        public float WeightKg { get; }
        public int NumberOfRepetitions { get; }
    }
}
