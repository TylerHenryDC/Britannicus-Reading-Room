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


        public AddDealerProduct(string businessTitle)
        {
            InitializeComponent();
            businessTitleProductAdd = businessTitle;
            AddItemBuisnessTitle.Text = businessTitle;
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            itemNameInput = AddItemName.Text;
            itemDescription = AddItemDescription.Text;
            //itemCondition = AddItemCondition.Text;
            dealersPrice = AddDealerPrice.Text;
            itemType = AddItemType.Text;
            ISBN = AddISBN.Text;

            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\Downloads\\Database Project\\Database Project\\Database Project\\BritannicusReadingRoom-3.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand("getDealerID", dbConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DealerBusinessTitle", businessTitleProductAdd);

            try
            {
                dbConnection.Open();

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

            
            SqlConnection dbConnection1 = new SqlConnection(connectString);
            SqlCommand command1 = new SqlCommand("InsertDealerItem", dbConnection1);
            command1.CommandType = System.Data.CommandType.StoredProcedure;
            command1.Parameters.AddWithValue("@DealerID", dealerID);
            command1.Parameters.AddWithValue("@ItemName", itemNameInput);
            command1.Parameters.AddWithValue("@ItemDescription", itemDescription);
            command1.Parameters.AddWithValue("@ItemType", itemType);
            command1.Parameters.AddWithValue("@ISBN", ISBN);
            try
            {
                dbConnection1.Open();

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

                dbConnection1.Close();
            }



            SqlConnection dbConnection2 = new SqlConnection(connectString);
            SqlCommand command2 = new SqlCommand("getItemID", dbConnection2);
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.Parameters.AddWithValue("@ISBN", ISBN);



            try
            {
                dbConnection2.Open();

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


            SqlConnection dbConnection3 = new SqlConnection(connectString);
            SqlCommand command3 = new SqlCommand("InsertDealerPrice", dbConnection3);
            command3.CommandType = System.Data.CommandType.StoredProcedure;
            command3.Parameters.AddWithValue("@DealerItemID", dealerItemID);
            command3.Parameters.AddWithValue("@DealerConditionID", (conditionComboBox.SelectedIndex) + 1);
            command3.Parameters.AddWithValue("@DealerPrice", dealersPrice);


            try
            {
                dbConnection3.Open();

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


            this.Close();
            DealerWindow dw1 = new DealerWindow();

            dw1.ShowDialog();
        }
    }
}
