using Sprint5.StudentManagement.Data;
using Sprint5.StudentManagement.Entities;
using Sprint5.StudentManagement.Services;

var context = new AppDbContext();

var studentService = new StudentService(context);
var courseService = new CourseService(context);
var enrollmentService = new EnrollmentService(context);

var allStudents = await studentService.GetAllStudentsAsync();
Console.WriteLine($"Total Students: {allStudents.Count}");

var searchResult = await studentService.SearchStudentsByNameAsync("Fatma");
Console.WriteLine($"Search Results Found: {searchResult.Count}");

var enrollment = new Enrollment
{
    StudentId = 1,
    CourseId = 1,
    EnrollmentDate = DateTime.Now,
    Grade = 95
};
await enrollmentService.AddEnrollmentAsync(enrollment);

var avgGrade = await enrollmentService.GetAverageGradePerCourseAsync(1);
Console.WriteLine($"Average Grade for Course 1: {avgGrade}");

Console.WriteLine("All tests executed successfully!");