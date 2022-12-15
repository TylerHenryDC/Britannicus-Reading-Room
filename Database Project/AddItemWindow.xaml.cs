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
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        string msg = "";
        string itemID = "";
        string isbn = "";
        string error = "";
        bool valid = true;
        public AddItemWindow(string incISBN)
        {
            InitializeComponent();
            isbn = incISBN;
            isbnTextBox.Text = incISBN;
        }

        private void CheckValid()
        {
            error = "";
            valid = true;
            if (nameTextBox.Text == "")
            {
                error += "\nName Cannot Be Empty";
            }
            if (barcodeTextBox.Text == "")
            {
                error += "\n Barcode Cannot Be Empty";
            }
            else if (!int.TryParse(barcodeTextBox.Text, out int value))
            {
                valid = false;
                error = "Barcode Must Only Be Numbers";
            }
            if (catagoryComboBox.Text == "")
            {
                error += "\nCatagory Cannot Be Empty";
            }
            if (priceTextBox.Text == "")
            {
                error += "\nPrice Cannot Be Empty";
            }
            else if (!double.TryParse(priceTextBox.Text, out double value))
            {
                valid = false;
                error = "Price Must Be A Valid Price";
            }
            if (error != "")
            {
                valid = false;
            }
        }



        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            CheckValid();
            if (valid == false)
            {
                MessageBox.Show(error, "Error");

            }
            else
            {
                string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\source\\repos\\Database Project\\Database Project\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
                SqlConnection dbConnection = new SqlConnection(connectString);
                SqlCommand command = new SqlCommand("Item_Insert", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ItemName", nameTextBox.Text);
                command.Parameters.AddWithValue("@ItemCatID", (catagoryComboBox.SelectedIndex) + 1);
                command.Parameters.AddWithValue("@ItemDesc", descriptionTextbox.Text);
                command.Parameters.AddWithValue("@ISBN", isbnTextBox.Text);
                command.Parameters.AddWithValue("@ItemAuth", authorTextBox.Text);
                command.Parameters.AddWithValue("@ItemLang", languageTextBox.Text);
                command.Parameters.AddWithValue("@RetPric", priceTextBox.Text);
                command.Parameters.AddWithValue("@ItemBar", barcodeTextBox.Text);

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
                ISBNChecker(isbn);
                AddInventoryWindow av = new AddInventoryWindow(msg, itemID);
                av.ShowDialog();
                Close();
            }
        }

        private void ISBNChecker(string isbn)
        {
            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\source\\repos\\Database Project\\Database Project\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand("ISBN_Check", dbConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ISBN", isbnTextBox.Text);



            // Try to connect to the database, and use the adapter to fill the table
            try
            {
                dbConnection.Open();
                SqlDataReader isb = command.ExecuteReader();



                while (isb.Read())
                {
                    itemID = isb["ITEM_ID"].ToString();
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


        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
