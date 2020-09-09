using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public class AxiumFeeScheduleTracking
    {
        public int AxiumFeeScheduleTrackingID { get; set; }
        public string Status { get; set; }
        public int CaseAuditID { get; set; }
        public CaseAudit CaseAudit { get; set; }
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Requeste Type")]
        public virtual AxiumSchedRequestType? AxiumSchedRequestType { get; set; }

        [Display(Name = "Fee Schedule Type")]
        public virtual AxiumScheduleType? AxiumScheduleType { get; set; }

        [Display(Name = "Procedure Code Type")]
        public virtual AxiumCodeType? AxiumCodeType { get; set; }

        public virtual Discipline? Discipline { get; set; }

        public virtual Site? Site { get; set; }

        [Display(Name = "Procedure Code")]
        public string ProcedureCode { get; set; }

        [Display(Name = "Procedure Code Description")]
        public string ProdCodeDescription { get; set; }

        [Display(Name = "Fee")]
        public string Fee { get; set; }

        [Display(Name = "Justification")]
        public string Justification { get; set; }

        [Display(Name = "Units Factored?")]
        public bool UnitsFactored { get; set; }
    }
}
