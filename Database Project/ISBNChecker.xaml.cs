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
    /// Interaction logic for ISBNChecker.xaml
    /// </summary>
    public partial class ISBNChecker : Window
    {
        //Variable declaration
        string itemID = "";

        /// <summary>
        /// Initialize window
        /// </summary>
        public ISBNChecker()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On click search if ISBN exists in the system, if not create new
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {

            string msg = "";
            bool exist = false;
            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\src\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand("ISBN_Check", dbConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ISBN", isbnTextBox.Text);
            
            

            // Try to connect to the database, and use the adapter to fill the table
            try
            {
                dbConnection.Open();
                SqlDataReader isbn = command.ExecuteReader();
               
                    if (isbn.HasRows)
                {
                    exist = true;
                    while (isbn.Read())
                    {
                        itemID = isbn["ITEM_ID"].ToString();
                    }
                    msg = "Item exists, please add new Inventory Record";
                }

               else  if (!isbn.HasRows)
                {
                    exist = false;
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

            //If item doesnt exist open add new item window
            if (exist == false)
            {
                AddItemWindow at = new AddItemWindow(isbnTextBox.Text);
                at.ShowDialog();
            }

            //if item exists open add new inv window
            else if(exist == true)
            {
                AddInventoryWindow av = new AddInventoryWindow(msg, itemID);
                av.ShowDialog();
            }
        }
    }
}
