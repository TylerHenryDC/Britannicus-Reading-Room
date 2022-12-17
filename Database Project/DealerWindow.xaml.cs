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

        string itemName = "";
        string businessTitle = "";
        string itemDescription = "";
        string itemCondition = "";
        string dealerPrice = "";
        string ISBN = "";
        string dealerItemType = "";
        string dealerPriceID = "";


        public DealerWindow()
        {
            InitializeComponent();
            DisplayDealers();
        }

        private void DisplayDealers()
        {

            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\src\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";

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
            this.Close();
            ad.ShowDialog();
        }
        private void DealerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            foreach (DataRowView row in dealerDataGrid.SelectedItems)
            {

                itemName = row.Row[0].ToString();
                itemDescription = row.Row[2].ToString();
                itemCondition = row.Row[3].ToString();
                dealerPrice = row.Row[4].ToString();
                dealerItemType = row[5].ToString();
                ISBN = row[6].ToString();
                // dealerPriceID = row[7].ToString();
                //   condition = row.Row[9].ToString();
                //  quantity = row.Row[11].ToString();
                //  string type = row.Value.ToString();
                // string condition = row.Cells[1].ToString();
                //   string price = row.Value.ToString();
                //string ISBN = row.Cells[1].ToString();
                //  string Quantity = row.Cells[1].ToString();
                //...
            }
        }
        private void editButton_Click(object sender, RoutedEventArgs e)
        {

            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\Downloads\\Database Project\\Database Project\\Database Project\\BritannicusReadingRoom-3.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand("EXEC getItemID @ISBN = " + ISBN, dbConnection);


            try
            {
                dbConnection.Open();
                //command.ExecuteNonQuery();
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

                dbConnection.Close();
            }


            DealerEditWindow ad = new DealerEditWindow(itemName, businessTitle, itemDescription, itemCondition, dealerPrice, dealerItemType, ISBN, dealerPriceID);
            this.Close();
            ad.ShowDialog();
        }

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
