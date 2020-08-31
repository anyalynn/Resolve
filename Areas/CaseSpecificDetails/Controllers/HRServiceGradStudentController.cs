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
    public class HRServiceGradStudentController : Controller
    {

        private readonly ResolveCaseContext _context;

        public HRServiceGradStudentController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, HRServiceGradStudent hrGradStudent)
        {
            if (ModelState.IsValid)
            {
                HRServiceGradStudent newCase = new HRServiceGradStudent
                {
                    CaseID = id,
                   
                    StudentName = hrGradStudent.StudentName,
                    GradRequestType = hrGradStudent.GradRequestType,
                    GradJobProfile = hrGradStudent.GradJobProfile,
                    EffectiveStartDate = hrGradStudent.EffectiveStartDate,
                    EffectiveEndDate = hrGradStudent.EffectiveEndDate,
                    Department = hrGradStudent.Department,
                    StepStipendAllowance = hrGradStudent.StepStipendAllowance,
                    BudgetNumbers = hrGradStudent.BudgetNumbers,
                    Note = hrGradStudent.Note,
                };
                _context.Add(newCase);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(hrGradStudent);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HRServiceGradStudent editCase = _context.HRServiceGradStudent.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,StudentName,GradRequestType,GradJobProfile,EffectiveStartDate,EffectiveEndDate,StepStipendAllowance,Department,Note,BudgetNumbers")] HRServiceGradStudent hrGradStudent)

        {
            if (id != hrGradStudent.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<HRServiceGradStudent> beforeCases = _context.HRServiceGradStudent.Where(c => c.CaseID == id).AsNoTracking<HRServiceGradStudent>();
                    HRServiceGradStudent beforeCase = beforeCases.FirstOrDefault();
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

                        var old_details = new HRServiceGradStudentTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            StudentName = beforeCase.StudentName,
                            GradRequestType = beforeCase.GradRequestType,
                            GradJobProfile = beforeCase.GradJobProfile,                            
                            EffectiveStartDate = beforeCase.EffectiveStartDate,
                            EffectiveEndDate = beforeCase.EffectiveEndDate,
                            Department = beforeCase.Department,
                            StepStipendAllowance = beforeCase.StepStipendAllowance,
                            Note = beforeCase.Note,
                            BudgetNumbers = beforeCase.BudgetNumbers
                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new HRServiceGradStudentTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = hrGradStudent.CaseID,
                            StudentName = hrGradStudent.StudentName,
                            GradRequestType = hrGradStudent.GradRequestType,
                            GradJobProfile = hrGradStudent.GradJobProfile,
                            EffectiveStartDate = hrGradStudent.EffectiveStartDate,
                            EffectiveEndDate = hrGradStudent.EffectiveEndDate,
                            Department = hrGradStudent.Department,
                            StepStipendAllowance = hrGradStudent.StepStipendAllowance,
                            Note = hrGradStudent.Note,
                            BudgetNumbers = hrGradStudent.BudgetNumbers
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(hrGradStudent);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HRServiceGradStudentExists(hrGradStudent.CaseID))
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
            return View(hrGradStudent);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.HRServiceGradStudentTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool HRServiceGradStudentExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}