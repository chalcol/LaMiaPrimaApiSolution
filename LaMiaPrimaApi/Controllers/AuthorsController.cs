using AutoMapper;
using CourseLibrary.API.Services;
using LaMiaPrimaApi.Models;
using LaMiaPrimaApi.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpHeadAttribute = System.Web.Http.HttpHeadAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace LaMiaPrimaApi.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibreryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibreryRepository,IMapper mapper)
        {
            _courseLibreryRepository = courseLibreryRepository ??
                throw new ArgumentNullException(nameof(courseLibreryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(courseLibreryRepository));
            //throw new ArgumentNullException(nameof(courseLibreryRepository));
        }



        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery]AuthorsResourceParameters authorsResourceParameters)//[FromQuery] AuthorsResourceParameters authorsResourceParameters) //viene usato [FromQuery] perche il parametro authorsResourceParameters è un parametro complesso
        {

            var authorsFromRepo = _courseLibreryRepository.GetAuthors(authorsResourceParameters);
            //var authors = new List<AuthorDto>();
            //foreach(var author in authorsFromRep)
            //{
            //    authors.Add(new AuthorDto()
            //    {
            //        Id= author.Id,
            //        Name = $"{author.FirstName} {author.LastName}",
            //        MainCategory = author.MainCategory,
            //        age = author.DateOfBirth.GetCurrentAge()
            //    }
            //        );
            //}

            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));


        }

        [HttpGet("{authorId}", Name ="GetAuthor")]
        public IActionResult GetAuthor(Guid authorId)
        {
            //if (!_courseLibreryRepository.AuthorExists(authorId))
            //{
            //    return NotFound();
            //}
            var authorsFromRepo = _courseLibreryRepository.GetAuthor(authorId);
            if (authorsFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AuthorDto>(authorsFromRepo));
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorForCreationDto author)
        {
            //if (author == null)
            // {
            //     return BadRequest();
            // }

            var authorEntity = _mapper.Map<CourseLibrary.API.Entities.Author>(author);
            _courseLibreryRepository.AddAuthor(authorEntity);
            _courseLibreryRepository.Save();

            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtRoute("GetAuthor", new {authorId = authorToReturn.Id },authorToReturn);
        }

       
    }
}
