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
    public class MoveWorkerController : Controller
    {

        private readonly ResolveCaseContext _context;

        public MoveWorkerController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, MoveWorker moveWorker)
        {
            if (ModelState.IsValid)
            {
                MoveWorker newCase = new MoveWorker
                {
                    CaseID = id,
                    Name = moveWorker.Name,
                    CWorkerType = moveWorker.CWorkerType,
                    EffectiveStartDate = moveWorker.EffectiveStartDate,
                    SupOrg = moveWorker.SupOrg,
                    EmployeeEID = moveWorker.EmployeeEID,
                    Note = moveWorker.Note,
                    BudgetNumbers = moveWorker.BudgetNumbers,
                    DetailedDescription = moveWorker.DetailedDescription
                };
                _context.Add(newCase);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(moveWorker);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MoveWorker editCase = _context.MoveWorker.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,Name,EffectiveStartDate,Note,CWorkerType,SupOrg,Department,JobTitle,EmployeeEID,BudgetNumbers,DetailedDescription")]  MoveWorker moveWorker)
        {
            if (id != moveWorker.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<MoveWorker> beforeCases = _context.MoveWorker.Where(c => c.CaseID == id).AsNoTracking<MoveWorker>();
                    MoveWorker beforeCase = beforeCases.FirstOrDefault();
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

                        var old_details = new MoveWorkerTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            Name = beforeCase.Name,
                            CWorkerType = beforeCase.CWorkerType,
                            EffectiveStartDate = beforeCase.EffectiveStartDate,                            
                            SupOrg = beforeCase.SupOrg,
                            EmployeeEID = beforeCase.EmployeeEID,
                            Note = beforeCase.Note,
                            DetailedDescription = beforeCase.DetailedDescription,
                            BudgetNumbers = beforeCase.BudgetNumbers
                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new MoveWorkerTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = moveWorker.CaseID,
                            Name = moveWorker.Name,
                            CWorkerType = moveWorker.CWorkerType,
                            EffectiveStartDate = moveWorker.EffectiveStartDate,                           
                            SupOrg = moveWorker.SupOrg,
                            EmployeeEID = moveWorker.EmployeeEID,
                            Note = moveWorker.Note,
                            DetailedDescription = moveWorker.DetailedDescription,
                            BudgetNumbers = moveWorker.BudgetNumbers
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(moveWorker);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoveWorkerExists(moveWorker.CaseID))
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
            return View(moveWorker);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.MoveWorkerTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool MoveWorkerExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}