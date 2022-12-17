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
    /// Interaction logic for AddDealerProduct.xaml
    /// </summary>
    public partial class AddDealerProduct : Window
    {
        /// <summary>
        /// vairbles declared to store the appropriate informations
        /// </summary>
        string businessTitleProductAdd = "";
        string itemNameInput = "";
        string itemDescription = "";
        string itemCondition = "";
        string dealersPrice = "";
        string itemType = "";
        string ISBN = "";
        string dealerID = "";
        string dealerItemID = "";
        string dealerItemID1 = "";

        /// <summary>
        /// opens the window with some information loaded
        /// </summary>
        /// <param name="businessTitle"></param>
        public AddDealerProduct(string businessTitle)
        {
            InitializeComponent();
            // adds to the varible
            businessTitleProductAdd = businessTitle;
            // adds to the field in the window
            AddItemBuisnessTitle.Text = businessTitle;
        }
        /// <summary>
        /// adds the product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            // storing the inputted value
            itemNameInput = AddItemName.Text;
            itemDescription = AddItemDescription.Text;
            //itemCondition = AddItemCondition.Text;
            dealersPrice = AddDealerPrice.Text;
            itemType = AddItemType.Text;
            ISBN = AddISBN.Text;

            // if anythis is empty then show error
            if (itemNameInput == "" || itemDescription == "" || dealersPrice == "" || itemType == "" || ISBN == "")
            {
                MessageBox.Show("Error ! Empty Input is not allowed.");
            }
            else { 
                // else open the connection (declared one) and find the dealer id

            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\src\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
                // executes the query
            SqlCommand command = new SqlCommand("getDealerID", dbConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DealerBusinessTitle", businessTitleProductAdd);

           
                // try to connect and execute the data
                try
                {
                    dbConnection.Open();
                    // execute query and stores the retured value

                    dealerID = command.ExecuteScalar().ToString();


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

                // second connection to insert the item 
                SqlConnection dbConnection1 = new SqlConnection(connectString);
                // exec the store procedure
                SqlCommand command1 = new SqlCommand("InsertDealerItem", dbConnection1);

                command1.CommandType = System.Data.CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@DealerID", dealerID);
                command1.Parameters.AddWithValue("@ItemName", itemNameInput);
                command1.Parameters.AddWithValue("@ItemDescription", itemDescription);
                command1.Parameters.AddWithValue("@ItemType", itemType);
                command1.Parameters.AddWithValue("@ISBN", ISBN);
                // try to connect and execute the data

                try
                {
                    //opens the connection
                    dbConnection1.Open();
                    // executes the query
                    command1.ExecuteNonQuery();


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
                    dbConnection1.Close();
                }


                // connection to get the entered item id 
                SqlConnection dbConnection2 = new SqlConnection(connectString);
                // executes the store procedure
                SqlCommand command2 = new SqlCommand("getItemID", dbConnection2);
                command2.CommandType = System.Data.CommandType.StoredProcedure;
                command2.Parameters.AddWithValue("@ISBN", ISBN);

                // try to connect to database

                try
                {
                    // opens the database
                    dbConnection2.Open();
                    // executes the query and stores the returned value
                    dealerItemID = command2.ExecuteScalar().ToString();

                }
                catch (Exception ex)
                {
                    // If there is an error, re-throw the exception to be handled by the presentation tier.
                    // (You could also just do error messaging here but that's not as nice.)
                    throw ex;
                }
                finally
                {

                    dbConnection2.Close();
                }

                // connection declared to insert the price
                SqlConnection dbConnection3 = new SqlConnection(connectString);
                // execute the store procedure
                SqlCommand command3 = new SqlCommand("InsertDealerPrice", dbConnection3);
                command3.CommandType = System.Data.CommandType.StoredProcedure;
                command3.Parameters.AddWithValue("@DealerItemID", dealerItemID);
                command3.Parameters.AddWithValue("@DealerConditionID", (conditionComboBox.SelectedIndex) + 1);
                command3.Parameters.AddWithValue("@DealerPrice", dealersPrice);

                // try to connect to the database
                try
                {
                    // opens the database
                    dbConnection3.Open();
                    // executes the qurey
                    command3.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    // If there is an error, re-throw the exception to be handled by the presentation tier.
                    // (You could also just do error messaging here but that's not as nice.)
                    throw ex;
                }
                finally
                {

                    dbConnection3.Close();
                }

                // close the window
                this.Close();
                // opens the dealer window 
                DealerWindow dw1 = new DealerWindow();

                dw1.ShowDialog();

                }
            }

        }
    }

