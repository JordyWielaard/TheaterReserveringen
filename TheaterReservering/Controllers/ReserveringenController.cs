using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheaterReservering.Data;
using TheaterReservering.Models;

namespace TheaterReservering.Controllers
{
    public class ReserveringenController : Controller
    {
        private readonly TheaterContext _context;

        public ReserveringenController(TheaterContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> EditWithKlantId(int? id)
        {
            var klant = await _context.Klanten.FirstOrDefaultAsync(k => k.Id == id);
            if (klant == null)
            {
                return NotFound();
            }
            ViewData["klantId"] = klant.Id;
            ViewData["klant"] = $"{klant.Naam}, {klant.Email}, {klant.Woonplaats}";

            return View(await _context.Reserveringen.ToListAsync());
        }

        // POST: Reserveringen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWithKlantId(int id, int[] reserveringId)
        {
            if (ModelState.IsValid)
            {
                var reserveringenklant = await _context.Reserveringen.Where(r => r.KlantId == id).ToListAsync();
                foreach (var item in reserveringenklant)
                {
                    item.Bezet = false;
                    item.KlantId = null;
                    _context.Update(item);
                    _context.SaveChanges();
                }
                foreach(var item in reserveringId)
                {
                    var reservering = await _context.Reserveringen.FindAsync(item);
                    reservering.Bezet = true;
                    reservering.KlantId = id;
                    _context.Update(reservering);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Index), "Klanten");
        }

        // GET: Reserveringen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reserveringen.ToListAsync());
        }

        // GET: Reserveringen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservering = await _context.Reserveringen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservering == null)
            {
                return NotFound();
            }

            return View(reservering);
        }

        // GET: Reserveringen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reserveringen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam,KlantId,Bezet")] Reservering reservering)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservering);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservering);
        }

        // GET: Reserveringen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservering = await _context.Reserveringen.FindAsync(id);
            if (reservering == null)
            {
                return NotFound();
            }
            return View(reservering);
        }

        // POST: Reserveringen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naam,KlantId,Bezet")] Reservering reservering)
        {
            if (id != reservering.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservering);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReserveringExists(reservering.Id))
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
            return View(reservering);
        }

        // GET: Reserveringen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservering = await _context.Reserveringen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservering == null)
            {
                return NotFound();
            }

            return View(reservering);
        }

        // POST: Reserveringen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservering = await _context.Reserveringen.FindAsync(id);
            _context.Reserveringen.Remove(reservering);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReserveringExists(int id)
        {
            return _context.Reserveringen.Any(e => e.Id == id);
        }
    }
}
