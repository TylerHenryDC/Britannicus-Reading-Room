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
    /// Interaction logic for DealerEditWindow.xaml
    /// </summary>
    public partial class DealerEditWindow : Window
    {

        /// <summary>
        ///  variable declared for storing the price
        /// </summary>
        string dealerPriceIDFinal = "";

        /// <summary>
        /// initializes the window with all other prior info coming from prev screen to edit
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="businessTitle"></param>
        /// <param name="itemDescription"></param>
        /// <param name="itemCondition"></param>
        /// <param name="dealerPrice"></param>
        /// <param name="dealerItemType"></param>
        /// <param name="ISBN"></param>
        /// <param name="dealerPriceID"></param>
        public DealerEditWindow(string itemName, string businessTitle, string itemDescription, string itemCondition, string dealerPrice, string dealerItemType, string ISBN, string dealerPriceID)
        {
            InitializeComponent();
            titleItem.Text = itemName;
            titleDescription.Text = itemDescription;
            titleCondition.Text = itemCondition;
            titlePrice.Text = dealerPrice;
            titleType.Text = dealerItemType;
            titleISBN.Text = ISBN;
            dealerPriceIDFinal = dealerPriceID;

        }

        /// <summary>
        /// on this button click close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// click on this to confirm and add the data to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Confirm(object sender, RoutedEventArgs e)
        {
            //if any of the field is kept blank show the error
            if (titleItem.Text.Length == 0 || titleDescription.Text.Length == 0 || titlePrice.Text.Length == 0 || titleType.Text.Length == 0 || titleISBN.Text.Length == 0 || dealerPriceIDFinal == "")
            {
                MessageBox.Show("Can't process the empty value");
            }
            else
            {
                // else open the connection, run the stored procedure and add the data to database to add the edited data to database
                string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\src\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
                SqlConnection dbConnection = new SqlConnection(connectString);
                SqlCommand command = new SqlCommand("DealerItemUpdate", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ITEMNAME", titleItem.Text);
                command.Parameters.AddWithValue("@ITEMDESCRIPTION", titleDescription.Text);
                command.Parameters.AddWithValue("@ITEMTYPE", titleType.Text);
                command.Parameters.AddWithValue("@ISBN", titleISBN.Text);
                command.Parameters.AddWithValue("@DEALERPRICE", titlePrice.Text);
                command.Parameters.AddWithValue("@DEALERITEMEID", dealerPriceIDFinal);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                // Try to connect to the database, and use the adapter to fill the table
                try
                {
                    dbConnection.Open();
                    // executes the query
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
                // closes the window
                Close();
                // opens (calls) the dealers window
                DealerWindow dw1 = new DealerWindow();
                // shows the dealers window
                dw1.ShowDialog();

            }


        }
    }
}
