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
    public class FoodEventController : Controller
    {
        
        private readonly ResolveCaseContext _context;

        public FoodEventController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, [Bind("EmployeeName,EmployeeEID,EventDate,Department,BudgetNumbers,BudgetType,BudgetPurpose,Note,Item1,Item2,item3,Item4,Item5,Item6,Item7,Total,Justification,EventDescription")] FoodEvent foodEvent)
        {
            if (ModelState.IsValid)
            {
                foodEvent.CaseID = id;
                _context.Add(foodEvent);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(foodEvent);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editCase = _context.FoodEvent.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,EmployeeName,EmployeeEID,EventDate,Department,BudgetNumbers,BudgetType,BudgetPurpose,Note,Item1,Item2,item3,Item4,Item5,Item6,Item7,Total,Justification,EventDescription")] FoodEvent foodEvent)

        {
            if (id != foodEvent.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<FoodEvent> beforeCases = _context.FoodEvent.Where(c => c.CaseID == id).AsNoTracking<FoodEvent>();
                    FoodEvent beforeCase = beforeCases.FirstOrDefault();
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
                        var old_details = new FoodEventTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            EventDescription = beforeCase.EventDescription,
                            EmployeeName = beforeCase.EmployeeName,
                            EventDate = beforeCase.EventDate,
                            Department = beforeCase.Department,
                            BudgetNumbers = beforeCase.BudgetNumbers,
                            BudgetPurpose = beforeCase.BudgetPurpose,
                            BudgetType = beforeCase.BudgetType,
                            Total = beforeCase.Total
                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new FoodEventTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = foodEvent.CaseID,
                            EventDescription = foodEvent.EventDescription,
                            EmployeeName = foodEvent.EmployeeName,
                            Department = foodEvent.Department,
                            EventDate = foodEvent.EventDate,
                             BudgetNumbers = foodEvent.BudgetNumbers,
                            BudgetPurpose = foodEvent.BudgetPurpose,
                            BudgetType = foodEvent.BudgetType,
                            Total = foodEvent.Total
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(foodEvent);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodEventExists(foodEvent.CaseID))
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
            return View(foodEvent);
        }

        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.FoodEventTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool FoodEventExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}