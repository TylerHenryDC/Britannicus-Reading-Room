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


namespace Database_Project
{
    /// <summary>
    /// Interaction logic for AddDealerWindow.xaml
    /// </summary>
    public partial class AddDealerWindow : Window
    {
        // vairables declared to store the business title 
        string businessTitleInput = "";
        string buisnessTitle = "";
        /// <summary>
        /// initializes the window
        /// </summary>
        public AddDealerWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// close the window when canel button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// add the product to the database when this button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            // DealerEditWindow dw = new DealerEditWindow();
            // dw.ShowDialog();
        }
        /// <summary>
        /// add the product to the database when this button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitName(object sender, RoutedEventArgs e)
        {
            // take the input from the field
            businessTitleInput = businessTitleUser.Text;

            // if its empty shows the error
            if (businessTitleInput == "")
            {
                MessageBox.Show("Business Title Input Can't be submitted as Empty");
            }
            else
            {
                // otherwise opens the db and checks for the dealer's existence in the database
                string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\src\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
                SqlConnection dbConnection = new SqlConnection(connectString);
                // executing the store procedure
                SqlCommand command = new SqlCommand("dealerBusinessNameCheck", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DEALERBusinessTitle", businessTitleInput);
                // Try to connect to the database,
                try
                {
                    dbConnection.Open();
                    //command.ExecuteNonQuery();
                    SqlDataReader isb = command.ExecuteReader();


                    // if found then directly add the product under the specific dealer
                    while (isb.Read())
                    {
                        buisnessTitle = isb["DEALER_BUSINESS_TITLE"].ToString();
                        // opens the dealer product window
                        AddDealerProduct ap = new AddDealerProduct(businessTitleInput);
                        this.Close();
                        ap.ShowDialog();
                        break;

                    }
                    // if not add the dealer then open the add product window
                    while (!isb.Read())
                    {
                        // add dealer
                        addDealer(businessTitleInput);
                        this.Close();
                        // opens the dealers product window
                        AddDealerProduct ap = new AddDealerProduct(businessTitleInput);
                        ap.ShowDialog();

                        break;
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

                    dbConnection.Close();
                }
            }



        }

        /// <summary>
        /// adds the dealer to the database when the method is called
        /// </summary>
        /// <param name="businessTitleInput"></param>
        public void addDealer(string businessTitleInput)
        {
            // opens the connection (conenction is declared) to add the dealer
            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\src\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
            // executes the store procedure
            SqlCommand command = new SqlCommand("addDealer", dbConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DealerBusinessTitle", businessTitleInput);
            // Try to connect to the database,
            try
            {
                //opens the connection
                dbConnection.Open();
                // execute query
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
                // close connection
                dbConnection.Close();
            }

        }
    }
}
