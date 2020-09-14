using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public enum BasePayChange
    {
        [Display(Name = "Ingrade – Change in Responsibilities")]
        IngradeChange,
        [Display(Name = "Ingrade – Competitive Offer(non-UW)")]
        IngradeCompete,
        [Display(Name = "Ingrade – Market/Retention")]
        IngradeRetain,
        [Display(Name = "Ingrade – Merit/Increased Functioning")]
        IngradeMerit,
        [Display(Name = "Ingrade – Preemptive Offer(non-UW)")]
        IngradePrempt,
        [Display(Name = "Internal Equity")]
        InternalEquity,
        [Display(Name = "Salary / Hourly Rate Reduction")]
        HourlyReduction,
        [Display(Name = "Step Adjustment – Career Enhancement/Growth Program(CEGP)")]
        StepCareer,
        [Display(Name = "Step Adjustment – Contractual Change")]
        StepContract,
        [Display(Name = "Step Adjustment – Other")]
        StepOther,
        [Display(Name = "Step Adjustment –Recruitment / Retention Adjustment")]
        StepRecruit,
        [Display(Name = "Maintain Progression Start Date")]
        MaintainProgress
    }
  

    public enum HireType
    {
        [Display(Name = "Student")]
        Student,
        [Display(Name = "Staff/Classified")]
        Classified,
        [Display(Name = "Staff/Professional")]
        Professional
    }

    public enum AWorkerType
    {
        [Display(Name = "Staff/Student")]
        Staff,
        [Display(Name = "Faculty")]
        Faculty,
        [Display(Name = "Postdoctoral Scholar/Resident")]
        Scholar,
        [Display(Name = "Graduate Student")]
        Grad
    }

    public enum BWorkerType
    {
        [Display(Name = "Staff/Student")]
        Student,     
        [Display(Name = "Postdoctoral Scholar/Resident")]
        Scholar,
        [Display(Name = "Graduate Student")]
        Grad
    }

    public enum CWorkerType
    {
        [Display(Name = "Staff/Student")]
        Staff
    }

    public enum DWorkerType
    {
        [Display(Name = "Postdoctoral Scholar/Resident")]
        Scholar,
        [Display(Name = "Graduate Student")]
        Grad
    }

    public enum EWorkerType
    {
        [Display(Name = "Staff/Student")]
        Staff,
        [Display(Name = "Postdoctoral Scholar/Resident")]
        Scholar        
    }

    

    public enum SupOrg
    {

        [Display(Name = "Clinical Accounting: Cash Applications (Cooley, Joel)")] Clinical1,
        [Display(Name = "Deans Office: Clinical Affairs (Schwedhelm, E. Ricardo)")] Dean1,
        [Display(Name = "Deans Office: Compliance (Brown, Robert S.) ")] Dean2,
        [Display(Name = "Deans Office: Compliance JM Student (Brown, Robert S.)")] Dean3,
        [Display(Name = "Deans Office: D1 Simulation JM Student (Croom, Jeffrey R.)")] Dean4,
        [Display(Name = "Deans Office: Deans Office Administration (Farris, Gary E)")] Dean5,
        [Display(Name = "Deans Office: Deans Office Administration JM Student (Wee, Christina)")] Dean6,
        [Display(Name = "Deans Office: Dental Maintenance (Fox, David G.)")] Dean7,
        [Display(Name = "Deans Office: Derouen Center (Seminario, Ana Lucia)")] Dean8,
        [Display(Name = "Deans Office: Derouen Center JM Student (Seminario, Ana Lucia)")] Dean9,
        [Display(Name = "Deans Office: Educational Partnerships & Diversity (Gandara, Beatrice K)")] Dean11,
        [Display(Name = "Deans Office: Educational Partnerships & Diversity JM Contingent Worker (Gandara, Beatrice K)")] Dean12,
        [Display(Name = "Deans Office: Finance Administration (Farris, Gary E)")] Dean1136,
        [Display(Name = "Deans Office: Human Resources Operations (Bispham, Renni)")] Dean14,
        [Display(Name = "Deans Office: Information Management (Farris, Gary E)")] Dean15,
        [Display(Name = "Deans Office: Qi & Safety (Phillips, Sandra L)")] Dean16,
        [Display(Name = "Deans Office: School of Dentistry IT (Ruddle, Tom)")] Dean17,
        [Display(Name = "Deans Office: School of Dentistry IT JM Student (Ruddle, Tom)")] Dean18,
        [Display(Name = "Deans Office: Student Services (Reang Sperry, Chanira)")] Dean19,
        [Display(Name = "Deans Office: Student Services JM Student (Reang Sperry, Chanira)")] Dean20,
        [Display(Name = "Dental Clinic: Clinic Operations (Kim, Mihwa (On Leave))")] Clinic2,
        [Display(Name = "Dental Clinic: Patient Intake (Kim, Mihwa (On Leave))")] Clinic3,
        [Display(Name = "Dental Clinic: Patient Intake JM Student (Kim, Mihwa (On Leave))")] Clinic4,
        [Display(Name = "Dental Clinic: Patient Relations (Phillips, Sandra L)")] Clinic5,
        [Display(Name = "Dental Clinic: Pre-Doc Clinic Coordination (Kim, Mihwa (On Leave))")] Clinic6,
        [Display(Name = "Dental Clinic: Pre-Doc Clinic Coordination JM Student (Kim, Mihwa (On Leave))")] Clinic7,
        [Display(Name = "Dental Clinic: Pre-Doc Clinics (Baird, Christine (On Leave))")] Clinic8,
        [Display(Name = "Dental Clinic: Sterilization (Harvey, Carol J)")] Clinic9,
        [Display(Name = "Dental Clinic: Sterilization Magnuson (Ashford-Rimstad, Shelley)")] Endo1,
        [Display(Name = "Department of Endodontics (Johnson, James D)")] Endo2,
        [Display(Name = "Department of Endodontics JM Academic (Johnson, James D)")] Endo3,
        [Display(Name = "Department of Oral And Maxillo Facial Surgery (Dodson, Thomas B)")] OMS1,
        [Display(Name = "Department of Oral And Maxillo Facial Surgery JM Academic (Dodson, Thomas B)")] OMS2,
        [Display(Name = "Department of Oral And Maxillo Facial Surgery JM Resident/Fellow (Dillon, Jasjit K)")] OMS3,
        [Display(Name = "Department of Oral Health Sciences (Ramsay, Douglas S)")] OHS1,
        [Display(Name = "Department of Oral Health Sciences JM Academic (Ramsay, Douglas S)")] OHS2,
        [Display(Name = "Department of Oral Health Sciences JM Resident/Fellow (Ramsay, Douglas S)")] OHS3,
        [Display(Name = "Department of Oral Health Sciences Postdoctoral Scholars (McLaughlin, Barb)")] OHS4,
        [Display(Name = "Department of Oral Medicine (Drangsholt, Mark Thomas)")] OralMed1,
        [Display(Name = "Department of Oral Medicine Graduate Program JM Student (Dean, David R.)")] OralMed2,
        [Display(Name = "Department of Oral Medicine JM Academic (Drangsholt, Mark Thomas)")] OralMed3,
        [Display(Name = "Department of Oral Medicine JM Resident/Fellow (Dean, David R.)")] OralMed4,
        [Display(Name = "Department of Oral Medicine: Oral Maxillofacial Radiology JM Resident/Fellow (Lee, Peggy)")] OralMed5,
        [Display(Name = "Department of Orthodontics (Huang, Greg J.)")] Ortho1,
        [Display(Name = "Department of Orthodontics JM Academic (Huang, Greg J.)")] Ortho2,
        [Display(Name = "Department of Orthodontics JM Contingent Worker (Huang, Greg J.)")] Ortho3,
        [Display(Name = "Department of Pediatric Dentistry (Nelson, Travis M.)")] Pedo1,
        [Display(Name = "Department of Pediatric Dentistry JM Academic (Nelson, Travis M.)")] Pedo2,
        [Display(Name = "Department of Pediatric Dentistry JM Resident/Fellow (Xu, Zheng)")] Pedo3,
        [Display(Name = "Department of Periodontics (Roberts, Frank A.)")] Perio1,
        [Display(Name = "Department of Periodontics JM Academic (Roberts, Frank A.)")] Perio2,
        [Display(Name = "Department of Restorative Dentistry (Chan, Daniel C. N.)")] Restore1,
        [Display(Name = "Department of Restorative Dentistry JM Academic (Chan, Daniel C. N.)")] Restore2,
        [Display(Name = "Endodontics: Administration (Collins, Margaret M)")] Endo4,
        [Display(Name = "Endodontics: Grad Endo Clinic (Sanchez, Jazmin)")] Endo5,
        [Display(Name = "Endodontics: Grad Endo Clinic JM Students (Sanchez, Jazmin)")] Endo6,
        [Display(Name = "External Affairs: Advancement (Newquist, Randall D)")] Alum1,
        [Display(Name = "External Affairs: Advancement JM Student (Newquist, Randall D)")] Alum2,
        [Display(Name = "External Affairs: Continuing Dental Education (Gee, Sally A.)")] CDE1,
        [Display(Name = "External Affairs: Continuing Dental Education JM Student (Gee, Sally A.)")] CDE2,
        [Display(Name = "Faculty Practice Administration (Eanes, Debbie)")] FAC1,
        [Display(Name = "Finance Admin: Banking Operations (Pike, Pam)")] Fin1,
        [Display(Name = "Finance Admin: Clinical Coding (Lowe, LaMar Andre)")] Fin2,
        [Display(Name = "Finance Admin: Dental Central Purchasing (Douglas, Teresa N.)")] CP1,
        [Display(Name = "Finance Admin: Dental Central Purchasing JM Student (Dunlap, Cheryle S)")] CP2,
        [Display(Name = "Finance Admin: Revenue Cycles (Lowe, LaMar Andre)")] Fin3,
        [Display(Name = "Finance Admin:Clinical Services Accounting (Farris, Gary E)")] Fin5,
        [Display(Name = "Office of Academic Affairs (Gordon, Sara C)")] Acad1,
        [Display(Name = "Office of Academic Affairs JM Student (Howell, Patricia)")] Acad2,
        [Display(Name = "Office of Regional Affairs (RIDE) (Grant, Jennifer H.)")] RIDE1,
        [Display(Name = "Office of Regional Affairs (RIDE) JM Student (Grant, Jennifer H.)")] RIDE2,
        [Display(Name = "Office of Regional Affairs Leadership (Roberts, Frank A.)")] RIDE3,
        [Display(Name = "Office of Research (Ramsay, Douglas S)")] Research1,
        [Display(Name = "Office of Research: Research Administration JM Resident/Fellow (McLaughlin, Barb)")] Research2,
        [Display(Name = "Office of Research: Research Administration JM Student (Ramsay, Douglas S)")] Research3,
        [Display(Name = "Office of Research: Research Administration Postdoctoral Scholars (Ramsay, Douglas S)")] Research4,
        [Display(Name = "Office of Student Services & Admissions (Coldwell, Susan)")] SS2,
        [Display(Name = "Office of Student Services & Admissions JM Student (Brock, Memory)")] SS3,
        [Display(Name = "Oral Health Sciences: Administation JM Student (Ramsay, Douglas S)")] OHS5,
        [Display(Name = "Oral Health Sciences: Administration (Dean, Maggie)")] OHS6,
        [Display(Name = "Oral Health Sciences: Maxpro Clinic (Rubenstein, Jeffrey E)")] OH57,
        [Display(Name = "Oral Health Sciences: RCDRC (Rothen, Marilynn L.)")] OHS8,
        [Display(Name = "Oral Health Sciences: Research- Chi JM Resident/Fellow (Ramsay, Douglas S)")] OHS9,
        [Display(Name = "Oral Health Sciences: Research- Chi JM Student (Chi, Donald L)")] OHS10,
        [Display(Name = "Oral Health Sciences: Research- Leroux (Leroux, Brian G)")] OHS11,
        [Display(Name = "Oral Health Sciences: Research- Ramsay (Ramsay, Douglas S)")] OHS12,
        [Display(Name = "Oral Health Sciences: Research- Silva (Cunha-Cruz, Joana)")] OHS13,
        [Display(Name = "Oral Health Sciences: Research-Chi Staff (Chi, Donald L)")] OHS14,
        [Display(Name = "Oral Health Sciences: Research-Silva JM Student (Cunha-Cruz, Joana)")] OHS15,
        [Display(Name = "Oral Medicine: Administration & OM Clinic (Sebring, Dalila V.)")] OralMed6,
        [Display(Name = "Oral Surgery: Administration (Paul, Sara E)")] OMS4,
        [Display(Name = "Oral Surgery: Administration JM Student (Chung, Alyssa)")] OMS5,
        [Display(Name = "Oral Surgery: AGD_JM_Student (O'Connor, Ryan T.)")] OMS6,
        [Display(Name = "Oral Surgery: GPR Residency JM Resident/Fellow (O'Connor, Ryan T.)")] OMS7,
        [Display(Name = "Oral Surgery: OP (Oda, Dolphine)")] OMS8,
        [Display(Name = "Oral Surgery: OP JM Student (Oda, Dolphine)")] OMS9,
        [Display(Name = "Oral Surgery: OS Clinical (Paul, Sara E)")] OMS10,
        [Display(Name = "Oral Surgery: OS Clinical Services (McCloud, Mesha)")] OMS11,
        [Display(Name = "Oral Surgery: OS Clinical Services JM Student (McCloud, Mesha)")] OMS12,
        [Display(Name = "Oral Surgery: Patient Services (Cossette, Rebecca C)")] OMS13,
        [Display(Name = "Oral Surgery: Patient Services JM Student (Cossette, Rebecca C)")] OMS14,
        [Display(Name = "Oral Surgery-Nursing (Paul, Sara E)")] OMS15,
        [Display(Name = "Orthodontics: Administrative (Liao, Ellen Aihua)")] Ortho4,
        [Display(Name = "Orthodontics: Administrative JM Student (Liao, Ellen Aihua)")] Ortho5,
        [Display(Name = "Orthodontics: Clinic Admin (Greenlee, Geoffrey M)")] Ortho6,
        [Display(Name = "Orthodontics: Clinic Staff (Allen, Becky)")] Ortho7,
        [Display(Name = "Orthodontics: Research (Huang, Greg J.)")] Ortho8,
        [Display(Name = "Othodontics: Research JM Student (Huang, Greg J.)")] Ortho9,
        [Display(Name = "Pediatric Dentistry: ABCD Research (Kim, Amy S)")] Pedo4,
        [Display(Name = " Dentistry: Administration (Coombes, Elizabeth)")] Pedo5,
        [Display(Name = "Pediatric Dentistry: CPD Clinic (Coombes, Elizabeth)")] Pedo6,
        [Display(Name = "Pediatric Dentistry: Dental Assistants (Evans, Katie)")] Pedo7,
        [Display(Name = "Pediatric Dentistry: Dental Surgical Center (Evans, Katie)")] Pedo8,
        [Display(Name = "Pediatric Dentistry: Patient Services (Coombes, Elizabeth)")] Pedo9,
        [Display(Name = "Pediatric Dentistry: Patient Services JM Student (Coombes, Elizabeth)")] Pedo10,
        [Display(Name = "Periodontics: Administrative (Collins, Margaret M)")] Perio3,
        [Display(Name = "Periodontics: Administrative JM Student (Collins, Margaret M)")] Perio4,
        [Display(Name = "Periodontics: Grad Perio Clinic Staff (Daubert, Diane M.)")] Perio5,
        [Display(Name = "Periodontics: Research Darveau (Darveau, Richard P.)")] Perio6,
        [Display(Name = "Periodontics: Research Mclean (McLean, Jeffrey S)")] Perio7,
        [Display(Name = "Periodontics: Research Mclean JM Resident/Fellow (McLean, Jeffrey S)")] Perio8,
        [Display(Name = "Restorative Dentistry: Administration (Low, Betty)")] Restore3,
        [Display(Name = "Restorative Dentistry: Administration JM Student (Low, Betty)")] Restore4,
        [Display(Name = "Restorative Dentistry: Grad Pros Clinic Admin (Ramos, Van)")] Restore5,
        [Display(Name = "Restorative Dentistry: Grad Pros Clinic staff (Green, Carole K.)")] Restore6,
        [Display(Name = "School of Dentistry: (Chiodo, Gary T)")] Dean21,
        [Display(Name = "Sterilization (White, Chalet)")] Sterile1,
        [Display(Name = "UW Dentistry Campus Dental Center (UWDCDC) (O'Connor, Ryan T.)")] UWDCDC1,
        [Display(Name = "UW Dentistry Campus Dental Center Operations (O'Connor, Ryan T.)")] UWDCDC2

    }

    public enum TerminationReason
    {
        [Display(Name = "Death")]
        Death,
        [Display(Name = "Layoff - Funding")]
        LayoffFund,
        [Display(Name = "Layoff - Lack of Work")]
        LayoffWork,
        [Display(Name = "Layoff - Reorganization")]
        LayoffReorg,
        [Display(Name = "Retirement")]
        Retirement,
        [Display(Name = "Separation - Better Job Opportunities")]
        SeparationBetterJob,
        [Display(Name = "Separation - Commute")]
        SeparationCommute,
        [Display(Name = "Separation - Denied Tenure / Promotion")]
        SeparationNoTenure,
        [Display(Name = "Separation - Educational Pursuit")]
        SeparationEducation,
        [Display(Name = "Separation - End of Student Status")]
        SeparationEndStudent,
        [Display(Name = "Separation - Fixed Term Job Ended")]
        SeparationFixedTerm,
        [Display(Name = "Separation - General Resignation")]
        SeparationResignation,
        [Display(Name = "Separation - Job Dissatisfaction")]
        SeparationDissat,
        [Display(Name = "Separation - Medical Disability")]
        SepartionMedDis,
        [Display(Name = "Separation - Not Reappointed/Renewed")]
        SeparationNoRenew,
        [Display(Name = "Separation - Personal Reasons")]
        SeparationPersonal,
        [Display(Name = "Separation - Probationary Rejection")]
        SeparationProbation,
        [Display(Name = "Separation - Relocation")]
        SeparationReloc,
        [Display(Name = "Separation - Resigned-Accepted non-Academic Position")]
        SeparationResAca,
        [Display(Name = "Separation - Resign in Lieu of Dismissal")]
        SepartionDismiss,
        [Display(Name = "Separation - Return to Rehire List")]
        SeparationRehire,
        [Display(Name = "Separation - Separated for Cause")]
        SeparationCause,
        [Display(Name = "Separation - Transfer Within UW Medicine")]
        SeparationTransfer
    }

    public enum Department
    {
        [Display(Name = "Endodontics")]
        Endodontics,
        [Display(Name = "Office of Research")]
        Research,
        [Display(Name = "Oral and Maxillofacial Surgery")]
        OMS,
        [Display(Name = "Oral Health Sciences")]
        OHS,
        [Display(Name = "Oral Medicine")]
        OMed,
        [Display(Name = "Orthodontics")]
        Ortho,
        [Display(Name = "Pediatric Dentistry")]
        Pedo,
        [Display(Name = "Periodontics")]
        Perio,
        [Display(Name = "Restorative Dentistry")]
        Restore
    }
    public class CompBasePayChange
    {
        [Required, Key, ForeignKey("Case")]
        public int CaseID { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Effective Start Date")]
        [DataType(DataType.Date)]
        public DateTime EffectiveStartDate { get; set; }

        [Display(Name = "Effective End Date")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveEndDate { get; set; }

        [Display(Name = "Worker Type")]
        public virtual CWorkerType? CWorkerType { get; set; }

        [Display(Name = "Hire Type")]
        public virtual HireType? HireType { get; set; }

        [Display(Name = "Base Pay Change Type")]
        public virtual BasePayChange? BasePayChange { get; set; }

        [Display(Name = "Supervisory Organization")]
        public virtual SupOrg? SupOrg { get; set; }

        [Display(Name = "Employee EID")]
        public string EmployeeEID { get; set; }

        [Display(Name = "Name"), Required]
        public string Name { get; set; }

        [Display(Name = "Budget Numbers")]
        public string BudgetNumbers { get; set; }

        [Display(Name = "Amount/Percent/Step Increase")]
        public string Amount { get; set; }

        [Display(Name = "Additional Notes")]
        [MaxLength(1024)]
        public string Note { get; set; }

        [Display(Name = "Detailed Description")]
        [MaxLength(1024)]
        public string DetailedDescription { get; set; }

        public virtual Department? Department { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
    }
}
