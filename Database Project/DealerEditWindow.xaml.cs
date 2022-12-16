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


        string dealerPriceIDFinal = "";

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



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Confirm(object sender, RoutedEventArgs e)
        {
            if (titleItem.Text.Length == 0 || titleDescription.Text.Length == 0 || titlePrice.Text.Length == 0 || titleType.Text.Length == 0 || titleISBN.Text.Length == 0 || dealerPriceIDFinal == "")
            {
                MessageBox.Show("Can't process the empty value");
            }
            else
            {
                string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\devan\\Downloads\\Database Project\\Database Project\\Database Project\\BritannicusReadingRoom-3.mdf\"; Integrated Security = True;";
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

                DealerWindow dw1 = new DealerWindow();

                dw1.ShowDialog();

            }


        }
    }
}
