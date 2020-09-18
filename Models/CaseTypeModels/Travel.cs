using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public enum BudgetType
    {
        [Display(Name = "Clinic")]
        Clinic,
        [Display(Name = "GOF")]
        GOF,
        [Display(Name = "Gift")]
        Gift,
        [Display(Name = "Grant")]
        Grant,
        [Display(Name = "Multiple")]
        Multiple,
        [Display(Name = "Other")]
        Other
    }

    public enum BudgetPurpose
    {
        [Display(Name = "Administration")]
        Administration,
        [Display(Name = "Clinical")]
        Clinical,
        [Display(Name = "Research")]
        Research,
        [Display(Name = "Teaching")]
        Teaching,
        [Display(Name = "Multiple")]
        Multiple,
        [Display(Name = "Other")]
        Other
    }

    public enum TravelFoodDepartment
    {
        [Display(Name = "Academic Affairs")]
        AcademicAffairs,
        [Display(Name = "CDE - Continuing Dental Education")]
        CDE,
        [Display(Name = "CPD - Center for Pediatric Dentistry")]
        CPD,
        [Display(Name = "Dean's Office")]
        Dean,
        [Display(Name = "Endodontics")]
        Endo,
        [Display(Name = "Office of Clinical Services")]
        Clinicalh,
        [Display(Name = "OMS - Oral & Maxillofacial Surgery")]
        OMS,
        [Display(Name = "Oral Biology")]
        OralBio,
        [Display(Name = "Oral Health Sciences")]
        OHS,
        [Display(Name = "Oral Medicine")]
        OralMed,
        [Display(Name = "Orthodontics")]
        Ortho,
        [Display(Name = "Pediatric Dentistry")]
        Pedo,
        [Display(Name = "Periodontics")]
        Perio,
        [Display(Name = "Prosthodontics")]
        Pros,
        [Display(Name = "Radiology")]
        Radio,
        [Display(Name = "Research")]
        Research,
        [Display(Name = "Restorative Dentistry")]
        Restore,
        [Display(Name = "RIDE")]
        RIDE,
        [Display(Name = "Student Services")]
        Student
    }

    public class Travel

    {
        [Required, Key, ForeignKey("Case")]
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Travel Start Date")]
        [DataType(DataType.Date)]
        public DateTime TravelStartDate { get; set; }

        [Display(Name = "Travel End Date")]
        [DataType(DataType.Date)]
        public DateTime? TravelEndDate { get; set; }

        [Display(Name = "Department")]
        public virtual TravelFoodDepartment TravelFoodDepartment { get; set; }

        [Display(Name = "Budget Purpose")]
        public virtual BudgetPurpose BudgetPurpose { get; set; }

        [Display(Name = "Employee Name"), Required]
        public string EmployeeName { get; set; }

        [Display(Name = "Budget Number(s)")]
        public string BudgetNumbers { get; set; }

        [Display(Name = "Additional Notes")]
        public string Note { get; set; }

        public string Destination { get; set; }

        [Display(Name = "Reason for Travel")]
        public string Reason { get; set; }

        [Display(Name = "Other")]
        public string Other1 { get; set; }

        [Display(Name = "Other")]
        public string Other2 { get; set; }

        [Display(Name = "Airfare Cost")]
        public float? AirfareCost { get; set; }

        [Display(Name = "Registration Cost")]
        public float? RegistrationCost { get; set; }

        [Display(Name = "Transportation Cost")]
        public float? TransportationCost { get; set; }

        [Display(Name = "Meals Cost")]
        public float? MealsCost { get; set; }

        [Display(Name = "Hotels Cost")]
        public float? HotelsCost { get; set; }

        [Display(Name = "Other Cost")]
        public float? OtherCost1 { get; set; }

        [Display(Name = "Other Cost")]
        public float? OtherCost2 { get; set; }

        public float? Total { get; set; }
      
    }
}
