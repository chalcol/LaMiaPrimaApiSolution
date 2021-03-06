﻿using LaMiaPrimaApi.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LaMiaPrimaApi.Models
{
    [CourseTitleMustBeDifferentFromDescriptionAttribute(ErrorMessage ="Title must be different from description")]
    public class CourseForCreationDto //: IValidatableObject
    {
        [Required(ErrorMessage ="You should fill out a title.")]
        [MaxLength(100,ErrorMessage ="The title shouldn't have more than 100 characters")]
        public string Title { get; set; }

        
        [MaxLength(500,ErrorMessage = "The description shouldn't have more than 500 characters")]
        public string Description { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title == Description)
        //    {
        //        yield return new ValidationResult("The provider description should be different from the title.",
        //            new[] { "CourseForCreationDto" });
        //    }
        //}
    }
}
