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
                    EffectiveStartDate = securityChange.EffectiveStartDate,
                    SupOrg = securityChange.SupOrg,
                    EmployeeEID = securityChange.EmployeeEID,
                    Note = securityChange.Note,
                    BudgetNumbers = securityChange.BudgetNumbers,
                    DetailedDescription = securityChange.DetailedDescription,
                    Department=securityChange.Department,
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
            EndDateChange editCase = _context.EndDateChange.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,EmployeeName,EffectiveStartDate,EffectiveEndDate,Note,AWorkerType,SupOrg,Department,JobTitle,EmployeeEID,BudgetNumbers,DetailedDescription")]  SecurityRolesChange securityChange)
        {
            if (id != securityChange.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
                            Department = beforeCase.Department,
                            JobTitle = beforeCase.JobTitle,
                            EffectiveStartDate = beforeCase.EffectiveStartDate,                            
                            SupOrg = beforeCase.SupOrg,
                            EmployeeEID = beforeCase.EmployeeEID,
                            Note = beforeCase.Note,
                            DetailedDescription = beforeCase.DetailedDescription,
                            BudgetNumbers = beforeCase.BudgetNumbers
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
                            Department = securityChange.Department,
                            JobTitle = securityChange.JobTitle,
                            EffectiveStartDate = securityChange.EffectiveStartDate,
                            SupOrg = securityChange.SupOrg,
                            EmployeeEID = securityChange.EmployeeEID,
                            Note = securityChange.Note,
                            DetailedDescription = securityChange.DetailedDescription,
                            BudgetNumbers = securityChange.BudgetNumbers
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