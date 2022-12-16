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
        //Global Variables
        bool update = false;
        string email = "";
        string ID = "";
        string error = "";
        bool valid = true;


        /// <summary>
        /// Adds customer chosens values to edit tect boxes so they can be edited
        /// </summary>
        /// <param name="custEmail"></param>
        /// <param name="custID"></param>
        public CustomerEditWindow(string custEmail, string custID)
        {
            InitializeComponent();

            if (custEmail != null)
            {
                update = true;
                email = custEmail;
                ID = custID;
                
                string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\src\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
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


        /// <summary>
        /// On click close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            
        }


        /// <summary>
        /// Checks if information is valid, if so checks whether or not the data is to be added to the databases or an existing entry is to be updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            //Checks if info is valud
            CheckValid();
            if (valid == true)
            {
                //Either updates or adds customers
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

            //Show error is any
            else
            {
                MessageBox.Show(error, "Error");
            }
            
        }

        /// <summary>
        /// Checks if information is valid
        /// </summary>
        private void CheckValid()
        {
            valid = true;
            error = "";
            if(firstNameTextBox.Text == "")
            {
                error += "\nFirst Name Cannot Be Empty";               
            }
            if (lastNameTextBox.Text == "")
            {
                error += "\nLast Name Cannot Be Empty";
            }
            if (emailNameTextBox.Text == "")
            {
                error += "\nEmail Cannot Be Empty";
            }
            else
            {
                string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\Downloads\\Database Project\\Database Project\\Database Project\\BritannicusReadingRoom (1).mdf\"; Integrated Security = True;";
                SqlConnection dbConnection = new SqlConnection(connectString);
                SqlCommand command = new SqlCommand("Customer_Dashboard", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", emailNameTextBox.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                int EmailExists = (int)command.ExecuteScalar();
                if(EmailExists > 0)
                {
                    error += "\nEmail Already Exists";
                }
            }
            if (addressTextBox.Text == "")
            {
                error += "\nAddress Cannot Be Empty";
            }
            if (cityTextBox.Text == "")
            {
                error += "\nCity Cannot Be Empty";
            }
            if (postCodeTextBox.Text == "")
            {
                error += "\nPostal Code Cannot Be Empty";
            }
            else if(postCodeTextBox.Text.Length != 6)
            {
                error += "\nPostal Code Has To Be 6 Characters";
            }
            if (error != "")
            {
                valid = false;
            }    
        }

        /// <summary>
        /// Updates customer based on given parameters
        /// </summary>
        private void CustomerUpdate()
        {
            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\Downloads\\Database Project\\Database Project\\Database Project\\BritannicusReadingRoom (1).mdf\"; Integrated Security = True;";
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

        /// <summary>
        /// Adds new customer with given parameters
        /// </summary>
        private void CustomerAdd()
        {
            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\Downloads\\Database Project\\Database Project\\Database Project\\BritannicusReadingRoom (1).mdf\"; Integrated Security = True;";
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
