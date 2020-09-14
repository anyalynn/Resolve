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
    public class CompAllowanceChangeController : Controller
    {

        private readonly ResolveCaseContext _context;

        public CompAllowanceChangeController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, CompAllowanceChange compallowChange)
        {
            if (ModelState.IsValid)
            {
                CompAllowanceChange newCase = new CompAllowanceChange
                {
                    CaseID = id,
                    Name = compallowChange.Name,
                    AllowanceChange = compallowChange.AllowanceChange,
                    ScholarCompAllowanceChange = compallowChange.ScholarCompAllowanceChange,
                    Amount = compallowChange.Amount,
                    HireType = compallowChange.HireType,
                    JobTitle = compallowChange.JobTitle,
                    EWorkerType = compallowChange.EWorkerType,
                    EffectiveStartDate = compallowChange.EffectiveStartDate,
                    EffectiveEndDate = compallowChange.EffectiveEndDate,
                    SupOrg = compallowChange.SupOrg,
                    EmployeeEID = compallowChange.EmployeeEID,
                    Note = compallowChange.Note,
                    BudgetNumbers = compallowChange.BudgetNumbers,
                    DetailedDescription = compallowChange.DetailedDescription
                };
                _context.Add(newCase);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(compallowChange);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CompAllowanceChange editCase = _context.CompAllowanceChange.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,Name,AllowanceChange,ScholarCompAllowanceChange,Amount,EffectiveStartDate,EffectiveEndDate,HireType,JobTitle,Note,EWorkerType,SupOrg,EmployeeEID,BudgetNumbers,DetailedDescription")] CompAllowanceChange compallowChange)
        {
            if (id != compallowChange.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<CompAllowanceChange> beforeCases = _context.CompAllowanceChange.Where(c => c.CaseID == id).AsNoTracking<CompAllowanceChange>();
                    CompAllowanceChange beforeCase = beforeCases.FirstOrDefault();
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

                        var old_details = new CompAllowanceChangeTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            Name = beforeCase.Name,
                            AllowanceChange = beforeCase.AllowanceChange,
                            Amount = beforeCase.Amount,
                            EWorkerType = beforeCase.EWorkerType,
                            EffectiveStartDate = beforeCase.EffectiveStartDate,
                            EffectiveEndDate = beforeCase.EffectiveEndDate,
                            SupOrg = beforeCase.SupOrg,
                            EmployeeEID = beforeCase.EmployeeEID,
                            Note = beforeCase.Note,
                            DetailedDescription = beforeCase.DetailedDescription,
                            BudgetNumbers = beforeCase.BudgetNumbers
                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new CompAllowanceChangeTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = compallowChange.CaseID,
                            Name = compallowChange.Name,
                            AllowanceChange = compallowChange.AllowanceChange,
                            Amount = compallowChange.Amount,
                            EWorkerType = compallowChange.EWorkerType,
                            EffectiveStartDate = compallowChange.EffectiveStartDate,
                            EffectiveEndDate = compallowChange.EffectiveEndDate,
                            SupOrg = compallowChange.SupOrg,
                            EmployeeEID = compallowChange.EmployeeEID,
                            Note = compallowChange.Note,
                            DetailedDescription = compallowChange.DetailedDescription,
                            BudgetNumbers = compallowChange.BudgetNumbers
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(compallowChange);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompAllowanceChangeExists(compallowChange.CaseID))
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
            return View(compallowChange);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.CompAllowanceChangeTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool CompAllowanceChangeExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}