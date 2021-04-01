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
    public class HiringAffiliateFacultyController : Controller
    {

        private readonly ResolveCaseContext _context;

        public HiringAffiliateFacultyController(ResolveCaseContext context)
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
        public async Task<IActionResult> Create(int id, HiringAffiliateFaculty hrAffFaculty)
        {
            if (ModelState.IsValid)
            {
                HiringAffiliateFaculty newCase = new HiringAffiliateFaculty
                {
                    CaseID = id,
                    FacAffiliateTitle = hrAffFaculty.FacAffiliateTitle,
                    FacAffiliateCitizenStatus = hrAffFaculty.FacAffiliateCitizenStatus,
                    Name = hrAffFaculty.Name,
                    AffiliateStudentNetID = hrAffFaculty.AffiliateStudentNetID,
                    HireDate = hrAffFaculty.HireDate,
                    Department = hrAffFaculty.Department,
                    Note = hrAffFaculty.Note
                };
                _context.Add(newCase);
                await _context.SaveChangesAsync();
                var cid = id;
                return RedirectToAction("Details", "Cases", new { id = cid, area = "" });
                //return RedirectToAction("Index", "Home");
            }
            return View(hrAffFaculty);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
               
            }
            HiringAffiliateFaculty editCase = _context.HiringAffiliateFaculty.Find(id);
            if (editCase == null)
            {
                var newid = id;
                //return NotFound();
                return RedirectToAction("Create", "HiringAffiliateFaculty", new { id = newid });
            }
            return View(editCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseID,Name,AffiliateStudentNetID,FacAffiliateCitizenStatus,FacAffiliateTitle,HireDate,Department,Note")] HiringAffiliateFaculty hrAffFaculty)
        {
            if (id != hrAffFaculty.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<HiringAffiliateFaculty> beforeCases = _context.HiringAffiliateFaculty.Where(c => c.CaseID == id).AsNoTracking<HiringAffiliateFaculty>();
                    HiringAffiliateFaculty beforeCase = beforeCases.FirstOrDefault();
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
                        
                         var old_details = new HiringAffiliateFacultyTracking
                        {
                            Status = "old",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = beforeCase.CaseID,
                            HireDate = beforeCase.HireDate,
                            Department = beforeCase.Department,
                            Name = beforeCase.Name,
                            FacAffiliateTitle = beforeCase.FacAffiliateTitle,
                            Note = beforeCase.Note,
                            AffiliateStudentNetID=beforeCase.AffiliateStudentNetID,
                            FacAffiliateCitizenStatus=beforeCase.FacAffiliateCitizenStatus
                         };
                        _context.Add(old_details);
                        // Adding current details to tracking
                        var new_details = new HiringAffiliateFacultyTracking
                        {
                            Status = "new",
                            CaseAuditID = audit.CaseAuditID,
                            CaseID = hrAffFaculty.CaseID,
                            HireDate = hrAffFaculty.HireDate,
                            Department = hrAffFaculty.Department,
                            Name = hrAffFaculty.Name,                            
                            FacAffiliateTitle = hrAffFaculty.FacAffiliateTitle,
                            Note = hrAffFaculty.Note,
                            AffiliateStudentNetID = hrAffFaculty.AffiliateStudentNetID,
                            FacAffiliateCitizenStatus = hrAffFaculty.FacAffiliateCitizenStatus
                        };
                        _context.Add(new_details);
                        // Adding current details to actual Case Type entity
                        _context.Update(hrAffFaculty);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HiringAffiliateFacultyExists(hrAffFaculty.CaseID))
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
            return View(hrAffFaculty);
        }
        public IActionResult EditLog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var logs = _context.HiringAffiliateFacultyTracking.Where(p => p.CaseAuditID == id).ToList();
                ViewData["Logs"] = logs;
                return View();
            }
            catch (Exception)
            {
                var cid = Convert.ToInt32(id);
                return RedirectToAction("Details", "Cases", new { id = cid, area = "", err_message = "Can not fetch the edit log details currently!" });
            }

        }

        private bool HiringAffiliateFacultyExists(int id)
        {
            return _context.CaseAudit.Any(e => e.CaseAuditID == id);
        }

    }
}