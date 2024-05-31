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
    public class Rezervacije
    {
        public Kupac kupac {  get; set; }
        public Vozilo vozilo { get; set; }
        public DateTime PocetakRezervacije{ get; set; }
        public DateTime KrajRezervacije { get; set; }
        public decimal cena { get; set; }

        public Rezervacije(Kupac kupac, Vozilo vozilo, DateTime PocetakRezervacije, DateTime KrajRezervacije)
        {
            this.kupac = kupac;
            this.vozilo = vozilo;
            this.PocetakRezervacije = PocetakRezervacije;
            this.KrajRezervacije = KrajRezervacije;
            
        }

        public static List<Rezervacije> ucitajRezervacije(string lokacijaRezervacije, List<Kupac> kupci, List<Vozilo> vozila)
        {
            List<Rezervacije> rezervacije = new List<Rezervacije>();
            using (var reader = new StreamReader(Path.Combine(lokacijaRezervacije, "C:\\Users\\BitComp PC\\source\\repos\\RentACarAgencija\\Csv fajlovi\\rezervacije.csv")))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>().ToList();
                foreach (var record in records)
                {
                    int kupacId = int.Parse(record.KupacId);
                    int voziloId = int.Parse(record.VoziloId);
                    DateTime PocetakRezervacije = DateTime.Parse(record.PocetakRezervacije);
                    DateTime KrajRezervacije = DateTime.Parse(record.KrajRezervacije);
                    

                    var kupac = kupci.FirstOrDefault(k => k.Id == kupacId);
                    var vozilo = vozila.FirstOrDefault(v => v.Id == voziloId);

                    if (kupac != null && vozilo != null)
                    {
                        rezervacije.Add(new Rezervacije(kupac, vozilo, PocetakRezervacije, KrajRezervacije));
                    }
                }
            }
            return rezervacije;
        }
       
    }
}
