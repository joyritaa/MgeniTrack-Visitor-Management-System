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
    public class VisitorInvitationsController : Controller
    {
        private readonly MgenitrackContext _context;

        public VisitorInvitationsController(MgenitrackContext context)
        {
            _context = context;
        }

        // GET: VisitorInvitations
        public async Task<IActionResult> Index()
        {
            var mgenitrackContext = _context.VisitorInvitations.Include(v => v.Resident);
            return View(await mgenitrackContext.ToListAsync());
        }

        // GET: VisitorInvitations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitorInvitation = await _context.VisitorInvitations
                .Include(v => v.Resident)
                .FirstOrDefaultAsync(m => m.InvitationId == id);
            if (visitorInvitation == null)
            {
                return NotFound();
            }

            return View(visitorInvitation);
        }

        // GET: VisitorInvitations/Create
        public IActionResult Create()
        {
            ViewData["ResidentId"] = new SelectList(_context.Residents, "ResidentId", "ResidentId");
            return View();
        }

        // POST: VisitorInvitations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvitationId,ResidentId,VisitorName,VisitorPhone,VisitorEmail,PurposeOfVisit,ExpectedDate,InvitationToken,QrCodePath,VisitStatus,CreatedAt")] VisitorInvitation visitorInvitation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visitorInvitation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResidentId"] = new SelectList(_context.Residents, "ResidentId", "ResidentId", visitorInvitation.ResidentId);
            return View(visitorInvitation);
        }

        // GET: VisitorInvitations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitorInvitation = await _context.VisitorInvitations.FindAsync(id);
            if (visitorInvitation == null)
            {
                return NotFound();
            }
            ViewData["ResidentId"] = new SelectList(_context.Residents, "ResidentId", "ResidentId", visitorInvitation.ResidentId);
            return View(visitorInvitation);
        }

        // POST: VisitorInvitations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvitationId,ResidentId,VisitorName,VisitorPhone,VisitorEmail,PurposeOfVisit,ExpectedDate,InvitationToken,QrCodePath,VisitStatus,CreatedAt")] VisitorInvitation visitorInvitation)
        {
            if (id != visitorInvitation.InvitationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitorInvitation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitorInvitationExists(visitorInvitation.InvitationId))
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
            ViewData["ResidentId"] = new SelectList(_context.Residents, "ResidentId", "ResidentId", visitorInvitation.ResidentId);
            return View(visitorInvitation);
        }

        // GET: VisitorInvitations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitorInvitation = await _context.VisitorInvitations
                .Include(v => v.Resident)
                .FirstOrDefaultAsync(m => m.InvitationId == id);
            if (visitorInvitation == null)
            {
                return NotFound();
            }

            return View(visitorInvitation);
        }

        // POST: VisitorInvitations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visitorInvitation = await _context.VisitorInvitations.FindAsync(id);
            if (visitorInvitation != null)
            {
                _context.VisitorInvitations.Remove(visitorInvitation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitorInvitationExists(int id)
        {
            return _context.VisitorInvitations.Any(e => e.InvitationId == id);
        }
    }
}
