namespace MongoDBConnection.Dtos.CourseDto
{
    public class UpdateCourseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? CreditHours { get; set; }
        public List<string>? Student {  get; set; }
    }
}
