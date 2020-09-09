using Microsoft.EntityFrameworkCore;
using Resolve.Models;

namespace Resolve.Data
{
    public class ResolveCaseContext : DbContext
    {
        public ResolveCaseContext(DbContextOptions<ResolveCaseContext> options)
            : base(options)
        {
        }

        public DbSet<LocalUser> LocalUser { get; set; }
        public DbSet<EmailPreference> EmailPreference { get; set; }
        public DbSet<LocalGroup> LocalGroup { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<CaseType> CaseType { get; set; }
        public DbSet<CaseTypeGroup> CaseTypeGroup { get; set; }
        public DbSet<Case> Case { get; set; }
        public DbSet<Approver> Approver { get; set; }
        public DbSet<GroupAssignment> GroupAssignment { get; set; }
        public DbSet<CaseAudit> CaseAudit { get; set; }
        public DbSet<CaseComment> CaseComment { get; set; }
        public DbSet<CaseAttachment> CaseAttachment { get; set; }
        public DbSet<OnBehalf> OnBehalf { get; set; }
        public DbSet<SampleCaseType> SampleCaseType { get; set; }
        public DbSet<SampleCaseTypeTracking> SampleCaseTypeTracking { get; set; }
        public DbSet<HRServiceStaff> HRServiceStaff { get; set; }
        public DbSet<HRServiceStaffTracking> HRServiceStaffTracking { get; set; }
        public DbSet<HRServiceFaculty> HRServiceFaculty { get; set; }
        public DbSet<HRServiceFacultyTracking> HRServiceFacultyTracking { get; set; }
        public DbSet<HRServiceGradStudent> HRServiceGradStudent { get; set; }
        public DbSet<PerioLimitedCare> PerioLimitedCare { get; set; }
        public DbSet<HiringAffiliateFaculty> HiringAffiliateFaculty { get; set; }
        public DbSet<HiringAffiliateFacultyTracking> HiringAffiliateFacultyTracking { get; set; }
        public DbSet<HiringFaculty> HiringFaculty { get; set; }
        public DbSet<HiringFacultyTracking> HiringFacultyTracking { get; set; }
        public DbSet<HRServiceScholarResident> HRServiceScholarResident { get; set; }
        public DbSet<HRServiceGradStudentTracking> HRServiceGradStudentTracking { get; set; }
        public DbSet<HRServiceScholarResidentTracking> HRServiceScholarResidentTracking { get; set; }
        public DbSet<PerioLimitedCareTracking> PerioLimitedCareTracking { get; set; }
        public DbSet<Travel> Travel { get; set; }
        public DbSet<FoodEvent> FoodEvent { get; set; }
        public DbSet<TravelTracking> TravelTracking { get; set; }
        public DbSet<FoodEventTracking> FoodEventTracking { get; set; }
        public DbSet<HiringStaff> HiringStaff { get; set; }
        public DbSet<HiringStaffTracking> HiringStaffTracking { get; set; }
        public DbSet<PatientEvent> PatientEvent { get; set; }
        public DbSet<PatientEventTracking> PatientEventTracking { get; set; }
        public DbSet<AxiumFeeSchedule> AxiumFeeSchedule { get; set; }
        public DbSet<AxiumFeeScheduleTracking> AxiumFeeScheduleTracking { get; set; }
        public DbSet<DistributionChange> DistributionChange { get; set; }
        public DbSet<EndDateChange> EndDateChange { get; set; }
        public DbSet<FTEChange> FTEChange { get; set; }
        public DbSet<CompAllowanceChange> CompAllowanceChange { get; set; }
        public DbSet<CompBasePayChange> CompBasePayChange { get; set; }
        public DbSet<MoveWorker> MoveWorker { get; set; }
        public DbSet<SecurityChange> SecurityChange { get; set; }
        public DbSet<Termination> Termination { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Case>()
                .HasOne(p => p.LocalUser)
                .WithMany(q => q.Cases)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Case>()
                .Property(p => p.CaseCID)
                .HasComputedColumnSql("'CASE' + CONVERT([nvarchar](23),[CaseID]+10000000)");
            modelBuilder.Entity<Case>()
                .HasOne(p => p.CaseType)
                .WithMany(q => q.Cases)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Case>()
            .Property(b => b.CaseCreationTimestamp)
            .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Case>()
                .Property(r => r.Processed)
                .HasDefaultValue(0);

            modelBuilder.Entity<Approver>()
                .HasKey(c => new { c.CaseID, c.LocalUserID });
            modelBuilder.Entity<Approver>()
                .Property(e => e.Order)
                .HasDefaultValue(1);
            modelBuilder.Entity<Approver>()
                .Property(e => e.Approved)
                .HasDefaultValue(0);

            modelBuilder.Entity<CaseType>()
                .Property(e => e.GroupNumber)
                .HasDefaultValue(1);
            modelBuilder.Entity<CaseType>()
                .HasAlternateKey(e => e.CaseTypeTitle);

            modelBuilder.Entity<CaseTypeGroup>()
                .HasKey(c => new { c.CaseTypeID, c.LocalGroupID });
            modelBuilder.Entity<CaseTypeGroup>()
                .Property(e => e.Order)
                .HasDefaultValue(1);

            modelBuilder.Entity<GroupAssignment>()
                .HasKey(c => new { c.CaseID, c.LocalGroupID });

            modelBuilder.Entity<OnBehalf>()
                .HasKey(c => new { c.CaseID, c.LocalUserID });

            modelBuilder.Entity<UserGroup>()
                .HasKey(c => new { c.LocalUserID, c.LocalGroupID });

            modelBuilder.Entity<LocalGroup>()
                .HasAlternateKey(e => e.GroupName);

            modelBuilder.Entity<LocalUser>()
                .HasAlternateKey(e => e.EmailID);

            modelBuilder.Entity<CaseComment>()
            .Property(b => b.CommentTimestamp)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<CaseAudit>()
            .Property(b => b.AuditTimestamp)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<CaseAttachment>()
            .Property(b => b.AttachmentTimestamp)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<HRServiceStaff>()
               .Property(h => h.Offboarding)
               .HasDefaultValue(0);
            modelBuilder.Entity<HRServiceStaff>()
                .Property(h => h.ClosePosition)
                .HasDefaultValue(0);
            modelBuilder.Entity<HRServiceStaff>()
                .Property(h => h.LeaveWA)
                .HasDefaultValue(0);
            modelBuilder.Entity<Termination>()
              .Property(h => h.Offboarding)
              .HasDefaultValue(0);
            modelBuilder.Entity<Termination>()
                .Property(h => h.ClosePosition)
                .HasDefaultValue(0);
            modelBuilder.Entity<Termination>()
                .Property(h => h.LeaveWA)
                .HasDefaultValue(0);
            modelBuilder.Entity<HRServiceFaculty>()
              .Property(i => i.Offboarding)
              .HasDefaultValue(0);
            modelBuilder.Entity<HRServiceFaculty>()
                .Property(i => i.ClosePosition)
                .HasDefaultValue(0);
            modelBuilder.Entity<HRServiceFaculty>()
                .Property(i => i.LeaveWA)
                .HasDefaultValue(0);
            modelBuilder.Entity<PerioLimitedCare>()
                .Property(i => i.PerioExam)
                .HasDefaultValue(0);
            modelBuilder.Entity<PerioLimitedCare>()
               .Property(i => i.RestorativeExam)
               .HasDefaultValue(0);
            modelBuilder.Entity<PerioLimitedCare>()
              .Property(i => i.bwxrays)
              .HasDefaultValue(0);
            modelBuilder.Entity<PerioLimitedCare>()
                .Property(i => i.paxrays)
                .HasDefaultValue(0);
            modelBuilder.Entity<PerioLimitedCare>()
             .Property(i => i.Prophy)
             .HasDefaultValue(0);
            modelBuilder.Entity<PerioLimitedCare>()
             .Property(i => i.Other)
             .HasDefaultValue(0);

            modelBuilder.Entity<SampleCaseTypeTracking>()
                .HasOne(p => p.CaseAudit)
                .WithMany(q => q.SampleCaseTypeTrackings)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HiringAffiliateFacultyTracking>()
                .HasOne(p => p.CaseAudit)
                .WithMany(q => q.HiringAffiliateFacultyTrackings)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HiringFacultyTracking>()
               .HasOne(p => p.CaseAudit)
               .WithMany(q => q.HiringFacultyTrackings)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HiringStaffTracking>()
              .HasOne(p => p.CaseAudit)
              .WithMany(q => q.HiringStaffTrackings)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HRServiceStaffTracking>()
                .HasOne(p => p.CaseAudit)
                .WithMany(q => q.HRServiceStaffTrackings)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HRServiceFacultyTracking>()
               .HasOne(p => p.CaseAudit)
               .WithMany(q => q.HRServiceFacultyTrackings)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HRServiceGradStudentTracking>()
              .HasOne(p => p.CaseAudit)
              .WithMany(q => q.HRServiceGradStudentTrackings)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HRServiceScholarResidentTracking>()
              .HasOne(p => p.CaseAudit)
              .WithMany(q => q.HRServiceScholarResidentTrackings)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PerioLimitedCareTracking>()
              .HasOne(p => p.CaseAudit)
              .WithMany(q => q.PerioLimitedCareTrackings)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FoodEventTracking>()
                .HasOne(p => p.CaseAudit)
                .WithMany(q => q.FoodEventTrackings)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PatientEventTracking>()
                .HasOne(p => p.CaseAudit)
                .WithMany(q => q.PatientEventTrackings)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AxiumFeeScheduleTracking>()
               .HasOne(p => p.CaseAudit)
               .WithMany(q => q.AxiumFeeScheduleTrackings)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AxiumFeeSchedule>()
                .Property(s => s.UnitsFactored)
                .HasDefaultValue(0);           

            modelBuilder.Entity<PatientEvent>()
              .Property(s => s.FactsDocumented)
              .HasDefaultValue(0);

            modelBuilder.Entity<TravelTracking>()
                .HasOne(p => p.CaseAudit)
                .WithMany(q => q.TravelTrackings)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HiringStaff>()
                .Property(s => s.OvertimeEligible)
                .HasDefaultValue(0);
          
            modelBuilder.Entity<HiringStaff>()
                .Property(s => s.Super)
                .HasDefaultValue(0);

            modelBuilder.Entity<HiringStaff>()
               .Property(s => s.CandidateSelected)
               .HasDefaultValue(0);

            modelBuilder.Entity<HiringStaff>()
               .Property(s => s.Workstudy)
               .HasDefaultValue(0);
        }
    }
}