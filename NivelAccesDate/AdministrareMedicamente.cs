using System.Collections.Generic;
using System.Linq; // Necesar pentru LINQ
using LibrarieModele;

namespace NivelAccesDate
{
    public class AdministrareMedicamente
    {
        private List<Medicament> medicamente;

        public AdministrareMedicamente()
        {
            medicamente = new List<Medicament>();
        }

        public void Adauga(Medicament m)
        {
            medicamente.Add(m);
        }

        public List<Medicament> GetToate()
        {
            return medicamente;
        }

        // Cerința 3: Utilizare LINQ pentru căutare
        public List<Medicament> CautaDupaNume(string criteriu)
        {
            return medicamente
                .Where(m => m.Nume.ToLower().Contains(criteriu.ToLower()))
                .ToList();
        }

        public bool Sterge(int id)
        {
            var deSters = medicamente.FirstOrDefault(m => m.Id == id);
            if (deSters != null)
            {
                medicamente.Remove(deSters);
                return true;
            }
            return false;
        }
    }
}