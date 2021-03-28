using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterReservering.Data;

namespace TheaterReservering.Models
{
    public static class SeedData
    {

        public static void init(TheaterContext context)
        {
            AddKlant(context, "Pieter Baan", "Dorpstraat 1", "Nudrop", "pieterbaan@centrum.nl");
            AddKlant(context, "Sjoukje van Leeuwen", "Brink 32", "Dwingloo", "leewerik@live.nl");
            AddKlant(context, "Maria van der Voorst", "Engergieweg 23", "Eindhoven", "matia@ikke.com");
            AddKlant(context, "Ad Veenstra", "Amesfoorseweg 12A", "Hilversum", "venie@utc.org");
            AddPlaatsen("A", context);
            AddPlaatsen("B", context);
            AddPlaatsen("C", context);
            AddPlaatsen("D", context);
            AddPlaatsen("E", context);

        }
        public static void AddKlant(TheaterContext context, string naam, string adres, string woonplaats, string email)
        {
            var check = context.Klanten.FirstOrDefault(Klant => Klant.Naam == naam);
            if (check == null)
            {
                context.Klanten.Add(new Klant()
                {
                    Naam = naam,
                    Adres = adres,
                    Woonplaats = woonplaats,
                    Email = email
                });
                context.SaveChanges();
            }
        }

        public static void AddPlaatsen(string letter, TheaterContext context)
        {
            var check = context.Reserveringen.FirstOrDefault(reservering => reservering.Naam.Contains(letter));
            if (check == null)
            {
                for (int i = 1; i < 7; i++)
                {
                    string plek = letter + i.ToString();
                    context.Reserveringen.Add(new Reservering()
                    {
                        Naam = plek,
                        Bezet = false
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
