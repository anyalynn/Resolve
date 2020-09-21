using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{    
    public class Termination
    {
        [Required, Key, ForeignKey("Case")]
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Effective Date")]
        [DataType(DataType.Date)]
        public DateTime EffectiveStartDate { get; set; }
            
        [Display(Name = "Worker Type")]
        public virtual AWorkerType? AWorkerType { get; set; }

        [Display(Name = "Hire Type")]
        public virtual HireType? HireType { get; set; }

        [Display(Name = "Termination Reason")]
        public virtual TerminationReason? TerminationReason { get; set; }

        [Display(Name = "Supervisory Organization")]
        public virtual SupOrg? SupOrg { get; set; }

        [Display(Name = "Employee EID")]
        public string EmployeeEID { get; set; }

        [Display(Name = "Name"), Required]
        public string Name { get; set; }

        [Display(Name = "Budget Number(s)")]
        public string BudgetNumbers { get; set; }

        [Display(Name = "Additional Notes")]
        [MaxLength(1024)]
        public string Note { get; set; }

        [Display(Name = "Detailed Description")]
        [MaxLength(1024)]
        public string DetailedDescription { get; set; }

        [Display(Name = "Offboarding?")]
        public bool Offboarding { get; set; }

        [Display(Name = "Close Position?")]
        public bool ClosePosition { get; set; }

        [Display(Name = "Leave WA?")]
        public bool LeaveWA { get; set; }

        public virtual Department? Department { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }      
   
    }
}
