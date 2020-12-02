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
    public class CPPaymentRequestController : Controller
    {

        private readonly ResolveCaseContext _context;

        public CPPaymentRequestController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, CPPaymentRequest cpPayment)
        {
            if (ModelState.IsValid)
            {
                CPPaymentRequest newCase = new CPPaymentRequest
                {
                    CaseID = id,
                    RequesterName = cpPayment.RequesterName,
                    DueDate = cpPayment.DueDate,
                    Payee = cpPayment.Payee,
                    Amount = cpPayment.Amount,
                    Explanation= cpPayment.Explanation,
                    Note = cpPayment.Note,
                    BudgetNumber = cpPayment.BudgetNumber
                 };
                _context.Add(newCase);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(cpPayment);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CPPaymentRequest editCase = _context.CPPaymentRequest.Find(id);
            if (editCase == null)
            {
                var newid = id;
                //return NotFound();
                return RedirectToAction("Create", "CPPaymentRequest", new { id = newid });
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,RequesterName,DueDate,Payee,Amount,BudgetNumber,Explanation,Note")] CPPaymentRequest cpPayment)
        {
            if (id != cpPayment.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<CPPaymentRequest> beforeCases = _context.CPPaymentRequest.Where(c => c.CaseID == id).AsNoTracking<CPPaymentRequest>();
                    CPPaymentRequest beforeCase = beforeCases.FirstOrDefault();
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

                        var old_details = new CPPaymentRequestTracking
                        {   Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            RequesterName = beforeCase.RequesterName,
                            DueDate = beforeCase.DueDate,
                            Payee = beforeCase.Payee,
                            Amount = beforeCase.Amount,
                            Explanation = beforeCase.Explanation,
                            Note = beforeCase.Note,
                            BudgetNumber = beforeCase.BudgetNumber,
                        };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new CPPaymentRequestTracking
                        {   Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = cpPayment.CaseID,
                            RequesterName = cpPayment.RequesterName,
                            DueDate = cpPayment.DueDate,
                            Payee = cpPayment.Payee,
                            Amount = cpPayment.Amount,
                            Explanation = cpPayment.Explanation,
                            Note = cpPayment.Note,
                            BudgetNumber = cpPayment.BudgetNumber,
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(cpPayment);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CPPaymentRequestExists(cpPayment.CaseID))
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
            return View(cpPayment);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.CPPaymentRequestTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool CPPaymentRequestExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}