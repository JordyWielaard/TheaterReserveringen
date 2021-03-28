using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterReservering.Data;
using TheaterReservering.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheaterReservering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BerekenController : ControllerBase
    {
        public TheaterContext _context { get; set; }

        public BerekenController(TheaterContext context)
        {
            _context = context;
        }

        [HttpGet("Bereken/{id}")]
        public int BerekenPrijs(int id)
        {
            var klantenreserveringen = _context.Reserveringen.Where(k => k.KlantId == id).ToList();
            if (klantenreserveringen == null)
            {
                return 0;
            }
            var totaalPrijs = 0;
            foreach (var reservering in klantenreserveringen)
            {
                var number = reservering.Naam[1];
                if (reservering.Naam.Contains("A"))  totaalPrijs += 40;
                else if (reservering.Naam.Contains("B")) totaalPrijs += 35;
                else if (reservering.Naam.Contains("C")) totaalPrijs += 30;
                else if (reservering.Naam.Contains("D")) totaalPrijs += 25;
                else if (reservering.Naam.Contains("E")) totaalPrijs += 20;
                if (reservering.Naam.Contains("3") || reservering.Naam.Contains("4")) totaalPrijs += 5;
            }               
            return totaalPrijs;
        }

        // GET: api/<BerekenController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BerekenController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BerekenController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BerekenController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BerekenController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
