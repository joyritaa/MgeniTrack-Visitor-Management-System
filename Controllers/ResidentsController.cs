using MgeniTrack.Models;
using MgeniTrack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            var residents = await _context.Residents
                .Include(r => r.User)
                .Include(r => r.Unit)
                .ToListAsync();

            return View(residents);
        }

        // GET: Create Resident
        public IActionResult Create(int? userId)
        {
            ViewData["UserId"] = new SelectList(
                _context.Users.Where(u => !_context.Residents.Any(r => r.UserId == u.UserId)),
                "UserId",
                "Email"
            );

            ViewData["UnitId"] = new SelectList(
                _context.Units.Where(u => !u.IsOccupied),
                "UnitId",
                "UnitNumber"
            );

            return View();
        }

        // POST: Create Resident
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResidentCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ReloadDropdowns();
                return View(model);
            }

            var unit = await _context.Units.FindAsync(model.UnitId);

            if (unit == null || unit.IsOccupied)
            {
                ModelState.AddModelError("", "Unit is not available.");
                ReloadDropdowns();
                return View(model);
            }

            var resident = new Resident
            {
                UserId = model.UserId,
                UnitId = model.UnitId,
                HouseNumber = unit.UnitNumber
            };

            unit.IsOccupied = true;

     
            if (resident == null)
            {
                ModelState.AddModelError("", "Invalid resident selected.");
                ReloadDropdowns();
                return View(resident);
            }

 
            _context.Residents.Add(resident);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Edit Resident
        public async Task<IActionResult> Edit(int id)
        {
            var resident = await _context.Residents
                .Include(r => r.User)
                .Include(r => r.Unit)
                .FirstOrDefaultAsync(r => r.ResidentId == id);

            if (resident == null) return NotFound();

            var vm = new ResidentEditViewModel
            {
                ResidentId = resident.ResidentId,
                UserId = resident.UserId,
                UserName = resident.User.Email,
                UnitId = (int)resident.UnitId,
                CurrentUnitNumber = resident.Unit.UnitNumber
            };

            ViewData["UnitId"] = new SelectList(
                _context.Units.Where(u => !u.IsOccupied || u.UnitId == resident.UnitId),
                "UnitId",
                "UnitNumber",
                resident.UnitId
            );

            return View(vm);
        }

        // POST: Edit Resident
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResidentEditViewModel vm)
        {
            if (id != vm.ResidentId) return NotFound();

            var resident = await _context.Residents
       .Include(r => r.Unit)
       .FirstOrDefaultAsync(r => r.ResidentId == id);

            if (resident == null) return NotFound();

            var newUnit = await _context.Units.FindAsync(vm.UnitId);

            if (newUnit == null)
            {
                ModelState.AddModelError("", "Invalid unit.");
                return View(vm);
            }

            // 🔥 IF UNIT CHANGED → UPDATE OCCUPANCY
            if (resident.UnitId != vm.UnitId)
            {
                // free old unit
                if (resident.Unit != null)
                    resident.Unit.IsOccupied = false;

                // occupy new unit
                if (newUnit.IsOccupied)
                {
                    ModelState.AddModelError("", "Selected unit is already occupied.");
                    return View(vm);
                }

                newUnit.IsOccupied = true;
                resident.UnitId = vm.UnitId;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

 // DELETE Resident + FREE UNIT
        public async Task<IActionResult> Delete(int id)
        {
            var resident = await _context.Residents
                .Include(r => r.Unit)
                .FirstOrDefaultAsync(r => r.ResidentId == id);

            if (resident != null)
            {
                if (resident.Unit != null)
                    resident.Unit.IsOccupied = false;

                _context.Residents.Remove(resident);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
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


        private void ReloadDropdowns(Resident? resident = null)
        {
            ViewData["UserId"] = new SelectList(
                _context.Users.Where(u => !_context.Residents.Any(r => r.UserId == u.UserId)),
                "UserId",
                "Email",
                resident?.UserId
            );

            ViewData["UnitId"] = new SelectList(
                _context.Units.Where(u => !u.IsOccupied || (resident != null && u.UnitId == resident.UnitId)),
                "UnitId",
                "UnitNumber",
                resident?.UnitId
            );
        }
        private bool ResidentExists(int id)
        {
            return _context.Residents.Any(e => e.ResidentId == id);
        }
    }
}