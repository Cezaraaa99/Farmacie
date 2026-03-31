using System;

namespace GestiuneFarmacie
{
    // Aceasta este "matrița" pentru un medicament
    public class Medicament
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Producator { get; set; }
        public double Pret { get; set; }
        public int Stoc { get; set; }

        public Medicament(int id, string nume, string producator, double pret, int stoc)
        {
            Id = id;
            Nume = nume;
            Producator = producator;
            Pret = pret;
            Stoc = stoc;
        }

        public override string ToString()
        {
            return $"[ID: {Id}] {Nume} | Producător: {Producator} | Preț: {Pret} RON | Stoc: {Stoc} buc.";
        }
    }
}