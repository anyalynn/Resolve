using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Resolve.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaseType",
                columns: table => new
                {
                    CaseTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseTypeTitle = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupNumber = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    Hierarchical_Approval = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseType", x => x.CaseTypeID);
                    table.UniqueConstraint("AK_CaseType_CaseTypeTitle", x => x.CaseTypeTitle);
                });

            migrationBuilder.CreateTable(
                name: "LocalUser",
                columns: table => new
                {
                    LocalUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUser", x => x.LocalUserID);
                    table.UniqueConstraint("AK_LocalUser_EmailID", x => x.EmailID);
                });

            migrationBuilder.CreateTable(
                name: "Case",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseCID = table.Column<string>(type: "nvarchar(max)", nullable: true, computedColumnSql: "'CASE' + CONVERT([nvarchar](23),[CaseID]+10000000)"),
                    LocalUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OnBehalfOf = table.Column<bool>(type: "bit", nullable: false),
                    CaseStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseCreationTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CaseTypeID = table.Column<int>(type: "int", nullable: false),
                    Processed = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Case", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_Case_CaseType_CaseTypeID",
                        column: x => x.CaseTypeID,
                        principalTable: "CaseType",
                        principalColumn: "CaseTypeID");
                    table.ForeignKey(
                        name: "FK_Case_LocalUser_LocalUserID",
                        column: x => x.LocalUserID,
                        principalTable: "LocalUser",
                        principalColumn: "LocalUserID");
                });

            migrationBuilder.CreateTable(
                name: "EmailPreference",
                columns: table => new
                {
                    LocalUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CaseCreation = table.Column<bool>(type: "bit", nullable: false),
                    GroupAssignment = table.Column<bool>(type: "bit", nullable: false),
                    CaseAssignment = table.Column<bool>(type: "bit", nullable: false),
                    CommentCreation = table.Column<bool>(type: "bit", nullable: false),
                    AttachmentCreation = table.Column<bool>(type: "bit", nullable: false),
                    CaseProcessed = table.Column<bool>(type: "bit", nullable: false),
                    CasesCreatedByUser = table.Column<bool>(type: "bit", nullable: false),
                    CasesAssignedToUser = table.Column<bool>(type: "bit", nullable: false),
                    CasesAssignedToUsersGroups = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailPreference", x => x.LocalUserID);
                    table.ForeignKey(
                        name: "FK_EmailPreference_LocalUser_LocalUserID",
                        column: x => x.LocalUserID,
                        principalTable: "LocalUser",
                        principalColumn: "LocalUserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalGroup",
                columns: table => new
                {
                    LocalGroupID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalUserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalGroup", x => x.LocalGroupID);
                    table.UniqueConstraint("AK_LocalGroup_GroupName", x => x.GroupName);
                    table.ForeignKey(
                        name: "FK_LocalGroup_LocalUser_LocalUserID",
                        column: x => x.LocalUserID,
                        principalTable: "LocalUser",
                        principalColumn: "LocalUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AxiumFeeSchedule",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    AxiumSchedRequestType = table.Column<int>(type: "int", nullable: true),
                    AxiumScheduleType = table.Column<int>(type: "int", nullable: true),
                    AxiumCodeType = table.Column<int>(type: "int", nullable: true),
                    Discipline = table.Column<int>(type: "int", nullable: true),
                    Site = table.Column<int>(type: "int", nullable: true),
                    ProcedureCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProdCodeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitsFactored = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AxiumFeeSchedule", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_AxiumFeeSchedule_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseAttachment",
                columns: table => new
                {
                    CaseAttachmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    LocalUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseAttachment", x => x.CaseAttachmentID);
                    table.ForeignKey(
                        name: "FK_CaseAttachment_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseAttachment_LocalUser_LocalUserID",
                        column: x => x.LocalUserID,
                        principalTable: "LocalUser",
                        principalColumn: "LocalUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CaseAudit",
                columns: table => new
                {
                    CaseAuditID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    AuditLog = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    LocalUserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseAudit", x => x.CaseAuditID);
                    table.ForeignKey(
                        name: "FK_CaseAudit_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseAudit_LocalUser_LocalUserID",
                        column: x => x.LocalUserID,
                        principalTable: "LocalUser",
                        principalColumn: "LocalUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CaseComment",
                columns: table => new
                {
                    CaseCommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    LocalUserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseComment", x => x.CaseCommentID);
                    table.ForeignKey(
                        name: "FK_CaseComment_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseComment_LocalUser_LocalUserID",
                        column: x => x.LocalUserID,
                        principalTable: "LocalUser",
                        principalColumn: "LocalUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompAllowanceChange",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EWorkerType = table.Column<int>(type: "int", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    AllowanceChange = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ScholarCompAllowanceChange = table.Column<int>(type: "int", nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompAllowanceChange", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_CompAllowanceChange_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompBasePayChange",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CWorkerType = table.Column<int>(type: "int", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    BasePayChange = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompBasePayChange", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_CompBasePayChange_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostAllocationChange",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AWorkerType = table.Column<int>(type: "int", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostAllocationChange", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_CostAllocationChange_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CPPaymentRequest",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequesterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Payee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPPaymentRequest", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_CPPaymentRequest_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EndDateChange",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EffectiveEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AWorkerType = table.Column<int>(type: "int", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndDateChange", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_EndDateChange_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodEvent",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TravelFoodDepartment = table.Column<int>(type: "int", nullable: false),
                    BudgetPurpose = table.Column<int>(type: "int", nullable: false),
                    FoodApprovalForm = table.Column<int>(type: "int", nullable: true),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberAttending = table.Column<int>(type: "int", nullable: false),
                    BudgetDeficit = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemCost1 = table.Column<float>(type: "real", nullable: false),
                    ItemCost2 = table.Column<float>(type: "real", nullable: true),
                    ItemCost3 = table.Column<float>(type: "real", nullable: true),
                    ItemCost4 = table.Column<float>(type: "real", nullable: true),
                    ItemCost5 = table.Column<float>(type: "real", nullable: true),
                    ItemCost6 = table.Column<float>(type: "real", nullable: true),
                    ItemCost7 = table.Column<float>(type: "real", nullable: true),
                    Total = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodEvent", x => x.CaseID);
                    table.CheckConstraint("CK_FoodEvent_BudgetPurpose_Enum_Constraint", "[BudgetPurpose] IN(0, 1, 2, 3, 4, 5, 6, 7)");
                    table.CheckConstraint("CK_FoodEvent_TravelFoodDepartment_Enum_Constraint", "[TravelFoodDepartment] IN(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18)");
                    table.ForeignKey(
                        name: "FK_FoodEvent_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FTEChange",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AWorkerType = table.Column<int>(type: "int", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentFTE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProposedFTE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FTEChange", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_FTEChange_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HiringAffiliateFaculty",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacAffiliateTitle = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringAffiliateFaculty", x => x.CaseID);
                    table.CheckConstraint("CK_HiringAffiliateFaculty_Department_Enum_Constraint", "[Department] IN(0, 1, 2, 3, 4, 5, 6, 7, 8)");
                    table.CheckConstraint("CK_HiringAffiliateFaculty_FacAffiliateTitle_Enum_Constraint", "[FacAffiliateTitle] IN(0, 1, 2, 3, 4)");
                    table.ForeignKey(
                        name: "FK_HiringAffiliateFaculty_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HiringFaculty",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeReplaced = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CandidateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FTE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Consequences = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barriers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacTitle = table.Column<int>(type: "int", nullable: false),
                    FacHireReason = table.Column<int>(type: "int", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringFaculty", x => x.CaseID);
                    table.CheckConstraint("CK_HiringFaculty_BudgetType_Enum_Constraint", "[BudgetType] IN(0, 1, 2, 3, 4, 5)");
                    table.CheckConstraint("CK_HiringFaculty_Department_Enum_Constraint", "[Department] IN(0, 1, 2, 3, 4, 5, 6, 7, 8)");
                    table.CheckConstraint("CK_HiringFaculty_FacHireReason_Enum_Constraint", "[FacHireReason] IN(0, 1, 2)");
                    table.CheckConstraint("CK_HiringFaculty_FacTitle_Enum_Constraint", "[FacTitle] IN(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38)");
                    table.ForeignKey(
                        name: "FK_HiringFaculty_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HiringStaff",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualHireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployeeReplaced = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CandidateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    PayRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HireeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PosNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkdayReq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UWHiresReq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FTE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobPostingTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CampusBox = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OvertimeEligible = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Super = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MultipleBudgetsExplain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consequences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barriers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupOrgManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UWHiresContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetType = table.Column<int>(type: "int", nullable: true),
                    BudgetPurpose = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Citizenship = table.Column<int>(type: "int", nullable: true),
                    Workstudy = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CandidateSelected = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LimitedRecruitment = table.Column<int>(type: "int", nullable: true),
                    RecruitmentRun = table.Column<int>(type: "int", nullable: true),
                    WeeklyHours = table.Column<int>(type: "int", nullable: true),
                    Supervised = table.Column<int>(type: "int", nullable: true),
                    StaffHireReason = table.Column<int>(type: "int", nullable: true),
                    StaffPositionType = table.Column<int>(type: "int", nullable: true),
                    StaffWorkerType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringStaff", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_HiringStaff_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoveWorker",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CWorkerType = table.Column<int>(type: "int", nullable: true),
                    OSupOrg = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveWorker", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_MoveWorker_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnBehalf",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    LocalUserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnBehalf", x => new { x.CaseID, x.LocalUserID });
                    table.ForeignKey(
                        name: "FK_OnBehalf_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnBehalf_LocalUser_LocalUserID",
                        column: x => x.LocalUserID,
                        principalTable: "LocalUser",
                        principalColumn: "LocalUserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientEvent",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventDepartment = table.Column<int>(type: "int", nullable: true),
                    EventDescription = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    EventTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactsDocumented = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DentalRecordNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Witness1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Witness2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotifiedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupervisorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstReportedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Causes = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    ReporterActionTaken = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    ManagerActionTaken = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    EventLocation = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientEvent", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_PatientEvent_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerioLimitedCare",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complaint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TreatmentPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TChart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestorativeExam = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PerioExam = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    bwxrays = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    paxrays = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Prophy = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Other = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OtherProcedure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerioLimitedCare", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_PerioLimitedCare_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SampleCaseType",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    CaseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleCaseType", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_SampleCaseType_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScholarResGradHire",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DWorkerType = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StipendAllowance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScholarJobProfile = table.Column<int>(type: "int", nullable: true),
                    GradJobProfile = table.Column<int>(type: "int", nullable: true),
                    ScholarReqType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarResGradHire", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_ScholarResGradHire_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityRolesChange",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AWorkerType = table.Column<int>(type: "int", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupervisedAccess = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    UWNetID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IncludeSubordinates = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SecurityRequestType = table.Column<int>(type: "int", nullable: false),
                    AcademicChair = table.Column<int>(type: "int", nullable: false),
                    AcademicDean = table.Column<int>(type: "int", nullable: false),
                    HCMInit1 = table.Column<int>(type: "int", nullable: false),
                    HCMInit2 = table.Column<int>(type: "int", nullable: false),
                    I9 = table.Column<int>(type: "int", nullable: false),
                    Manager = table.Column<int>(type: "int", nullable: false),
                    UWHiresManager = table.Column<int>(type: "int", nullable: false),
                    VOStaff = table.Column<int>(type: "int", nullable: false),
                    VOStaffCompCost = table.Column<int>(type: "int", nullable: false),
                    VOStaffCompPersonal = table.Column<int>(type: "int", nullable: false),
                    TimeAbsenceApprover = table.Column<int>(type: "int", nullable: false),
                    TimeAbsenceInitiate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityRolesChange", x => x.CaseID);
                    table.CheckConstraint("CK_SecurityRolesChange_AcademicChair_Enum_Constraint", "[AcademicChair] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChange_AcademicDean_Enum_Constraint", "[AcademicDean] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChange_HCMInit1_Enum_Constraint", "[HCMInit1] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChange_HCMInit2_Enum_Constraint", "[HCMInit2] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChange_I9_Enum_Constraint", "[I9] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChange_Manager_Enum_Constraint", "[Manager] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChange_SecurityRequestType_Enum_Constraint", "[SecurityRequestType] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChange_TimeAbsenceApprover_Enum_Constraint", "[TimeAbsenceApprover] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChange_TimeAbsenceInitiate_Enum_Constraint", "[TimeAbsenceInitiate] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChange_UWHiresManager_Enum_Constraint", "[UWHiresManager] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChange_VOStaff_Enum_Constraint", "[VOStaff] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChange_VOStaffCompCost_Enum_Constraint", "[VOStaffCompCost] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChange_VOStaffCompPersonal_Enum_Constraint", "[VOStaffCompPersonal] IN(0, 1, 2)");
                    table.ForeignKey(
                        name: "FK_SecurityRolesChange_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Termination",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AWorkerType = table.Column<int>(type: "int", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    TerminationReason = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Offboarding = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ClosePosition = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LeaveWA = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Department = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termination", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_Termination_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Travel",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    TravelStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TravelEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TravelFoodDepartment = table.Column<int>(type: "int", nullable: false),
                    BudgetPurpose = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumNights = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirfareCost = table.Column<float>(type: "real", nullable: true),
                    RegistrationCost = table.Column<float>(type: "real", nullable: true),
                    TransportationCost = table.Column<float>(type: "real", nullable: true),
                    MealsCost = table.Column<float>(type: "real", nullable: true),
                    HotelsCost = table.Column<float>(type: "real", nullable: true),
                    OtherCost1 = table.Column<float>(type: "real", nullable: true),
                    OtherCost2 = table.Column<float>(type: "real", nullable: true),
                    Total = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travel", x => x.CaseID);
                    table.CheckConstraint("CK_Travel_BudgetPurpose_Enum_Constraint", "[BudgetPurpose] IN(0, 1, 2, 3, 4, 5, 6, 7)");
                    table.CheckConstraint("CK_Travel_TravelFoodDepartment_Enum_Constraint", "[TravelFoodDepartment] IN(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18)");
                    table.ForeignKey(
                        name: "FK_Travel_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Approver",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    LocalUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocalGroupID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Approved = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Order = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approver", x => new { x.CaseID, x.LocalUserID });
                    table.ForeignKey(
                        name: "FK_Approver_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approver_LocalGroup_LocalGroupID",
                        column: x => x.LocalGroupID,
                        principalTable: "LocalGroup",
                        principalColumn: "LocalGroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Approver_LocalUser_LocalUserID",
                        column: x => x.LocalUserID,
                        principalTable: "LocalUser",
                        principalColumn: "LocalUserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseTypeGroup",
                columns: table => new
                {
                    CaseTypeID = table.Column<int>(type: "int", nullable: false),
                    LocalGroupID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseTypeGroup", x => new { x.CaseTypeID, x.LocalGroupID });
                    table.ForeignKey(
                        name: "FK_CaseTypeGroup_CaseType_CaseTypeID",
                        column: x => x.CaseTypeID,
                        principalTable: "CaseType",
                        principalColumn: "CaseTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseTypeGroup_LocalGroup_LocalGroupID",
                        column: x => x.LocalGroupID,
                        principalTable: "LocalGroup",
                        principalColumn: "LocalGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupAssignment",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    LocalGroupID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupAssignment", x => new { x.CaseID, x.LocalGroupID });
                    table.ForeignKey(
                        name: "FK_GroupAssignment_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupAssignment_LocalGroup_LocalGroupID",
                        column: x => x.LocalGroupID,
                        principalTable: "LocalGroup",
                        principalColumn: "LocalGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    LocalUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocalGroupID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => new { x.LocalUserID, x.LocalGroupID });
                    table.ForeignKey(
                        name: "FK_UserGroup_LocalGroup_LocalGroupID",
                        column: x => x.LocalGroupID,
                        principalTable: "LocalGroup",
                        principalColumn: "LocalGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroup_LocalUser_LocalUserID",
                        column: x => x.LocalUserID,
                        principalTable: "LocalUser",
                        principalColumn: "LocalUserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AxiumFeeScheduleTracking",
                columns: table => new
                {
                    AxiumFeeScheduleTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    AxiumSchedRequestType = table.Column<int>(type: "int", nullable: true),
                    AxiumScheduleType = table.Column<int>(type: "int", nullable: true),
                    AxiumCodeType = table.Column<int>(type: "int", nullable: true),
                    Discipline = table.Column<int>(type: "int", nullable: true),
                    Site = table.Column<int>(type: "int", nullable: true),
                    ProcedureCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProdCodeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitsFactored = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AxiumFeeScheduleTracking", x => x.AxiumFeeScheduleTrackingID);
                    table.ForeignKey(
                        name: "FK_AxiumFeeScheduleTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AxiumFeeScheduleTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "CompAllowanceChangeTracking",
                columns: table => new
                {
                    CompAllowanceChangeTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EWorkerType = table.Column<int>(type: "int", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    AllowanceChange = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ScholarCompAllowanceChange = table.Column<int>(type: "int", nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompAllowanceChangeTracking", x => x.CompAllowanceChangeTrackingID);
                    table.ForeignKey(
                        name: "FK_CompAllowanceChangeTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompAllowanceChangeTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "CompBasePayChangeTracking",
                columns: table => new
                {
                    CompBasePayChangeTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CWorkerType = table.Column<int>(type: "int", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    BasePayChange = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompBasePayChangeTracking", x => x.CompBasePayChangeTrackingID);
                    table.ForeignKey(
                        name: "FK_CompBasePayChangeTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompBasePayChangeTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "CostAllocationChangeTracking",
                columns: table => new
                {
                    CostAllocationChangeTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AWorkerType = table.Column<int>(type: "int", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostAllocationChangeTracking", x => x.CostAllocationChangeTrackingID);
                    table.ForeignKey(
                        name: "FK_CostAllocationChangeTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostAllocationChangeTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "CPPaymentRequestTracking",
                columns: table => new
                {
                    CPPaymentRequestTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequesterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Payee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPPaymentRequestTracking", x => x.CPPaymentRequestTrackingID);
                    table.ForeignKey(
                        name: "FK_CPPaymentRequestTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CPPaymentRequestTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "EndDateChangeTracking",
                columns: table => new
                {
                    EndDateChangeTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EffectiveEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AWorkerType = table.Column<int>(type: "int", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndDateChangeTracking", x => x.EndDateChangeTrackingID);
                    table.ForeignKey(
                        name: "FK_EndDateChangeTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EndDateChangeTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "FoodEventTracking",
                columns: table => new
                {
                    FoodEventTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TravelFoodDepartment = table.Column<int>(type: "int", nullable: false),
                    BudgetPurpose = table.Column<int>(type: "int", nullable: false),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodApprovalForm = table.Column<int>(type: "int", nullable: true),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberAttending = table.Column<int>(type: "int", nullable: true),
                    BudgetDeficit = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemCost1 = table.Column<float>(type: "real", nullable: true),
                    ItemCost2 = table.Column<float>(type: "real", nullable: true),
                    ItemCost3 = table.Column<float>(type: "real", nullable: true),
                    ItemCost4 = table.Column<float>(type: "real", nullable: true),
                    ItemCost5 = table.Column<float>(type: "real", nullable: true),
                    ItemCost6 = table.Column<float>(type: "real", nullable: true),
                    ItemCost7 = table.Column<float>(type: "real", nullable: true),
                    Total = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodEventTracking", x => x.FoodEventTrackingID);
                    table.CheckConstraint("CK_FoodEventTracking_BudgetPurpose_Enum_Constraint", "[BudgetPurpose] IN(0, 1, 2, 3, 4, 5, 6, 7)");
                    table.CheckConstraint("CK_FoodEventTracking_TravelFoodDepartment_Enum_Constraint", "[TravelFoodDepartment] IN(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18)");
                    table.ForeignKey(
                        name: "FK_FoodEventTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodEventTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "FTEChangeTracking",
                columns: table => new
                {
                    FTEChangeTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AWorkerType = table.Column<int>(type: "int", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentFTE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProposedFTE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FTEChangeTracking", x => x.FTEChangeTrackingID);
                    table.ForeignKey(
                        name: "FK_FTEChangeTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FTEChangeTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "HiringAffiliateFacultyTracking",
                columns: table => new
                {
                    HiringAffiliateFacultyTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacAffiliateTitle = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringAffiliateFacultyTracking", x => x.HiringAffiliateFacultyTrackingID);
                    table.ForeignKey(
                        name: "FK_HiringAffiliateFacultyTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HiringAffiliateFacultyTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "HiringFacultyTracking",
                columns: table => new
                {
                    HiringFacultyTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeReplaced = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CandidateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FTE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacTitle = table.Column<int>(type: "int", nullable: false),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consequences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barriers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacHireReason = table.Column<int>(type: "int", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringFacultyTracking", x => x.HiringFacultyTrackingID);
                    table.CheckConstraint("CK_HiringFacultyTracking_BudgetType_Enum_Constraint", "[BudgetType] IN(0, 1, 2, 3, 4, 5)");
                    table.CheckConstraint("CK_HiringFacultyTracking_Department_Enum_Constraint", "[Department] IN(0, 1, 2, 3, 4, 5, 6, 7, 8)");
                    table.CheckConstraint("CK_HiringFacultyTracking_FacHireReason_Enum_Constraint", "[FacHireReason] IN(0, 1, 2)");
                    table.CheckConstraint("CK_HiringFacultyTracking_FacTitle_Enum_Constraint", "[FacTitle] IN(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38)");
                    table.ForeignKey(
                        name: "FK_HiringFacultyTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HiringFacultyTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "HiringStaffTracking",
                columns: table => new
                {
                    HiringStaffTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualHireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployeeReplaced = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CandidateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    PayRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HireeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PosNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkdayReq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UWHiresReq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FTE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobPostingTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CampusBox = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OvertimeEligible = table.Column<bool>(type: "bit", nullable: false),
                    Super = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MultipleBudgetsExplain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consequences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barriers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupOrgManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UWHiresContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetType = table.Column<int>(type: "int", nullable: true),
                    BudgetPurpose = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Citizenship = table.Column<int>(type: "int", nullable: true),
                    Workstudy = table.Column<bool>(type: "bit", nullable: false),
                    CandidateSelected = table.Column<bool>(type: "bit", nullable: false),
                    LimitedRecruitment = table.Column<int>(type: "int", nullable: true),
                    RecruitmentRun = table.Column<int>(type: "int", nullable: true),
                    WeeklyHours = table.Column<int>(type: "int", nullable: true),
                    Supervised = table.Column<int>(type: "int", nullable: true),
                    StaffHireReason = table.Column<int>(type: "int", nullable: true),
                    StaffPositionType = table.Column<int>(type: "int", nullable: true),
                    StaffWorkerType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringStaffTracking", x => x.HiringStaffTrackingID);
                    table.ForeignKey(
                        name: "FK_HiringStaffTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HiringStaffTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "MoveWorkerTracking",
                columns: table => new
                {
                    MoveWorkerTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CWorkerType = table.Column<int>(type: "int", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    OSupOrg = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveWorkerTracking", x => x.MoveWorkerTrackingID);
                    table.ForeignKey(
                        name: "FK_MoveWorkerTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveWorkerTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "PatientEventTracking",
                columns: table => new
                {
                    PatientEventTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventDepartment = table.Column<int>(type: "int", nullable: true),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactsDocumented = table.Column<bool>(type: "bit", nullable: false),
                    DentalRecordNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Witness1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Witness2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotifiedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupervisorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstReportedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Causes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReporterActionTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerActionTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventLocation = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientEventTracking", x => x.PatientEventTrackingID);
                    table.ForeignKey(
                        name: "FK_PatientEventTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientEventTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "PerioLimitedCareTracking",
                columns: table => new
                {
                    PerioLimitedCareTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complaint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TreatmentPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TChart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestorativeExam = table.Column<bool>(type: "bit", nullable: false),
                    PerioExam = table.Column<bool>(type: "bit", nullable: false),
                    bwxrays = table.Column<bool>(type: "bit", nullable: false),
                    paxrays = table.Column<bool>(type: "bit", nullable: false),
                    Prophy = table.Column<bool>(type: "bit", nullable: false),
                    Other = table.Column<bool>(type: "bit", nullable: false),
                    OtherProcedure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerioLimitedCareTracking", x => x.PerioLimitedCareTrackingID);
                    table.ForeignKey(
                        name: "FK_PerioLimitedCareTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerioLimitedCareTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "SampleCaseTypeTracking",
                columns: table => new
                {
                    SampleCaseTypeTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    CaseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleCaseTypeTracking", x => x.SampleCaseTypeTrackingID);
                    table.ForeignKey(
                        name: "FK_SampleCaseTypeTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SampleCaseTypeTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "ScholarResGradHireTracking",
                columns: table => new
                {
                    ScholarResGradHireTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DWorkerType = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StipendAllowance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScholarJobProfile = table.Column<int>(type: "int", nullable: true),
                    GradJobProfile = table.Column<int>(type: "int", nullable: true),
                    ScholarReqType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarResGradHireTracking", x => x.ScholarResGradHireTrackingID);
                    table.ForeignKey(
                        name: "FK_ScholarResGradHireTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScholarResGradHireTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "SecurityRolesChangeTracking",
                columns: table => new
                {
                    SecurityRolesChangeTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AWorkerType = table.Column<int>(type: "int", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupervisedAccess = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    UWNetID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IncludeSubordinates = table.Column<bool>(type: "bit", nullable: false),
                    SecurityRequestType = table.Column<int>(type: "int", nullable: false),
                    AcademicChair = table.Column<int>(type: "int", nullable: false),
                    AcademicDean = table.Column<int>(type: "int", nullable: false),
                    HCMInit1 = table.Column<int>(type: "int", nullable: false),
                    HCMInit2 = table.Column<int>(type: "int", nullable: false),
                    I9 = table.Column<int>(type: "int", nullable: false),
                    Manager = table.Column<int>(type: "int", nullable: false),
                    UWHiresManager = table.Column<int>(type: "int", nullable: false),
                    VOStaff = table.Column<int>(type: "int", nullable: false),
                    VOStaffCompCost = table.Column<int>(type: "int", nullable: false),
                    VOStaffCompPersonal = table.Column<int>(type: "int", nullable: false),
                    TimeAbsenceApprover = table.Column<int>(type: "int", nullable: false),
                    TimeAbsenceInitiate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityRolesChangeTracking", x => x.SecurityRolesChangeTrackingID);
                    table.CheckConstraint("CK_SecurityRolesChangeTracking_AcademicChair_Enum_Constraint", "[AcademicChair] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChangeTracking_AcademicDean_Enum_Constraint", "[AcademicDean] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChangeTracking_HCMInit1_Enum_Constraint", "[HCMInit1] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChangeTracking_HCMInit2_Enum_Constraint", "[HCMInit2] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChangeTracking_I9_Enum_Constraint", "[I9] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChangeTracking_Manager_Enum_Constraint", "[Manager] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChangeTracking_SecurityRequestType_Enum_Constraint", "[SecurityRequestType] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChangeTracking_TimeAbsenceApprover_Enum_Constraint", "[TimeAbsenceApprover] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChangeTracking_TimeAbsenceInitiate_Enum_Constraint", "[TimeAbsenceInitiate] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChangeTracking_UWHiresManager_Enum_Constraint", "[UWHiresManager] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChangeTracking_VOStaff_Enum_Constraint", "[VOStaff] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChangeTracking_VOStaffCompCost_Enum_Constraint", "[VOStaffCompCost] IN(0, 1, 2)");
                    table.CheckConstraint("CK_SecurityRolesChangeTracking_VOStaffCompPersonal_Enum_Constraint", "[VOStaffCompPersonal] IN(0, 1, 2)");
                    table.ForeignKey(
                        name: "FK_SecurityRolesChangeTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecurityRolesChangeTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "TerminationTracking",
                columns: table => new
                {
                    TerminationTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AWorkerType = table.Column<int>(type: "int", nullable: true),
                    HireType = table.Column<int>(type: "int", nullable: true),
                    TerminationReason = table.Column<int>(type: "int", nullable: true),
                    SupOrg = table.Column<int>(type: "int", nullable: true),
                    EmployeeEID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DetailedDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Offboarding = table.Column<bool>(type: "bit", nullable: false),
                    ClosePosition = table.Column<bool>(type: "bit", nullable: false),
                    LeaveWA = table.Column<bool>(type: "bit", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminationTracking", x => x.TerminationTrackingID);
                    table.ForeignKey(
                        name: "FK_TerminationTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TerminationTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateTable(
                name: "TravelTracking",
                columns: table => new
                {
                    TravelTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseAuditID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    TravelStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TravelEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TravelFoodDepartment = table.Column<int>(type: "int", nullable: false),
                    BudgetPurpose = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumNights = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BudgetNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirfareCost = table.Column<float>(type: "real", nullable: true),
                    RegistrationCost = table.Column<float>(type: "real", nullable: true),
                    TransportationCost = table.Column<float>(type: "real", nullable: true),
                    MealsCost = table.Column<float>(type: "real", nullable: true),
                    HotelsCost = table.Column<float>(type: "real", nullable: true),
                    OtherCost1 = table.Column<float>(type: "real", nullable: true),
                    OtherCost2 = table.Column<float>(type: "real", nullable: true),
                    Total = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelTracking", x => x.TravelTrackingID);
                    table.CheckConstraint("CK_TravelTracking_BudgetPurpose_Enum_Constraint", "[BudgetPurpose] IN(0, 1, 2, 3, 4, 5, 6, 7)");
                    table.CheckConstraint("CK_TravelTracking_TravelFoodDepartment_Enum_Constraint", "[TravelFoodDepartment] IN(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18)");
                    table.ForeignKey(
                        name: "FK_TravelTracking_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelTracking_CaseAudit_CaseAuditID",
                        column: x => x.CaseAuditID,
                        principalTable: "CaseAudit",
                        principalColumn: "CaseAuditID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Approver_LocalGroupID",
                table: "Approver",
                column: "LocalGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Approver_LocalUserID",
                table: "Approver",
                column: "LocalUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AxiumFeeScheduleTracking_CaseAuditID",
                table: "AxiumFeeScheduleTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_AxiumFeeScheduleTracking_CaseID",
                table: "AxiumFeeScheduleTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Case_CaseTypeID",
                table: "Case",
                column: "CaseTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Case_LocalUserID",
                table: "Case",
                column: "LocalUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseAttachment_CaseID",
                table: "CaseAttachment",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseAttachment_LocalUserID",
                table: "CaseAttachment",
                column: "LocalUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseAudit_CaseID",
                table: "CaseAudit",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseAudit_LocalUserID",
                table: "CaseAudit",
                column: "LocalUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseComment_CaseID",
                table: "CaseComment",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseComment_LocalUserID",
                table: "CaseComment",
                column: "LocalUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseTypeGroup_LocalGroupID",
                table: "CaseTypeGroup",
                column: "LocalGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_CompAllowanceChangeTracking_CaseAuditID",
                table: "CompAllowanceChangeTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_CompAllowanceChangeTracking_CaseID",
                table: "CompAllowanceChangeTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_CompBasePayChangeTracking_CaseAuditID",
                table: "CompBasePayChangeTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_CompBasePayChangeTracking_CaseID",
                table: "CompBasePayChangeTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_CostAllocationChangeTracking_CaseAuditID",
                table: "CostAllocationChangeTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_CostAllocationChangeTracking_CaseID",
                table: "CostAllocationChangeTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_CPPaymentRequestTracking_CaseAuditID",
                table: "CPPaymentRequestTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_CPPaymentRequestTracking_CaseID",
                table: "CPPaymentRequestTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_EndDateChangeTracking_CaseAuditID",
                table: "EndDateChangeTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_EndDateChangeTracking_CaseID",
                table: "EndDateChangeTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodEventTracking_CaseAuditID",
                table: "FoodEventTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodEventTracking_CaseID",
                table: "FoodEventTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_FTEChangeTracking_CaseAuditID",
                table: "FTEChangeTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_FTEChangeTracking_CaseID",
                table: "FTEChangeTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupAssignment_LocalGroupID",
                table: "GroupAssignment",
                column: "LocalGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_HiringAffiliateFacultyTracking_CaseAuditID",
                table: "HiringAffiliateFacultyTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_HiringAffiliateFacultyTracking_CaseID",
                table: "HiringAffiliateFacultyTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_HiringFacultyTracking_CaseAuditID",
                table: "HiringFacultyTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_HiringFacultyTracking_CaseID",
                table: "HiringFacultyTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_HiringStaffTracking_CaseAuditID",
                table: "HiringStaffTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_HiringStaffTracking_CaseID",
                table: "HiringStaffTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_LocalGroup_LocalUserID",
                table: "LocalGroup",
                column: "LocalUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MoveWorkerTracking_CaseAuditID",
                table: "MoveWorkerTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_MoveWorkerTracking_CaseID",
                table: "MoveWorkerTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_OnBehalf_LocalUserID",
                table: "OnBehalf",
                column: "LocalUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientEventTracking_CaseAuditID",
                table: "PatientEventTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientEventTracking_CaseID",
                table: "PatientEventTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_PerioLimitedCareTracking_CaseAuditID",
                table: "PerioLimitedCareTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_PerioLimitedCareTracking_CaseID",
                table: "PerioLimitedCareTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_SampleCaseTypeTracking_CaseAuditID",
                table: "SampleCaseTypeTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_SampleCaseTypeTracking_CaseID",
                table: "SampleCaseTypeTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_ScholarResGradHireTracking_CaseAuditID",
                table: "ScholarResGradHireTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_ScholarResGradHireTracking_CaseID",
                table: "ScholarResGradHireTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityRolesChangeTracking_CaseAuditID",
                table: "SecurityRolesChangeTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityRolesChangeTracking_CaseID",
                table: "SecurityRolesChangeTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_TerminationTracking_CaseAuditID",
                table: "TerminationTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_TerminationTracking_CaseID",
                table: "TerminationTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelTracking_CaseAuditID",
                table: "TravelTracking",
                column: "CaseAuditID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelTracking_CaseID",
                table: "TravelTracking",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_LocalGroupID",
                table: "UserGroup",
                column: "LocalGroupID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Approver");

            migrationBuilder.DropTable(
                name: "AxiumFeeSchedule");

            migrationBuilder.DropTable(
                name: "AxiumFeeScheduleTracking");

            migrationBuilder.DropTable(
                name: "CaseAttachment");

            migrationBuilder.DropTable(
                name: "CaseComment");

            migrationBuilder.DropTable(
                name: "CaseTypeGroup");

            migrationBuilder.DropTable(
                name: "CompAllowanceChange");

            migrationBuilder.DropTable(
                name: "CompAllowanceChangeTracking");

            migrationBuilder.DropTable(
                name: "CompBasePayChange");

            migrationBuilder.DropTable(
                name: "CompBasePayChangeTracking");

            migrationBuilder.DropTable(
                name: "CostAllocationChange");

            migrationBuilder.DropTable(
                name: "CostAllocationChangeTracking");

            migrationBuilder.DropTable(
                name: "CPPaymentRequest");

            migrationBuilder.DropTable(
                name: "CPPaymentRequestTracking");

            migrationBuilder.DropTable(
                name: "EmailPreference");

            migrationBuilder.DropTable(
                name: "EndDateChange");

            migrationBuilder.DropTable(
                name: "EndDateChangeTracking");

            migrationBuilder.DropTable(
                name: "FoodEvent");

            migrationBuilder.DropTable(
                name: "FoodEventTracking");

            migrationBuilder.DropTable(
                name: "FTEChange");

            migrationBuilder.DropTable(
                name: "FTEChangeTracking");

            migrationBuilder.DropTable(
                name: "GroupAssignment");

            migrationBuilder.DropTable(
                name: "HiringAffiliateFaculty");

            migrationBuilder.DropTable(
                name: "HiringAffiliateFacultyTracking");

            migrationBuilder.DropTable(
                name: "HiringFaculty");

            migrationBuilder.DropTable(
                name: "HiringFacultyTracking");

            migrationBuilder.DropTable(
                name: "HiringStaff");

            migrationBuilder.DropTable(
                name: "HiringStaffTracking");

            migrationBuilder.DropTable(
                name: "MoveWorker");

            migrationBuilder.DropTable(
                name: "MoveWorkerTracking");

            migrationBuilder.DropTable(
                name: "OnBehalf");

            migrationBuilder.DropTable(
                name: "PatientEvent");

            migrationBuilder.DropTable(
                name: "PatientEventTracking");

            migrationBuilder.DropTable(
                name: "PerioLimitedCare");

            migrationBuilder.DropTable(
                name: "PerioLimitedCareTracking");

            migrationBuilder.DropTable(
                name: "SampleCaseType");

            migrationBuilder.DropTable(
                name: "SampleCaseTypeTracking");

            migrationBuilder.DropTable(
                name: "ScholarResGradHire");

            migrationBuilder.DropTable(
                name: "ScholarResGradHireTracking");

            migrationBuilder.DropTable(
                name: "SecurityRolesChange");

            migrationBuilder.DropTable(
                name: "SecurityRolesChangeTracking");

            migrationBuilder.DropTable(
                name: "Termination");

            migrationBuilder.DropTable(
                name: "TerminationTracking");

            migrationBuilder.DropTable(
                name: "Travel");

            migrationBuilder.DropTable(
                name: "TravelTracking");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "CaseAudit");

            migrationBuilder.DropTable(
                name: "LocalGroup");

            migrationBuilder.DropTable(
                name: "Case");

            migrationBuilder.DropTable(
                name: "CaseType");

            migrationBuilder.DropTable(
                name: "LocalUser");
        }
    }
}
