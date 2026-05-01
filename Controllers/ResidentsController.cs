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
    public class ResidentsController : Controller
    {
        private readonly MgenitrackContext _context;

        public ResidentsController(MgenitrackContext context)
        {
            _context = context;
        }

        // GET: Residents
        public async Task<IActionResult> Index()
        {
            var mgenitrackContext = _context.Residents.Include(r => r.User);
            return View(await mgenitrackContext.ToListAsync());
        }

        // GET: Residents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resident = await _context.Residents
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ResidentId == id);
            if (resident == null)
            {
                return NotFound();
            }

            return View(resident);
        }

        // GET: Residents/Create
        public async Task<IActionResult> Create()
        {
            var users = new SelectList(_context.Users, "UserId", "Email");
            var unoccupiedUnits = await _context.Units
                .Where(u => !u.IsOccupied)
                .OrderBy(u => u.Block)
                .ThenBy(u => u.UnitNumber)
                .ToListAsync();

            ViewData["UserId"] = users;
            ViewData["Units"] = unoccupiedUnits;
            return View();
        }

        // POST: Residents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResidentId,UserId,HouseNumber,UnitId")] Resident resident)
        {
            if (ModelState.IsValid)
            {
                // If UnitId is provided, mark the unit as occupied
                if (resident.UnitId.HasValue)
                {
                    var unit = await _context.Units.FindAsync(resident.UnitId.Value);
                    if (unit != null)
                    {
                        unit.IsOccupied = true;
                        _context.Update(unit);
                    }
                }

                _context.Add(resident);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var users = new SelectList(_context.Users, "UserId", "Email", resident.UserId);
            var unoccupiedUnits = await _context.Units
                .Where(u => !u.IsOccupied)
                .OrderBy(u => u.Block)
                .ThenBy(u => u.UnitNumber)
                .ToListAsync();

            ViewData["UserId"] = users;
            ViewData["Units"] = unoccupiedUnits;
            return View(resident);
        }

        // GET: Residents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resident = await _context.Residents.FindAsync(id);
            if (resident == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", resident.UserId);
            return View(resident);
        }

        // POST: Residents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResidentId,UserId,HouseNumber")] Resident resident)
        {
            if (id != resident.ResidentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resident);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResidentExists(resident.ResidentId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", resident.UserId);
            return View(resident);
        }

        // GET: Residents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resident = await _context.Residents
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ResidentId == id);
            if (resident == null)
            {
                return NotFound();
            }

            return View(resident);
        }

        // POST: Residents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resident = await _context.Residents.FindAsync(id);
            if (resident != null)
            {
                _context.Residents.Remove(resident);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResidentExists(int id)
        {
            return _context.Residents.Any(e => e.ResidentId == id);
        }
    }
}
