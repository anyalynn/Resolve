using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public class HiringStaffTracking
    {
        public int HiringStaffTrackingID { get; set; }
        public string Status { get; set; }
        public int CaseAuditID { get; set; }
        public CaseAudit CaseAudit { get; set; }
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Proposed Hire Date")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Display(Name = "Actual Hire Date")]
        [DataType(DataType.Date)]
        public DateTime? ActualHireDate { get; set; }

        [Display(Name = "Post for Recruitment Date")]
        [DataType(DataType.Date)]
        public DateTime? PostDate { get; set; }

        [Display(Name = "Proposed End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Acutal End Date")]
        [DataType(DataType.Date)]
        public DateTime? ActualEndDate { get; set; }

        [Display(Name = "Employee Replaced")]
        public string EmployeeReplaced { get; set; }

        [Display(Name = "Candidate Name")]
        public string CandidateName { get; set; }

        [Display(Name = "Student Number")]
        public string StudentNum { get; set; }

        [Display(Name = "Employee ID")]
        public string EmployeeID { get; set; }

        [Display(Name = "Supervisory Organization")]
        public virtual SupOrg? SupOrg { get; set; }

        public string SuperOrg { get; set; }

        [Display(Name = "Proposed Pay Rate")]
        public string PayRate { get; set; }

        [Display(Name = "Hiree Name")]
        public string HireeName { get; set; }

        [Display(Name = "Position Number")]
        public string PosNum { get; set; }

        [Display(Name = "Workday Job Req Number")]
        public string WorkdayReq { get; set; }

        [Display(Name = "UW Hires Job Req Number")]
        public string UWHiresReq { get; set; }

        [Display(Name = "%FTE")]
        public string FTE { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Job Posting Title")]
        public string JobPostingTitle { get; set; }

        [Display(Name = "Campus Box")]
        public string CampusBox { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Overtime Eligible?")]
        public bool OvertimeEligible { get; set; }

        [Display(Name = "Supervisory Position?")]
        public bool Super { get; set; }

        [Display(Name = "Additional Notes")]
        public string Note { get; set; }

        [Display(Name = "Explain Multiple Budgets")]
        public string MultipleBudgetsExplain { get; set; }

        [Display(Name = "Justification")]
        public string Justification { get; set; }

        [Display(Name = "Consequences of not hiring for position")]
        public string Consequences { get; set; }

        [Display(Name = "Barriers")]
        public string Barriers { get; set; }

        [Display(Name = "SupOrg Manager")]
        public string SupOrgManager { get; set; }

        [Display(Name = "UW Hires Contact")]
        public string UWHiresContact { get; set; }

        [Display(Name = "Budget Numbers")]
        public string BudgetNumbers { get; set; }

        [Display(Name = "Budget Type")]
        public virtual BudgetType? BudgetType { get; set; }

        [Display(Name = "Budget Purpose")]
        public virtual BudgetPurpose? BudgetPurpose { get; set; }

        [Display(Name = "Gender")]
        public virtual Gender? Gender { get; set; }

        [Display(Name = "Citizenship Status")]
        public virtual Citizenship? Citizenship { get; set; }

        [Display(Name = "Workstudy?")]
        public bool Workstudy { get; set; }

        [Display(Name = "Candiate Selected?")]
        public bool CandidateSelected { get; set; }

        [Display(Name = "Recruitment Type")]
        public virtual LimitedRecruitment? LimitedRecruitment { get; set; }

        [Display(Name = "Length of Recruitment Run")]
        public virtual RecruitmentRun? RecruitmentRun { get; set; }

        [Display(Name = "Weekly Hours")]
        public virtual WeeklyHours? WeeklyHours { get; set; }

        [Display(Name = "# Supervised")]
        public virtual Supervised? Supervised { get; set; }

        [Display(Name = "Reason for Hire")]
        public virtual StaffHireReason? StaffHireReason { get; set; }

        [Display(Name = "Position Type")]
        public virtual StaffPositionType? StaffPositionType { get; set; }

        [Display(Name = "Worker Type")]
        public virtual StaffWorkerType? StaffWorkerType { get; set; }

    }
}
