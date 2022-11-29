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
    /// Interaction logic for InventoryWindow.xaml
    /// </summary>
    public partial class InventoryWindow : Window
    {
        System.Data.SqlClient.SqlConnection con;
        public InventoryWindow()
        {
            InitializeComponent();
            DisplayInv();
            
        }
        
        private void DisplayInv()
        {
            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\source\\repos\\Database Project\\Database Project\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand("EXEC Inventory_Basic", dbConnection);          
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable inventoryTable = new DataTable();

            // Try to connect to the database, and use the adapter to fill the table
            try
            {
                dbConnection.Open();
                adapter.Fill(inventoryTable);
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

            inventoryDataGrid.ItemsSource = inventoryTable.DefaultView;
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

        private void inventoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (DataGridRow row in inventoryDataGrid.SelectedItems)
            {
                string title = row.Value.ToString();
                string author = row.Cells[1].ToString();
                string type = row.Value.ToString();
                string condition = row.Cells[1].ToString();
                string price = row.Value.ToString();
                string ISBN = row.Cells[1].ToString();
                string Quantity = row.Cells[1].ToString();
                //...
            }
        }
    }
}
