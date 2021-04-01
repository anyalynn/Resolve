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
                    SuperOrg=compallowChange.SupOrgName,                   
                    EmployeeEID = compallowChange.EmployeeEID,
                    Note = compallowChange.Note,
                    BudgetNumbers = compallowChange.BudgetNumbers,
                    DetailedDescription = compallowChange.DetailedDescription,
                    Department = compallowChange.Department
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
                var newid = id;
                //return NotFound();
                return RedirectToAction("Create", "CompAllowanceChange", new { id = newid });
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,Name,AllowanceChange,ScholarCompAllowanceChange,Department,Amount,EffectiveStartDate,EffectiveEndDate,HireType,JobTitle,Note,EWorkerType,SupOrg,EmployeeEID,BudgetNumbers,DetailedDescription")] CompAllowanceChange compallowChange)
        {
            if (id != compallowChange.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(compallowChange.EWorkerType.ToString()=="Staff" || compallowChange.EWorkerType.ToString() == "Faculty")
                {
                    compallowChange.ScholarCompAllowanceChange = null;
                    compallowChange.Department = null;
                    compallowChange.SuperOrg = compallowChange.SupOrgName;

                }
                else if (compallowChange.EWorkerType.ToString() == "Scholar")
                {
                    compallowChange.AllowanceChange = null;
                    compallowChange.HireType = null;
                    compallowChange.SuperOrg = compallowChange.SupOrgName;

                }
                
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
                            ScholarCompAllowanceChange = beforeCase.ScholarCompAllowanceChange,
                            Amount = beforeCase.Amount,
                            EWorkerType = beforeCase.EWorkerType,
                            HireType = beforeCase.HireType,
                            JobTitle = beforeCase.JobTitle,
                            EffectiveStartDate = beforeCase.EffectiveStartDate,
                            EffectiveEndDate = beforeCase.EffectiveEndDate,
                            SuperOrg = beforeCase.SuperOrg,
                          
                            EmployeeEID = beforeCase.EmployeeEID,
                            Note = beforeCase.Note,
                            DetailedDescription = beforeCase.DetailedDescription,
                            BudgetNumbers = beforeCase.BudgetNumbers,
                            Department = beforeCase.Department

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
                            ScholarCompAllowanceChange = compallowChange.ScholarCompAllowanceChange,
                            Amount = compallowChange.Amount,
                            EWorkerType = compallowChange.EWorkerType,
                            HireType = compallowChange.HireType,
                            JobTitle = compallowChange.JobTitle,
                            EffectiveStartDate = compallowChange.EffectiveStartDate,
                            EffectiveEndDate = compallowChange.EffectiveEndDate,
                            SuperOrg = compallowChange.SuperOrg,
                         
                            EmployeeEID = compallowChange.EmployeeEID,
                            Note = compallowChange.Note,
                            DetailedDescription = compallowChange.DetailedDescription,
                            BudgetNumbers = compallowChange.BudgetNumbers,
                            Department = compallowChange.Department
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