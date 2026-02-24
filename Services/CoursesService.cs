using MongoDBConnection.Models;
using MongoDBConnection.Config;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace MongoDBConnection.Services
{
    public class CoursesService
    {
        private readonly IMongoCollection<Course> _coursesCollection;

        public CoursesService(IMongoClient client, IOptions<DatabaseConfig> databaseConfig)
        {
            var database = client.GetDatabase(databaseConfig.Value.DatabaseName);

            _coursesCollection = database.GetCollection<Course>(databaseConfig.Value.CoursesCollectionName);

        }

        // create course
        public async Task CreateCourseAsync(Course newCourse) =>
            await _coursesCollection.InsertOneAsync(newCourse);

        // read all courses
        public async Task<List<Course>> GetAllCourseAsync() =>
            await _coursesCollection.Find(_ => true).ToListAsync();

        // read course by id
        public async Task<Course?> GetCourseByIdAsync(string id) => 
            await _coursesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // update course 
        public async Task UpdateCourseAsync(string id, Course updatedCourse) =>
            await _coursesCollection.ReplaceOneAsync(x => x.Id == id, updatedCourse);

        // adding currentStudent
        public async Task AddStudentAync(string courseId, string studentId) =>
            await _coursesCollection.UpdateOneAsync(
                c => c.Id == courseId,
                Builders<Course>.Update.Push(c => c.CurrentStudent, studentId)
                );

        // deleting currentStudent
        public async Task RemoveStudentAsync(string courseId, string studentId) =>
            await _coursesCollection.UpdateOneAsync(
                c => c.Id == courseId,
                Builders<Course>.Update.Pull(c => c.CurrentStudent, studentId)
               );

        // move currentStudent to passedOutStudent
        public async Task MoveStudentAsync(string courseId, string oldStudentId, string newStudentId) =>
            await _coursesCollection.UpdateOneAsync(
                c => c.Id == courseId,
                Builders<Course>.Update.Pull(c => c.CurrentStudent, oldStudentId).Push(c => c.PassedOutStudent, newStudentId)
                );

    }
}
