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

        [Display(Name = "Budget Type")]
        public virtual BudgetType BudgetType { get; set; }

        [Display(Name = "Budget Purpose")]
        public virtual BudgetPurpose BudgetPurpose { get; set; }

        [Display(Name = "Event Description")]
        public string EventDescription { get; set; }

        [Display(Name = "UW Food Approval Form")]
        public virtual FoodApprovalForm? FoodApprovalForm { get; set; }

        public string Justification { get; set; }

        public string EmployeeEID { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Budget Numbers")]
        public string BudgetNumbers { get; set; }

        [Display(Name = "# Attending")]
        public int NumberAttending { get; set; }

        [Display(Name = "Budget In Deficit")]
        public bool BudgetDeficit { get; set; }

        public string Note { get; set; }

        [Display(Name = "Item 1")]
        public string ItemName1 { get; set; }

        [Display(Name = "Item 2")]
        public string ItemName2 { get; set; }

        [Display(Name = "Item 3")]
        public string ItemName3 { get; set; }

        [Display(Name = "Item 4")]
        public string ItemName4 { get; set; }

        [Display(Name = "Item 5")]
        public string ItemName5 { get; set; }

        [Display(Name = "Item 6")]
        public string ItemName6 { get; set; }

        [Display(Name = "Item 7")]
        public string ItemName7 { get; set; }

        [Display(Name = "Cost 1")]
        public float? ItemCost1 { get; set; }

        [Display(Name = "Cost 2")]
        public float? ItemCost2 { get; set; }

        [Display(Name = "Cost 3")]
        public float? ItemCost3 { get; set; }

        [Display(Name = "Cost 4")]
        public float? ItemCost4 { get; set; }

        [Display(Name = "Cost 5")]
        public float? ItemCost5 { get; set; }

        [Display(Name = "Cost 6")]
        public float? ItemCost6 { get; set; }

        [Display(Name = "Cost 7")]
        public float? ItemCost7 { get; set; }

        public float? Total { get; set; }
    }
}
