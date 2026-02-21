using Microsoft.AspNetCore.Mvc;
using MongoDBConnection.Models;
using MongoDBConnection.Services;

namespace MongoDBConnection.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class PassedOutStudentsController : ControllerBase
    {
        private readonly PassedOutStudentsService _studentsService;

        public PassedOutStudentsController(PassedOutStudentsService studentsService)
        {
            _studentsService = studentsService;

        }

        // move student record to passedOutStudents record

        // get all record

        // get record by id

    }
}
