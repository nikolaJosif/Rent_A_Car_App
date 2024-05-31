using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;


namespace RentACarAgencija
{
    public class Oprema 
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int Cena { get; set; }
        public int PovecavaCenu { get; set; }
        public Oprema(int Id, string Naziv, int Cena, int PovecavaCenu) 
        {
            this.Id = Id;
            this.Naziv = Naziv;
            this.Cena = Cena;
            this.PovecavaCenu = PovecavaCenu;
        }
          public static List<Oprema> ucitajOpremu(string lokacijaOpreme)
        {
            List<Oprema> oprema;
            using (var reader = new StreamReader(Path.Combine(lokacijaOpreme, "C:\\Users\\BitComp PC\\source\\repos\\RentACarAgencija\\Csv fajlovi\\oprema.csv")))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                oprema = csv.GetRecords<Oprema>().ToList();
            }
            return oprema;
        }
    }
}
