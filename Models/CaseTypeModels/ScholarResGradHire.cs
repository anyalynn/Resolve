using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public enum ScholarJobProfile
    {
        [Display(Name = "Resident")]
        Residents,
        [Display(Name = "Chief Resident")]
        ChiefResident,
        [Display(Name = "Postdoctoral Scholar")]
        PostScholar,
        [Display(Name = "Postdoctoral Scholar Fellow")]
        PostFellow,
        [Display(Name = "Interim Postdoctoral Scholar")]
        InterimPostFellow
    }

    public enum GradJobProfile
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

    public enum ScholarReqType
    {
        [Display(Name = "New Hire")]
        Hire,
        [Display(Name = "Rehire")]
        Rehire,
        [Display(Name = "Promotion (Scholar/Resident only)")]
        Promotion

    }
    public class ScholarResGradHire
    {
        [Required, Key, ForeignKey("Case")]
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
