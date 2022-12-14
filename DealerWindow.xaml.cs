using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
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
            DisplayDealers();
        }

        private void DisplayDealers()
        {
            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\source\\repos\\Database Project\\Database Project\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand("EXEC Dealer_Dashboard", dbConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dealerTable = new DataTable();

            // Try to connect to the database, and use the adapter to fill the table
            try
            {
                dbConnection.Open();
                adapter.Fill(dealerTable);
            }
            catch (Exception ex)
            {
                // If there is an error, re-throw the exception to be handled by the presentation tier.
                // (You could also just do error messaging here but that's not as nice.)
                throw ex;
            }
            finally
            {
                adapter.Dispose();
                dbConnection.Close();
            }

            dealerDataGrid.ItemsSource = dealerTable.DefaultView;
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
