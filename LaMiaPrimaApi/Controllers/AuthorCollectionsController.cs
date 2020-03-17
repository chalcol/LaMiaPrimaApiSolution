using AutoMapper;
using CourseLibrary.API.Services;
using LaMiaPrimaApi.Helpers;
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

        [HttpGet("({ids})",Name ="GetAuthorCollection")]
        public ActionResult GetAuthorCollection([FromRoute][ModelBinder(BinderType =typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var authorEntities = _courseLibreryRepository.GetAuthors(ids);

            if (ids.Count() != authorEntities.Count())
            {
                return NotFound();
            }
            var authorsToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            return Ok(authorsToReturn);
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

            var authorCollectionToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            var idsAsString = String.Join(",", authorCollectionToReturn.Select(a => a.Id));

            return CreatedAtRoute("GetAuthorCollection", new { ids = idsAsString },
                authorCollectionToReturn);
           
        }

       
    }
}
