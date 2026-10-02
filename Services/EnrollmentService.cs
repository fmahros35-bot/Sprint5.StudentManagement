using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sprint5.StudentManagement.Data;
using Sprint5.StudentManagement.Entities;

namespace Sprint5.StudentManagement.Services
{
    public class EnrollmentService
    {
        private readonly AppDbContext _context;

        public EnrollmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Enrollment>> GetAllEnrollmentsAsync()
        {
            return await _context.Enrollments.AsNoTracking().ToListAsync();
        }

        public async Task<Enrollment?> GetEnrollmentByIdAsync(int studentId, int courseId)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }

        public async Task AddEnrollmentAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEnrollmentAsync(Enrollment enrollment)
        {
            _context.Enrollments.Update(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEnrollmentAsync(int studentId, int courseId)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Student>> GetStudentsInCourseAsync(int courseId)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Where(e => e.CourseId == courseId)
                .Select(e => e.Student)
                .ToListAsync();
        }

        public async Task<List<Enrollment>> GetCoursesForStudentAsync(int studentId)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Course)
                .ToListAsync();
        }

        public async Task<double?> GetAverageGradePerCourseAsync(int courseId)
        {
            var grades = await _context.Enrollments
                .AsNoTracking()
                .Where(e => e.CourseId == courseId && e.Grade.HasValue)
                .Select(e => e.Grade!.Value)
                .ToListAsync();

            if (!grades.Any())
                return null;

            return grades.Average();
        }
    }
}
