using System;
using Volo.Abp.Application.Dtos;

namespace PWD.Identity.DtoModels
{
    public class PostingDto : EntityDto<Guid>
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
        public string UserName { get; set; }
        public Guid? OrgUniId { get; set; }

    }
    public class PostingConsumeDto
    {
        public int id { get; set; }
        public int employeeId { get; set; }
        public string name { get; set; }
        public string nameBn { get; set; }
        public string post { get; set; }
        public string designation { get; set; }
        public string designationBn { get; set; }
        public string office { get; set; }
        public string officeBn { get; set; }

    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class employeeDto
    {
        public int employeeId { get; set; }
        public string gradationNo { get; set; }
        public string fullName { get; set; }
        public string fullNameBN { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string nationalId { get; set; }
        public DateTime dob { get; set; }
        public int districtId { get; set; }
        public string district { get; set; }
        public string gender { get; set; }
        public string religion { get; set; }
        public object maritalStatus { get; set; }
        public string bloodGroup { get; set; }
        public string phoneResidence { get; set; }
        public string phoneMobile { get; set; }
        public string imagePath { get; set; }
        public string presentAddress { get; set; }
        public object presentPO { get; set; }
        public string presentThana { get; set; }
        public int presentDistrictId { get; set; }
        public string presentDistrict { get; set; }
        public string permanentAddress { get; set; }
        public string permanentPO { get; set; }
        public string permanentThana { get; set; }
        public int permanentDistrictId { get; set; }
        public string permanentDistrict { get; set; }
        public string email { get; set; }
        public string taxId { get; set; }
        public int isGovtAccomodation { get; set; }
        public int freedomFighterStatus { get; set; }
        public string freedomFighterStatusName { get; set; }
        public int isFreedomFighter { get; set; }
        public int isChildFreedomFighter { get; set; }
        public int isGrandchildFreedomFighter { get; set; }
        public string officeEmail { get; set; }
        public int engineerType { get; set; }
        public int orderId { get; set; }
        public int id { get; set; }
    }
}
