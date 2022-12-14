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
    /// Interaction logic for CustomerCollectionWindow.xaml
    /// </summary>
    public partial class CustomerCollectionWindow : Window
    {
        public CustomerCollectionWindow(string custID)
        {
            InitializeComponent();
            
            showCollection(custID);
        }

        private void showCollection(string custID)
        {
            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\source\\repos\\Database Project\\Database Project\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand("Customer_Collection", dbConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", custID);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable collectionTable = new DataTable();

            // Try to connect to the database, and use the adapter to fill the table
            try
            {
                dbConnection.Open();
                adapter.Fill(collectionTable);
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

            customerCollectionDataGrid.ItemsSource = collectionTable.DefaultView;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow ab = new AddBookWindow();
            ab.ShowDialog();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow ab = new AddBookWindow();
            ab.ShowDialog();
        }
    }
}
