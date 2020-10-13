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
        public async Task<IActionResult> Create(int id, [Bind("BudgetDeficit,FoodApprovalForm,EmployeeName,EventDate,TravelFoodDepartment,BudgetNumbers,BudgetPurpose,Note,ItemCost1,ItemCost2,ItemCost3,ItemCost4,ItemCost5,ItemCost6,ItemCost7,ItemName1,ItemName2,ItemName3,ItemName4,ItemName5,ItemName6,ItemName7,NumberAttending,Total,Justification,EventDescription")] FoodEvent foodEvent)
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
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,BudgetDeficit,FoodApprovalForm,EmployeeName,EventDate,TravelFoodDepartment,BudgetNumbers,BudgetPurpose,Note,ItemCost1,ItemCost2,ItemCost3,ItemCost4,ItemCost5,ItemCost6,ItemCost7,ItemName1,ItemName2,ItemName3,ItemName4,ItemName5,ItemName6,ItemName7,NumberAttending,Total,Justification,EventDescription")] FoodEvent foodEvent)

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
                            TravelFoodDepartment = beforeCase.TravelFoodDepartment,
                            BudgetDeficit = beforeCase.BudgetDeficit,
                            FoodApprovalForm=beforeCase.FoodApprovalForm,
                            BudgetNumbers = beforeCase.BudgetNumbers,
                            Note = beforeCase.Note,
                            BudgetPurpose = beforeCase.BudgetPurpose,
                            Total = beforeCase.Total,
                            ItemCost1 = beforeCase.ItemCost1,
                            ItemCost2 = beforeCase.ItemCost2,
                            ItemCost3 = beforeCase.ItemCost3,
                            ItemCost4 = beforeCase.ItemCost4,
                            ItemCost5 = beforeCase.ItemCost5,
                            ItemCost6 = beforeCase.ItemCost6,
                            ItemCost7 = beforeCase.ItemCost7,
                            ItemName1 = beforeCase.ItemName1,
                            ItemName2 = beforeCase.ItemName2,
                            ItemName3 = beforeCase.ItemName3,
                            ItemName4 = beforeCase.ItemName4,
                            ItemName5 = beforeCase.ItemName5,
                            ItemName6 = beforeCase.ItemName6,
                            ItemName7 = beforeCase.ItemName7,
                             NumberAttending = beforeCase.NumberAttending,
                            Justification = beforeCase.Justification

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
                            TravelFoodDepartment = foodEvent.TravelFoodDepartment,
                            BudgetDeficit = foodEvent.BudgetDeficit,
                            FoodApprovalForm = foodEvent.FoodApprovalForm,
                            EventDate = foodEvent.EventDate,
                            BudgetNumbers = foodEvent.BudgetNumbers,
                            Note = foodEvent.Note,
                            BudgetPurpose = foodEvent.BudgetPurpose,                          
                            Total = foodEvent.Total,
                            ItemCost1 = foodEvent.ItemCost1,
                            ItemCost2 = foodEvent.ItemCost2,
                            ItemCost3 = foodEvent.ItemCost3,
                            ItemCost4 = foodEvent.ItemCost4,
                            ItemCost5 = foodEvent.ItemCost5,
                            ItemCost6 = foodEvent.ItemCost6,
                            ItemCost7 = foodEvent.ItemCost7,
                            ItemName1 = foodEvent.ItemName1,
                            ItemName2 = foodEvent.ItemName2,
                            ItemName3 = foodEvent.ItemName3,
                            ItemName4 = foodEvent.ItemName4,
                            ItemName5 = foodEvent.ItemName5,
                            ItemName6 = foodEvent.ItemName6,
                            ItemName7 = foodEvent.ItemName7,
                            NumberAttending = foodEvent.NumberAttending,
                            Justification = foodEvent.Justification
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