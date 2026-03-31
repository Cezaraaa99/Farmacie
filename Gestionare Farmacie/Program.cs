using System;
using LibrarieModele;
using NivelAccesDate;

namespace InterfataUtilizator
{
    class Program
    {
        static AdministrareMedicamente admin = new AdministrareMedicamente();

        static void Main(string[] args)
        {
            string optiune;
            do
            {
                Console.WriteLine("\n--- GESTIUNE FARMACIE (3 Proiecte) ---");
                Console.WriteLine("1. Adăugare medicament");
                Console.WriteLine("2. Afișare listă");
                Console.WriteLine("3. Căutare (LINQ)");
                Console.WriteLine("4. Ștergere");
                Console.WriteLine("0. Ieșire");
                Console.Write("Alege: ");
                optiune = Console.ReadLine();

                switch (optiune)
                {
                    case "1": MeniuAdaugare(); break;
                    case "2": MeniuAfisare(); break;
                    case "3": MeniuCautare(); break;
                    case "4": MeniuStergere(); break;
                }
            } while (optiune != "0");
        }

        static void MeniuAdaugare()
        {
            Console.Write("ID: "); int id = int.Parse(Console.ReadLine());
            Console.Write("Nume: "); string nume = Console.ReadLine();
            Console.Write("Preț: "); double pret = double.Parse(Console.ReadLine());

            // Citire Enum simplu
            Console.WriteLine("Categorie (1-Analgezic, 2-Antibiotic, 3-Antiviral, 4-Supliment): ");
            CategorieMedicament cat = (CategorieMedicament)int.Parse(Console.ReadLine());

            // Exemplu setare Flags (se pot combina cu operatorul | )
            FacilitatiDepozitare optiuni = FacilitatiDepozitare.Frigider | FacilitatiDepozitare.LocIntunecos;

            admin.Adauga(new Medicament(id, nume, pret, cat, optiuni));
        }

        static void MeniuAfisare()
        {
            var lista = admin.GetToate();
            lista.ForEach(m => Console.WriteLine(m));
        }

        static void MeniuCautare()
        {
            Console.Write("Căutare după nume: ");
            string nume = Console.ReadLine();
            var rez = admin.CautaDupaNume(nume);
            rez.ForEach(r => Console.WriteLine(r));
        }

        static void MeniuStergere()
        {
            Console.Write("ID de șters: ");
            int id = int.Parse(Console.ReadLine());
            admin.Sterge(id);
        }
    }
}