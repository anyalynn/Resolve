using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public class HiringFacultyTracking
    {
        public int HiringFacultyTrackingID { get; set; }
        public string Status { get; set; }
        public int CaseAuditID { get; set; }
        public CaseAudit CaseAudit { get; set; }
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Proposed Hire Date")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Display(Name = "Employee Replaced")]
        public string EmployeeReplaced { get; set; }

        [Display(Name = "Candidate Name")]
        public string CandidateName { get; set; }

        public virtual Department Department { get; set; }

        [Display(Name = "Proposed Salary")]
        public string Salary { get; set; }

        [Display(Name = "FTE")]
        public string FTE { get; set; }

        [Display(Name = "Administrative Role")]
        public string AdminRole { get; set; }

        public string Note { get; set; }

        [Display(Name = "Job Title")]
        public virtual FacTitle FacTitle { get; set; }

        [Display(Name = "Justification")]
        public string Justification { get; set; }

        [Display(Name = "Consequences of not hiring for position")]
        public string Consequences { get; set; }

        [Display(Name = "Barriers")]
        public string Barriers { get; set; }

        [Display(Name = "Hire Reason")]
        public virtual FacHireReason FacHireReason { get; set; }

        [Display(Name = "Budget Numbers")]
        public string BudgetNumbers { get; set; }

        [Display(Name = "Budget Type")]
        public virtual BudgetType BudgetType { get; set; }

        
    }
}
