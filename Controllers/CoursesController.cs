using Microsoft.AspNetCore.Mvc;
using MongoDBConnection.Dtos.CourseDto;
using MongoDBConnection.Models;
using MongoDBConnection.Services;

namespace MongoDBConnection.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly CoursesService _coursesService;

        public CoursesController(CoursesService coursesService) => _coursesService = coursesService;

        // get all courses
        [HttpGet]
        public async Task<List<Course>> GetAllCourse() =>
            await _coursesService.GetAllCourseAsync();

        // get course by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseById(string id)
        {
            var course = await _coursesService.GetCourseByIdAsync(id);

            if (course is null)
            {
                return NotFound();
            }

            return course;
        }

        // create course
        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course newCourse)
        {
            await _coursesService.CreateCourseAsync(newCourse);

            return CreatedAtAction(nameof(GetCourseById), new { id = newCourse.Id}, newCourse);
        }

        // update course by id
        [HttpPut("{id}")]
        public async Task<ActionResult<Course>> UpdateCourse(string id, UpdateCourseDto dtoCourse)
        {
            var course = await _coursesService.GetCourseByIdAsync(id);

            if (course is null)
            {
                return NotFound();
            }

            var updatedCourse = new Course
            {
                Id = id,
                Title = dtoCourse.Title is not null ? dtoCourse.Title : course.Title,
                Description = dtoCourse.Description is not null ? dtoCourse.Description : course.Description,
                CreditHours = (int)(dtoCourse.CreditHours is not null ? dtoCourse.CreditHours : course.CreditHours),
                CurrentStudent = course.CurrentStudent,
                PassedOutStudent = course.PassedOutStudent,
            };

            await _coursesService.UpdateCourseAsync(id, updatedCourse);

            return updatedCourse;

        }

    }

}
