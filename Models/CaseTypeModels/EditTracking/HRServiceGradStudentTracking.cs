using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public class HRServiceGradStudentTracking
    {
        public int HRServiceGradStudentTrackingID { get; set; }
        public string Status { get; set; }
        public int CaseAuditID { get; set; }
        public CaseAudit CaseAudit { get; set; }
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Effective Start Date")]
        [DataType(DataType.Date)]
        public DateTime EffectiveStartDate { get; set; }

        [Display(Name = "Effective End Date")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveEndDate { get; set; }

        [Display(Name = "Request Type"), Required]
        public GradRequestType GradRequestType { get; set; }

        [Display(Name = "Job Profile"), Required]
        public virtual GradJobProfile GradJobProfile { get; set; }

        public virtual Department Department { get; set; }

        [Display(Name = "Student Name"), Required]
        public string StudentName { get; set; }

        [Display(Name = "Step/Stipend/Allowance")]
        public string StepStipendAllowance { get; set; }

        public string BudgetNumbers { get; set; }

        [Display(Name = "Detailed Description")]
        [MaxLength(1024)]
        public string DetailedDescription { get; set; }

        [MaxLength(1024)]
        public string Note { get; set; }
    }
}
