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
    public class PatientEventController : Controller
    {
        
        private readonly ResolveCaseContext _context;

        public PatientEventController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, [Bind("NotifyDate,BirthDate,EventDate,EventDepartment," +
            "EventLocation,NotifiedName,DentalRecordNum,FactsDocumented,Witness1,Witness2,PatientName,ProviderName," +
            "Gender,FirstReportedBy,Causes,ReporterActionTaken,ManagerActionTaken,SupervisorName,EventDescription," +
            "EventTime,Note")] PatientEvent patientEvent)
        {
            if (ModelState.IsValid)
            {
                patientEvent.CaseID = id;
                _context.Add(patientEvent);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(patientEvent);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editCase = _context.PatientEvent.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,NotifyDate,BirthDate,EventDate,EventDepartment," +
            "EventLocation,NotifiedName,DentalRecordNum,FactsDocumented,Witness1,Witness2,PatientName,ProviderName," +
            "Gender,FirstReportedBy,Causes,ReporterActionTaken,ManagerActionTaken,SupervisorName,EventDescription," +
            "EventTime,Note")] PatientEvent patientEvent)

        {
            if (id != patientEvent.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<PatientEvent> beforeCases = _context.PatientEvent.Where(c => c.CaseID == id).AsNoTracking<PatientEvent>();
                    PatientEvent beforeCase = beforeCases.FirstOrDefault();
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
                        var old_details = new PatientEventTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            EventDescription = beforeCase.EventDescription,                          
                            EventDate = beforeCase.EventDate,
                            EventTime = beforeCase.EventTime,
                            EventLocation = beforeCase.EventLocation,
                            EventDepartment = beforeCase.EventDepartment,
                            DentalRecordNum = beforeCase.DentalRecordNum,
                            NotifiedName = beforeCase.NotifiedName,
                            PatientName = beforeCase.PatientName,
                            ProviderName = beforeCase.ProviderName,
                            FactsDocumented=beforeCase.FactsDocumented,
                            NotifyDate = beforeCase.NotifyDate,
                            BirthDate = beforeCase.BirthDate,                           
                            Witness1 = beforeCase.Witness1,
                            Witness2 = beforeCase.Witness2,
                            Gender = beforeCase.Gender,
                            FirstReportedBy = beforeCase.FirstReportedBy,
                            Causes = beforeCase.Causes,
                            ReporterActionTaken = beforeCase.ReporterActionTaken,
                            ManagerActionTaken = beforeCase.ManagerActionTaken,
                            SupervisorName = beforeCase.SupervisorName,                           
                            Note = beforeCase.Note
                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new PatientEventTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = patientEvent.CaseID,
                            EventDescription = patientEvent.EventDescription,                           
                            EventDate = patientEvent.EventDate,
                            EventTime = patientEvent.EventTime,
                            EventLocation = patientEvent.EventLocation,
                            EventDepartment = patientEvent.EventDepartment,
                            DentalRecordNum = patientEvent.DentalRecordNum,
                            NotifiedName = patientEvent.NotifiedName,
                            PatientName = patientEvent.PatientName,
                            ProviderName = patientEvent.ProviderName,
                            FactsDocumented = patientEvent.FactsDocumented,
                            NotifyDate = patientEvent.NotifyDate,
                            BirthDate = patientEvent.BirthDate,
                            Witness1 = patientEvent.Witness1,
                            Witness2 = patientEvent.Witness2,
                            Gender = patientEvent.Gender,
                            FirstReportedBy = patientEvent.FirstReportedBy,
                            Causes = patientEvent.Causes,
                            ReporterActionTaken = patientEvent.ReporterActionTaken,
                            ManagerActionTaken = patientEvent.ManagerActionTaken,
                            SupervisorName = patientEvent.SupervisorName,
                            Note = patientEvent.Note
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(patientEvent);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientEventExists(patientEvent.CaseID))
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
            return View(patientEvent);
        }

        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.PatientEventTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool PatientEventExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}