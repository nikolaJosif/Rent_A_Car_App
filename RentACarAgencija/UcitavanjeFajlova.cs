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
    public static class UcitavanjeFajlova
    {
        public static List<Vozilo> ucitajVzolio(string lokacijaVozilo, string lokacijaVozOp, string lokacijaOpreme)
        {
            var vozila = new List<Vozilo>();
            var oprema = Oprema.ucitajOpremu(lokacijaOpreme);
            var voziloOpremaLista = UcitajVoziloOpremu(lokacijaVozOp);

            using (var readers = new StreamReader(Path.Combine(lokacijaVozilo, "C:\\Users\\BitComp PC\\source\\repos\\RentACarAgencija\\Csv fajlovi\\vozila.csv")))
            using (var csv = new CsvReader(readers, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>().ToList();
                foreach (var record in records)
                {
                    string TipVozila = record.TipVozila;
                    if (TipVozila == "Automobil")
                    {
                        Automobil automobil = new Automobil(
                            int.Parse(record.Id),
                            record.Model,
                            double.Parse(record.Potrosnja),
                            double.Parse(record.Kilometraza),
                            record.Marka

                            );
                        var opremaIds = voziloOpremaLista
                                .Where(vo => vo.VoziloId == int.Parse(record.Id))
                                .Select(vo => vo.OpremaId)
                                .ToList();

                        foreach (var opremaId in opremaIds)
                        {
                            var opremaItem = oprema.Find(o => o.Id == opremaId);
                            if (opremaItem != null)
                            {
                                automobil.opremaVozila.Add(opremaItem);
                            }
                        }

                        vozila.Add(automobil);

                    } else if(TipVozila == "Motor")
                        {
                        vozila.Add(new Motor(
                            int.Parse(record.Id),
                            record.Model,
                            record.Marka,
                            double.Parse(record.Potrosnja),
                            double.Parse(record.Kubikaza),
                            double.Parse(record.Snaga)
                         
                            ));
                        }
                }
            }
            return vozila;
        }

        public static List<OpremaVozilo> UcitajVoziloOpremu(string lokacijaVozOp)
        {
            var voziloOpremaLista = new List<OpremaVozilo>();

            using (var reader = new StreamReader(Path.Combine(lokacijaVozOp, "C:\\Users\\BitComp PC\\source\\repos\\RentACarAgencija\\Csv fajlovi\\vozilo_oprema.csv")))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<OpremaVozilo>().ToList();
                voziloOpremaLista.AddRange(records);
            }

            return voziloOpremaLista;
        }

    }
}
