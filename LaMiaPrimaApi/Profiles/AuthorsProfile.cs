using AutoMapper;
using LaMiaPrimaApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace LaMiaPrimaApi.Profiles
{
    public class AuthorsProfile : Profile
    {
      public AuthorsProfile()
        {
            CreateMap<CourseLibrary.API.Entities.Author, Models.AuthorDto>()
                .ForMember(
                dest=>dest.Name,
                opt=> opt.MapFrom(scr=>$"{scr.FirstName} {scr.LastName}"))
                .ForMember(dest=>dest.age,
                opt=>opt.MapFrom(scr=>scr.DateOfBirth.GetCurrentAge()));

            CreateMap<Models.AuthorForCreationDto, CourseLibrary.API.Entities.Author>();
        }
    }

}
