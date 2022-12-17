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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// On click searches for user name and password logs in if exists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logInButton_Click(object sender, RoutedEventArgs e)
        {
            //Encrypts password
            byte xorConstant = 0x53;
          
            string input = passwordTextBox.Text;
            byte[] data = Encoding.UTF8.GetBytes(input);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ xorConstant);
            }
            string password = Convert.ToBase64String(data);
            

            
            bool exist = false;

            string connectString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\src\\BritannicusReadingRoom.mdf\"; Integrated Security = True;";
            SqlConnection dbConnection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand("PasswordCheck", dbConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User", userTextBox.Text);
            command.Parameters.AddWithValue("@Password", password);



            // Try to connect to the database, and use the adapter to fill the table
            try
            {
                dbConnection.Open();
                SqlDataReader user = command.ExecuteReader();

                if (user.HasRows)
                {
                    exist = true;
                }

                else if (!user.HasRows)
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
                MessageBox.Show("User Name or Password Is Incorrect Please Enter Valid Credentials");
            }

            //if item exists open add new inv window
            else if (exist == true)
            {
                DashboardWindow db = new DashboardWindow();
                db.ShowDialog();
                Close();
            }


        }

        /// <summary>
        /// On click close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
