using System;
using System.Windows;
using ModeleDateFarmacie;

namespace AplicatieFarmacie.Ferestre
{
    public partial class AdaugareMedicament : Window
    {
        public Medicament MedicamentNou { get; private set; }

        public AdaugareMedicament()
        {
            InitializeComponent();
            DpExpirare.SelectedDate = DateTime.Today.AddYears(1);
        }

        private void BtnSalveaza_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidareFormular()) return;

            MedicamentNou = new Medicament
            {
                Denumire = TxtDenumire.Text.Trim(),
                Producator = TxtProducator.Text.Trim(),
                Categorie = (CmbCategorie.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content?.ToString()
                             ?? CmbCategorie.Text.Trim(),
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

            return true;
        }

        private void Eroare(string mesaj)
        {
            MessageBox.Show(mesaj, "Date invalide", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
