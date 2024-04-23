namespace Results.Api.Models
{
    public class ResultRequest
    {
        public ResultRequest(string exercise, float weightKg, int numberOfRepetitions, Guid userId)
        {
            Exercise = exercise;
            WeightKg = weightKg;
            NumberOfRepetitions = numberOfRepetitions;
            UserId = userId;
        }

        public string Exercise { get; }
        public float WeightKg { get; }
        public int NumberOfRepetitions { get; }
        public Guid UserId { get; }
    }
}
