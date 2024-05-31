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
    public class ZahteviZaRezervaciju
    {
        public Kupac Kupac { get; set; }
        public Vozilo Vozilo { get; set; }
        public DateTime DatumDolaska { get; set; }
        public DateTime PocetakRezervacije { get; set; }
        public int BrojDana { get; set; }

        public ZahteviZaRezervaciju(Kupac kupac, Vozilo vozilo, DateTime DatumDolaska, DateTime PocetakRezervacije, int BrojDana)
        {
            this.Kupac = kupac;
            this.Vozilo = vozilo;
            this.DatumDolaska = DatumDolaska;
            this.PocetakRezervacije = PocetakRezervacije;
            this.BrojDana = BrojDana;
        }


        public static List<ZahteviZaRezervaciju> UcitajZahtevZaRezervacije(string lokacijaZahteva, List<Kupac> kupci, List<Vozilo> vozila)
        {
            List<ZahteviZaRezervaciju> zahtevi = new List<ZahteviZaRezervaciju>();
            using (var reader = new StreamReader(Path.Combine(lokacijaZahteva, "C:\\Users\\BitComp PC\\source\\repos\\RentACarAgencija\\Csv fajlovi\\zahtevi_za_rezervacije.csv")))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>().ToList();
                foreach (var record in records)
                {
                    int kupacId = int.Parse(record.KupacId);
                    int voziloId = int.Parse(record.VoziloId);
                    DateTime datumDolaska = DateTime.Parse(record.DatumDolaska);
                    DateTime PocetakRezervacije = DateTime.Parse(record.PocetakRezervacije);
                    int brojDana = int.Parse(record.BrojDana);

                    var kupac = kupci.FirstOrDefault(k => k.Id == kupacId);
                    var vozilo = vozila.FirstOrDefault(v => v.Id == voziloId);

                    if (kupac != null && vozilo != null)
                    {
                        zahtevi.Add(new ZahteviZaRezervaciju(kupac, vozilo, datumDolaska, PocetakRezervacije, brojDana));
                    }
                }
            }
            return zahtevi;
        }


    }
            
 }
    

    

