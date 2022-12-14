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
        string businessTitleInput = "";
        string buisnessTitle = "";
        public AddDealerWindow()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
           // DealerEditWindow dw = new DealerEditWindow();
           // dw.ShowDialog();
        }

        private void SubmitName(object sender, RoutedEventArgs e)
        {
            businessTitleInput = businessTitleUser.Text;

            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\Downloads\\Database Project\\Database Project\\Database Project\\BritannicusReadingRoom-3.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand("dealerBusinessNameCheck", dbConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DEALERBusinessTitle", businessTitleInput);

            try
            {
                dbConnection.Open();
                //command.ExecuteNonQuery();
                SqlDataReader isb = command.ExecuteReader();



                while (isb.Read())
                {
                    buisnessTitle = isb["DEALER_BUSINESS_TITLE"].ToString();
                    AddDealerProduct ap = new AddDealerProduct(businessTitleInput);
                    this.Close();
                    ap.ShowDialog();
                    break;

                }

                while (!isb.Read())
                {

                    addDealer(businessTitleInput);
                    this.Close();
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


        public void addDealer(string businessTitleInput)
        {
            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\rudeb\\Downloads\\Database Project\\Database Project\\Database Project\\BritannicusReadingRoom-3.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);

            SqlCommand command = new SqlCommand("addDealer", dbConnection);
             command.CommandType = System.Data.CommandType.StoredProcedure;
             command.Parameters.AddWithValue("@DealerBusinessTitle", businessTitleInput);
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

                dbConnection.Close();
            }

        }
    }
}
