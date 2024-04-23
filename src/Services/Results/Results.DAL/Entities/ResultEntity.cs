namespace Results.DAL.Entities
{
    public class ResultEntity
    {
        public ResultEntity(Guid id, string exercise, float weightKg, int numberOfRepetitions, Guid userId)
        {
            Id = id;
            Exercise = exercise;
            WeightKg = weightKg;
            NumberOfRepetitions = numberOfRepetitions;
            UserId = userId;
        }

        public Guid Id { get; }
        public string Exercise { get; }
        public float WeightKg { get; }
        public int NumberOfRepetitions { get; }
        public Guid UserId { get; }
    }
}
