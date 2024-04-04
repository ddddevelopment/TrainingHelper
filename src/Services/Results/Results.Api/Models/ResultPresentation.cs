namespace Results.Api.Models
{
    public class ResultPresentation
    {
        public ResultPresentation(string exercise, float weightKg, int numberOfRepetitions)
        {
            Exercise = exercise;
            WeightKg = weightKg;
            NumberOfRepetitions = numberOfRepetitions;
        }

        public string Exercise { get; }
        public float WeightKg { get; }
        public int NumberOfRepetitions { get; }
    }
}
