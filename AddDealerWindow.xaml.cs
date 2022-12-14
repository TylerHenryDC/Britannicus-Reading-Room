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
    /// Interaction logic for AddDealerWindow.xaml
    /// </summary>
    public partial class AddDealerWindow : Window
    {
        public AddDealerWindow()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            DealerEditWindow dw = new DealerEditWindow();
            dw.ShowDialog();
        }
    }
}
