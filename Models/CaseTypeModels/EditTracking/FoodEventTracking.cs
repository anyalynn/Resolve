using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public class FoodEventTracking
    {
        public int FoodEventTrackingID { get; set; }
        public string Status { get; set; }
        public int CaseAuditID { get; set; }
        public CaseAudit CaseAudit { get; set; }
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        public virtual Department Department { get; set; }

        public virtual BudgetType BudgetType { get; set; }

        public virtual BudgetPurpose BudgetPurpose { get; set; }

        public string EventDescription { get; set; }

        public string Justification { get; set; }

        public string EmployeeEID { get; set; }

        [Display(Name = "Employee Name"), Required]
        public string EmployeeName { get; set; }

        [Display(Name = "Budget Numbers")]
        public string BudgetNumbers { get; set; }

        public string Note { get; set; }

        public int? Item1 { get; set; }

        public int? Item2 { get; set; }

        public int? Item3 { get; set; }

        public int? Item4 { get; set; }

        public int? Item5 { get; set; }

        public int? Item6 { get; set; }

        public int? Item7 { get; set; }

        public int? Total { get; set; }
    }
}
