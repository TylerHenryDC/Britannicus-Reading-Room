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
        /// <summary>
        /// Initialize Window
        /// </summary>
        public DashboardWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes Window and opens login screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow mw = new MainWindow();
            this.Close();
            mw.ShowDialog();
            
        }

        /// <summary>
        /// On click opens inventory window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inventoryButton_Click(object sender, RoutedEventArgs e)
        {
            InventoryWindow iw = new InventoryWindow();
            iw.ShowDialog();
        }

        /// <summary>
        /// On click opens customer window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customerButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow cw = new CustomerWindow();
            cw.ShowDialog();
        }

        /// <summary>
        /// On Click opens dealer window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dealerButton_Click(object sender, RoutedEventArgs e)
        {
            DealerWindow dw = new DealerWindow();
            
            dw.ShowDialog();

        }

        /// <summary>
        /// On click gives info on the other buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void infoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Customer - Add, Edit or View a Customers Information\n" +
                            "Dealer - Add, Edit or View Dealers and Dealers Product Information\n"+
                            "Inventory - View Current Inventory or Add New Product\n" +
                            "Log Out - Log Out of Your Current Session");    
        }
    }
}
