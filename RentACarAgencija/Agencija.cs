using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarAgencija
{
    public class Agencija
    {
        public List<Kupac> Kupci { get; set; } = new List<Kupac>();
        public List<Vozilo> Vozila { get; set; } = new List<Vozilo>();
        public List<Rezervacije> Rezervacije { get; set; } = new List<Rezervacije>();
        public List<ZahteviZaRezervaciju> ZahteviZaRezervaciju { get; set; } = new List<ZahteviZaRezervaciju>();

       

        public bool DaLiJeVoziloDostupno(int voziloId, DateTime periodOd, DateTime periodDo)
        {
            foreach (var rezervacija in Rezervacije)
            {
                if (rezervacija.vozilo.Id == voziloId && !(periodDo <= rezervacija.PocetakRezervacije || periodOd >= rezervacija.KrajRezervacije))
                {
                    return false;
                }
            }
            return true;
        }

        public void SacuvajRezervacije(List<Rezervacije> rezervacije)
        {
            string lokacijaDirektorijuma = "C:\\Users\\BitComp PC\\source\\repos\\RentACarAgencija\\Csv fajlovi";
            string lokacijaFajla = Path.Combine(lokacijaDirektorijuma, "nove_rezervacije.csv");

            if (!Directory.Exists(lokacijaDirektorijuma))
            {
                Directory.CreateDirectory(lokacijaFajla);
            }
            using (var writer = new StreamWriter(lokacijaFajla))
                using(var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(rezervacije);
            }
        }

        public void SimulirajIznajmljivanje()
        {
            var neuspesneRezervacije = new List<string>();
            var uspesneRezervacije = new List<Rezervacije>();

            
            var sortiraniZahtevi = ZahteviZaRezervaciju.OrderByDescending(z => z.Kupac.Clanarina).ToList();

            foreach (var zahtev in sortiraniZahtevi)
            {
                var kupac = Kupci.FirstOrDefault(k => k.Id == zahtev.Kupac.Id);
                var vozilo = Vozila.FirstOrDefault(v => v.Id == zahtev.Vozilo.Id);

                if (kupac == null || vozilo == null)
                {
                    neuspesneRezervacije.Add($"Kupac ID: {zahtev.Kupac.Id} ili Vozilo ID: {zahtev.Vozilo.Id} ne postoji.");
                    continue;
                }

                decimal osnovnaCena = vozilo.izracunajCenu() * zahtev.BrojDana;
                int cenaSaPopustomVar = kupac.cenaSaPopustom(kupac, vozilo.izracunajCenu());

                if (kupac.Budzet < cenaSaPopustomVar)
                {
                    neuspesneRezervacije.Add($"{kupac.Ime} {kupac.Prezime} nema dovoljno budžeta.");
                    continue;
                }

                if (!DaLiJeVoziloDostupno(zahtev.Vozilo.Id, zahtev.DatumDolaska, zahtev.PocetakRezervacije))
                {
                    neuspesneRezervacije.Add($"{kupac.Ime} {kupac.Prezime} - vozilo nije dostupno u traženom periodu.");
                    continue;
                }

                
                kupac.Budzet -= cenaSaPopustomVar;
                var novaRezervacija = new Rezervacije(kupac, vozilo, zahtev.DatumDolaska, zahtev.PocetakRezervacije)
                {
                    cena = cenaSaPopustomVar
                };
                uspesneRezervacije.Add(novaRezervacija);
                Rezervacije.Add(novaRezervacija);
            }

            
            foreach (var neuspesna in neuspesneRezervacije)
            {
                Console.WriteLine(neuspesna);
            }


            SacuvajRezervacije(uspesneRezervacije);
        }

    }
}
