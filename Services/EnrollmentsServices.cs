using MongoDBConnection.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDBConnection.Config;

namespace MongoDBConnection.Services
{
    public class EnrollmentsServices
    {
        private readonly IMongoCollection<Enrollment> _enrollmentsCollection;

        public EnrollmentsServices(IMongoClient client, IOptions<DatabaseConfig> databaseConfig)
        {
            var database = client.GetDatabase(databaseConfig.Value.DatabaseName);
            _enrollmentsCollection = database.GetCollection<Enrollment>(databaseConfig.Value.EnrollmentsCollectionName);
        }

        // create enrollment
        public async Task CreateEnrollmentAsync(Enrollment newEnrollment) =>
            await _enrollmentsCollection.InsertOneAsync(newEnrollment);

         // get all enrollment
        public async Task<List<Enrollment>> GetAllEnrollmentAsync() =>
            await _enrollmentsCollection.Find(_ => true).ToListAsync();

        // get enrollment by id 
        public async Task<Enrollment?> GetEnrollmentById(string id) =>
            await _enrollmentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // delete enrollment by id
        public async Task DeleteAsync(string id) =>
            await _enrollmentsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
