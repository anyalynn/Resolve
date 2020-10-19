using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public enum FWorkerType
    {
        [Display(Name = "Classified  Staff")]
        ClassStaff,
        [Display(Name = "Professional Staff")]
        ProStaff,
        [Display(Name = "Student")]
        Student,
        [Display(Name = "Faculty")]
        Faculty
    }
    public class MoveWorker
    {
        [Required, Key, ForeignKey("Case")]
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Effective Start Date")]
        [DataType(DataType.Date)]
        public DateTime EffectiveStartDate { get; set; }

        [Display(Name = "Worker Type")]
        public virtual FWorkerType? FWorkerType { get; set; }

        [Display(Name = "Original SupOrg")]
        public virtual SupOrg? OSupOrg { get; set; }

        [Display(Name = "Proposed SupOrg")]
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

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Detailed Description")]
        [MaxLength(1024)]
        public string DetailedDescription { get; set; }

    }
}
