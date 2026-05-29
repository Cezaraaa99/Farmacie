using System;
using System.Windows;
using System.Windows.Controls;
using ModeleDateFarmacie;

namespace AplicatieFarmacie.Ferestre
{
    public partial class EditareMedicament : Window
    {
        private readonly int _idOriginal;
        public Medicament MedicamentEditat { get; private set; }

        public EditareMedicament(Medicament medicament)
        {
            InitializeComponent();
            _idOriginal = medicament.Id;
            PopuleazaCampuri(medicament);
        }

        private void PopuleazaCampuri(Medicament m)
        {
            TxtIdInfo.Text = $"ID medicament: {m.Id}";
            TxtDenumire.Text = m.Denumire;
            TxtProducator.Text = m.Producator;
            CmbCategorie.Text = m.Categorie.ToString(); // enum -> string pentru afisare
            TxtPret.Text = m.Pret.ToString("F2");
            TxtStoc.Text = m.Stoc.ToString();
            DpExpirare.SelectedDate = m.DataExpirare;
            TxtDescriere.Text = m.Descriere;
        }

        private void BtnActualizeaza_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidareFormular()) return;

            CategorieMedicament categorie = Enum.TryParse(CmbCategorie.Text.Trim(), out CategorieMedicament cat)
                ? cat : CategorieMedicament.AltTip;

            MedicamentEditat = new Medicament
            {
                Id = _idOriginal,
                Denumire = TxtDenumire.Text.Trim(),
                Producator = TxtProducator.Text.Trim(),
                Categorie = categorie,
                Pret = double.Parse(TxtPret.Text.Trim().Replace(',', '.'),
                       System.Globalization.CultureInfo.InvariantCulture),
                Stoc = int.Parse(TxtStoc.Text.Trim()),
                DataExpirare = DpExpirare.SelectedDate.Value,
                Descriere = TxtDescriere.Text.Trim()
            };

            DialogResult = true;
            Close();
        }

        private void BtnAnuleaza_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool ValidareFormular()
        {
            if (string.IsNullOrWhiteSpace(TxtDenumire.Text))
            { Eroare("Campul Denumire este obligatoriu."); return false; }
            if (string.IsNullOrWhiteSpace(TxtProducator.Text))
            { Eroare("Campul Producator este obligatoriu."); return false; }
            if (string.IsNullOrWhiteSpace(CmbCategorie.Text))
            { Eroare("Selectati sau introduceti o categorie."); return false; }
            if (!double.TryParse(TxtPret.Text.Trim().Replace(',', '.'),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double pret) || pret < 0)
            { Eroare("Pretul trebuie sa fie un numar pozitiv."); return false; }
            if (!int.TryParse(TxtStoc.Text.Trim(), out int stoc) || stoc < 0)
            { Eroare("Stocul trebuie sa fie un numar intreg pozitiv."); return false; }
            if (!DpExpirare.SelectedDate.HasValue)
            { Eroare("Selectati data de expirare."); return false; }
            if (DpExpirare.SelectedDate.Value.Date <= DateTime.Today)
            { Eroare("Data de expirare trebuie sa fie dupa data de astazi."); return false; }
            return true;
        }

        private void Eroare(string mesaj)
        {
            MessageBox.Show(mesaj, "Date invalide", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}