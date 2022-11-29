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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerWindow()
        {
            InitializeComponent();
        }

        private void editCustButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerEditWindow ce = new CustomerEditWindow();
            ce.ShowDialog();
        }
        private void searchCustButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerEditWindow ce = new CustomerEditWindow();
            ce.ShowDialog();
        }

        private void viewColButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerCollectionWindow cc = new CustomerCollectionWindow();
            cc.ShowDialog();
        }

        private void addCustButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerEditWindow ce = new CustomerEditWindow();
            ce.ShowDialog();
        }
    }
}
