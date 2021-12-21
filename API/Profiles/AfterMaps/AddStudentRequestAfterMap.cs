using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DomainModels;
using AutoMapper;
using DataModels=API.DataModels;

namespace API.Profiles.AfterMaps
{
  public class AddStudentRequestAfterMap : IMappingAction<AddStudentRequest, DataModels.Student>
  {
    public void Process(AddStudentRequest source, DataModels.Student destination, ResolutionContext context)
    {
      destination.Id=Guid.NewGuid();
      destination.Address=new DataModels.Address()
      {
          Id=Guid.NewGuid(),
          PhysicalAddress=source.PhysicalAddress,
          PostalAddress=source.PostalAddress
      };
    }
  }
}