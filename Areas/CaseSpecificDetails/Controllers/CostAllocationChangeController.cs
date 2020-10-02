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
    public class CostAllocationChangeController : Controller
    {

        private readonly ResolveCaseContext _context;

        public CostAllocationChangeController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, CostAllocationChange distChange)
        {
            if (ModelState.IsValid)
            {
                CostAllocationChange newCase = new CostAllocationChange
                {
                    CaseID = id,
                    Name = distChange.Name,  
                    HireType = distChange.HireType,
                    AWorkerType = distChange.AWorkerType,
                    EffectiveStartDate = distChange.EffectiveStartDate,
                    EffectiveEndDate = distChange.EffectiveEndDate,
                    SupOrg = distChange.SupOrg,
                    EmployeeEID = distChange.EmployeeEID,
                    Note = distChange.Note,
                    BudgetNumbers = distChange.BudgetNumbers,
                    DetailedDescription = distChange.DetailedDescription,
                    Department = distChange.Department,
                    JobTitle = distChange.JobTitle
                };
                _context.Add(newCase);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(distChange);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CostAllocationChange editCase = _context.CostAllocationChange.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,Name,EffectiveStartDate,EffectiveEndDate,Note,AWorkerType,HireType,SupOrg,Department,JobTitle,EmployeeEID,BudgetNumbers,DetailedDescription")]  CostAllocationChange distChange)
        {
            if (id != distChange.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (distChange.AWorkerType.ToString() == "Staff")
                {
                    distChange.Department = null;

                }
                else 
                {
                    distChange.HireType = null;

                }

                try
                {
                    IQueryable<CostAllocationChange> beforeCases = _context.CostAllocationChange.Where(c => c.CaseID == id).AsNoTracking<CostAllocationChange>();
                    CostAllocationChange beforeCase = beforeCases.FirstOrDefault();
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

                        var old_details = new CostAllocationChangeTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            Name = beforeCase.Name,
                            AWorkerType = beforeCase.AWorkerType,
                            HireType = beforeCase.HireType,
                            Department =beforeCase.Department,
                            JobTitle=beforeCase.JobTitle,
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
                        var new_details = new CostAllocationChangeTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = distChange.CaseID,
                            Name = distChange.Name,                           
                            AWorkerType = distChange.AWorkerType,
                            HireType = distChange.HireType,
                            Department = distChange.Department,
                            JobTitle = distChange.JobTitle,
                            EffectiveStartDate = distChange.EffectiveStartDate,
                            EffectiveEndDate = distChange.EffectiveEndDate,
                            SupOrg = distChange.SupOrg,
                            EmployeeEID = distChange.EmployeeEID,
                            Note = distChange.Note,
                            DetailedDescription = distChange.DetailedDescription,
                            BudgetNumbers = distChange.BudgetNumbers
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(distChange);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostAllocationChangeExists(distChange.CaseID))
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
            return View(distChange);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.CostAllocationChangeTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool CostAllocationChangeExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}