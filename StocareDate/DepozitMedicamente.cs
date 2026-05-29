using System;
using System.Collections.Generic;
using System.Linq;
using ModeleDateFarmacie;

namespace StocareDate
{
    public class DepozitMedicamente
    {
        private List<Medicament> _lista;
        private int _contorId;

        public DepozitMedicamente()
        {
            _lista = new List<Medicament>();
            _contorId = 1;
            IncarcaDateDemo();
        }

        private void IncarcaDateDemo()
        { // constante validare
            Adauga(new Medicament(0, "Nurofen", "Reckitt", CategorieMedicament.Antiinflamator, 15.50, 120, new DateTime(2026, 8, 1), "Antiinflamator nesteroidian"));
            Adauga(new Medicament(0, "Paracetamol", "Terapia", CategorieMedicament.Analgezic, 7.20, 200, new DateTime(2026, 12, 31), "Analgezic si antipiretic"));
            Adauga(new Medicament(0, "Augmentin", "GSK", CategorieMedicament.Antibiotic, 42.00, 50, new DateTime(2025, 11, 15), "Antibiotic cu spectru larg"));
            Adauga(new Medicament(0, "Claritine", "Bayer", CategorieMedicament.Antihistaminic, 18.90, 80, new DateTime(2027, 3, 20), "Pentru alergii sezoniere"));
            Adauga(new Medicament(0, "Omeprazol", "Ranbaxy", CategorieMedicament.Gastric, 9.80, 150, new DateTime(2026, 6, 10), "Inhibitor de pompa de protoni"));
        }

        public void Adauga(Medicament m)
        {
            m.Id = _contorId++;
            _lista.Add(m);
        }

        public bool Sterge(int id)
        {
            var med = _lista.FirstOrDefault(x => x.Id == id);
            if (med == null) return false;
            _lista.Remove(med);
            return true;
        }

        public bool Actualizeaza(Medicament medicamentActualizat)
        {
            var index = _lista.FindIndex(x => x.Id == medicamentActualizat.Id);
            if (index < 0) return false;
            _lista[index] = medicamentActualizat;
            return true;
        }

        public List<Medicament> ObtineTot()
        {
            return new List<Medicament>(_lista);
        }

        public Medicament GasesteById(int id)
        {
            return _lista.FirstOrDefault(x => x.Id == id);
        }

        public List<Medicament> Cauta(string termen)
        {
            if (string.IsNullOrWhiteSpace(termen))
                return ObtineTot();

            termen = termen.ToLower();
            return _lista.Where(m =>
                m.Denumire.ToLower().Contains(termen) ||
                m.Producator.ToLower().Contains(termen) ||
                m.Categorie.ToString().ToLower().Contains(termen) ||
                m.Descriere.ToLower().Contains(termen)
            ).ToList();
        }

        public List<string> ObtineCategoriUnice()
        {
            return _lista.Select(m => m.Categorie.ToString()).Distinct().OrderBy(c => c).ToList();
        }
    }
}
