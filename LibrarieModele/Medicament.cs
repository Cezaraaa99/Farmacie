using System;

namespace LibrarieModele
{
    public class Medicament
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public double Pret { get; set; }

        // Câmpuri de tip Enum (Cerința 2)
        public CategorieMedicament Categorie { get; set; }
        public FacilitatiDepozitare OptiuniStocare { get; set; }

        public Medicament(int id, string nume, double pret, CategorieMedicament cat, FacilitatiDepozitare optiuni)
        {
            Id = id;
            Nume = nume;
            Pret = pret;
            Categorie = cat;
            OptiuniStocare = optiuni;
        }

        public override string ToString()
        {
            return $"ID: {Id} | {Nume} | Cat: {Categorie} | Preț: {Pret} RON | Stocare: {OptiuniStocare}";
        }
    }
}