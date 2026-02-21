using Microsoft.AspNetCore.Mvc;
using MongoDBConnection.Dtos.StudentDto;
using MongoDBConnection.Models;
using MongoDBConnection.Services;

namespace MongoDBConnection.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsService _studentsService;
        private readonly PassedOutStudentsService _passedOutStudentsService;
        private readonly CoursesService _coursesService;

        public StudentsController(StudentsService studentsService, PassedOutStudentsService passedOutStudentsService, CoursesService coursesService)
        {
            _studentsService = studentsService;
            _passedOutStudentsService = passedOutStudentsService;
            _coursesService = coursesService;

        }

        // get all students
        [HttpGet]
        public async Task<List<Student>> GetAllStudent() =>
            await _studentsService.GetAllStudentAsync();

        // get student by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(string id)
        {
            var student = await _studentsService.GetStudentByIdAsync(id);

            if(student == null)
            {
                return NotFound();
            }
            return student;
        }


        // create student
        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentDto dtoStudent)
        {
            var newStudent = new Student
            {
                Name = dtoStudent.Name,
                Email = dtoStudent.Email.ToLower(),
                Address = dtoStudent.Address,
                AdmissionOn = dtoStudent.AdmissionOn,
                Course = dtoStudent.Course,
            };

            await _studentsService.CreateStudentAsync(newStudent);

            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id }, newStudent);
        }

        // update student by id
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(string id, UpdateStudentDto dtoStudent)
        {
            var student = await _studentsService.GetStudentByIdAsync(id);

            if(student is null)
            {
                return NotFound();
            }

            var updatedStudent = new Student
            {
                Id = id,
                Name = dtoStudent.Name is not null ? dtoStudent.Name : student.Name,
                Email = dtoStudent.Email is not null ? dtoStudent.Email.ToLower() : student.Email,
                Address = dtoStudent.Address is not null ? dtoStudent.Address : student.Address,
                AdmissionOn = student.AdmissionOn,
                Course = student.Course,
            };

            await _studentsService.UpdateStudentAsync(id, updatedStudent);

            return updatedStudent;
        }

        // move student record to passedOutStudent collection by id
        [HttpDelete("{id}/softDelete")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var student = await _studentsService.GetStudentByIdAsync(id);

            if(student is null)
            {
                return NotFound();
            }

            var passedOutStudent = new PassedOutStudent
            {
                Name = student.Name,
                Email = student.Email,
                Address = student.Address,
                AdmissionOn = student.AdmissionOn,
                Course = student.Course,
            };

            await _passedOutStudentsService.CreateRecordAsync(passedOutStudent);

            foreach(string c in passedOutStudent.Course)
            {
                await _coursesService.MoveStudentAsync(c, id);
            }

            await _studentsService.DeleteStudentAsync(id);

            return NoContent();
        }

    }
}
