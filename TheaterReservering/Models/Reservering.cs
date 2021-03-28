using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheaterReservering.Models
{
    public class Reservering
    {
        public int Id { get; set; }
        [Required]
        public string Naam { get; set; }
        public int? KlantId { get; set; }
        public bool Bezet { get; set; }
        
    }
}
