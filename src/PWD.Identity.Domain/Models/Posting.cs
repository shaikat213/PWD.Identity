using System;
using Volo.Abp.Domain.Entities;

namespace PWD.Identity.Models
{
    public class Posting: Entity<Guid>
    {
        public int PostingId { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string NameBn { get; set; }
        public string Post { get; set; }
        public string Designation { get; set; }
        public string DesignationBn { get; set; }
        public string Office { get; set; }
        public string OfficeBn { get; set; }

    }
}
