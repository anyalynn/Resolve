using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public class SecurityRolesChangeTracking
    {
        public int SecurityRolesChangeTrackingID { get; set; }
        public string Status { get; set; }
        public int CaseAuditID { get; set; }
        public CaseAudit CaseAudit { get; set; }
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Effective Date of Change")]
        [DataType(DataType.Date)]
        public DateTime EffectiveDate { get; set; }

        [Display(Name = "Worker Type")]
        public virtual AWorkerType? AWorkerType { get; set; }

        [Display(Name = "Hire Type")]
        public virtual HireType? HireType { get; set; }

        [Display(Name = "Supervisory Organization")]
        public virtual SupOrg? SupOrg { get; set; }

        [Display(Name = "Employee EID")]
        public string EmployeeEID { get; set; }

        [Display(Name = "Supervisees Requiring Additional Access")]
        public string SupervisedAccess { get; set; }

        [Display(Name = "Name"), Required]
        public string Name { get; set; }

        [Display(Name = "Additional Notes")]
        [MaxLength(1024)]
        public string Note { get; set; }

        public virtual Department? Department { get; set; }

        [Display(Name = "UWNETID")]
        public string UWNetID { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Training Completed?")]
        public bool TrainingCompleted { get; set; }

        [Display(Name = "Include Subordinates?")]
        public bool IncludeSubordinates { get; set; }

        [Display(Name = "Type of Request")]
        public virtual SecurityRequestType SecurityRequestType { get; set; }

        [Display(Name = "Academic Chair/ Chair's Delegate")]
        public virtual AcademicChair AcademicChair { get; set; }

        [Display(Name = "Academic Dean / Dean's Delegate")]
        public virtual AcademicDean AcademicDean { get; set; }

        [Display(Name = "HCM Initiate 1")]
        public virtual HCMInit1 HCMInit1 { get; set; }

        [Display(Name = "HCM Initiate 2")]
        public virtual HCMInit2 HCMInit2 { get; set; }

        [Display(Name = "I-9 Coordinator")]
        public virtual I9 I9 { get; set; }

        [Display(Name = "Manager")]
        public virtual Manager Manager { get; set; }

        [Display(Name = "UWHires Hiring Manager")]
        public virtual UWHiresManager UWHiresManager { get; set; }

        [Display(Name = "VO-Staff")]
        public virtual VOStaff VOStaff { get; set; }

        [Display(Name = "VO-STAFF-COMP-COST")]
        public virtual VOStaffCompCost VOStaffCompCost { get; set; }

        [Display(Name = "VO-STAFF-COMP-PERSONAL")]
        public virtual VOStaffCompPersonal VOStaffCompPersonal { get; set; }

        [Display(Name = "Time and Absence Approver")]
        public virtual TimeAbsenceApprover TimeAbsenceApprover { get; set; }

        [Display(Name = "Time and Absence Initiate")]
        public virtual TimeAbsenceInitiate TimeAbsenceInitiate { get; set; }

    }
}
