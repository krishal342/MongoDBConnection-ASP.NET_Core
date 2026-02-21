using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDBConnection.Models;
using MongoDBConnection.Services;

namespace MongoDBConnection.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly EnrollmentsServices _enrollmentsServices;
        private readonly StudentsService _studentsService;
        private readonly CoursesService _coursesService;

        public EnrollmentsController(EnrollmentsServices enrollmentsServices, StudentsService studentsService, CoursesService coursesService)
        {
            _enrollmentsServices = enrollmentsServices;
            _studentsService = studentsService;
            _coursesService = coursesService;

        }


        // get all enrollment
        [HttpGet]
        public async Task<List<Enrollment>> GetAllEnrollment() =>
            await _enrollmentsServices.GetAllEnrollmentAsync();

        // get enrollment by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollmentById(string id)
        {
            var enrollment = await _enrollmentsServices.GetEnrollmentById(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return enrollment;
        }


        [HttpPost]
        // create enrollment
        public async Task<IActionResult> CreateEnrollment(Enrollment newEnrollment)
        {
            // create enrollment record
            await _enrollmentsServices.CreateEnrollmentAsync(newEnrollment);

            // adding courseId in student record
            await _studentsService.AddCourseAsync(newEnrollment.Student, newEnrollment.Course);

            // adding studentId in course record
            await _coursesService.AddStudentAync(newEnrollment.Course, newEnrollment.Student);

            return CreatedAtAction(nameof(GetEnrollmentById), new { id = newEnrollment.Id }, newEnrollment);
        }

        // delete enrollment by id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEnrollment(string id)
        {
            var enrollment = await _enrollmentsServices.GetEnrollmentById(id);
            if (enrollment is null)
            {
                return NotFound();
            }

            // delete courseId from student record
            await _studentsService.RemoveCourseAsync(enrollment.Student, enrollment.Course);

            // delete studentId from course record
            await _coursesService.RemoveStudentAsync(enrollment.Course, enrollment.Student);

            // delete enrollment record
            await _enrollmentsServices.DeleteAsync(id);

            return NoContent();
        }
    }
}
