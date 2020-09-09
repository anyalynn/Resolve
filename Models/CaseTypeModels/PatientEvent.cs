using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public enum EventLocation
    {
        [Display(Name = "Location not known")]
        Unknow,
        [Display(Name = "Administration")]
        Admin,
        [Display(Name = "AGD")]
        AGD,
        [Display(Name = "Charts /Record Room")]
        Records,
        [Display(Name = "D2 /D3 Dispensary")]
        D2D3,
        [Display(Name = "DECOD")]
        DECOD,
        [Display(Name = "Dental Lab")]
        Lab,
        [Display(Name = "Dental Clinic")]
        DentalClinic,
        [Display(Name = "Dental Fears Clinic")]
        Fears,
        [Display(Name = "Endodontics")]
        Endo,
        [Display(Name = "Campus Dental Center")]
        FACPRAC,
        [Display(Name = "Oral Medicine-BA")]
        ORALMBA,
        [Display(Name = "Oral Medicine-DUCC")]
        Urgent,
        [Display(Name = "Oral Medicine")]
        ORALM,
        [Display(Name = "Oral Health Sciences")]
        OHS,
        [Display(Name = "Oral Surgery")]
        OMS,
        [Display(Name = "Orthodontics")]
        Ortho,
        [Display(Name = "Pediatric Dentistry")]
        PEDO,
        [Display(Name = "Periodontics")]
        Perio,
        [Display(Name = "Prosthodontics")]
        Prosth,
        [Display(Name = "Radiology")]
        Radio,
        [Display(Name = "Restorative")]
        Restore,
        [Display(Name = "Sterilization /Pre-sterilization")]
        Sterilize

    }

    public enum EventDepartment
    {
        [Display(Name = "Administration")]
        Admin,
        [Display(Name = "CDE")]
        CDE,
        [Display(Name = "Dean's Office")]
        Deans,
        [Display(Name = "Endodontics")]
        Endo,
        [Display(Name = "Faculty Practice")]
        FACPRAC,
        [Display(Name = "Information Technology")]
        IT,
        [Display(Name = "OCS")]
        OCS,
        [Display(Name = "Office of Research")]
        Research,
        [Display(Name = "Oral Health Sciences")]
        OHS,
        [Display(Name = "Oral and Maxillofacial Surgery")]
        ORM,
        [Display(Name = "Oral Medicine")]
        ORALM,
        [Display(Name = "Orthodontics")]
        Ortho,
        [Display(Name = "PAO")]
        PAO,
        [Display(Name = "Pediatric Dentistry")]
        PEDO,
        [Display(Name = "Periodontics")]
        Perio,
        [Display(Name = "Prosthodontics")]
        Prosth,
        [Display(Name = "Radiology")]
        Radio,
        [Display(Name = "Restorative")]
        Restore

    }


    public class PatientEvent

    {
        [Required, Key, ForeignKey("Case")]
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Date Manager Notified")]
        [DataType(DataType.Date)]
        public DateTime? NotifyDate { get; set; }

        [Display(Name = "Department")]
        public virtual EventDepartment? EventDepartment { get; set; }

        [Display(Name = "Brief Description of Event")]
        public string EventDescription { get; set; }

        [Display(Name = "Facts Documented?")]
        public bool FactsDocumented { get; set; }

        [Display(Name = "Dental Record #")]
        public string DentalRecordNum  { get; set; }

        [Display(Name = "First Witness")]
        public string Witness1 { get; set; }

        [Display(Name = "Second Witness")]
        public string Witness2 { get; set; }

        [Display(Name = "Name of Manager Notified")]
        public string NotifiedName { get; set; }

        [Display(Name = "Supervisor Name")]
        public string SupervisorName { get; set; }

        [Display(Name = "Visit Provider Name")]
        public string ProviderName { get; set; }

        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Display(Name = "First Reported By")]
        public string FirstReportedBy { get; set; }
     
        [Display(Name = "Causes")]
        public string Causes { get; set; }

        [Display(Name = "Actions Taken by Reporter")]
        public string ReporterActionTaken { get; set; }

        [Display(Name = "Actions Taken by Manager")]
        public string ManagerActionTaken { get; set; }

        [Display(Name = "Event Location")]
        public virtual EventLocation? EventLocation { get; set; }

        [Display(Name = "Gender")]
        public virtual Gender? Gender{ get; set; }

        [Display(Name = "Additional Notes")]
        public string Note { get; set; }

    }
}
