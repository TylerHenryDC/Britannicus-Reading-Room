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
    /// Interaction logic for DealerWindow.xaml
    /// </summary>
    public partial class DealerWindow : Window
    {
        public DealerWindow()
        {
            InitializeComponent();
        }

        private void addNewButton_Click(object sender, RoutedEventArgs e)
        {
            AddDealerWindow ad = new AddDealerWindow();
            ad.ShowDialog();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            AddDealerWindow ad = new AddDealerWindow();
            ad.ShowDialog();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
