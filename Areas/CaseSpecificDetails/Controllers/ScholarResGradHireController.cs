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
    public class ScholarResGradHireController : Controller
    {

        private readonly ResolveCaseContext _context;

        public ScholarResGradHireController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, ScholarResGradHire scholarHire)
        {
            if (ModelState.IsValid)
            {
                ScholarResGradHire newCase = new ScholarResGradHire
                {
                    CaseID = id,
                    Name = scholarHire.Name,
                    ScholarReqType = scholarHire.ScholarReqType,
                    DWorkerType = scholarHire.DWorkerType,
                    EffectiveDate = scholarHire.EffectiveDate,
                    SupOrg = scholarHire.SupOrg,
                    GradJobProfile = scholarHire.GradJobProfile,
                    Note = scholarHire.Note,
                    BudgetNumbers = scholarHire.BudgetNumbers,
                    DetailedDescription = scholarHire.DetailedDescription,
                    Department = scholarHire.Department,
                    JobTitle = scholarHire.JobTitle,
                    ScholarJobProfile = scholarHire.ScholarJobProfile,
                    NewTitle = scholarHire.NewTitle,
                    StipendAllowance= scholarHire.StipendAllowance,
                    
                };
                _context.Add(newCase);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(scholarHire);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ScholarResGradHire editCase = _context.ScholarResGradHire.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,Name,EffectiveDate,ScholarReqType,Note,DWorkerType,SupOrg,Department,JobTitle,GradJobProfile,ScholarJobProfile,StipendAllowance,BudgetNumbers,DetailedDescription,NewTitle")]   ScholarResGradHire scholarHire)
        {
            if (id != scholarHire.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<ScholarResGradHire> beforeCases = _context.ScholarResGradHire.Where(c => c.CaseID == id).AsNoTracking<ScholarResGradHire>();
                    ScholarResGradHire beforeCase = beforeCases.FirstOrDefault();
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

                        var old_details = new ScholarResGradHireTracking
                        {   Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            Name = beforeCase.Name,
                            DWorkerType = beforeCase.DWorkerType,
                            ScholarReqType = beforeCase.ScholarReqType,
                            Department = beforeCase.Department,
                            JobTitle = beforeCase.JobTitle,                            
                            EffectiveDate = beforeCase.EffectiveDate,
                            SupOrg = beforeCase.SupOrg,
                            GradJobProfile = beforeCase.GradJobProfile,
                            Note = beforeCase.Note,
                            DetailedDescription = beforeCase.DetailedDescription,
                            BudgetNumbers = beforeCase.BudgetNumbers,
                            NewTitle = beforeCase.NewTitle,
                            ScholarJobProfile = beforeCase.ScholarJobProfile,
                            StipendAllowance=beforeCase.StipendAllowance

                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new ScholarResGradHireTracking
                        {   Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = scholarHire.CaseID,
                            Name = scholarHire.Name,
                            DWorkerType = scholarHire.DWorkerType,
                            ScholarReqType = scholarHire.ScholarReqType,
                            Department = scholarHire.Department,
                            JobTitle = scholarHire.JobTitle,
                            EffectiveDate = beforeCase.EffectiveDate,
                            SupOrg = scholarHire.SupOrg,
                            GradJobProfile = scholarHire.GradJobProfile,
                            Note = scholarHire.Note,
                            DetailedDescription = scholarHire.DetailedDescription,
                            BudgetNumbers = scholarHire.BudgetNumbers,
                            NewTitle = scholarHire.NewTitle,
                            ScholarJobProfile = scholarHire.ScholarJobProfile,
                            StipendAllowance = scholarHire.StipendAllowance
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(scholarHire);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScholarResGradHireExists(scholarHire.CaseID))
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
            return View(scholarHire);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.ScholarResGradHireTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool ScholarResGradHireExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}