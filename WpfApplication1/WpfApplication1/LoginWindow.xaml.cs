using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {

            SplashScreen sp = new SplashScreen("Splash.png");
            sp.Show(true);
            System.Threading.Thread.Sleep(2000);
          
            InitializeComponent();
        }


        string ConnectionString =
              ConfigurationManager.ConnectionStrings["LibraryConString"].ConnectionString;


        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    MainWindow m = new MainWindow();
        //    m.Show();
        //    this.Hide();
        //}

        //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";


        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            UsernameTextBox.Text = "";
            PasswordTextBox.Password = "";

        }

        private void LogIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TryLogin();
            }
        }


        void TryLogin()
        {
            String query = string.Format("select Username,Password from Login  where Username= '" + UsernameTextBox.Text +
                                 "' and  Password='" + PasswordTextBox.Password + "' ");

            var connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count++;
                }
                if (count == 1)
                {
                   // MessageBox.Show("Welcome ur Successfully Logged In");
                    var main = new MainWindow();

                    main.Show();
                    this.Close();

                    //Close();
                    //main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Username or Password  or Usertype is Wrong try again");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();

            }
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == string.Empty || PasswordTextBox.Password == string.Empty)
            {
                MessageBox.Show("Enter values on both fields !!", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (
                !Regex.Match(UsernameTextBox.Text,
                    @"^([A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+)$").Success)
            {
                MessageBox.Show("Error in User Name", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (!Regex.Match(PasswordTextBox.Password, @"^([a-z0-9_-]{6,18})$").Success)
            {
                MessageBox.Show("Error in  Password", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (true)
            {
                TryLogin();

            }

        }
    }
}
