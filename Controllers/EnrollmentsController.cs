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
            await _enrollmentsServices.CreateEnrollmentAsync(newEnrollment);

            var student = await _studentsService.GetStudentByIdAsync(newEnrollment.Student);
            if (student is null)
            {
                return NotFound("Student record not found.");
            }

            var course = await _coursesService.GetCourseByIdAsync(newEnrollment.Course);
            if (course is null)
            {
                return NotFound("Course record not found.");
            }

            student.Course.Add(newEnrollment.Course);
            course.Student.Add(newEnrollment.Student);

            await _studentsService.UpdateStudentAsync(student.Id!, student);
            await _coursesService.UpdateCourseAsync(course.Id!, course);

            return CreatedAtAction(nameof(GetEnrollmentById), new { id = newEnrollment.Id }, newEnrollment);
        }

        // delete enrollment by id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEnrollment(string id)
        {
            var enrollment = await _enrollmentsServices.GetEnrollmentById(id);
            if (enrollment is null)
            {
                return NotFound("Enrollment record not found.");
            }


            var student = await _studentsService.GetStudentByIdAsync(enrollment.Student);
            if (student is null)
            {
                return NotFound("Student record not found.");
            }

            var course = await _coursesService.GetCourseByIdAsync(enrollment.Course);
            if (course is null)
            {
                return NotFound("Course record not found.");
            }

            //Console.WriteLine("Student Courses: " + string.Join(", ", student.Course));
            //Console.WriteLine("Course Students: " + string.Join(", ", course.Student));
            //Console.WriteLine("Enrollment.Course: " + enrollment.Course);
            //Console.WriteLine("Enrollment.Student: " + enrollment.Student);

            //student.Course.Remove(enrollment.Course);
            //course.Student.Remove(enrollment.Student);
            student.Course.RemoveAll(c => c == enrollment.Course);
            course.Student.RemoveAll(s => s == enrollment.Student);

            await _enrollmentsServices.DeleteAsync(id);

            return NoContent();
        }
    }
}
