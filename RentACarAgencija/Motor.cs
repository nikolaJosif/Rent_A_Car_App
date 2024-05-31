using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarAgencija
{
   
    public class Motor : Vozilo
    {
        public double Kubikaza {  get; set; }
        public double Snaga { get; set; }
        
        public decimal pocetnaCena { get; set; } = 100;
        public Motor(int Id, string Marka, string Model, double potrosnja, double Kubikaza, double Snaga)
              : base(Id, Model, potrosnja, Marka)
        {
            this.Kubikaza = Kubikaza;
            this.Snaga = Snaga; 
            
        }


        public override int izracunajCenu()
        {
            int cena = (int)pocetnaCena;
            switch (Model)
            {
                case "Harley":
                    cena += (int)(pocetnaCena * 0.15m);
                    if (Kubikaza > 1200) { cena += (int)(pocetnaCena * 0.10m); }
                    else { cena -= (int)(pocetnaCena * 0.05m); }
                    break;

                case "Yamaha":
                    cena += (int)(pocetnaCena * 0.10m);
                    if (Snaga > 180) { cena += (int)(pocetnaCena * 0.05m); }
                    else { cena -= (int)(pocetnaCena * 0.10m); }
                    if (Tip == Tip.Heritage) { cena += (int)(pocetnaCena + 50m); }
                    else if (Tip == Tip.Sport) { cena += (int)(pocetnaCena + 100m); }
                    else { cena -= (int)(pocetnaCena - 10m); }
                    break;
            }
            return cena;
        }

    }
}
