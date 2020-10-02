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
    public class AxiumFeeScheduleController : Controller
    {
        
        private readonly ResolveCaseContext _context;

        public AxiumFeeScheduleController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, [Bind("AxiumSchedRequestType,AxiumScheduleType," +
            "AxiumCodeType,Discipline,Site,ProcedureCode,ProdCodeDescription,Fee,Justification," +
            "UnitsFactored")] AxiumFeeSchedule feeSchedule)
        {
            if (ModelState.IsValid)
            {
                feeSchedule.CaseID = id;
                _context.Add(feeSchedule);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(feeSchedule);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editCase = _context.AxiumFeeSchedule.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,AxiumSchedRequestType,AxiumScheduleType," +
            "AxiumCodeType,Discipline,Site,ProcedureCode,ProdCodeDescription,Fee,Justification," +
            "UnitsFactored")] AxiumFeeSchedule feeSchedule)

        {
            if (id != feeSchedule.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<AxiumFeeSchedule> beforeCases = _context.AxiumFeeSchedule.Where(c => c.CaseID == id).AsNoTracking<AxiumFeeSchedule>();
                    AxiumFeeSchedule beforeCase = beforeCases.FirstOrDefault();
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
                        var old_details = new AxiumFeeScheduleTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            AxiumSchedRequestType = beforeCase.AxiumSchedRequestType,                          
                            AxiumScheduleType = beforeCase.AxiumScheduleType,
                            AxiumCodeType = beforeCase.AxiumCodeType,
                            Fee = beforeCase.Fee,
                            Site = beforeCase.Site,
                            UnitsFactored = beforeCase.UnitsFactored,
                            ProcedureCode = beforeCase.ProcedureCode,
                            ProdCodeDescription = beforeCase.ProdCodeDescription,
                            Justification=beforeCase.Justification,
                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new AxiumFeeScheduleTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = feeSchedule.CaseID,
                            AxiumSchedRequestType = feeSchedule.AxiumSchedRequestType,
                            AxiumScheduleType = feeSchedule.AxiumScheduleType,
                            AxiumCodeType = feeSchedule.AxiumCodeType,
                            Fee = feeSchedule.Fee,
                            Site = feeSchedule.Site,
                            UnitsFactored = feeSchedule.UnitsFactored,
                            ProcedureCode = feeSchedule.ProcedureCode,
                            ProdCodeDescription = feeSchedule.ProdCodeDescription,
                            Justification = feeSchedule.Justification,
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(feeSchedule);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AxiumFeeScheduleExists(feeSchedule.CaseID))
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
            return View(feeSchedule);
        }

        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.AxiumFeeScheduleTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool AxiumFeeScheduleExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}