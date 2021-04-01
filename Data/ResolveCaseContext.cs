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
        public DbSet<PerioLimitedCare> PerioLimitedCare { get; set; }
        public DbSet<HiringAffiliateFaculty> HiringAffiliateFaculty { get; set; }
        public DbSet<HiringAffiliateFacultyTracking> HiringAffiliateFacultyTracking { get; set; }
        public DbSet<HiringFaculty> HiringFaculty { get; set; }
        public DbSet<HiringFacultyTracking> HiringFacultyTracking { get; set; }
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
        public DbSet<CostAllocationChange> CostAllocationChange { get; set; }
        public DbSet<EndDateChange> EndDateChange { get; set; }
        public DbSet<FTEChange> FTEChange { get; set; }
        public DbSet<CompAllowanceChange> CompAllowanceChange { get; set; }
        public DbSet<CompBasePayChange> CompBasePayChange { get; set; }
        public DbSet<MoveWorker> MoveWorker { get; set; }
        public DbSet<SecurityRolesChange> SecurityRolesChange { get; set; }
        public DbSet<Termination> Termination { get; set; }
        public DbSet<CostAllocationChangeTracking> CostAllocationChangeTracking { get; set; }
        public DbSet<EndDateChangeTracking> EndDateChangeTracking { get; set; }
        public DbSet<FTEChangeTracking> FTEChangeTracking { get; set; }
        public DbSet<CompAllowanceChangeTracking> CompAllowanceChangeTracking { get; set; }
        public DbSet<CompBasePayChangeTracking> CompBasePayChangeTracking { get; set; }
        public DbSet<MoveWorkerTracking> MoveWorkerTracking { get; set; }
        public DbSet<SecurityRolesChangeTracking> SecurityRolesChangeTracking { get; set; }
        public DbSet<TerminationTracking> TerminationTracking { get; set; }
        public DbSet<ScholarResGradHire> ScholarResGradHire { get; set; }
        public DbSet<ScholarResGradHireTracking> ScholarResGradHireTracking { get; set; }
        public DbSet<CPPaymentRequest> CPPaymentRequest { get; set; }
        public DbSet<CPPaymentRequestTracking> CPPaymentRequestTracking { get; set; }
       

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

            modelBuilder.Entity<SupOrgs2>()
           .HasNoKey();

            modelBuilder.Entity<Termination>()
              .Property(h => h.Offboarding)
              .HasDefaultValue(0);
            modelBuilder.Entity<Termination>()
                .Property(h => h.ClosePosition)
                .HasDefaultValue(0);
            modelBuilder.Entity<Termination>()
                .Property(h => h.LeaveWA)
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

            modelBuilder.Entity<SecurityRolesChange>()
                .Property(i => i.TrainingCompleted)
                .HasDefaultValue(0);
            modelBuilder.Entity<SecurityRolesChange>()
                .Property(i => i.IncludeSubordinates)
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
            modelBuilder.Entity<CompBasePayChangeTracking>()
               .HasOne(p => p.CaseAudit)
               .WithMany(q => q.CompBasePayChangeTrackings)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CompAllowanceChangeTracking>()
              .HasOne(p => p.CaseAudit)
              .WithMany(q => q.CompAllowanceChangeTrackings)
              .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CostAllocationChangeTracking>()
               .HasOne(p => p.CaseAudit)
               .WithMany(q => q.CostAllocationChangeTrackings)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<EndDateChangeTracking>()
              .HasOne(p => p.CaseAudit)
              .WithMany(q => q.EndDateChangeTrackings)
              .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<FTEChangeTracking>()
              .HasOne(p => p.CaseAudit)
              .WithMany(q => q.FTEChangeTrackings)
              .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MoveWorkerTracking>()
              .HasOne(p => p.CaseAudit)
              .WithMany(q => q.MoveWorkerTrackings)
              .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SecurityRolesChangeTracking>()
              .HasOne(p => p.CaseAudit)
              .WithMany(q => q.SecurityRolesChangeTrackings)
              .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TerminationTracking>()
              .HasOne(p => p.CaseAudit)
              .WithMany(q => q.TerminationTrackings)
              .OnDelete(DeleteBehavior.NoAction);
           
            modelBuilder.Entity<ScholarResGradHireTracking>()
            .HasOne(p => p.CaseAudit)
            .WithMany(q => q.ScholarResGradHireTrackings)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CPPaymentRequestTracking>()
           .HasOne(p => p.CaseAudit)
           .WithMany(q => q.CPPaymentRequestTrackings)
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