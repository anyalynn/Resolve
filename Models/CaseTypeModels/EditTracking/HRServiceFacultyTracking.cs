﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public class HRServiceFacultyTracking
    {
        public int HRServiceFacultyTrackingID { get; set; }
        public string Status { get; set; }
        public int CaseAuditID { get; set; }
        public CaseAudit CaseAudit { get; set; }
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Effective Start Date")]
        [DataType(DataType.Date)]
        public DateTime EffectiveStartDate { get; set; }

        [Display(Name = "Effective End/Termination Date")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveEndDate { get; set; }

        [Display(Name = "Request Type"), Required]
        public FacRequestType FacRequestType { get; set; }

        [Display(Name = "Proposed SupOrg")]
        public virtual SupOrg? SupOrg { get; set; }

        public virtual Department Department { get; set; }

        [Display(Name = "Termination Reason")]
        public virtual TerminationReason? TerminationReason { get; set; }

        public string EmployeeEID { get; set; }

        [Display(Name = "Employee Name"), Required]
        public string EmployeeName { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Current FTE")]
        public string CurrentFTE { get; set; }

        [Display(Name = "Proposed FTE")]
        public string ProposedFTE { get; set; }

        [Display(Name = "Budget Numbers")]
        public string BudgetNumbers { get; set; }

        [Display(Name = "Detailed Description")]
        public string DetailedDescription { get; set; }
        
        public string Note { get; set; }

        [Display(Name = "Offboarding?")]
        public bool Offboarding { get; set; }

        [Display(Name = "Close Position?")]
        public bool ClosePosition { get; set; }

        [Display(Name = "Leave WA?")]
        public bool LeaveWA { get; set; }

        [Display(Name = "Salary/Pay Rate")]
        public string Salary { get; set; }

        [Display(Name = "Amount/Percent/Step Increase")]
        public string Amount { get; set; }
    }
}
