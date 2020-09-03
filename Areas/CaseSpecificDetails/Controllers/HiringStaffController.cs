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
    public class HiringStaffController : Controller
    {

        private readonly ResolveCaseContext _context;

        public HiringStaffController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, [Bind("JobTitle,HireDate,SupOrg,StaffPositionType,StaffWorkerType,StaffHireReason," +
            "Supervised,WeeklyHours,RecruitmentRun,LimitedRecruitment,Consequences,Barriers,Justification,FTE,Note,PayRate,EmployeeReplaced,CandidateName," +
            "MulitpleBudgetExplain,Super,OvertimeEligible,EndDate,PostDate,ActualHireDate,ActualEndDate,HireeName,PosNum,WorkdayReq,UWHiresReq,BudgetNumbers,BudgetType," +
            "JobPostingTitle,CampusBox,Location,EmployeeNum,SupOrgManager,UWHiresContact,ActualEndDate")] HiringStaff hrStaff)
        {
            if (ModelState.IsValid)
            {
                hrStaff.CaseID = id;
                _context.Add(hrStaff);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(hrStaff);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HiringStaff editCase = _context.HiringStaff.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,JobTitle,HireDate,SupOrg,StaffPositionType,StaffWorkerType,StaffHireReason," +
            "Supervised,WeeklyHours,RecruitmentRun,LimitedRecruitment,Consequences,Barriers,Justification,FTE,Note,PayRate,EmployeeReplaced,CandidateName," +
            "MulitpleBudgetExplain,Super,OvertimeEligible,EndDate,PostDate,ActualHireDate,ActualEndDate,HireeName,PosNum,WorkdayReq,UWHiresReq,BudgetNumbers,BudgetType," +
            "JobPostingTitle,CampusBox,Location,EmployeeNum,SupOrgManager,UWHiresContact,ActualEndDate")] HiringStaff hrStaff)
        { 
            if (id != hrStaff.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<HiringStaff> beforeCases = _context.HiringStaff.Where(c => c.CaseID == id).AsNoTracking<HiringStaff>();
                    HiringStaff beforeCase = beforeCases.FirstOrDefault();
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

                        var old_details = new HiringStaffTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            HireDate = beforeCase.HireDate,
                            SupOrg = beforeCase.SupOrg,
                            PayRate = beforeCase.PayRate,
                            JobTitle = beforeCase.JobTitle,
                            StaffWorkerType = beforeCase.StaffWorkerType,
                            StaffHireReason = beforeCase.StaffHireReason,
                            BudgetNumbers=beforeCase.BudgetNumbers,
                            BudgetType = beforeCase.BudgetType,
                            FTE = beforeCase.FTE
                         };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new HiringStaffTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = hrStaff.CaseID,
                            HireDate = hrStaff.HireDate,
                            SupOrg = hrStaff.SupOrg,
                            PayRate = hrStaff.PayRate,
                            JobTitle = hrStaff.JobTitle,
                            StaffWorkerType = hrStaff.StaffWorkerType,
                            StaffHireReason = hrStaff.StaffHireReason,
                            BudgetNumbers = hrStaff.BudgetNumbers,
                            BudgetType = hrStaff.BudgetType,
                            FTE = hrStaff.FTE
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(hrStaff);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HiringStaffExists(hrStaff.CaseID))
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
            return View(hrStaff);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.HiringStaffTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool HiringStaffExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}