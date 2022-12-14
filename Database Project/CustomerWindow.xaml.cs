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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        //Variable declarations
        string custID = "";
        string custEmail = null;

        /// <summary>
        /// Initialization
        /// </summary>
        public CustomerWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// On click sends selected customer info to be edited
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editCustButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerEditWindow ce = new CustomerEditWindow(custEmail, custID);
            ce.ShowDialog();
        }


        /// <summary>
        /// Grabs customer information based on given email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchCustButton_Click(object sender, RoutedEventArgs e)
        {

            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\src\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand("Customer_Dashboard", dbConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Email", custEmailTextBox.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable inventoryTable = new DataTable();

            // Try to connect to the database, and use the adapter to fill the table
            try
            {
                dbConnection.Open();
                SqlDataReader cust = command.ExecuteReader();
                while (cust.Read())
                {
                    custIDLabel.Content = cust["CUSTOMER_ID"].ToString();
                    custID = cust["CUSTOMER_ID"].ToString();
                    custNameLabel.Content = cust["CUSTOMER_FIRST_NAME"].ToString() + " " + cust["CUSTOMER_LAST_NAME"].ToString();
                    custEmailLabel.Content = cust["CUSTOMER_EMAIL"].ToString();
                    custEmail = cust["CUSTOMER_EMAIL"].ToString();
                    custAddressLabel.Content = cust["CUSTOMER_STREET_NAME"].ToString() + " " + cust["CUSTOMER_POSTAL_CODE"].ToString();
                }
                
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

            
        }

        /// <summary>
        /// On click opens window to view selected customers collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewColButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerCollectionWindow cc = new CustomerCollectionWindow(custID);
            cc.ShowDialog();
        }

        /// <summary>
        /// On click opens window to add new customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addCustButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerEditWindow ce = new CustomerEditWindow(null, null);
            ce.ShowDialog();
        }
    }
}
