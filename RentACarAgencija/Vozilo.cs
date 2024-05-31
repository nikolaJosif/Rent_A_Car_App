using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarAgencija
{
    public enum Tip
    {
        Limuzina,
        Hecbek,
        Karavan,
        Kupe,
        Kabriolet,
        Minivan,
        SUV,
        Pickup,
        Adventure,
        Heritage,
        Tour,
        Roadster,
        UrbanMobility,
        Sport
    }
    
    public abstract class Vozilo
    {
        public int Id {  get; set; }    
        public String Model { get; set; }
        public string Marka { get; set; }
        public double Potrosnja { get; set; }
        public Tip Tip { get; set; }
        String TipVozila { get; set; }


        public Vozilo(String Model, double Potrosnja, string Marka, int Id)
        {
            this.Model = Model;
            this.Potrosnja = Potrosnja;
            this.Marka = Marka;
            this.Id = Id;
            
        }

        protected Vozilo(int id, string model, double potrosnja, string marka)
        {
            Id = id;
            Model = model;
            Potrosnja = potrosnja;
            Marka = marka;
        }

        public abstract int izracunajCenu();
    }
}
