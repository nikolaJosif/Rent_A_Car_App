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
    public enum Clanarina
    {
        VIP,
        Basic
    }
    public class Kupac
    {
        public int Id { get; set; }
        public String Ime { get; set; }
        public String Prezime { get; set; }
        public int Budzet { get; set; } 
        
        public Clanarina Clanarina { get; set; }

        public Kupac (string Ime, string Prezime, int Budzet, Clanarina Clanarina, int Id)
        {
            this.Id = Id;
            this.Ime = Ime;
            this.Prezime = Prezime;
            this.Budzet = Budzet;
            this.Clanarina = Clanarina;
            
        }

        public int cenaSaPopustom(Kupac kupac, int cena)
        {
            double popust = 0;

            if (kupac.Clanarina == Clanarina.Basic)
            {
                popust = 0.10;
            }
            else if (kupac.Clanarina == Clanarina.VIP)
            {
                popust = 0.20;
            }
            else
            {
                popust = 0;
            }

            double cenaSaPopustomDouble = cena * (1 - popust);

            
            int cenaSaPopustomInt = (int)Math.Round(cenaSaPopustomDouble);

            return cenaSaPopustomInt;
            
        }
        public static List<Kupac>ucitajKupca(string lokacijaKupca)
        {
            List<Kupac> kupac = new List<Kupac>();
            using(var readers = new StreamReader(Path.Combine(lokacijaKupca, "C:\\Users\\BitComp PC\\source\\repos\\RentACarAgencija\\Csv fajlovi\\kupci.csv"))) 
            using(var csv = new CsvReader(readers, CultureInfo.InvariantCulture))
            {
                if (!csv.Read())
                {
                    Console.WriteLine("Fajl je prazan.");
                    return kupac;
                }

                csv.ReadHeader();

                while (csv.Read())
                {
                    try
                    {
                        int id = csv.GetField<int>("Id");
                        string ime = csv.GetField<string>("Ime");
                        string prezime = csv.GetField<string>("Prezime");
                        int budzet = csv.GetField<int>("Budzet");
                        string clanarinaText = csv.GetField<string>("Clanarina");

                        if (!string.IsNullOrEmpty(clanarinaText))
                        {
                            Clanarina clanarina;
                            if (Enum.TryParse(clanarinaText, out clanarina))
                            {
                                kupac.Add(new Kupac(ime, prezime, id, clanarina, budzet));
                            }
                            else
                            {
                                Console.WriteLine($"Nije moguće konvertovati tekst '{clanarinaText}' u enum za kupca sa ID {id}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Kupac {ime} nema clanskukartu");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            return kupac;
        }





    }
}

