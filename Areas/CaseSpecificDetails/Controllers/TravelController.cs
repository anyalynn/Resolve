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
    public class TravelController : Controller
    {
        
        private readonly ResolveCaseContext _context;

        public TravelController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, [Bind("EmployeeName,Budget,BudgetPurpose,BudgetNumbers,AirfareCost,RegistrationCost,TransportationCost,OtherCost1,OtherCost2,MealsCost,HotelsCost,Other1,Other2,Total,TravelStartDate,TravelEndDate, TravelFoodDepartment, Destination,Note,Reason")] Travel travel)
        {
            if (ModelState.IsValid)
            {
                travel.CaseID = id;
                _context.Add(travel);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(travel);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editCase = _context.Travel.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,EmployeeName,Budget,BudgetPurpose,BudgetNumbers,AirfareCost,RegistrationCost,TransportationCost,OtherCost1,OtherCost2,MealsCost,HotelsCost,Other1,Other2,Total,TravelStartDate,TravelEndDate, TravelFoodDepartment,Note,Destination,Reason")] Travel travel)

        {
            if (id != travel.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<Travel> beforeCases = _context.Travel.Where(c => c.CaseID == id).AsNoTracking<Travel>();
                    Travel beforeCase = beforeCases.FirstOrDefault();
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
                        var old_details = new TravelTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            Destination = beforeCase.Destination,
                            EmployeeName = beforeCase.EmployeeName,
                            TravelStartDate = beforeCase.TravelStartDate,
                            TravelEndDate = beforeCase.TravelEndDate,
                            Reason = beforeCase.Reason,
                            BudgetNumbers = beforeCase.BudgetNumbers,
                            BudgetPurpose = beforeCase.BudgetPurpose,
                            TravelFoodDepartment = beforeCase.TravelFoodDepartment,
                            Total = beforeCase.Total
                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new TravelTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = travel.CaseID,
                            Destination = travel.Destination,
                            EmployeeName = travel.EmployeeName,
                            TravelStartDate = travel.TravelStartDate,
                            TravelEndDate = travel.TravelEndDate,
                            Reason = travel.Reason,
                            BudgetNumbers = travel.BudgetNumbers,
                            BudgetPurpose = travel.BudgetPurpose,
                            TravelFoodDepartment = travel.TravelFoodDepartment,
                            Total = travel.Total
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(travel);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelExists(travel.CaseID))
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
            return View(travel);
        }

        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.TravelTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool TravelExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}

    