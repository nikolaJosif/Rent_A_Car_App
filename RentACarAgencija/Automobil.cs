using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace RentACarAgencija
{
   
    public class Automobil : Vozilo
    {

        public double Kilometraza { get; set; }
        
        public int pocetnaCena { get; } = 200;
        public List<Oprema> opremaVozila { get; set; } = new List<Oprema>();



        public Automobil(int Id, string Model, double Potrosnja, double Kilometraza, string Marka) : base(Id, Model, Potrosnja, Marka)
        {
            this.Kilometraza = Kilometraza;
        }
        public override int izracunajCenu()
        {
            int cena = (int)pocetnaCena; 
            switch (Marka)
            {
                case "Mercedes":
                    if (Kilometraza < 5000) { cena += (int)(pocetnaCena * 0.06m); }
                    if (Tip == Tip.Limuzina) { cena += (int)(pocetnaCena * 0.07m); }
                    if (Tip == Tip.Hecbek && Kilometraza > 100000) { cena -= (int)(pocetnaCena * 0.03m); }
                    break;

                case "BMW":
                    if (Potrosnja < 7) { cena += (int)(pocetnaCena * 0.15m); }
                    if (Potrosnja > 7) { cena -= (int)(pocetnaCena * 0.10m); }
                    else { cena -= (int)(pocetnaCena * 0.15m); }
                    break;

                case "Peugeot":
                    if (Tip == Tip.Limuzina) { cena += (int)(pocetnaCena * 0.15m); }
                    if (Tip == Tip.Karavan) { cena += (int)(pocetnaCena * 0.20m); }
                    else { cena -= (int)(pocetnaCena * 0.05m); }
                    break;
            }
            foreach (var oprema in opremaVozila)
            {
                if (oprema.PovecavaCenu == 1)
                {
                    cena += oprema.Cena;
                }
                else
                {
                    cena -= oprema.Cena;
                }
            }

            return cena;
        }


    }
}

