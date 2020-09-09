using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public enum AxiumSchedRequestType
    {
        [Display(Name = "Add new fee schedule code(s)")]
        Add,
        [Display(Name = "Modify new fee schedule code(s)")]
        Modify,
        [Display(Name = "Inactivate fee schedule code(s)")]
        Inactivate

    }

    public enum AxiumScheduleType
    {
        [Display(Name = "ALLFAC")]
        AllFac,
        [Display(Name = "HOSDEN")]
        Hosden,
        [Display(Name = "PREDOC")]
        Predoc,
        [Display(Name = "PSTDOC")]
        Postdoc

    }

    public enum Discipline
    {
        [Display(Name = "Diagnostic")]
        Diag,
        [Display(Name = "DSC")]
        Dsc,
        [Display(Name = "Endo")]
        Endo,
        [Display(Name = "FIXEDPROS")]
        FixedPros,
        [Display(Name = "REMPROS")]
        RemPros,
        [Display(Name = "MAXPROS")]
        MaxPros,
        [Display(Name = "OM")]
        OM,
        [Display(Name = "ORTHO")]
        Ortho,
        [Display(Name = "OS")]
        OS,
        [Display(Name = "PEDO")]
        Pedo,
        [Display(Name = "PERIO")]
        Perio,
        [Display(Name = "RECALL")]
        Recall,
        [Display(Name = "REST")]
        Rest,
        [Display(Name = "TxPlan")]
        TxPlan,
        [Display(Name = "Other")]
        Other


    }

    public enum AxiumCodeType
    {
        [Display(Name = "ADA-Dental")]
        Ada,
        [Display(Name = "CPT - Medical")]
        Cpt,
        [Display(Name = "UW Code Administrative")]
        UW
    }

    public enum Site
    {
        [Display(Name = "Arch")]
        Arch,
        [Display(Name = "Quadrant")]
        Quad,
        [Display(Name = "Sextant")]
        Sextant,
        [Display(Name = "Tooth")]
        Tooth,
        [Display(Name = "Full Mouth")]
        Full,
        [Display(Name = "NA")]
        NA
    }

    public class AxiumFeeSchedule

    {
        [Required, Key, ForeignKey("Case")]
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
