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
    /// Interaction logic for EditInventoryWindow.xaml
    /// </summary>
    public partial class EditInventoryWindow : Window
    {
        //Variable Declartion
        bool valid = true;
        string error = "";

        /// <summary>
        /// Initital components and add variables to global varaibles
        /// </summary>
        /// <param name="invID"></param>
        /// <param name="condition"></param>
        /// <param name="quantity"></param>
        public EditInventoryWindow(string invID, string condition, string quantity)
        {
            InitializeComponent();

            invIDLabel.Content = invID;
            conditionLabel.Content = condition;
            quantityTextBox.Text = quantity;

        }

        /// <summary>
        /// On click close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Check if quantity value is a valid
        /// </summary>
        private void CheckValid()
        {
            valid = true;
            error = "";
            if (!int.TryParse(quantityTextBox.Text, out int value))
            {
                valid = false;
                error = "Quantity Must Be A Valid Number";
            }
        }

        /// <summary>
        /// On click check if information is valid and if so, update the new quantity of the inv item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            CheckValid();
            if (valid == false){
                MessageBox.Show(error, "Error");
            }
            else {
                string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\src\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
                SqlConnection dbConnection = new SqlConnection(connectString);
                SqlCommand command = new SqlCommand("Quantity_Update", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", invIDLabel.Content);
                command.Parameters.AddWithValue("@Quantity", quantityTextBox.Text);
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
                Close();
            }
        }
    }
}
