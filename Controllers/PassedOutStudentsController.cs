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


        // get all record
        [HttpGet]
        public async Task<List<PassedOutStudent>> GetAllRecord() =>
            await _studentsService.GetAllRecord();

        // get record by id
        [HttpGet("{id}")]
        public async Task<ActionResult<PassedOutStudent>> GetRecordById(string id)
        {
            
            var student = await _studentsService.GetRecordById(id);

            if(student is null)
            {
                return NotFound();
            }

            return student;
        }

    }
}
