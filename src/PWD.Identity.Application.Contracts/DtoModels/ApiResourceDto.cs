using System;
using System.Collections.Generic;
using System.Text;

namespace PWD.Identity.DtoModels
{
    public class ApiResourceDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
    }
}
