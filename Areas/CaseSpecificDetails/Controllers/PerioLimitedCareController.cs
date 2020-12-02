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
    public class PerioLimitedCareController : Controller
    {

        private readonly ResolveCaseContext _context;

        public PerioLimitedCareController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, [Bind("StudentName,PatientName,PatientAddress,PatientPhone,BirthDate,Complaint,TreatmentPlan,bwxrays,paxrays,RestorativeExam,PerioExam,Prophy,Other,OtherProcedure,TChart,Note")] PerioLimitedCare perioLimitedCare)
        {
            if (ModelState.IsValid)
            {
                perioLimitedCare.CaseID = id;
                _context.Add(perioLimitedCare);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(perioLimitedCare);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PerioLimitedCare editCase = _context.PerioLimitedCare.Find(id);
            if (editCase == null)
            {
                var newid = id;
                //return NotFound();
                return RedirectToAction("Create", "PerioLimitedCare", new { id = newid });
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,StudentName,PatientName,PatientAddress,PatientPhone,BirthDate,Complaint,TreatmentPlan,bwxrays,paxrays,RestorativeExam,PerioExam,Prophy,Other, OtherProcedure,TChart,Note")] PerioLimitedCare perioLimitedCare)

        {
            if (id != perioLimitedCare.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<PerioLimitedCare> beforeCases = _context.PerioLimitedCare.Where(c => c.CaseID == id).AsNoTracking<PerioLimitedCare>();
                    PerioLimitedCare beforeCase = beforeCases.FirstOrDefault();
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

                        var old_details = new PerioLimitedCareTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            BirthDate = beforeCase.BirthDate,
                            StudentName = beforeCase.StudentName,
                            PatientName = beforeCase.PatientName,
                            TreatmentPlan = beforeCase.TreatmentPlan,
                            Complaint = beforeCase.Complaint,
                            PatientAddress = beforeCase.PatientAddress,
                            PatientPhone = beforeCase.PatientPhone,
                            PerioExam = beforeCase.PerioExam,
                            OtherProcedure = beforeCase.OtherProcedure,
                            RestorativeExam = beforeCase.RestorativeExam,
                            bwxrays = beforeCase.bwxrays,
                            paxrays = beforeCase.paxrays,
                            TChart = beforeCase.TChart,
                            Prophy = beforeCase.Prophy,
                            Other = beforeCase.Other,
                            Note = beforeCase.Note
                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new PerioLimitedCareTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = perioLimitedCare.CaseID,
                            BirthDate = perioLimitedCare.BirthDate,
                            StudentName = perioLimitedCare.StudentName,
                            PatientName = perioLimitedCare.PatientName,
                            TreatmentPlan = perioLimitedCare.TreatmentPlan,
                            Complaint = perioLimitedCare.Complaint,
                            PatientAddress = perioLimitedCare.PatientAddress,
                            PatientPhone = perioLimitedCare.PatientPhone,
                            PerioExam = perioLimitedCare.PerioExam,
                            OtherProcedure = perioLimitedCare.OtherProcedure,
                            RestorativeExam = perioLimitedCare.RestorativeExam,
                            bwxrays = perioLimitedCare.bwxrays,
                            paxrays = perioLimitedCare.paxrays,
                            TChart = perioLimitedCare.TChart,
                            Prophy = perioLimitedCare.Prophy,
                            Other = perioLimitedCare.Other,
                            Note = perioLimitedCare.Note
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(perioLimitedCare);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerioLimitedCareExists(perioLimitedCare.CaseID))
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
            return View(perioLimitedCare);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.PerioLimitedCareTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool PerioLimitedCareExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}