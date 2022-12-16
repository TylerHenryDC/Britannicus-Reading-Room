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
        //Varaible Declaration
        string invID = "";
        string condition = "";
        string quantity = "";

        /// <summary>
        /// Initialize Window, display entire inventory in datagrid
        /// </summary>
        public InventoryWindow()
        {
            InitializeComponent();
            DisplayInv();
            editButton.IsEnabled = false;
            inventoryDataGrid.IsReadOnly = true;

        }

        /// <summary>
        /// Gets all inv items from database and displays them on a datagrid
        /// </summary>
        private void DisplayInv()
        {
<<<<<<< Updated upstream

            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\Downloads\\Database Project\\Database Project\\Database Project\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
=======
           
            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\src\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
>>>>>>> Stashed changes
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

        /// <summary>
        /// on click Add new book to inv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            ISBNChecker ic = new ISBNChecker();
            ic.ShowDialog();
        }

        /// <summary>
        /// Edit currently selected book
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            EditInventoryWindow ab = new EditInventoryWindow(invID, condition, quantity);
            ab.ShowDialog();
        }

        /// <summary>
        /// grabs item selection if new entry is clicked on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inventoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editButton.IsEnabled = true;
            foreach (DataRowView row in inventoryDataGrid.SelectedItems)
            {

                invID = row.Row[0].ToString();
                condition = row.Row[9].ToString();
                quantity = row.Row[11].ToString();
              //  string type = row.Value.ToString();
               // string condition = row.Cells[1].ToString();
             //   string price = row.Value.ToString();
                //string ISBN = row.Cells[1].ToString();
              //  string Quantity = row.Cells[1].ToString();
                //...
            }
        }

        /// <summary>
        /// Refreshes page if new items were added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayInv();
        }
    }
}
