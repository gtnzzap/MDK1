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

namespace MDK1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadPartnerData();
        }

        private void LoadPartnerData()
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            List<Partner> partners = dbHelper.GetPartnersWithSales();

            partnersDataGrid.ItemsSource = partners;
        }

        private void AddPartner_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditPartner_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeletePartner_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

