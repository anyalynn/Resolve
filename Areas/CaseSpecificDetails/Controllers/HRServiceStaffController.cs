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
    public class HRServiceStaffController : Controller
    {

        private readonly ResolveCaseContext _context;

        public HRServiceStaffController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, HRServiceStaff hrStaff)
        {
            if (ModelState.IsValid)
            {
                HRServiceStaff newCase = new HRServiceStaff
                {
                    CaseID = id,
                    EmployeeName = hrStaff.EmployeeName,
                    RequestType = hrStaff.RequestType,
                    BasePayChange = hrStaff.BasePayChange,
                    AllowanceChange = hrStaff.AllowanceChange,
                    TerminationReason = hrStaff.TerminationReason,
                    Offboarding = hrStaff.Offboarding,
                    LeaveWA = hrStaff.LeaveWA,
                    ClosePosition = hrStaff.ClosePosition,
                    Amount = hrStaff.Amount,
                    WorkerType = hrStaff.WorkerType,
                    EffectiveStartDate = hrStaff.EffectiveStartDate,
                    EffectiveEndDate = hrStaff.EffectiveEndDate,
                    SupOrg = hrStaff.SupOrg,
                    EmployeeEID = hrStaff.EmployeeEID,
                    Note = hrStaff.Note,
                    BudgetNumbers = hrStaff.BudgetNumbers
                };
                _context.Add(newCase);
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
            HRServiceStaff editCase = _context.HRServiceStaff.Find(id);
            if (editCase == null)
            {
                return NotFound();
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,EmployeeName,RequestType,BasePayChange,AllowanceChange,EffectiveStartDate,EffectiveEndDate,TerminationReason,Offboarding,Note,ClosePosition,LeaveWA,WorkerType,Amount,SupOrg,EmployeeEID,BudgetNumbers")] HRServiceStaff hrStaff)
        {
            if (id != hrStaff.CaseID)
            {
                return NotFound();
        }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<HRServiceStaff> beforeCases = _context.HRServiceStaff.Where(c => c.CaseID == id).AsNoTracking<HRServiceStaff>();
                    HRServiceStaff beforeCase = beforeCases.FirstOrDefault();
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

                        var old_details = new HRServiceStaffTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            EmployeeName = beforeCase.EmployeeName,
                            RequestType = beforeCase.RequestType,
                            BasePayChange = beforeCase.BasePayChange,
                            AllowanceChange = beforeCase.AllowanceChange,
                            TerminationReason = beforeCase.TerminationReason,
                            Offboarding = beforeCase.Offboarding,
                            LeaveWA = beforeCase.LeaveWA,
                            ClosePosition = beforeCase.ClosePosition,
                            Amount = beforeCase.Amount,
                            WorkerType = beforeCase.WorkerType,
                            EffectiveStartDate = beforeCase.EffectiveStartDate,
                            EffectiveEndDate = beforeCase.EffectiveEndDate,
                            SupOrg = beforeCase.SupOrg,
                            EmployeeEID = beforeCase.EmployeeEID,
                            Note = beforeCase.Note,
                            BudgetNumbers = beforeCase.BudgetNumbers
                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new HRServiceStaffTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = hrStaff.CaseID,                            
                            EmployeeName = hrStaff.EmployeeName,
                            RequestType = hrStaff.RequestType,
                            BasePayChange = hrStaff.BasePayChange,
                            AllowanceChange = hrStaff.AllowanceChange,
                            TerminationReason = hrStaff.TerminationReason,
                            Offboarding = hrStaff.Offboarding,
                            LeaveWA = hrStaff.LeaveWA,
                            ClosePosition = hrStaff.ClosePosition,
                            Amount = hrStaff.Amount,
                            WorkerType = hrStaff.WorkerType,
                            EffectiveStartDate = hrStaff.EffectiveStartDate,
                            EffectiveEndDate = hrStaff.EffectiveEndDate,
                            SupOrg = hrStaff.SupOrg,
                            EmployeeEID = hrStaff.EmployeeEID,
                            Note = hrStaff.Note,
                            BudgetNumbers = hrStaff.BudgetNumbers
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(hrStaff);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HRServiceStaffExists(hrStaff.CaseID))
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
        var logs = _context.HRServiceStaffTracking.Where(p => p.CaseAuditID == id).ToList();
        ViewData["Logs"] = logs;
        return View();
    }
    catch (Exception)
    {
        var cid = Convert.ToInt32(id);
        return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
    }

}

private bool HRServiceStaffExists(int id)
{
    return _context.CaseAudit.Any(e => e.CaseAuditID == id);
}

    }
}