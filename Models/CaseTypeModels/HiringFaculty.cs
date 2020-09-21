using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public enum FacTitle
    {
        [Display(Name = "Professor with Tenure")]
        ProfTen,
        [Display(Name = "Associate Professor with Tenure")]
        AssocProfTen,
        [Display(Name = "Professor Tenure Track")]
        ProfTrack,
        [Display(Name = "Associate Professor Tenure Track")]
        AssocProfTrack,
        [Display(Name = "Assistant Professor")]
        AssistProf,
        [Display(Name = "Professor Without Tenure (WOT)")]
        ProfWOT,
        [Display(Name = "Associate Professor  Without Tenure (WOT)")]
        AssociateProfWOT,
        [Display(Name = "Assistant Professor Without Tenure (WOT)")]
        AffiliateProfWOT,
        [Display(Name = "Research Professor")]
        ResearchProf,
        [Display(Name = "Research Associate Professor")]
        ResearchAssocProf,
        [Display(Name = "Research Assistant Professor")]
        ResearchAssistantProf,
        [Display(Name = "Teaching Professor")]
        TeachingProf,
        [Display(Name = "Associate Teaching Professor")]
        AssocTeachingProf,
        [Display(Name = "Assistant Teaching Professor")]
        AssistTeachingProf,
        [Display(Name = "Lecturer Part-time, Temporary")]
        LecturePT,
        [Display(Name = "Acting  Professor")]
        ActingProf,
        [Display(Name = "Acting Associate Professor")]
        ActingAssocProf,
        [Display(Name = "Acting Assistant Professor")]
        ActingAssistProf,
        [Display(Name = "Acting Instructor")]
        ActingInstructor,
        [Display(Name = "Clinical Professor Dental Pathway")]
        ClinicalPath,
        [Display(Name = "Clinical Associate Professor Dental Pathway")]
        ClinicalAssocPath,
        [Display(Name = "Clinical Assistant Professor Dental Pathway")]
        ClinicalAssistPath,
        [Display(Name = "Clinical Professor Salaried")]
        ClinicalProfSalary,
        [Display(Name = "Clinical Associate Professor Salaried")]
        ClinicalAssocProfSalary,
        [Display(Name = "Clinical Assistant Professor Salaried")]
        ClinicalAssistProfSalary,
        [Display(Name = "Clinical Instructor Salaried")]
        ClinicalInstructorSalary,
        [Display(Name = "Clinical Professor Non-Salaried")]
        ClinicalProfNonSalary,
        [Display(Name = "Clinical Associate Professor Non-Salaried")]
        ClinicalAssocProfNonSalary,
        [Display(Name = "Clinical Assistant Professor Non Salaried")]
        ClinicalAssistProfNonSalary,
        [Display(Name = "Clinical Instructor Non-Salaried")]
        ClinicalInstructorNonSalary,
        [Display(Name = "Adjunct Professor")]
        AdjunctProf,
        [Display(Name = "Adjunct Associate Professor")]
        AdjunctAssocProf,
        [Display(Name = "Adjunct Assistant Professor")]
        AdjunctAssistProf,
        [Display(Name = "Visiting Scholar")]
        VisitingScholar,
        [Display(Name = "Professor Emeritus")]
        ProfEmeritus,
        [Display(Name = "Associate Professor Emeritus")]
        AssoceProfEmeritus,
        [Display(Name = "Research Professor Emeritus")]
        ResearchProfEmeritus,
        [Display(Name = "Research Associate Professor Emeritus")]
        ResearchAssocProfEmeritus,
        [Display(Name = "Other")]
        Other
    }

    public enum FacHireReason
    {
        [Display(Name = "New Position")]
        New,
        [Display(Name = "Replacement")]
        Replace,
        [Display(Name = "Other")]
        Other
    }

   
    public class HiringFaculty
    {
        [Required, Key, ForeignKey("Case")]
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

        [Display(Name = "Proposed Salary Range")]
        public string Salary { get; set; }

        [Display(Name = "%FTE")]
        public string FTE { get; set; }

        [Display(Name = "Justification"), Required]
        public string Justification { get; set; }

        [Display(Name = "Consequences of not hiring for position"), Required]
        public string Consequences { get; set; }

        [Display(Name = "Barriers"), Required]
        public string Barriers { get; set; }

        [Display(Name = "Administrative Role"), Required]
        public string AdminRole { get; set; }

        [Display(Name = "Additional Notes")]
        public string Note { get; set; }

        [Display(Name = "Job Title")]
        public virtual FacTitle FacTitle { get; set; }

        [Display(Name = "Hire Reason")]
        public virtual FacHireReason FacHireReason { get; set; }

        [Display(Name = "Budget Number(s)")]
        public string BudgetNumbers { get; set; }

        [Display(Name = "Budget Type")]
        public virtual BudgetType BudgetType { get; set; }

    }
}
