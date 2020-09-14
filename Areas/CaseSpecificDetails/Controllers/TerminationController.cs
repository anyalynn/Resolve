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
    public class TerminationController : Controller
    {

        private readonly ResolveCaseContext _context;

        public TerminationController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, Termination termination)
        {
            if (ModelState.IsValid)
            {
                Termination newCase = new Termination
                {
                    CaseID = id,
                    Name = termination.Name,
                    HireType = termination.HireType,
                    AWorkerType = termination.AWorkerType,
                    EffectiveStartDate = termination.EffectiveStartDate,
                    SupOrg = termination.SupOrg,
                    EmployeeEID = termination.EmployeeEID,
                    Note = termination.Note,
                    BudgetNumbers = termination.BudgetNumbers,
                    DetailedDescription = termination.DetailedDescription,
                    Department = termination.Department,
                    JobTitle = termination.JobTitle,
                    TerminationReason = termination.TerminationReason,
                    Offboarding=termination.Offboarding,
                    LeaveWA=termination.LeaveWA,
                    ClosePosition=termination.ClosePosition
                };
                _context.Add(newCase);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(termination);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Termination editCase = _context.Termination.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,Name,HireType,EffectiveStartDate,EffectiveEndDate,Note,AWorkerType,SupOrg,Department,JobTitle,EmployeeEID,BudgetNumbers,DetailedDescription,TerminationReason,LeaveWA,Offboarding,ClosePosition")]  Termination termination)
        {
            if (id != termination.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<Termination> beforeCases = _context.Termination.Where(c => c.CaseID == id).AsNoTracking<Termination> ();
                    Termination beforeCase = beforeCases.FirstOrDefault();
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

                        var old_details = new TerminationTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            Name = beforeCase.Name,
                            HireType = beforeCase.HireType,
                            AWorkerType = beforeCase.AWorkerType,
                            Department = beforeCase.Department,
                            JobTitle = beforeCase.JobTitle,
                            EffectiveStartDate = beforeCase.EffectiveStartDate,                           
                            SupOrg = beforeCase.SupOrg,
                            EmployeeEID = beforeCase.EmployeeEID,
                            Note = beforeCase.Note,
                            DetailedDescription = beforeCase.DetailedDescription,
                            BudgetNumbers = beforeCase.BudgetNumbers,
                            TerminationReason = beforeCase.TerminationReason,
                            Offboarding = beforeCase.Offboarding,
                            LeaveWA = beforeCase.LeaveWA,
                            ClosePosition = beforeCase.ClosePosition
                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new TerminationTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = termination.CaseID,
                            Name = termination.Name,
                            AWorkerType = termination.AWorkerType,
                            HireType = termination.HireType,
                            Department = termination.Department,
                            JobTitle = termination.JobTitle,
                            EffectiveStartDate = termination.EffectiveStartDate,                            
                            SupOrg = termination.SupOrg,
                            EmployeeEID = termination.EmployeeEID,
                            Note = termination.Note,
                            DetailedDescription = termination.DetailedDescription,
                            BudgetNumbers = termination.BudgetNumbers,
                            TerminationReason = termination.TerminationReason,
                            Offboarding = termination.Offboarding,
                            LeaveWA = termination.LeaveWA,
                            ClosePosition = termination.ClosePosition
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(termination);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerminationExists(termination.CaseID))
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
            return View(termination);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.TerminationTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool TerminationExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}