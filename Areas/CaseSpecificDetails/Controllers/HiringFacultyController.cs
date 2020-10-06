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
    public class HiringFacultyController : Controller
    {

        private readonly ResolveCaseContext _context;

        public HiringFacultyController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, [Bind("Justification,Consequences,Barriers,CandidateName,FacTitle,FacHireReason,BudgetType,BudgetNumbers,HireDate,Department,Note,Salary,AdminRole,EmployeeReplaced,FTE")] HiringFaculty hrFaculty)
        {
            if (ModelState.IsValid)
            {
                hrFaculty.CaseID = id;
                _context.Add(hrFaculty);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(hrFaculty);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HiringFaculty editCase = _context.HiringFaculty.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,FacTitle,Justification,Consequences,Barriers,HireDate,Department,FacHireReason,FTE,AdminRole,Note,Salary,EmployeeReplaced,CandidateName,BudgetNumbers,BudgetType")] HiringFaculty hrFaculty)
        {
            if (id != hrFaculty.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (hrFaculty.FacHireReason.ToString() != "Replace" )
                {
                    hrFaculty.EmployeeReplaced = null;
                }
                try
                {
                    IQueryable<HiringFaculty> beforeCases = _context.HiringFaculty.Where(c => c.CaseID == id).AsNoTracking<HiringFaculty>();
                    HiringFaculty beforeCase = beforeCases.FirstOrDefault();
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
                        
                         var old_details = new HiringFacultyTracking
                        {
                             Status = "old",
                             CaseAuditID = audit.CaseAuditID,
                             CaseID = beforeCase.CaseID,
                             HireDate = beforeCase.HireDate,
                             Department = beforeCase.Department,
                             Salary = beforeCase.Salary,
                             FacTitle = beforeCase.FacTitle,
                             FacHireReason = beforeCase.FacHireReason,
                             BudgetNumbers = beforeCase.BudgetNumbers,
                             BudgetType = beforeCase.BudgetType,
                             FTE = beforeCase.FTE,
                             AdminRole = beforeCase.AdminRole,
                             EmployeeReplaced = beforeCase.EmployeeReplaced,
                             Justification = beforeCase.Justification,
                             Barriers = beforeCase.Barriers,
                             Consequences = beforeCase.Consequences,
                             CandidateName = beforeCase.CandidateName,
                             Note = beforeCase.Note
                         };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new HiringFacultyTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = hrFaculty.CaseID,
                            HireDate = hrFaculty.HireDate,
                            Department = hrFaculty.Department,
                            Salary = hrFaculty.Salary,
                            FacTitle = hrFaculty.FacTitle,
                            FacHireReason = hrFaculty.FacHireReason,
                            BudgetNumbers = hrFaculty.BudgetNumbers,
                            BudgetType = hrFaculty.BudgetType,
                            FTE = hrFaculty.FTE,
                            AdminRole = hrFaculty.AdminRole,
                            EmployeeReplaced = hrFaculty.EmployeeReplaced,
                            Justification = hrFaculty.Justification,
                            Barriers = hrFaculty.Barriers,
                            Consequences = hrFaculty.Consequences,
                            CandidateName = hrFaculty.CandidateName,
                            Note = hrFaculty.Note
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(hrFaculty);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HiringFacultyExists(hrFaculty.CaseID))
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
            return View(hrFaculty);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.HiringFacultyTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool HiringFacultyExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}