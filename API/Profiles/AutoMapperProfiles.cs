using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModels= API.DomainModels;
using DataModels= API.DataModels;
using AutoMapper;
using API.DomainModels;
using API.Profiles.AfterMaps;

namespace API.Profiles
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DataModels.Student,DomainModels.Student>().ReverseMap();
            CreateMap<DataModels.Gender,DomainModels.Gender>().ReverseMap();
            CreateMap<DataModels.Address,DomainModels.Address>().ReverseMap();
            CreateMap<UpdateStudentRequest,DataModels.Student>().AfterMap<UpdateStudentRequestAfterMap>();
            CreateMap<AddStudentRequest,DataModels.Student>().AfterMap<AddStudentRequestAfterMap>();
            
        }
    }
}