using PWD.Identity.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PWD.Identity.Permissions
{
    public class HRPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(HRPermissions.GroupName, L("Permission:HR"));

            //var affiliationsPermission = group.AddPermission(HRPermissions.Affiliations.Default, L("Permission:Affiliations"));
            //affiliationsPermission.AddChild(HRPermissions.Affiliations.Create, L("Permission:Affiliations.Create"));
            //affiliationsPermission.AddChild(HRPermissions.Affiliations.Edit, L("Permission:Affiliations.Edit"));
            //affiliationsPermission.AddChild(HRPermissions.Affiliations.Delete, L("Permission:Affiliations.Delete"));

            //var bcsBatchPermission = group.AddPermission(HRPermissions.BCSBatch.Default, L("Permission:BCSBatch"));
            //bcsBatchPermission.AddChild(HRPermissions.BCSBatch.Create, L("Permission:BCSBatch.Create"));
            //bcsBatchPermission.AddChild(HRPermissions.BCSBatch.Edit, L("Permission:BCSBatch.Edit"));
            //bcsBatchPermission.AddChild(HRPermissions.BCSBatch.Delete, L("Permission:BCSBatch.Delete"));

            //var countrySetupPermission = group.AddPermission(HRPermissions.Country.Default, L("Permission:Country"));
            //countrySetupPermission.AddChild(HRPermissions.Country.Create, L("Permission:Country.Create"));
            //countrySetupPermission.AddChild(HRPermissions.Country.Edit, L("Permission:Country.Edit"));
            //countrySetupPermission.AddChild(HRPermissions.Country.Delete, L("Permission:Country.Delete"));

            //var degreeSetupPermission = group.AddPermission(HRPermissions.Degree.Default, L("Permission:Degree"));
            //degreeSetupPermission.AddChild(HRPermissions.Degree.Create, L("Permission:Degree.Create"));
            //degreeSetupPermission.AddChild(HRPermissions.Degree.Edit, L("Permission:Degree.Edit"));
            //degreeSetupPermission.AddChild(HRPermissions.Degree.Delete, L("Permission:Degree.Delete"));

            //var designationSetupPermission = group.AddPermission(HRPermissions.Designation.Default, L("Permission:Designation"));
            //designationSetupPermission.AddChild(HRPermissions.Designation.Create, L("Permission:Designation.Create"));
            //designationSetupPermission.AddChild(HRPermissions.Designation.Edit, L("Permission:Designation.Edit"));
            //designationSetupPermission.AddChild(HRPermissions.Designation.Delete, L("Permission:Designation.Delete"));

            //var districtSetupPermission = group.AddPermission(HRPermissions.District.Default, L("Permission:District"));
            //districtSetupPermission.AddChild(HRPermissions.District.Create, L("Permission:District.Create"));
            //districtSetupPermission.AddChild(HRPermissions.District.Edit, L("Permission:District.Edit"));
            //districtSetupPermission.AddChild(HRPermissions.District.Delete, L("Permission:District.Delete"));

            //var employeeClassSetupPermission = group.AddPermission(HRPermissions.EmployeeClass.Default, L("Permission:EmployeeClass"));
            //employeeClassSetupPermission.AddChild(HRPermissions.EmployeeClass.Create, L("Permission:EmployeeClass.Create"));
            //employeeClassSetupPermission.AddChild(HRPermissions.EmployeeClass.Edit, L("Permission:EmployeeClass.Edit"));
            //employeeClassSetupPermission.AddChild(HRPermissions.EmployeeClass.Delete, L("Permission:EmployeeClass.Delete"));

            //var employeeGradeSetupPermission = group.AddPermission(HRPermissions.EmployeeGrade.Default, L("Permission:EmployeeGrade"));
            //employeeGradeSetupPermission.AddChild(HRPermissions.EmployeeGrade.Create, L("Permission:EmployeeGrade.Create"));
            //employeeGradeSetupPermission.AddChild(HRPermissions.EmployeeGrade.Edit, L("Permission:EmployeeGrade.Edit"));
            //employeeGradeSetupPermission.AddChild(HRPermissions.EmployeeGrade.Delete, L("Permission:EmployeeGrade.Delete"));

            //var examSetupPermission = group.AddPermission(HRPermissions.Exam.Default, L("Permission:Exam"));
            //examSetupPermission.AddChild(HRPermissions.Exam.Create, L("Permission:Exam.Create"));
            //examSetupPermission.AddChild(HRPermissions.Exam.Edit, L("Permission:Exam.Edit"));
            //examSetupPermission.AddChild(HRPermissions.Exam.Delete, L("Permission:Exam.Delete"));

            //var gradeSetupPermission = group.AddPermission(HRPermissions.Grade.Default, L("Permission:Grade"));
            //gradeSetupPermission.AddChild(HRPermissions.Grade.Create, L("Permission:Grade.Create"));
            //gradeSetupPermission.AddChild(HRPermissions.Grade.Edit, L("Permission:Grade.Edit"));
            //gradeSetupPermission.AddChild(HRPermissions.Grade.Delete, L("Permission:Grade.Delete"));

            //var instituteSetupPermission = group.AddPermission(HRPermissions.Institute.Default, L("Permission:Institute"));
            //instituteSetupPermission.AddChild(HRPermissions.Institute.Create, L("Permission:Institute.Create"));
            //instituteSetupPermission.AddChild(HRPermissions.Institute.Edit, L("Permission:Institute.Edit"));
            //instituteSetupPermission.AddChild(HRPermissions.Institute.Delete, L("Permission:Institute.Delete"));

            //var jobStatusSetupPermission = group.AddPermission(HRPermissions.JobStatus.Default, L("Permission:JobStatus"));
            //jobStatusSetupPermission.AddChild(HRPermissions.JobStatus.Create, L("Permission:JobStatus.Create"));
            //jobStatusSetupPermission.AddChild(HRPermissions.JobStatus.Edit, L("Permission:JobStatus.Edit"));
            //jobStatusSetupPermission.AddChild(HRPermissions.JobStatus.Delete, L("Permission:JobStatus.Delete"));

            //var languageSetupPermission = group.AddPermission(HRPermissions.Language.Default, L("Permission:Language"));
            //languageSetupPermission.AddChild(HRPermissions.Language.Create, L("Permission:Language.Create"));
            //languageSetupPermission.AddChild(HRPermissions.Language.Edit, L("Permission:Language.Edit"));
            //languageSetupPermission.AddChild(HRPermissions.Language.Delete, L("Permission:Language.Delete"));

            //var leaveTypeSetupPermission = group.AddPermission(HRPermissions.LeaveType.Default, L("Permission:LeaveType"));
            //leaveTypeSetupPermission.AddChild(HRPermissions.LeaveType.Create, L("Permission:LeaveType.Create"));
            //leaveTypeSetupPermission.AddChild(HRPermissions.LeaveType.Edit, L("Permission:LeaveType.Edit"));
            //leaveTypeSetupPermission.AddChild(HRPermissions.LeaveType.Delete, L("Permission:LeaveType.Delete"));

            //var payScaleSetupPermission = group.AddPermission(HRPermissions.PayScale.Default, L("Permission:PayScale"));
            //payScaleSetupPermission.AddChild(HRPermissions.PayScale.Create, L("Permission:PayScale.Create"));
            //payScaleSetupPermission.AddChild(HRPermissions.PayScale.Edit, L("Permission:PayScale.Edit"));
            //payScaleSetupPermission.AddChild(HRPermissions.PayScale.Delete, L("Permission:PayScale.Delete"));

            //var subjectSetupPermission = group.AddPermission(HRPermissions.Subject.Default, L("Permission:Subject"));
            //subjectSetupPermission.AddChild(HRPermissions.Subject.Create, L("Permission:Subject.Create"));
            //subjectSetupPermission.AddChild(HRPermissions.Subject.Edit, L("Permission:Subject.Edit"));
            //subjectSetupPermission.AddChild(HRPermissions.Subject.Delete, L("Permission:Subject.Delete"));

            //var trainingSponsorSetupPermission = group.AddPermission(HRPermissions.TrainingSponsor.Default, L("Permission:TrainingSponsor"));
            //trainingSponsorSetupPermission.AddChild(HRPermissions.TrainingSponsor.Create, L("Permission:TrainingSponsor.Create"));
            //trainingSponsorSetupPermission.AddChild(HRPermissions.TrainingSponsor.Edit, L("Permission:TrainingSponsor.Edit"));
            //trainingSponsorSetupPermission.AddChild(HRPermissions.TrainingSponsor.Delete, L("Permission:TrainingSponsor.Delete"));

            //var trainingTopicSetupPermission = group.AddPermission(HRPermissions.TrainingTopic.Default, L("Permission:TrainingTopic"));
            //trainingTopicSetupPermission.AddChild(HRPermissions.TrainingTopic.Create, L("Permission:TrainingTopic.Create"));
            //trainingTopicSetupPermission.AddChild(HRPermissions.TrainingTopic.Edit, L("Permission:TrainingTopic.Edit"));
            //trainingTopicSetupPermission.AddChild(HRPermissions.TrainingTopic.Delete, L("Permission:TrainingTopic.Delete"));

            var setupPermission = group.AddPermission(HRPermissions.Setup.Default, L("Permission:Setup"));
            //setupPermission.AddChild(HRPermissions.Setup.Create, L("Permission:Setup.Create"));
            //setupPermission.AddChild(HRPermissions.Setup.Edit, L("Permission:Setup.Edit"));
            //setupPermission.AddChild(HRPermissions.Setup.Delete, L("Permission:Setup.Delete"));

            var reportsPermission = group.AddPermission(HRPermissions.Reports.Default, L("Permission:Reports"));
            reportsPermission.AddChild(HRPermissions.Reports.PermanentDistrict, L("Permission:Reports.PermanentDistrict"));
            reportsPermission.AddChild(HRPermissions.Reports.ServiceHistory, L("Permission:Reports.ServiceHistory"));
            reportsPermission.AddChild(HRPermissions.Reports.PostOccupancy, L("Permission:Reports.PostOccupancy"));
            reportsPermission.AddChild(HRPermissions.Reports.Retirement, L("Permission:Reports.Retirement"));

            var employeeManagementPermission = group.AddPermission(HRPermissions.Employees.Default, L("Permission:Employees"));
            employeeManagementPermission.AddChild(HRPermissions.Employees.Create, L("Permission:Employees.Create"));
            employeeManagementPermission.AddChild(HRPermissions.Employees.Edit, L("Permission:Employees.Edit"));
            employeeManagementPermission.AddChild(HRPermissions.Employees.Delete, L("Permission:Employees.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }
    }
}
