using System;

namespace ModeleDateFarmacie
{
    public class Medicament
    {
        public int Id { get; set; }
        public string Denumire { get; set; }
        public string Producator { get; set; }
        public string Categorie { get; set; }
        public double Pret { get; set; }
        public int Stoc { get; set; }
        public DateTime DataExpirare { get; set; }
        public string Descriere { get; set; }

        public Medicament() { }

        public Medicament(int id, string denumire, string producator, string categorie,
                          double pret, int stoc, DateTime dataExpirare, string descriere)
        {
            Id = id;
            Denumire = denumire;
            Producator = producator;
            Categorie = categorie;
            Pret = pret;
            Stoc = stoc;
            DataExpirare = dataExpirare;
            Descriere = descriere;
        }

        public override string ToString()
        {
            return $"{Denumire} | {Producator} | {Categorie} | {Pret:F2} RON | Stoc: {Stoc}";
        }
    }
}
