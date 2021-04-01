using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resolve.Data;
using Resolve.Models;

namespace Resolve.Areas.CaseSpecificDetails.Controllers
{
    [Area("CaseSpecificDetails")]
    //[Route(nameof(CaseTypes) + "/[controller]")]
    public class SecurityRolesChangeController : Controller
    {

        private readonly ResolveCaseContext _context;

        public SecurityRolesChangeController(ResolveCaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, SecurityRolesChange securityChange)
        {
            if (ModelState.IsValid)
            {
                SecurityRolesChange newCase = new SecurityRolesChange
                {
                    CaseID = id,
                    Name = securityChange.Name,
                    HireType = securityChange.HireType,
                    AWorkerType = securityChange.AWorkerType,
                    EffectiveDate = securityChange.EffectiveDate,
                    SupOrg = securityChange.SupOrg,
                    SuperOrg = securityChange.SupOrgName,
                     EmployeeEID = securityChange.EmployeeEID,
                    Note = securityChange.Note,
                    UWNetID = securityChange.UWNetID,
                    SupervisedAccess = securityChange.SupervisedAccess,
                    TrainingCompleted = securityChange.TrainingCompleted,
                    IncludeSubordinates = securityChange.IncludeSubordinates,
                    SecurityRequestType = securityChange.SecurityRequestType,
                    AcademicChair = securityChange.AcademicChair,
                    AcademicDean = securityChange.AcademicDean,
                    HCMInit1 = securityChange.HCMInit1,
                    HCMInit2 = securityChange.HCMInit2,
                    I9 = securityChange.I9,
                    Manager = securityChange.Manager,
                    UWHiresManager = securityChange.UWHiresManager,
                    VOStaff = securityChange.VOStaff,
                    VOStaffCompCost = securityChange.VOStaffCompCost,
                    VOStaffCompPersonal = securityChange.VOStaffCompPersonal,
                    TimeAbsenceApprover = securityChange.TimeAbsenceApprover,
                    TimeAbsenceInitiate = securityChange.TimeAbsenceInitiate,
                    Department =securityChange.Department,
                    JobTitle=securityChange.JobTitle
                };
                _context.Add(newCase);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(securityChange);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SecurityRolesChange editCase = _context.SecurityRolesChange.Find(id);
            if (editCase == null)
            {
                var newid = id;
                //return NotFound();
                return RedirectToAction("Create", "SecurityRolesChange", new { id = newid });
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,Name,EffectiveDate,Note,AWorkerType,HireType,SupOrg,Department,JobTitle,EmployeeEID,UWNetID," +
            "SupervisedAccesss,TrainingCompleted,IncludeSubordinates,SupervisedAccess,SecurityRequestType,AcademicChair,AcademicDean,HCMInit1,HCMInit2,I9,Manager,UWHiresManager,VOStaff," +
            "VOStaffCompCost,VOStaffCompPersonal,TimeAbsenceApprover,TimeAbsenceInitiate")]  SecurityRolesChange securityChange)
        {
            if (id != securityChange.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (securityChange.AWorkerType.ToString() == "Staff")
                {
                    securityChange.Department = null;

                }
                else
                {
                    securityChange.HireType = null;

                }
                if (securityChange.IncludeSubordinates==false)
                {
                    securityChange.SupervisedAccess = null;
                }
                securityChange.SuperOrg = securityChange.SupOrgName;
                try
                {
                    IQueryable<SecurityRolesChange> beforeCases = _context.SecurityRolesChange.Where(c => c.CaseID == id).AsNoTracking<SecurityRolesChange>();
                    SecurityRolesChange beforeCase = beforeCases.FirstOrDefault();
                    if (beforeCase == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Creating an audit log
                        var audit = new CaseAudit { AuditLog = "Case Specific Details Edited", CaseID = id, LocalUserID = User.Identity.Name };
                        _context.Add(audit);
                        await _context.SaveChangesAsync();
                        // Adding old details to tracking

                        var old_details = new SecurityRolesChangeTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            Name = beforeCase.Name,
                            AWorkerType = beforeCase.AWorkerType,
                            HireType = beforeCase.HireType,
                            Department = beforeCase.Department,
                            JobTitle = beforeCase.JobTitle,
                            EffectiveDate = beforeCase.EffectiveDate,                            
                            SuperOrg = beforeCase.SuperOrg,
                            EmployeeEID = beforeCase.EmployeeEID,
                            Note = beforeCase.Note,
                            UWNetID = beforeCase.UWNetID,
                            SupervisedAccess = beforeCase.SupervisedAccess,
                            TrainingCompleted = beforeCase.TrainingCompleted,
                            IncludeSubordinates = beforeCase.IncludeSubordinates,
                            SecurityRequestType = beforeCase.SecurityRequestType,
                            AcademicChair = beforeCase.AcademicChair,
                            AcademicDean = beforeCase.AcademicDean,
                            HCMInit1 = beforeCase.HCMInit1,
                            HCMInit2 = beforeCase.HCMInit2,
                            I9 = beforeCase.I9,
                            Manager = beforeCase.Manager,
                            UWHiresManager = beforeCase.UWHiresManager,
                            VOStaff = beforeCase.VOStaff,
                            VOStaffCompCost = beforeCase.VOStaffCompCost,
                            VOStaffCompPersonal = beforeCase.VOStaffCompPersonal,
                            TimeAbsenceApprover = beforeCase.TimeAbsenceApprover,
                            TimeAbsenceInitiate = beforeCase.TimeAbsenceInitiate                            
                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new SecurityRolesChangeTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = securityChange.CaseID,
                            Name = securityChange.Name,
                            AWorkerType = securityChange.AWorkerType,
                            HireType = securityChange.HireType,
                            Department = securityChange.Department,
                            JobTitle = securityChange.JobTitle,
                            EffectiveDate = securityChange.EffectiveDate,
                            SuperOrg = securityChange.SuperOrg,
                            EmployeeEID = securityChange.EmployeeEID,
                            Note = securityChange.Note,
                            UWNetID = securityChange.UWNetID,
                            SupervisedAccess = securityChange.SupervisedAccess,
                            TrainingCompleted = securityChange.TrainingCompleted,
                            IncludeSubordinates = securityChange.IncludeSubordinates,
                            SecurityRequestType = securityChange.SecurityRequestType,
                            AcademicChair = securityChange.AcademicChair,
                            AcademicDean = securityChange.AcademicDean,
                            HCMInit1 = securityChange.HCMInit1,
                            HCMInit2 = securityChange.HCMInit2,
                            I9 = securityChange.I9,
                            Manager = securityChange.Manager,
                            UWHiresManager = securityChange.UWHiresManager,
                            VOStaff = securityChange.VOStaff,
                            VOStaffCompCost = securityChange.VOStaffCompCost,
                            VOStaffCompPersonal = securityChange.VOStaffCompPersonal,
                            TimeAbsenceApprover = securityChange.TimeAbsenceApprover,
                            TimeAbsenceInitiate = securityChange.TimeAbsenceInitiate
                          
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(securityChange);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecurityRolesChangeExists(securityChange.CaseID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
            }
            return View(securityChange);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.SecurityRolesChangeTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool SecurityRolesChangeExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}