using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public class HiringAffiliateFacultyTracking
    {
        public int HiringAffiliateFacultyTrackingID { get; set; }
        public string Status { get; set; }
        public int CaseAuditID { get; set; }
        public CaseAudit CaseAudit { get; set; }
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Proposed Hire Date")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        public virtual Department? Department { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        [Display(Name = "Job Title")]
        public virtual FacAffiliateTitle? FacAffiliateTitle { get; set; }
    }
}
