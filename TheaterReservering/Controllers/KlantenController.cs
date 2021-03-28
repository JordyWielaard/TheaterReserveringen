using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheaterReservering.Data;
using TheaterReservering.Models;

namespace TheaterReservering.Controllers
{
    public class KlantenController : Controller
    {
        private readonly TheaterContext _context;
        HttpClient client = new HttpClient();
        

        public KlantenController(TheaterContext context)
        {
            _context = context;
            client.BaseAddress = new Uri("https://localhost:5001");
        }

        // GET: Klanten
        public async Task<IActionResult> Index()
        {
            var klantlist = await _context.Klanten.OrderBy(k => k.Naam).Include(k => k.Reserveringen).ToListAsync();

            foreach (var item in klantlist)
            {
                string apiUri = "/api/Bereken/Bereken/" + item.Id;
                HttpResponseMessage responseMessage = await client.GetAsync(apiUri);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsStringAsync();
                    item.Prijs = Convert.ToInt32(result);
                }
            }
            
            //Met include haal je alle reserveringen van de klant op
            return View(await _context.Klanten.OrderBy(k => k.Naam).Include(k => k.Reserveringen).ToListAsync());
        }

        // GET: Klanten/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.Klanten
                .FirstOrDefaultAsync(m => m.Id == id);
            if (klant == null)
            {
                return NotFound();
            }

            return View(klant);
        }

        // GET: Klanten/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klanten/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam,Adres,Woonplaats,Email")] Klant klant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klant);
        }

        // GET: Klanten/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.Klanten.FindAsync(id);
            if (klant == null)
            {
                return NotFound();
            }
            return View(klant);
        }

        // POST: Klanten/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naam,Adres,Woonplaats,Email")] Klant klant)
        {
            if (id != klant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlantExists(klant.Id))
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
            return View(klant);
        }

        // GET: Klanten/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.Klanten
                .FirstOrDefaultAsync(m => m.Id == id);
            if (klant == null)
            {
                return NotFound();
            }

            return View(klant);
        }

        // POST: Klanten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var klant = await _context.Klanten.FindAsync(id);
            _context.Klanten.Remove(klant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlantExists(int id)
        {
            return _context.Klanten.Any(e => e.Id == id);
        }
    }
}
