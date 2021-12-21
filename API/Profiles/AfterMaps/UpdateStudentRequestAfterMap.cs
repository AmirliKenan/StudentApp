using System;
using System.Collections.Generic;
using System.Linq;
using DataModels=API.DataModels;
using System.Threading.Tasks;
using API.DomainModels;
using AutoMapper;

namespace API.Profiles.AfterMaps
{
  public class UpdateStudentRequestAfterMap : IMappingAction<UpdateStudentRequest, DataModels.Student>
  {
    public void Process(UpdateStudentRequest source, DataModels.Student destination, ResolutionContext context)
    {
      destination.Address=new DataModels.Address()
      {

          PhysicalAddress=source.PhysicalAddress,
          PostalAddress=source.PostalAddress
      };
    }
  }
}