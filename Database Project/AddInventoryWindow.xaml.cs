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
    /// Interaction logic for AddInventoryWindow.xaml
    /// </summary>
    public partial class AddInventoryWindow : Window
    {
        //varaible declaration
        string ID;
        string error = "";
        bool valid = true;

        /// <summary>
        /// Initial startup, add incoming varaibles to global varaibles
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="itemID"></param>
        public AddInventoryWindow(string msg, string itemID)
        {
            InitializeComponent();
            ID = itemID;
            msgLabel.Content = msg;
        }

        /// <summary>
        /// On Click close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Check if given information is valid, error if not
        /// </summary>
        private void CheckValid()
        {
            valid = true;
            error = "";
            if (conditionComboBox.Text == "")
            {
                error += "\nCondition Cannot Be Empty";
            }
            if (quantityTextBox.Text == "")
            {
                error += "\nQuantity Cannot Be Empty";
            }
            else if (!int.TryParse(quantityTextBox.Text, out int value))
            {                
                error = "Quantity Must Be A Valid Number";
            }
            if (priceTextBox.Text == "")
            {
                error += "\nPrice Cannot Be Empty";
            }
            else if (!double.TryParse(priceTextBox.Text, out double val))
            {
                valid = false;
                error = "Price Must Be A Valid Price";
            }
            if (error != "")
            {
                valid = false;
            }
           
        }

        /// <summary>
        /// On click , if valid, add item to item table, otherwise send error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            //Send error if info is not valid
            CheckValid();
            if (valid == false)
            {
                MessageBox.Show(error, "Error");
            }

            //If valid, add item to item table
            else
            {
                string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\Downloads\\Database Project\\Database Project\\Database Project\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
                SqlConnection dbConnection = new SqlConnection(connectString);
                SqlCommand command = new SqlCommand("Inventory_Insert", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ItemID", ID);
                command.Parameters.AddWithValue("@ItemCon", (conditionComboBox.SelectedIndex) + 1);
                command.Parameters.AddWithValue("@Quantity", quantityTextBox.Text);
                command.Parameters.AddWithValue("@Sales_Price", priceTextBox.Text);

                SqlDataAdapter adapter = new SqlDataAdapter(command);


                // Try to connect to the database, and use the adapter to fill the tableItemCon,

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
                Close();
            }
        }
    }
}
