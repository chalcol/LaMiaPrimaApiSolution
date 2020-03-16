using AutoMapper;
using CourseLibrary.API.Services;
using LaMiaPrimaApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaMiaPrimaApi.Controllers
{
    [ApiController]
    [Route("api/authorcollections")]
    public class AuthorCollectionsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibreryRepository;
        private readonly IMapper _mapper;

        public AuthorCollectionsController(ICourseLibraryRepository courseLibreryRepository, IMapper mapper)
        {
            _courseLibreryRepository = courseLibreryRepository ??
                throw new ArgumentNullException(nameof(courseLibreryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(courseLibreryRepository));
            //throw new ArgumentNullException(nameof(courseLibreryRepository));
        }

        [HttpPost]
        public ActionResult<IEnumerable<AuthorDto>>CreateAuthorCollection(
            IEnumerable<AuthorForCreationDto> authorCollection)
        {
            var authorEntities = _mapper.Map<IEnumerable<CourseLibrary.API.Entities.Author>>(authorCollection);
            foreach (var author in authorEntities)
            {
                _courseLibreryRepository.AddAuthor(author);
            }
            _courseLibreryRepository.Save();

            return Ok();
           
        }

       
    }
}
