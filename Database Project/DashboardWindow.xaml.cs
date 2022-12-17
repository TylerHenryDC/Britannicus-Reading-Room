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
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {
        /// <summary>
        /// Initialize Window
        /// </summary>
        public DashboardWindow()
        {
            InitializeComponent();
            onLoad();
        }

        /// <summary>
        /// Closes Window and opens login screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow mw = new MainWindow();
            this.Close();
            mw.ShowDialog();
            
        }

        /// <summary>
        /// On click opens inventory window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inventoryButton_Click(object sender, RoutedEventArgs e)
        {
            InventoryWindow iw = new InventoryWindow();
            iw.ShowDialog();
        }

        /// <summary>
        /// On click opens customer window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customerButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow cw = new CustomerWindow();
            cw.ShowDialog();
        }

        /// <summary>
        /// On Click opens dealer window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dealerButton_Click(object sender, RoutedEventArgs e)
        {
            DealerWindow dw = new DealerWindow();
            
            dw.ShowDialog();

        }
        private void onLoad()
        {




            // getting book count from the stored procedure
            var bookcount = "";
            // BookCountLabel.Content = bookcount;



            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\src\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand("EXEC  BookCount", dbConnection);




            try
            {
                dbConnection.Open();
                //command.ExecuteNonQuery();
                BookCountLabel.Content = command.ExecuteScalar().ToString();
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




            // getting inventory count from the stored procedure
            var inventorycount = "";
            SqlConnection dbConnection1 = new SqlConnection(connectString);
            SqlCommand command1 = new SqlCommand("EXEC  InventoryCount", dbConnection1);



            try
            {
                dbConnection1.Open();
                //command.ExecuteNonQuery();
                InventoryCountLabel.Content = command1.ExecuteScalar().ToString();
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







            // getting author count from the stored procedure
            var authorcount = "";
            SqlConnection dbConnection2 = new SqlConnection(connectString);
            SqlCommand command2 = new SqlCommand("EXEC  AuthorCount", dbConnection2);



            try
            {
                dbConnection2.Open();
                //command.ExecuteNonQuery();
                AuthorCountLabel.Content = command2.ExecuteScalar().ToString();
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



        }

        /// <summary>
        /// On click gives info on the other buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void infoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Customer - Add, Edit or View a Customers Information\n" +
                            "Dealer - Add, Edit or View Dealers and Dealers Product Information\n"+
                            "Inventory - View Current Inventory or Add New Product\n" +
                            "Log Out - Log Out of Your Current Session");    
        }
    }
}
