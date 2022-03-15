using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _6BP_OudouoirGuillaume_SOTrim2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Medicijn> Medicijnlijst = new List<Medicijn>();
        public MainWindow()
        {
            InitializeComponent();
            cmbSoort.Items.Add("dragee");//voeg items toe aan de cmb
            cmbSoort.Items.Add("capsule");
        }

        private void btnVoegToe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //voegt de ingegeven items toe aan de list en dan de namen aan de cmb daarna cleart hij alle values uit de txb
                Medicijnlijst.Add(new Medicijn(txbNaamNew.Text, cmbSoort.Text, Convert.ToInt32(txbAantalNew.Text),Convert.ToInt32(txbAantalDozenNew.Text), Convert.ToBoolean(rbnVoorschriftYES.IsChecked)));
                cmbNaamList.Items.Add(txbNaamNew.Text);
                cmbNaamRestock.Items.Add(txbNaamNew.Text);

                txbAantalDozenNew.Clear();
                txbAantalNew.Clear();
                txbNaamNew.Clear();
                cmbSoort.SelectedIndex = -1;
                Lsbupdate();

            }
            catch(Exception ex)
            {
                MessageBox.Show($"Get IT. Also this \n{ex}");
            }
        }

        private void btnLsbView_Click(object sender, RoutedEventArgs e)
        {
            Lsbupdate();//roept het lsbupdate op
        }
        public void Lsbupdate()//voegt alle gegeven items uit de list toe aan de lsb
        {
            lsbMedicijnen.Items.Clear();
            foreach (var i in Medicijnlijst)
            {
                lsbMedicijnen.Items.Add($"Nog {i.AantalBeschikbareDozen * i.AantalPillenPerDoosOrg + i.AantalPillenPerDoos} {i.getSoort}(s) over van {i.getNaam}");
            }
        }

        private void btnaftrekken_Click(object sender, RoutedEventArgs e)//trekt de gevraagde pillen af van het totale, als er te weinig of geen meer zijn dan meld hij dat ook
        {
            int check = Medicijnlijst[cmbNaamList.SelectedIndex].neemPilWeg(Convert.ToInt32(txbAANtalPillenAF.Text));
            if(check == -1)
            {
                MessageBox.Show($"Sorry, er zijn helaas maar {Medicijnlijst[cmbNaamList.SelectedIndex].AantalPillenPerDoos+Medicijnlijst[cmbNaamList.SelectedIndex].AantalBeschikbareDozen* Medicijnlijst[cmbNaamList.SelectedIndex].AantalPillenPerDoosOrg} {Medicijnlijst[cmbNaamList.SelectedIndex].Soort}(s) over.");
            }
            if(check <= 3 && check != -1)
            {
               MessageBox.Show($"Opgepast, er zijn maar {Medicijnlijst[cmbNaamList.SelectedIndex].AantalPillenPerDoos + Medicijnlijst[cmbNaamList.SelectedIndex].AantalBeschikbareDozen * Medicijnlijst[cmbNaamList.SelectedIndex].AantalPillenPerDoosOrg}{Medicijnlijst[cmbNaamList.SelectedIndex].Soort}(s) meer in stock.");
            }
            Lsbupdate();
        }

        private void btnRestock_Click(object sender, RoutedEventArgs e)//voegt extra dozen toe en update dan de lsb
        {
            Medicijnlijst[cmbNaamRestock.SelectedIndex].voegDoosjeToe(Convert.ToInt32(txbAantalDozenRestock.Text));
            Lsbupdate();
            txbAantalDozenRestock.Clear();
        }

        private void BtnZoeken_Click(object sender, RoutedEventArgs e)//zoekt achter de medicijn namen via een foreach en een if clause
        {
            bool found = false;
            foreach (var i in Medicijnlijst)
            {
                if(TxbZoekenNaam.Text == i.Naam)
                {
                    MessageBox.Show($"{i.Naam},{i.getSoort},aantal pillen:{i.AantalPillenPerDoos + i.AantalBeschikbareDozen * i.AantalPillenPerDoosOrg},Voorschrift: {i.opVoorschrift.ToString()}");
                    found = true;
                }
            }
            if(found!= true)
            {
                MessageBox.Show($"Sorry het medicijn genaamd {TxbZoekenNaam.Text} werd niet herkend in ons systeem gelieve het toe te voegen.");
            }
        }
    }
}
