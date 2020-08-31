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
    public class HRServiceFacultyController : Controller
    {

        private readonly ResolveCaseContext _context;

        public HRServiceFacultyController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, HRServiceFaculty hrFaculty)
        {
            if (ModelState.IsValid)
            {
                HRServiceFaculty newCase = new HRServiceFaculty
                {
                    CaseID = id,

                    EmployeeName = hrFaculty.EmployeeName,
                    FacRequestType = hrFaculty.FacRequestType,
                    EffectiveStartDate = hrFaculty.EffectiveStartDate,
                    EffectiveEndDate = hrFaculty.EffectiveEndDate,
                    SupOrg = hrFaculty.SupOrg,
                    Department = hrFaculty.Department,
                    Salary = hrFaculty.Salary,
                    Amount = hrFaculty.Amount,
                    CurrentFTE = hrFaculty.CurrentFTE,
                    ProposedFTE = hrFaculty.ProposedFTE,
                    TerminationReason = hrFaculty.TerminationReason,
                    Offboarding = hrFaculty.Offboarding,
                    ClosePosition = hrFaculty.ClosePosition,
                    LeaveWA = hrFaculty.LeaveWA,
                    EmployeeEID = hrFaculty.EmployeeEID,
                    BudgetNumbers = hrFaculty.BudgetNumbers,
                    JobTitle = hrFaculty.JobTitle,
                    Note = hrFaculty.Note
                };
                _context.Add(newCase);
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
            HRServiceFaculty editCase = _context.HRServiceFaculty.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,EmployeeName,FacRequestType,EffectiveStartDate,EffectiveEndDate,TerminationReason,Offboarding,Note,ClosePosition,LeaveWA,Salary,Amount,SupOrg,Department,CurrentFTE,ProposedFTE,JobTitle,EmployeeEID,BudgetNumbers")] HRServiceFaculty hrFaculty)

        {
            if (id != hrFaculty.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<HRServiceFaculty> beforeCases = _context.HRServiceFaculty.Where(c => c.CaseID == id).AsNoTracking<HRServiceFaculty>();
                    HRServiceFaculty beforeCase = beforeCases.FirstOrDefault();
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

                        var old_details = new HRServiceFacultyTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            EmployeeName = beforeCase.EmployeeName,
                            FacRequestType = beforeCase.FacRequestType,
                            TerminationReason = beforeCase.TerminationReason,
                            Offboarding = beforeCase.Offboarding,
                            LeaveWA = beforeCase.LeaveWA,
                            ClosePosition = beforeCase.ClosePosition,
                            Amount = beforeCase.Amount,
                            EffectiveStartDate = beforeCase.EffectiveStartDate,
                            EffectiveEndDate = beforeCase.EffectiveEndDate,
                            Department = beforeCase.Department,
                            EmployeeEID = beforeCase.EmployeeEID,
                            Note = beforeCase.Note,
                            BudgetNumbers = beforeCase.BudgetNumbers
                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new HRServiceFacultyTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = hrFaculty.CaseID,
                            EmployeeName = hrFaculty.EmployeeName,
                            FacRequestType = hrFaculty.FacRequestType,
                            TerminationReason = hrFaculty.TerminationReason,
                            Offboarding = hrFaculty.Offboarding,
                            LeaveWA = hrFaculty.LeaveWA,
                            ClosePosition = hrFaculty.ClosePosition,
                            Amount = hrFaculty.Amount,
                            EffectiveStartDate = hrFaculty.EffectiveStartDate,
                            EffectiveEndDate = hrFaculty.EffectiveEndDate,
                            Department = hrFaculty.Department,
                            EmployeeEID = hrFaculty.EmployeeEID,
                            Note = hrFaculty.Note,
                            BudgetNumbers = hrFaculty.BudgetNumbers
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(hrFaculty);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HRServiceFacultyExists(hrFaculty.CaseID))
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
                var logs = _context.HRServiceFacultyTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool HRServiceFacultyExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}