namespace MongoDBConnection.Dtos.StudentDto
{
    public class CreateStudentDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime AdmissionOn { get; set; } = DateTime.UtcNow;
        public List<string> Course { get; set; } = new();

    }
}
