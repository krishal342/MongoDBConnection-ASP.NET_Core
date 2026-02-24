using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBConnection.Config;
using MongoDBConnection.Models;

namespace MongoDBConnection.Services
{
    public class PassedOutStudentsService
    {
        private readonly IMongoCollection<PassedOutStudent> _studentsCollection;

        public PassedOutStudentsService(IMongoClient client, IOptions<DatabaseConfig> databaseConfig)
        {
            var database = client.GetDatabase(databaseConfig.Value.DatabaseName);

            _studentsCollection = database.GetCollection<PassedOutStudent>(databaseConfig.Value.PassedOutStudentsCollectionName);
        }

        // create
        public async Task CreateRecordAsync(PassedOutStudent record) =>
            await _studentsCollection.InsertOneAsync(record);

        // read all
        public async Task<List<PassedOutStudent>> GetAllRecord() =>
            await _studentsCollection.Find(_ => true).ToListAsync(); 

        // read by id
        public async Task<PassedOutStudent?> GetRecordById(string id) =>
            await _studentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
}
