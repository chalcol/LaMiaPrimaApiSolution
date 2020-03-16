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
    [Route("api/Authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibreryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibreryRepository, IMapper mapper)
        {
            _courseLibreryRepository = courseLibreryRepository??
                throw new ArgumentNullException(nameof(courseLibreryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(courseLibreryRepository));
            //throw new ArgumentNullException(nameof(courseLibreryRepository));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>>GetCoursesForAuthor(Guid authorId)
        {
            if (!_courseLibreryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var coursesForAuthorFromRepo = _courseLibreryRepository.GetCourses(authorId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(coursesForAuthorFromRepo));
        }
        [HttpGet("{courseId}",Name ="GetCourseForAuthor")]

        public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibreryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseForAuthorFromRepo = _courseLibreryRepository.GetCourse(authorId, courseId);

            if (courseForAuthorFromRepo == null)
            {
                return NotFound();
            }
           
            return Ok(_mapper.Map<CourseDto>(courseForAuthorFromRepo));
        }

        [HttpPost]
        public ActionResult<CourseDto> CreateCourseForAuthor(Guid authorId, CourseForCreationDto course)
        {
            if (!_courseLibreryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseEntity = _mapper.Map<CourseLibrary.API.Entities.Course>(course);
            _courseLibreryRepository.AddCourse(authorId, courseEntity);
            _courseLibreryRepository.Save();

            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);
            return CreatedAtRoute("GetCourseForAuthor", 
                new { authorId = authorId, courseId = courseToReturn.Id},courseToReturn);
        }

    }
}
