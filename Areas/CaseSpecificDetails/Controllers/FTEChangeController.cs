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
    public class FTEChangeController : Controller
    {

        private readonly ResolveCaseContext _context;

        public FTEChangeController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, FTEChange fteChange)
        {
            if (ModelState.IsValid)
            {
                FTEChange newCase = new FTEChange
                {
                    CaseID = id,
                    Name = fteChange.Name,
                    HireType = fteChange.HireType,
                    AWorkerType = fteChange.AWorkerType,
                    EffectiveEndDate = fteChange.EffectiveEndDate,
                    SupOrg = fteChange.SupOrg,
                    EmployeeEID = fteChange.EmployeeEID,
                    Note = fteChange.Note,
                    BudgetNumbers = fteChange.BudgetNumbers,
                    DetailedDescription = fteChange.DetailedDescription,
                    Department = fteChange.Department,
                    JobTitle = fteChange.JobTitle,
                    CurrentFTE = fteChange.CurrentFTE,
                    ProposedFTE = fteChange.ProposedFTE

                };
                _context.Add(newCase);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(fteChange);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FTEChange editCase = _context.FTEChange.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,Name,HireType,EffectiveEndDate,Note,AWorkerType,SupOrg,Department,JobTitle,EmployeeEID,BudgetNumbers,DetailedDescription,CurrentFTE,ProposedFTE")]   FTEChange fteChange)
        {
            if (id != fteChange.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<FTEChange> beforeCases = _context.FTEChange.Where(c => c.CaseID == id).AsNoTracking<FTEChange>();
                    FTEChange beforeCase = beforeCases.FirstOrDefault();
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

                        var old_details = new FTEChangeTracking
                        {   Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            Name = beforeCase.Name,
                            AWorkerType = beforeCase.AWorkerType,
                            HireType = beforeCase.HireType,
                            Department = beforeCase.Department,
                            JobTitle = beforeCase.JobTitle,                            
                            EffectiveEndDate = beforeCase.EffectiveEndDate,
                            SupOrg = beforeCase.SupOrg,
                            EmployeeEID = beforeCase.EmployeeEID,
                            Note = beforeCase.Note,
                            DetailedDescription = beforeCase.DetailedDescription,
                            BudgetNumbers = beforeCase.BudgetNumbers,
                            CurrentFTE = beforeCase.CurrentFTE,
                            ProposedFTE = beforeCase.ProposedFTE
                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new FTEChangeTracking
                        {   Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = fteChange.CaseID,
                            Name = fteChange.Name,
                            AWorkerType = fteChange.AWorkerType,
                            HireType = fteChange.HireType,
                            Department = fteChange.Department,
                            JobTitle = fteChange.JobTitle,                          
                            EffectiveEndDate = fteChange.EffectiveEndDate,
                            SupOrg = fteChange.SupOrg,
                            EmployeeEID = fteChange.EmployeeEID,
                            Note = fteChange.Note,
                            DetailedDescription = fteChange.DetailedDescription,
                            BudgetNumbers = fteChange.BudgetNumbers,
                            CurrentFTE = fteChange.CurrentFTE,
                            ProposedFTE = fteChange.ProposedFTE
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(fteChange);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FTEChangeExists(fteChange.CaseID))
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
            return View(fteChange);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.FTEChangeTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool FTEChangeExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}