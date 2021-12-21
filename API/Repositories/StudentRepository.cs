using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
  public class StudentRepository : IStudentRepository
  {
    private readonly StudentAppDataContext _context;

    public StudentRepository(StudentAppDataContext context)
      {
      _context = context;
    }

    public async Task<Student> AddStudent(Student request)
    {
    var student=await _context.AddAsync(request);
    await _context.SaveChangesAsync();
    return student.Entity;
    }

    public async Task<Student> DeleteStudent(Guid studentId)
    {
      var student=await GetStudentAsync(studentId);
      if(student != null)
      {
        _context.Students.Remove(student);
       await _context.SaveChangesAsync();
       return student;
      }
      return null;
    }

    public async Task<bool> Exists(Guid studentId)
    {
    return await _context.Students.AnyAsync(x=>x.Id==studentId);
    }

    public async Task<List<Gender>> GetGendersAsync()
    {
    return await _context.Genders.ToListAsync();
    }

    public async Task<Student> GetStudentAsync(Guid studentId)
    {
         return await _context.Students.Include(nameof(Gender)).Include(nameof(Address))
         .FirstOrDefaultAsync(x=>x.Id==studentId);
    }

    public async Task<List<Student>> GetStudentsAsync()
    {
   return await _context.Students.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
    }

    public async Task<bool> UpdateProfileImage(Guid studentId, string prfileImageUrl)
    {
      var student=await GetStudentAsync(studentId);
      if(student != null)
      {
        student.ProfileImageUrl=prfileImageUrl;
        await _context.SaveChangesAsync();
        return true;
      }
      return false;
    }

    public async Task<Student> UpdateStudent(Guid studentId, Student request)
    {
      var existingStudent=await GetStudentAsync(studentId);
      if(existingStudent !=null)
      {
existingStudent.FirstName=request.FirstName;
existingStudent.LastName=request.LastName;
existingStudent.DateOfBirth=request.DateOfBirth;
existingStudent.Email=request.Email;
existingStudent.Mobile=request.Mobile;
existingStudent.GenderId=request.GenderId;
existingStudent.Address.PhysicalAddress=request.Address.PhysicalAddress;
existingStudent.Address.PostalAddress=request.Address.PostalAddress;
await  _context.SaveChangesAsync();
return existingStudent;
      }
      return null;
    }
  }
}