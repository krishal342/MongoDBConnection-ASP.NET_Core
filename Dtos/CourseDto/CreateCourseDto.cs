namespace MongoDBConnection.Dtos.CourseDto
{
    public class CreateCourseDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CreditHours { get; set; }
        public List<string> Student { get; set; } = new();
    }
}
