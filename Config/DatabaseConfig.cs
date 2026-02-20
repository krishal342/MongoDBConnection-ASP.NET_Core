namespace MongoDBConnection.Config
{
    public class DatabaseConfig
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string StudentsCollectionName { get; set; } = null!;

        public string CoursesCollectionName { get; set; } = null!;

        public string EnrollmentsCollectionName { get; set; } = null!;
    }
}
