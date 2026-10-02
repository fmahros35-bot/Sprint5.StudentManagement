using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sprint5.StudentManagement.Data;
using Sprint5.StudentManagement.Entities;

namespace Sprint5.StudentManagement.Services;

    public class CourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.AsNoTracking().ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.CourseId == id);
        }

        public async Task AddCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }
    }
