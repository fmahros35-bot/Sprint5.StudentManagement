using Sprint5.StudentManagement.Data;
using Sprint5.StudentManagement.Entities;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint5.StudentManagement.Services
{
    public class StudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.AsNoTracking().ToListAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.StudentId == id);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Student>> SearchStudentsByNameAsync(string namePart)
        {
            return await _context.Students
                .AsNoTracking()
                .Where(s => s.FullName.Contains(namePart))
                .ToListAsync();
        }

        public async Task<Student?> GetStudentWithEnrollmentsAsync(int studentId)
        {
            return await _context.Students
                .AsNoTracking()
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);
        }
    }
}