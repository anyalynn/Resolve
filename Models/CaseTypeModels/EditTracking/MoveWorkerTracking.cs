using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public class MoveWorkerTracking
    {
        public int MoveWorkerTrackingID { get; set; }
        public string Status { get; set; }
        public int CaseAuditID { get; set; }
        public CaseAudit CaseAudit { get; set; }
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Effective Start Date")]
        [DataType(DataType.Date)]
        public DateTime EffectiveStartDate { get; set; }

        public virtual FWorkerType? FWorkerType { get; set; }

        [Display(Name = "Original SupOrg")]
        public virtual SupOrg? OSupOrg { get; set; }

        [Display(Name = "Proposed SupOrg")]
        public virtual SupOrg? SupOrg { get; set; }

        public string OSuperOrg { get; set; }

        public string PSuperOrg { get; set; }

        [Display(Name = "Employee EID")]
        public string EmployeeEID { get; set; }

        [Display(Name = "Name"), Required]
        public string Name { get; set; }

        [Display(Name = "Budget Numbers")]
        public string BudgetNumbers { get; set; }
      
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Additional Notes")]
        [MaxLength(1024)]
        public string Note { get; set; }

        [Display(Name = "Detailed Description")]
        [MaxLength(1024)]
        public string DetailedDescription { get; set; }

    }
}
