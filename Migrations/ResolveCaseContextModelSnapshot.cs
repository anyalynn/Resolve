﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Resolve.Data;

namespace Resolve.Migrations
{
    [DbContext(typeof(ResolveCaseContext))]
    partial class ResolveCaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-preview.3.20181.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Resolve.Models.Approver", b =>
                {
                    b.Property<int>("CaseID")
                        .HasColumnType("int");

                    b.Property<string>("LocalUserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Approved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("Order")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("CaseID", "LocalUserID");

                    b.HasIndex("LocalUserID");

                    b.ToTable("Approver");
                });

            modelBuilder.Entity("Resolve.Models.Case", b =>
                {
                    b.Property<int>("CaseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CaseCID")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasComputedColumnSql("'CASE' + CONVERT([nvarchar](23),[CaseID]+10000000)");

                    b.Property<DateTime>("CaseCreationTimestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("CaseStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CaseTypeID")
                        .HasColumnType("int");

                    b.Property<string>("LocalUserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("OnBehalfOf")
                        .HasColumnType("bit");

                    b.Property<int?>("Processed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("CaseID");

                    b.HasIndex("CaseTypeID");

                    b.HasIndex("LocalUserID");

                    b.ToTable("Case");
                });

            modelBuilder.Entity("Resolve.Models.CaseAttachment", b =>
                {
                    b.Property<int>("CaseAttachmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AttachmentTimestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("CaseID")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocalUserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CaseAttachmentID");

                    b.HasIndex("CaseID");

                    b.HasIndex("LocalUserID");

                    b.ToTable("CaseAttachment");
                });

            modelBuilder.Entity("Resolve.Models.CaseAudit", b =>
                {
                    b.Property<int>("CaseAuditID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuditLog")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AuditTimestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("CaseID")
                        .HasColumnType("int");

                    b.Property<string>("LocalUserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CaseAuditID");

                    b.HasIndex("CaseID");

                    b.HasIndex("LocalUserID");

                    b.ToTable("CaseAudit");
                });

            modelBuilder.Entity("Resolve.Models.CaseComment", b =>
                {
                    b.Property<int>("CaseCommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaseID")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CommentTimestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("LocalUserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CaseCommentID");

                    b.HasIndex("CaseID");

                    b.HasIndex("LocalUserID");

                    b.ToTable("CaseComment");
                });

            modelBuilder.Entity("Resolve.Models.CaseType", b =>
                {
                    b.Property<int>("CaseTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CaseTypeTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("GroupNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("LocalGroupID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LongDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CaseTypeID");

                    b.HasAlternateKey("CaseTypeTitle");

                    b.HasIndex("LocalGroupID");

                    b.ToTable("CaseType");
                });

            modelBuilder.Entity("Resolve.Models.CaseTypeGroup", b =>
                {
                    b.Property<int>("CaseTypeID")
                        .HasColumnType("int");

                    b.Property<string>("LocalGroupID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Order")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("CaseTypeID", "LocalGroupID");

                    b.HasIndex("LocalGroupID");

                    b.ToTable("CaseTypeGroup");
                });

            modelBuilder.Entity("Resolve.Models.GroupAssignment", b =>
                {
                    b.Property<int>("CaseID")
                        .HasColumnType("int");

                    b.Property<string>("LocalGroupID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CaseID", "LocalGroupID");

                    b.HasIndex("LocalGroupID");

                    b.ToTable("GroupAssignment");
                });

            modelBuilder.Entity("Resolve.Models.LocalGroup", b =>
                {
                    b.Property<string>("LocalGroupID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GroupDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LocalUserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LocalGroupID");

                    b.HasAlternateKey("GroupName");

                    b.HasIndex("LocalUserID");

                    b.ToTable("LocalGroup");
                });

            modelBuilder.Entity("Resolve.Models.LocalUser", b =>
                {
                    b.Property<string>("LocalUserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmailID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocalUserID");

                    b.ToTable("LocalUser");
                });

            modelBuilder.Entity("Resolve.Models.OnBehalf", b =>
                {
                    b.Property<int>("CaseID")
                        .HasColumnType("int");

                    b.Property<string>("LocalUserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CaseID", "LocalUserID");

                    b.HasIndex("LocalUserID");

                    b.ToTable("OnBehalf");
                });

            modelBuilder.Entity("Resolve.Models.SAR4", b =>
                {
                    b.Property<int>("CaseID")
                        .HasColumnType("int");

                    b.Property<string>("AbsenceDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AbsenceReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AbsenceRequested")
                        .HasColumnType("int");

                    b.Property<int>("GradYear")
                        .HasColumnType("int");

                    b.Property<string>("MakeupPlan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quarter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RequestEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RequestStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Student")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CaseID");

                    b.ToTable("SAR4");
                });

            modelBuilder.Entity("Resolve.Models.Sample2", b =>
                {
                    b.Property<int>("CaseID")
                        .HasColumnType("int");

                    b.Property<string>("SampleDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CaseID");

                    b.ToTable("Sample2");
                });

            modelBuilder.Entity("Resolve.Models.SampleCaseType", b =>
                {
                    b.Property<int>("CaseID")
                        .HasColumnType("int");

                    b.Property<string>("CaseDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CaseID");

                    b.ToTable("SampleCaseType");
                });

            modelBuilder.Entity("Resolve.Models.UserGroup", b =>
                {
                    b.Property<string>("LocalUserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LocalGroupID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LocalUserID", "LocalGroupID");

                    b.HasIndex("LocalGroupID");

                    b.ToTable("UserGroup");
                });

            modelBuilder.Entity("Resolve.Models.Approver", b =>
                {
                    b.HasOne("Resolve.Models.Case", "Case")
                        .WithMany("Approvers")
                        .HasForeignKey("CaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Resolve.Models.LocalUser", "LocalUser")
                        .WithMany("CasesforApproval")
                        .HasForeignKey("LocalUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Resolve.Models.Case", b =>
                {
                    b.HasOne("Resolve.Models.CaseType", "CaseType")
                        .WithMany("Cases")
                        .HasForeignKey("CaseTypeID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Resolve.Models.LocalUser", "LocalUser")
                        .WithMany("Cases")
                        .HasForeignKey("LocalUserID")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Resolve.Models.CaseAttachment", b =>
                {
                    b.HasOne("Resolve.Models.Case", "Case")
                        .WithMany("CaseAttachments")
                        .HasForeignKey("CaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Resolve.Models.LocalUser", "LocalUser")
                        .WithMany()
                        .HasForeignKey("LocalUserID");
                });

            modelBuilder.Entity("Resolve.Models.CaseAudit", b =>
                {
                    b.HasOne("Resolve.Models.Case", "Case")
                        .WithMany("CaseAudits")
                        .HasForeignKey("CaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Resolve.Models.LocalUser", "LocalUser")
                        .WithMany()
                        .HasForeignKey("LocalUserID");
                });

            modelBuilder.Entity("Resolve.Models.CaseComment", b =>
                {
                    b.HasOne("Resolve.Models.Case", "Case")
                        .WithMany("CaseComments")
                        .HasForeignKey("CaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Resolve.Models.LocalUser", "LocalUser")
                        .WithMany()
                        .HasForeignKey("LocalUserID");
                });

            modelBuilder.Entity("Resolve.Models.CaseType", b =>
                {
                    b.HasOne("Resolve.Models.LocalGroup", "LocalGroup")
                        .WithMany()
                        .HasForeignKey("LocalGroupID");
                });

            modelBuilder.Entity("Resolve.Models.CaseTypeGroup", b =>
                {
                    b.HasOne("Resolve.Models.CaseType", "CaseType")
                        .WithMany("CaseTypeGroups")
                        .HasForeignKey("CaseTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Resolve.Models.LocalGroup", "LocalGroup")
                        .WithMany("CaseTypeGroups")
                        .HasForeignKey("LocalGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Resolve.Models.GroupAssignment", b =>
                {
                    b.HasOne("Resolve.Models.Case", "Case")
                        .WithMany("GroupAssignments")
                        .HasForeignKey("CaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Resolve.Models.LocalGroup", "LocalGroup")
                        .WithMany("GroupCases")
                        .HasForeignKey("LocalGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Resolve.Models.LocalGroup", b =>
                {
                    b.HasOne("Resolve.Models.LocalUser", "LocalUser")
                        .WithMany()
                        .HasForeignKey("LocalUserID");
                });

            modelBuilder.Entity("Resolve.Models.OnBehalf", b =>
                {
                    b.HasOne("Resolve.Models.Case", "Case")
                        .WithMany("OnBehalves")
                        .HasForeignKey("CaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Resolve.Models.LocalUser", "LocalUser")
                        .WithMany()
                        .HasForeignKey("LocalUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Resolve.Models.SAR4", b =>
                {
                    b.HasOne("Resolve.Models.Case", "Case")
                        .WithMany("SAR4")
                        .HasForeignKey("CaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Resolve.Models.Sample2", b =>
                {
                    b.HasOne("Resolve.Models.Case", "Case")
                        .WithMany("Sample2")
                        .HasForeignKey("CaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Resolve.Models.SampleCaseType", b =>
                {
                    b.HasOne("Resolve.Models.Case", "Case")
                        .WithMany("SampleCaseType")
                        .HasForeignKey("CaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Resolve.Models.UserGroup", b =>
                {
                    b.HasOne("Resolve.Models.LocalGroup", "LocalGroup")
                        .WithMany()
                        .HasForeignKey("LocalGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Resolve.Models.LocalUser", "LocalUser")
                        .WithMany("UserGroups")
                        .HasForeignKey("LocalUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
