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
        public ICollection<HRServiceStaffTracking> HRServiceStaffTrackings { get; set; }
        public ICollection<HRServiceFacultyTracking> HRServiceFacultyTrackings { get; set; }
        public ICollection<HRServiceGradStudentTracking> HRServiceGradStudentTrackings { get; set; }
        public ICollection<HRServiceScholarResidentTracking> HRServiceScholarResidentTrackings { get; set; }
        public ICollection<PerioLimitedCareTracking> PerioLimitedCareTrackings { get; set; }
        public ICollection<TravelTracking> TravelTrackings { get; set; }
        public ICollection<FoodEventTracking> FoodEventTrackings { get; set; }
    }
}
