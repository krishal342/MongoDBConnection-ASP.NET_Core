using MongoDBConnection.Models;
using MongoDBConnection.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MongoDBConnection.Services
{
    public class StudentsService
    {
        private readonly IMongoCollection<Student> _studentsCollection;

        public StudentsService(IMongoClient client,IOptions<DatabaseConfig> databaseConfig)
        {

            var database = client.GetDatabase(databaseConfig.Value.DatabaseName);
            
            _studentsCollection = database.GetCollection<Student>(databaseConfig.Value.StudentsCollectionName);
        }

        //create student
        public async Task CreateStudentAsync(Student newStudent) =>
            await _studentsCollection.InsertOneAsync(newStudent);

        //get all students
        public async Task<List<Student>> GetAllStudentAsync() => 
            await _studentsCollection.Find(_ => true).ToListAsync();

        //get student by id
        public async Task<Student?> GetStudentByIdAsync(string id) =>
            await _studentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        //update student by id
        public async Task UpdateStudentAsync(string id,Student updatedStudent) =>
            await _studentsCollection.ReplaceOneAsync(x => x.Id == id, updatedStudent);
        
        //delete student by id
        public async Task DeleteStudentAsync(string id) => 
            await _studentsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
