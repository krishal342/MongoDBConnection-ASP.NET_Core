# MongoDBConnection-ASP.NET_Core

# Student routes
- /students -> POST request to create student -> take 'name', 'email', 'address'
- /students -> GET request to get all students
- /students/:id -> GET request to get student by id
- /students/:id -> PUT request to update student by id -> take 'name' or 'email' or 'address'
- /students/:id/softDelete -> DELETE request that move student record to passedOutStudents collection

# Course routes
- /courses -> POST request to create course -> take 'title', 'description', 'creditHours'
- /courses -> GET request to get all courses
- /courses/:id -> GET request to get course by id
- /courses/:id -> PUT request to update course by id -> take 'title' or 'description' or 'creditHours'
- /courses/:id -> DELETE request to delete course by id

# Enrollment routes

- /enrollments -> POST request to create enrollment -> take studentId as 'student' and courseId as 'course' 
- /enrollments -> GET request to get all enrollment
- /enrollments:id -> GET request to get enrollment by id
- /ebrikknebts:id -> DELETE request to delete enrollment by id