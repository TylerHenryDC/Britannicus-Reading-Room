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
using System.Windows.Shapes;

namespace Database_Project
{
    /// <summary>
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {
        public DashboardWindow()
        {
            InitializeComponent();
        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow mw = new MainWindow();
            this.Close();
            mw.ShowDialog();
            
        }

        private void inventoryButton_Click(object sender, RoutedEventArgs e)
        {
            InventoryWindow iw = new InventoryWindow();
            iw.ShowDialog();
        }

        private void customerButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow cw = new CustomerWindow();
            cw.ShowDialog();
        }

        private void dealerButton_Click(object sender, RoutedEventArgs e)
        {
            DealerWindow dw = new DealerWindow();
            dw.ShowDialog();
        }
    }
}
