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
    public class EndDateChangeController : Controller
    {

        private readonly ResolveCaseContext _context;

        public EndDateChangeController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, EndDateChange endChange)
        {
            if (ModelState.IsValid)
            {
                EndDateChange newCase = new EndDateChange
                {
                    CaseID = id,
                    Name = endChange.Name,
                    HireType = endChange.HireType,
                    AWorkerType = endChange.AWorkerType,
                    EffectiveStartDate = endChange.EffectiveStartDate,
                    EffectiveEndDate = endChange.EffectiveEndDate,
                    SupOrg = endChange.SupOrg,
                    EmployeeEID = endChange.EmployeeEID,
                    Note = endChange.Note,
                    BudgetNumbers = endChange.BudgetNumbers,
                    DetailedDescription = endChange.DetailedDescription,
                    Department = endChange.Department,
                    JobTitle = endChange.JobTitle
                };
                _context.Add(newCase);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(endChange);
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
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,Name,HireType,EffectiveStartDate,EffectiveEndDate,Note,AWorkerType,SupOrg,Department,JobTitle,EmployeeEID,BudgetNumbers,DetailedDescription")]  EndDateChange endChange)
        {
            if (id != endChange.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (endChange.AWorkerType.ToString() == "Staff")
                {
                    endChange.Department = null;

                }
                else
                {
                    endChange.HireType = null;

                }


                try
                {
                    IQueryable<EndDateChange > beforeCases = _context.EndDateChange.Where(c => c.CaseID == id).AsNoTracking<EndDateChange>();
                    EndDateChange beforeCase = beforeCases.FirstOrDefault();
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

                        var old_details = new EndDateChangeTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            Name = beforeCase.Name,
                            AWorkerType = beforeCase.AWorkerType,
                            HireType = beforeCase.HireType,
                            Department = beforeCase.Department,
                            JobTitle = beforeCase.JobTitle,
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
                        var new_details = new EndDateChangeTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = endChange.CaseID,
                            Name = endChange.Name,
                            AWorkerType = endChange.AWorkerType,
                            HireType = endChange.HireType,
                            Department = endChange.Department,
                            JobTitle = endChange.JobTitle,
                            EffectiveStartDate = endChange.EffectiveStartDate,
                            EffectiveEndDate = endChange.EffectiveEndDate,
                            SupOrg = endChange.SupOrg,
                            EmployeeEID = endChange.EmployeeEID,
                            Note = endChange.Note,
                            DetailedDescription = endChange.DetailedDescription,
                            BudgetNumbers = endChange.BudgetNumbers
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(endChange);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EndDateChangeExists(endChange.CaseID))
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
            return View(endChange);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.EndDateChangeTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool EndDateChangeExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}