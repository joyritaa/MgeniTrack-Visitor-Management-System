using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MgeniTrack.Models;

namespace MgeniTrack.Controllers
{
    public class VisitsController : Controller
    {
        private readonly MgenitrackContext _context;

        public VisitsController(MgenitrackContext context)
        {
            _context = context;
        }

        // GET: Visits
        public async Task<IActionResult> Index()
        {
            var mgenitrackContext = _context.Visits
                .Include(v => v.CheckedInByNavigation)
                .Include(v => v.CheckedOutByNavigation)
                .Include(v => v.Invitation)
                .Include(v => v.Visitor);
            return View(await mgenitrackContext.ToListAsync());
        }

        // GET: Visits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .Include(v => v.CheckedInByNavigation)
                .Include(v => v.CheckedOutByNavigation)
                .Include(v => v.Invitation)
                .Include(v => v.Visitor)
                .FirstOrDefaultAsync(m => m.VisitId == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // GET: Visits/Create
        public IActionResult Create()
        {
            ViewData["CheckedInBy"] = new SelectList(_context.Users, "UserId", "UserId");
            ViewData["CheckedOutBy"] = new SelectList(_context.Users, "UserId", "UserId");
            ViewData["InvitationId"] = new SelectList(_context.VisitorInvitations, "InvitationId", "InvitationId");
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "VisitorId", "VisitorId");
            return View();
        }

        // POST: Visits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitId,VisitorId,CheckedInBy,CheckedOutBy,HouseNumber,PurposeOfVisit,CarRegistration,NumberOfOccupants,TimeIn,TimeOut,VisitStatus,QrCode,VisitDuration,CreatedAt,InvitationId,CheckInMethod")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CheckedInBy"] = new SelectList(_context.Users, "UserId", "UserId", visit.CheckedInBy);
            ViewData["CheckedOutBy"] = new SelectList(_context.Users, "UserId", "UserId", visit.CheckedOutBy);
            ViewData["InvitationId"] = new SelectList(_context.VisitorInvitations, "InvitationId", "InvitationId", visit.InvitationId);
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "VisitorId", "VisitorId", visit.VisitorId);
            return View(visit);
        }

        // GET: Visits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits.FindAsync(id);
            if (visit == null)
            {
                return NotFound();
            }
            ViewData["CheckedInBy"] = new SelectList(_context.Users, "UserId", "UserId", visit.CheckedInBy);
            ViewData["CheckedOutBy"] = new SelectList(_context.Users, "UserId", "UserId", visit.CheckedOutBy);
            ViewData["InvitationId"] = new SelectList(_context.VisitorInvitations, "InvitationId", "InvitationId", visit.InvitationId);
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "VisitorId", "VisitorId", visit.VisitorId);
            return View(visit);
        }

        // POST: Visits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VisitId,VisitorId,CheckedInBy,CheckedOutBy,HouseNumber,PurposeOfVisit,CarRegistration,NumberOfOccupants,TimeIn,TimeOut,VisitStatus,QrCode,VisitDuration,CreatedAt,InvitationId,CheckInMethod")] Visit visit)
        {
            if (id != visit.VisitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitExists(visit.VisitId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CheckedInBy"] = new SelectList(_context.Users, "UserId", "UserId", visit.CheckedInBy);
            ViewData["CheckedOutBy"] = new SelectList(_context.Users, "UserId", "UserId", visit.CheckedOutBy);
            ViewData["InvitationId"] = new SelectList(_context.VisitorInvitations, "InvitationId", "InvitationId", visit.InvitationId);
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "VisitorId", "VisitorId", visit.VisitorId);
            return View(visit);
        }

        // GET: Visits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .Include(v => v.CheckedInByNavigation)
                .Include(v => v.CheckedOutByNavigation)
                .Include(v => v.Invitation)
                .Include(v => v.Visitor)
                .FirstOrDefaultAsync(m => m.VisitId == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visit = await _context.Visits.FindAsync(id);
            if (visit != null)
            {
                _context.Visits.Remove(visit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitExists(int id)
        {
            return _context.Visits.Any(e => e.VisitId == id);
        }
    }
}
