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
    public class CompBasePayChangeController : Controller
    {

        private readonly ResolveCaseContext _context;

        public CompBasePayChangeController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, CompBasePayChange compBasePay)
        {
            if (ModelState.IsValid)
            {
                CompBasePayChange newCase = new CompBasePayChange
                {
                    CaseID = id,
                    Name = compBasePay.Name,
                    BasePayChange = compBasePay.BasePayChange,
                    Amount = compBasePay.Amount,
                    HireType = compBasePay.HireType,
                    CWorkerType = compBasePay.CWorkerType,
                    EffectiveStartDate = compBasePay.EffectiveStartDate,
                    EffectiveEndDate = compBasePay.EffectiveEndDate,
                    SupOrg = compBasePay.SupOrg,
                    EmployeeEID = compBasePay.EmployeeEID,
                    JobTitle = compBasePay.JobTitle,
                    Note = compBasePay.Note,
                    BudgetNumbers = compBasePay.BudgetNumbers,
                    DetailedDescription = compBasePay.DetailedDescription
                };
                _context.Add(newCase);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(compBasePay);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CompBasePayChange editCase = _context.CompBasePayChange.Find(id);
            if (editCase == null)
            {
                var newid = id;
                //return NotFound();
                return RedirectToAction("Create", "CompBasePayChange", new { id = newid });
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,Name,BasePayChange,HireType,JobTitle,Amount,EffectiveStartDate,EffectiveEndDate,Note,CWorkerType,SupOrg,EmployeeEID,BudgetNumbers,DetailedDescription")] CompBasePayChange compBasePay)
        {
            if (id != compBasePay.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<CompBasePayChange> beforeCases = _context.CompBasePayChange.Where(c => c.CaseID == id).AsNoTracking<CompBasePayChange>();
                    CompBasePayChange beforeCase = beforeCases.FirstOrDefault();
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

                        var old_details = new CompBasePayChangeTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            Name = beforeCase.Name,
                            HireType = beforeCase.HireType,
                            BasePayChange = beforeCase.BasePayChange,
                            Amount = beforeCase.Amount,
                            JobTitle = beforeCase.JobTitle,
                            CWorkerType = beforeCase.CWorkerType,
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
                        var new_details = new CompBasePayChangeTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = compBasePay.CaseID,
                            Name = compBasePay.Name,
                            HireType = compBasePay.HireType,                            
                            BasePayChange = compBasePay.BasePayChange,
                            Amount = compBasePay.Amount,
                            JobTitle = compBasePay.JobTitle,
                            CWorkerType = compBasePay.CWorkerType,
                            EffectiveStartDate = compBasePay.EffectiveStartDate,
                            EffectiveEndDate = compBasePay.EffectiveEndDate,
                            SupOrg = compBasePay.SupOrg,
                            EmployeeEID = compBasePay.EmployeeEID,
                            Note = compBasePay.Note,
                            DetailedDescription = compBasePay.DetailedDescription,
                            BudgetNumbers = compBasePay.BudgetNumbers
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(compBasePay);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompBasePayChangeExists(compBasePay.CaseID))
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
            return View(compBasePay);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.CompBasePayChangeTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool CompBasePayChangeExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}