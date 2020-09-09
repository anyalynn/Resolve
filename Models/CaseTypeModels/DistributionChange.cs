using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{

    public enum JGradJobProfile
    {
        [Display(Name = "Graduate Fellow/Trainee Stipend w/o Benefits")]
        FellowStudentNoBenefits,
        [Display(Name = "Graduate Fellow Stipend w / Benefits(Historical 0863 job code)")]
        FellowStudentBenefits,
        [Display(Name = "Graduate Trainee Stipend (NE UAW ASE)")]
        GradTraineeStipend,
        [Display(Name = "Graduate Trainee Stipend w/Benefits (Historical 0864 job code)")]
        GradTraineeBenefits
    }

    public class DistributionChange
    {
        [Required, Key, ForeignKey("Case")]
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Effective Start Date")]
        [DataType(DataType.Date)]
        public DateTime EffectiveStartDate { get; set; }

        [Display(Name = "Effective End Date")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveEndDate { get; set; }
        
        [Display(Name = "Worker Type")]
        public virtual AWorkerType? AWorkerType { get; set; }

        [Display(Name = "Hire Type")]
        public virtual HireType? HireType { get; set; }

        [Display(Name = "Supervisory Organization")]
        public virtual SupOrg? SupOrg { get; set; }

        [Display(Name = "Employee EID")]
        public string EmployeeEID { get; set; }

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

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        
       
    }
}
