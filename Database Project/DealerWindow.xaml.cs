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
        // Declaring appropriate variables for the dealers data to be stored
        string itemName = "";
        string businessTitle = "";
        string itemDescription = "";
        string itemCondition = "";
        string dealerPrice = "";
        string ISBN = "";
        string dealerItemType = "";
        string dealerPriceID = "";

        /// <summary>
        /// Component is initialized and display dealers which displays the data appropriately is being called
        /// </summary>

        public DealerWindow()
        {
            InitializeComponent();
            DisplayDealers();
        }

        /// <summary>
        /// Display dealers method to display all the appropriate data
        /// </summary>
        private void DisplayDealers()
        {

            // connection string to connect to db for having dealers data on dashboard
            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\src\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
            // execution of the stored procedure
            SqlConnection dbConnection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand("EXEC Dealer_Dashboard", dbConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            // object name dealers table been declared for class DataTable for displaying data
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
                // close the db connection
                adapter.Dispose();
                dbConnection.Close();
            }

            dealerDataGrid.ItemsSource = dealerTable.DefaultView;

        }


        /// <summary>
        /// new button is clicked - opens up the new dealer window in order to add the dealer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNewButton_Click(object sender, RoutedEventArgs e)
        {
            AddDealerWindow ad = new AddDealerWindow();
            this.Close();
            ad.ShowDialog();
        }

        /// <summary>
        /// checking the datagrid changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DealerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            // for each loop to get the data
            foreach (DataRowView row in dealerDataGrid.SelectedItems)
            {
                
                itemName = row.Row[0].ToString();
                itemDescription = row.Row[2].ToString();
                itemCondition = row.Row[3].ToString();
                dealerPrice = row.Row[4].ToString();
                dealerItemType = row[5].ToString();
                ISBN = row[6].ToString();
                
            }
        }
        /// <summary>
        /// edit button to edit the product of the dealer 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            // connection string to connect to the database    
            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\src\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
            // execution of the store procedure
            SqlCommand command = new SqlCommand("EXEC getItemID @ISBN = " + ISBN, dbConnection);

            // Try to connect to the database, and get the apporopriate data
            try
            {
                dbConnection.Open();
                // stores the appropriate data to the dealerPriceID variable
                dealerPriceID = command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                // If there is an error, re-throw the exception to be handled by the presentation tier.
                // (You could also just do error messaging here but that's not as nice.)
                throw ex;
            }
            finally
            {
                // closes the connection
                dbConnection.Close();
            }

            // opens the edit window with all the prior info so for that parameters are passed
            DealerEditWindow ad = new DealerEditWindow(itemName, businessTitle, itemDescription, itemCondition, dealerPrice, dealerItemType, ISBN, dealerPriceID);
            this.Close();
            ad.ShowDialog();
        }
        /// <summary>
        /// to check comob box change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (DataRowView row in dealerDataGrid.SelectedItems)
            {

                itemName = row.Row[0].ToString();
                itemDescription = row.Row[1].ToString();

            }
        }
    }
}
