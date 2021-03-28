using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheaterReservering.Models
{
    public class Klant
    {
        public int Id { get; set; }
        [MinLength(2)]
        public string Naam { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required]
        public string Woonplaats { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [NotMapped]
        public int Prijs { get; set; }
        //je hebt ook ICollection, IEnumerable en IList
        //in dit geval IList, omdat er moet gekeken worden naar de index [i]
        public IList<Reservering> Reserveringen { get; set; }
    }
}
