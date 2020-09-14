using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public class ScholarResGradHireTracking
    {
        public int ScholarResGradHireTrackingID { get; set; }
        public string Status { get; set; }
        public int CaseAuditID { get; set; }
        public CaseAudit CaseAudit { get; set; }
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Effective Date")]
        [DataType(DataType.Date)]
        public DateTime EffectiveDate { get; set; }

        [Display(Name = "Worker Type")]
        public virtual DWorkerType? DWorkerType { get; set; }

        [Display(Name = "Supervisory Organization")]
        public virtual SupOrg? SupOrg { get; set; }

        [Display(Name = "Name"), Required]
        public string Name { get; set; }

        [Display(Name = "Budget Numbers")]
        public string BudgetNumbers { get; set; }

        [Display(Name = "Additional Notes")]
        [MaxLength(1024)]
        public string Note { get; set; }

        [Display(Name = "Detailed Description")]
        [MaxLength(1024)]
        public string DetailedDescription { get; set; }

        public virtual Department? Department { get; set; }

        [Display(Name = "Current Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Proposed Job Title")]
        public string NewTitle { get; set; }

        [Display(Name = "Stipend/Allowance")]
        public string StipendAllowance { get; set; }

        [Display(Name = "Scholar/Resident Job Profile")]
        public virtual ScholarJobProfile? ScholarJobProfile { get; set; }

        [Display(Name = "Graduate Student Job Profile")]
        public virtual GradJobProfile? GradJobProfile { get; set; }

        [Display(Name = "Request Type")]
        public virtual ScholarReqType? ScholarReqType { get; set; }

    }
}
