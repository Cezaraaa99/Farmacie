using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ModeleDateFarmacie;
using StocareDate;
using AplicatieFarmacie.Ferestre;

namespace AplicatieFarmacie
{
    public partial class MainWindow : Window
    {
        private readonly DepozitMedicamente _depozit;
        private DispatcherTimer _timerCeas;

        public MainWindow()
        {
            InitializeComponent();
            _depozit = new DepozitMedicamente();
            InitCeas();
            RefreshLista();
        }

        private void InitCeas()
        {
            _timerCeas = new DispatcherTimer();
            _timerCeas.Interval = TimeSpan.FromSeconds(1);
            _timerCeas.Tick += (s, e) =>
            {
                TxtDataOra.Text = DateTime.Now.ToString("dd.MM.yyyy   HH:mm:ss");
            };
            _timerCeas.Start();
        }

        private void RefreshLista(string termen = "")
        {
            List<Medicament> lista = string.IsNullOrWhiteSpace(termen)
                ? _depozit.ObtineTot()
                : _depozit.Cauta(termen);

            GridMedicamente.ItemsSource = null;
            GridMedicamente.ItemsSource = lista;

            TxtTotal.Text = $"Total: {lista.Count} medicamente";
            CurataDetalii();
        }

        private void CurataDetalii()
        {
            DetDenumire.Text = "-";
            DetProducator.Text = "-";
            DetCategorie.Text = "-";
            DetPret.Text = "-";
            DetStoc.Text = "-";
            DetExpirare.Text = "-";
            DetDescriere.Text = "-";
        }

        private void AfiseazaDetalii(Medicament m)
        {
            if (m == null) { CurataDetalii(); return; }
            DetDenumire.Text = m.Denumire;
            DetProducator.Text = m.Producator;
            DetCategorie.Text = m.Categorie;
            DetPret.Text = $"{m.Pret:F2} RON";
            DetStoc.Text = $"{m.Stoc} bucati";
            DetExpirare.Text = m.DataExpirare.ToString("dd MMMM yyyy");
            DetDescriere.Text = string.IsNullOrWhiteSpace(m.Descriere) ? "-" : m.Descriere;
        }

        private Medicament GetMedicamentSelectat()
        {
            return GridMedicamente.SelectedItem as Medicament;
        }

        private void GridMedicamente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AfiseazaDetalii(GetMedicamentSelectat());
            var m = GetMedicamentSelectat();
            TxtStatus.Text = m != null
                ? $"Selectat: {m.Denumire} (ID: {m.Id})"
                : "Selectati un medicament din lista.";
        }

        private void BtnAdauga_Click(object sender, RoutedEventArgs e)
        {
            var fereastra = new AdaugareMedicament();
            if (fereastra.ShowDialog() == true)
            {
                _depozit.Adauga(fereastra.MedicamentNou);
                RefreshLista();
                TxtStatus.Text = $"Medicament '{fereastra.MedicamentNou.Denumire}' adaugat cu succes.";
            }
        }

        private void BtnEditeaza_Click(object sender, RoutedEventArgs e)
        {
            var selectat = GetMedicamentSelectat();
            if (selectat == null)
            {
                MessageBox.Show("Selectati un medicament pentru editare.",
                    "Nicio selectie", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var fereastra = new EditareMedicament(selectat);
            if (fereastra.ShowDialog() == true)
            {
                _depozit.Actualizeaza(fereastra.MedicamentEditat);
                RefreshLista();
                TxtStatus.Text = $"Medicament '{fereastra.MedicamentEditat.Denumire}' actualizat.";
            }
        }

        private void BtnSterge_Click(object sender, RoutedEventArgs e)
        {
            var selectat = GetMedicamentSelectat();
            if (selectat == null)
            {
                MessageBox.Show("Selectati un medicament pentru stergere.",
                    "Nicio selectie", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var confirmare = MessageBox.Show(
                $"Sigur doriti sa stergeti medicamentul:\n\n\"{selectat.Denumire}\"?",
                "Confirmare stergere",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirmare == MessageBoxResult.Yes)
            {
                string nume = selectat.Denumire;
                _depozit.Sterge(selectat.Id);
                RefreshLista();
                TxtStatus.Text = $"Medicament '{nume}' a fost sters.";
            }
        }

        private void TxtCautare_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshLista(TxtCautare.Text);
            if (!string.IsNullOrWhiteSpace(TxtCautare.Text))
                TxtStatus.Text = $"Rezultate pentru: \"{TxtCautare.Text}\"";
        }

        private void BtnReseteaza_Click(object sender, RoutedEventArgs e)
        {
            TxtCautare.Clear();
            RefreshLista();
            TxtStatus.Text = "Lista resetata. Afisate toate medicamentele.";
        }
    }
}
