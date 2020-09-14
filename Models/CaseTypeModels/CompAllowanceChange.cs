using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    
    public enum ScholarCompAllowanceChange
    {
        [Display(Name = "Chief Resident Supplement")]
        ChiefSup,
        [Display(Name = "Other")]
        Other
    }

    public enum AllowanceChange
    {
        [Display(Name = "Administrative Supplement")]
        AdministrativeSupplement,
        [Display(Name = "Alaska RN Assignment Pay")]
        AlaskaRN,
        [Display(Name = "Car Allowance")]
        CarAllowance,
        [Display(Name = "Chief Resident Supplement")]
        ChiefResident,
        [Display(Name = "Car Allowance")]
        DentistryPractice,
        [Display(Name = " Employment Contract Allowance")]
        EmploymentContract,
        [Display(Name = "Endowed Supplement")]
        EndowedSupplement,
        [Display(Name = "Excess Compensation")]
        ExcessCompensation,
        [Display(Name = "Extension Lecture Summer Quarter Premium")]
        ExtensionPremium,
        [Display(Name = "International Location Allowance")]
        InternationalAllowance,
        [Display(Name = "K9 Officer Premium")]
        K9Premium,
        [Display(Name = "Language Premium")]
        LanguagePremium,
        [Display(Name = "Mobile Service Agreement")]
        MobileService,
        [Display(Name = "Teaching Assistant Summer Quarter Premium")]
        TeachingAssistant,
        [Display(Name = "Temporary Pay Increase")]
        TemporaryIncrease,
        [Display(Name = "Temporary Pay Supplement")]
        TemporarySupplement,
        [Display(Name = "Temporary Recruitment and Retention Increase CNU")]
        TemporaryRetention,
        [Display(Name = "Temporary Salary Increase")]
        TemporarySalary,
        [Display(Name = "UWPMA/Teamsters117 Longevity Pay")]
        LongevityPay
    }

    public class CompAllowanceChange
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
        public virtual EWorkerType? EWorkerType { get; set; }

        [Display(Name = "Hire Type")]
        public virtual HireType? HireType { get; set; }

        [Display(Name = "Staff/Student Compensation Allowance Change")]
        public virtual AllowanceChange? AllowanceChange { get; set; }

        [Display(Name = "Supervisory Organization")]
        public virtual SupOrg? SupOrg { get; set; }

        [Display(Name = "Employee EID")]
        public string EmployeeEID { get; set; }

        [Display(Name = "Name"), Required]
        public string Name { get; set; }

        [Display(Name = "Budget Numbers")]
        public string BudgetNumbers { get; set; }

        [Display(Name = "Amount/Percent/Step Increase")]
        public string Amount { get; set; }

        [Display(Name = "Additional Notes")]
        [MaxLength(1024)]
        public string Note { get; set; }

        [Display(Name = "Detailed Description")]
        [MaxLength(1024)]
        public string DetailedDescription { get; set; }

        [Display(Name = "Scholar Compensation Allowance Change")]
        public virtual ScholarCompAllowanceChange? ScholarCompAllowanceChange { get; set; }

        public virtual Department Department { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Scholar/Resident Job Profile")]
        public virtual ScholarJobProfile ScholarJobProfile { get; set; }

    }
}
