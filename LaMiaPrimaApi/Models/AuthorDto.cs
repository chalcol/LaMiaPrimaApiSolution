using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaMiaPrimaApi.Models
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public int age { get; set; }
        public String MainCategory { get; set; }

    }
}
