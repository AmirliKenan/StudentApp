using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DomainModels;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    private readonly IImageRepository _imageRepository;

    public StudentController(IStudentRepository studentRepository,IMapper mapper,IImageRepository imageRepository)
        {
      _studentRepository = studentRepository;
      _mapper = mapper;
      _imageRepository = imageRepository;
    }
        [HttpGet]
public async Task<IActionResult> GetAllStudents()
{
    
  var students= await _studentRepository.GetStudentsAsync();
  
  return Ok(_mapper.Map<List<Student>>(students));
}


 [HttpGet]
 [Route("{studentId:guid}"),ActionName("GetStudentAsync")]
public async Task<IActionResult> GetStudentAsync([FromRoute]Guid studentId)
{
var student=await _studentRepository.GetStudentAsync(studentId);
if(student==null)
{
  return NotFound();
}
  return Ok(_mapper.Map<Student>(student));
}
[HttpPut]
[Route("{studentId:guid}")]
public async Task<IActionResult> UpdateStudent([FromRoute] Guid studentId,[FromBody] UpdateStudentRequest request)
 {
  if(await _studentRepository.Exists(studentId))
  {

  var updatedStudent= await  _studentRepository.UpdateStudent(studentId,_mapper.Map<DataModels.Student>(request));
if(updatedStudent != null)
{
  return Ok(_mapper.Map<Student>(updatedStudent));
}

  }
 
   return NotFound();
 

 }
 
[HttpDelete]
[Route("{studentId:guid}")]
 public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
 {

   if(await _studentRepository.Exists(studentId))
   {
  var student=await _studentRepository.DeleteStudent(studentId);
  return Ok(_mapper.Map<Student>(student));
   }
   return NotFound();
 }
[HttpPost]
[Route("Add")]
public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
{
var student=await _studentRepository.AddStudent( _mapper.Map<DataModels.Student>(request));
return CreatedAtAction(nameof(GetStudentAsync),new {studentId=student.Id},
_mapper.Map<Student>(student));
}
[HttpPost]
[Route("{studentId:guid}/upload-image")]
 public async Task<IActionResult> UploadImage([FromRoute] Guid studentId,IFormFile profileImage)
 {

if(await _studentRepository.Exists(studentId))
{
  var fileName=Guid.NewGuid()+Path.GetExtension(profileImage.FileName);
var fileImagePath=await _imageRepository.Upload(profileImage,fileName);
if(await _studentRepository.UpdateProfileImage(studentId,fileImagePath))
{
  return Ok(fileImagePath);
};
return StatusCode(StatusCodes.Status500InternalServerError,"Error uploading image");
}
return NotFound();

 }
 }

}