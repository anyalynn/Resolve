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
    public class HRServiceScholarResidentController : Controller
    {

        private readonly ResolveCaseContext _context;

        public HRServiceScholarResidentController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, HRServiceScholarResident hrScholar)
        {
            if (ModelState.IsValid)
            {
                HRServiceScholarResident newCase = new HRServiceScholarResident
                {
                    CaseID = id,                     
                    Name = hrScholar.Name,
                    ScholarRequestType = hrScholar.ScholarRequestType,
                    ScholarJobProfile = hrScholar.ScholarJobProfile,
                    ScholarCompAllowanceChange=hrScholar.ScholarCompAllowanceChange,
                    EffectiveStartDate = hrScholar.EffectiveStartDate,
                    EffectiveEndDate = hrScholar.EffectiveEndDate,
                    CurrentFTE=hrScholar.CurrentFTE,
                    ProposedFTE = hrScholar.ProposedFTE,
                    JobTitle = hrScholar.JobTitle,
                    PropTitle = hrScholar.PropTitle,
                    Department = hrScholar.Department,
                    StepStipendAllowance = hrScholar.StepStipendAllowance,
                    BudgetNumbers = hrScholar.BudgetNumbers,
                    Note = hrScholar.Note,
                };
                
                _context.Add(newCase);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(hrScholar);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HRServiceScholarResident editCase = _context.HRServiceScholarResident.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,Name,ScholarRequestType,ScholarCompAllowanceChange,ScholarJobProfile,JobTitle,PropTitle,CurrentFTE,ProposedFTE,EffectiveStartDate,EffectiveEndDate,StepStipendAllowance,Department,Note,BudgetNumbers")] HRServiceScholarResident hrScholar)

        {
            if (id != hrScholar.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<HRServiceScholarResident> beforeCases = _context.HRServiceScholarResident.Where(c => c.CaseID == id).AsNoTracking<HRServiceScholarResident>();
                    HRServiceScholarResident beforeCase = beforeCases.FirstOrDefault();
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

                        var old_details = new HRServiceScholarResidentTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            Name = beforeCase.Name,
                            ScholarJobProfile = beforeCase.ScholarJobProfile,
                            ScholarRequestType = beforeCase.ScholarRequestType,
                            EffectiveStartDate = beforeCase.EffectiveStartDate,
                            EffectiveEndDate = beforeCase.EffectiveEndDate,
                            Department = beforeCase.Department,
                            StepStipendAllowance = beforeCase.StepStipendAllowance,
                            Note = beforeCase.Note,
                            BudgetNumbers = beforeCase.BudgetNumbers
                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new HRServiceScholarResidentTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = hrScholar.CaseID,
                            Name = hrScholar.Name,
                            ScholarRequestType = hrScholar.ScholarRequestType,
                            EffectiveStartDate = hrScholar.EffectiveStartDate,
                            EffectiveEndDate = hrScholar.EffectiveEndDate,
                            Department = hrScholar.Department,
                            ScholarJobProfile = hrScholar.ScholarJobProfile,
                            StepStipendAllowance = hrScholar.StepStipendAllowance,
                            Note = hrScholar.Note,
                            BudgetNumbers = hrScholar.BudgetNumbers
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(hrScholar);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HRServiceScholarResidentExists(hrScholar.CaseID))
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
            return View(hrScholar);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.HRServiceScholarResidentTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool HRServiceScholarResidentExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}