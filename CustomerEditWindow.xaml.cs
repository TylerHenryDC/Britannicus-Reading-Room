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
    /// Interaction logic for CustomerEditWindow.xaml
    /// </summary>
    public partial class CustomerEditWindow : Window
    {
        bool update = false;
        string email = "";
        string ID = "";
        public CustomerEditWindow(string custEmail, string custID)
        {
            InitializeComponent();

            if (custEmail != null)
            {
                update = true;
                email = custEmail;
                ID = custID;
                
                string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\source\\repos\\Database Project\\Database Project\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
                SqlConnection dbConnection = new SqlConnection(connectString);
                SqlCommand command = new SqlCommand("Customer_Dashboard", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", custEmail);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable inventoryTable = new DataTable();

                // Try to connect to the database, and use the adapter to fill the table
                try
                {
                    dbConnection.Open();
                    SqlDataReader cust = command.ExecuteReader();
                    while (cust.Read())
                    {
                        firstNameTextBox.Text = cust["CUSTOMER_FIRST_NAME"].ToString();
                        lastNameTextBox.Text = cust["CUSTOMER_LAST_NAME"].ToString();
                        emailNameTextBox.Text = cust["CUSTOMER_EMAIL"].ToString();
                        addressTextBox.Text = cust["CUSTOMER_STREET_NAME"].ToString();
                        cityTextBox.Text = cust["CUSTOMER_CITY"].ToString();
                        postCodeTextBox.Text = cust["CUSTOMER_POSTAL_CODE"].ToString();
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

                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (update == true)
            {
                CustomerUpdate();
            }
            else if (update == false)
            {
                CustomerAdd();
            }
            Close();
            

            
        }

        private void CustomerUpdate()
        {
            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\source\\repos\\Database Project\\Database Project\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand("Customer_Updater", dbConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@FirstName", firstNameTextBox.Text);
            command.Parameters.AddWithValue("@LastName", lastNameTextBox.Text);
            command.Parameters.AddWithValue("@Email", emailNameTextBox.Text);
            command.Parameters.AddWithValue("@Street", addressTextBox.Text);
            command.Parameters.AddWithValue("@City", cityTextBox.Text);
            command.Parameters.AddWithValue("@Postal", postCodeTextBox.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

           
            // Try to connect to the database, and use the adapter to fill the table
            try
            {
                dbConnection.Open();
                command.ExecuteNonQuery();

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

        private void CustomerAdd()
        {
            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\source\\repos\\Database Project\\Database Project\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand("Customer_Insert", dbConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FirstName", firstNameTextBox.Text);
            command.Parameters.AddWithValue("@LastName", lastNameTextBox.Text);
            command.Parameters.AddWithValue("@Email", emailNameTextBox.Text);
            command.Parameters.AddWithValue("@Street", addressTextBox.Text);
            command.Parameters.AddWithValue("@City", cityTextBox.Text);
            command.Parameters.AddWithValue("@Postal", postCodeTextBox.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(command);


            // Try to connect to the database, and use the adapter to fill the table
            try
            {
                dbConnection.Open();
                command.ExecuteNonQuery();

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

    }
}
