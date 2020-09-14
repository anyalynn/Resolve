using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public class CPPaymentRequestTracking
    {
        public int CPPaymentRequestTrackingID { get; set; }
        public string Status { get; set; }
        public int CaseAuditID { get; set; }
        public CaseAudit CaseAudit { get; set; }
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Display(Name = "Requester Name")]
        public string RequesterName { get; set; }

        [Display(Name = "Additional Notes")]
        public string Note { get; set; }

        [Display(Name = "Amount")]
        public string Amount { get; set; }

        [Display(Name = "Payee")]
        public string Payee { get; set; }

        [Display(Name = "Payment Explanation")]
        public string Explanation { get; set; }

        [Display(Name = "Budget Number")]
        public string BudgetNumber { get; set; }


    }
}
