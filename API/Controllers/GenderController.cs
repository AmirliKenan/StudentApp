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
    public class GenderController : ControllerBase
    {
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public GenderController(IStudentRepository studentRepository,IMapper mapper)
        {
      _studentRepository = studentRepository;
     _mapper = mapper;
    }

[HttpGet]
public async Task<IActionResult> GetAllGenders()
{

    var genderList=await _studentRepository.GetGendersAsync();
  if(genderList==null  || !genderList.Any())
  {
      return NotFound();
  }
    return Ok(_mapper.Map<List<Gender>>(genderList));
}

    }
}