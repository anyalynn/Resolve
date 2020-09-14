using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public class CaseAudit
    {
        public int CaseAuditID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime AuditTimestamp { get; set; }

        [Display(Name = "Audit Log"), Required]
        public string AuditLog { get; set; }

        public int CaseID { get; set; }
        public Case Case { get; set; }

        public string LocalUserID { get; set; }
        public LocalUser LocalUser { get; set; }

        // Tracking Case Edit Specific Details
        public ICollection<SampleCaseTypeTracking> SampleCaseTypeTrackings { get; set; }
        public ICollection<HiringAffiliateFacultyTracking> HiringAffiliateFacultyTrackings { get; set; }
        public ICollection<HiringFacultyTracking> HiringFacultyTrackings { get; set; }
        public ICollection<HiringStaffTracking> HiringStaffTrackings { get; set; }
        public ICollection<PerioLimitedCareTracking> PerioLimitedCareTrackings { get; set; }
        public ICollection<TravelTracking> TravelTrackings { get; set; }
        public ICollection<FoodEventTracking> FoodEventTrackings { get; set; }
        public ICollection<PatientEventTracking> PatientEventTrackings { get; set; }
        public ICollection<AxiumFeeScheduleTracking> AxiumFeeScheduleTrackings { get; set; }
        public ICollection<CostAllocationChangeTracking> CostAllocationChangeTrackings { get; set; }
        public ICollection<EndDateChangeTracking> EndDateChangeTrackings { get; set; }
        public ICollection<FTEChangeTracking> FTEChangeTrackings { get; set; }
        public ICollection<CompAllowanceChangeTracking> CompAllowanceChangeTrackings { get; set; }
        public ICollection<CompBasePayChangeTracking> CompBasePayChangeTrackings { get; set; }
        public ICollection<MoveWorkerTracking> MoveWorkerTrackings { get; set; }
        public ICollection<SecurityRolesChangeTracking> SecurityRolesChangeTrackings { get; set; }
        public ICollection<TerminationTracking> TerminationTrackings { get; set; }
        public ICollection<ScholarResGradHireTracking> ScholarResGradHireTrackings { get; set; }
        public ICollection<CPPaymentRequestTracking> CPPaymentRequestTrackings { get; set; }


    }
}
