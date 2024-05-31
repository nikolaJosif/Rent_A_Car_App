using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarAgencija
{
    public class Simulacija
    {
        static void Main(string[] args)
        {
            var lokacijaVozila = "C:\\Users\\BitComp PC\\source\\repos\\RentACarAgencija\\Csv fajlovi\\vozila.csv";
            var lokacijaVozOp = "C:\\Users\\BitComp PC\\source\\repos\\RentACarAgencija\\Csv fajlovi\\vozilo_oprema.csv";
            var lokacijaOpreme = "C:\\Users\\BitComp PC\\source\\repos\\RentACarAgencija\\Csv fajlovi\\oprema.csv";

            var vozila = UcitavanjeFajlova.ucitajVzolio(lokacijaVozila, lokacijaVozOp, lokacijaOpreme);

            var lokacijaKupca = "C:\\Users\\BitComp PC\\source\\repos\\RentACarAgencija\\Csv fajlovi\\kupci.csv";
            var kupci = Kupac.ucitajKupca(lokacijaKupca);

            var lokacijaRezervacije = "C:\\Users\\BitComp PC\\source\\repos\\RentACarAgencija\\Csv fajlovi\\rezervacije.csv";
            var rezervacije = Rezervacije.ucitajRezervacije(lokacijaRezervacije, kupci, vozila);

            var lokacijaZahteva = "C:\\Users\\BitComp PC\\source\\repos\\RentACarAgencija\\Csv fajlovi\\zahtevi_za_rezervacije.csv";
            var zahtev = ZahteviZaRezervaciju.UcitajZahtevZaRezervacije(lokacijaZahteva, kupci, vozila);

            Console.WriteLine("Podaci o vozilima i njihovim cenama: ");
            foreach(var vozilo in  vozila) 
            {
                Console.WriteLine($"Vozilo id: {vozilo.Id},\nMarka: {vozilo.Marka}, \nModel: {vozilo.Model}, \nPotrošnja: {vozilo.Potrosnja}, \nCena: {vozilo.izracunajCenu()}");
            }

            Console.WriteLine("\nPodaci o korisnicima: ");
            foreach (var kupac in kupci)
            {
                
                Console.WriteLine($"Korisnik ID: {kupac.Id}, \nIme: {kupac.Ime}, \nPrezime: {kupac.Prezime}, \nBudžet: {kupac.Budzet}," +
                    $" \nČlanarina: {kupac.Clanarina} ");
            }
            
            var agencija = new Agencija
            {
                Kupci = kupci,
                Vozila = vozila,
                ZahteviZaRezervaciju = zahtev
            };
            agencija.SimulirajIznajmljivanje();
        }

        
    }
}
