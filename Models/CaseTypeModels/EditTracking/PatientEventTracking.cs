using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public class PatientEventTracking
    {
        public int PatientEventTrackingID { get; set; }
        public string Status { get; set; }
        public int CaseAuditID { get; set; }
        public CaseAudit CaseAudit { get; set; }
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime? EventDate { get; set; }

        [Display(Name = "Event Date/Time")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? EventDateTime { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Notification Date")]
        [DataType(DataType.Date)]
        public DateTime? NotifyDate { get; set; }

        public virtual EventDepartment? EventDepartment { get; set; }

        [Display(Name = "Event Description")]
        public string EventDescription { get; set; }

        [Display(Name = "Facts Documented")]
        public bool FactsDocumented { get; set; }

        [Display(Name = "Dental Record Number")]
        public string DentalRecordNum { get; set; }

        [Display(Name = "First Witness")]
        public string Witness1 { get; set; }

        [Display(Name = "Second Witness")]
        public string Witness2 { get; set; }

        [Display(Name = "Manager Notified Name")]
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
        public virtual Gender? Gender { get; set; }

        [Display(Name = "Additional Notes")]
        public string Note { get; set; }
    }
}
