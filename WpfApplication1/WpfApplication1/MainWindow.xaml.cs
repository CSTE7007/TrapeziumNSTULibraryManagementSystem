using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            SplashScreen sp = new SplashScreen("Splash.png");
            sp.Show(true);
            System.Threading.Thread.Sleep(2000);


            InitializeComponent();
            LoadDeptName();
            ShowAllBooks();   
        }

        string ConnectionString =
               ConfigurationManager.ConnectionStrings["LibraryConString"].ConnectionString;


        //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

        private void AddMember_Selected(object sender, RoutedEventArgs e)
        {

            MemberPageNavigator.Navigate(new AddMemUserControl());
            //Panel.Children.Clear();
            //var page = new AddMemUserControl();
            //Panel.Children.Add(page);
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            MemberPageNavigator.Navigate(new UpMemUserControl());
        }

        private void ListBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            MemberPageNavigator.Navigate(new DelMemUserControl());
        }

        private void ListBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {


            MemberPageNavigator.Navigate(new AllMemUserControl());
            /*
             * if it was a window
            AllMemberWindow am = new AllMemberWindow();
            am.Show();
            this.Hide();
            */



            //to show MemListUserControl()
           // MemberPageNavigator.Navigate(new MemListUserControl());


            //Panel.Children.Clear();
            //var page = new MemListUserControl();
            //Panel.Children.Add(page);
        }

        private void ListBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            TransactionPageNavigator.Navigate(new IssueUserControl());
        //    IssuePanel.Children.Clear();
        //    var page = new IssueUserControl();
        //    IssuePanel.Children.Add(page);
        }

        private void ListBoxItem_Selected_4(object sender, RoutedEventArgs e)
        {
            TransactionPageNavigator.Navigate(new ReturnUserControl());

            //IssuePanel.Children.Clear();
            //var page = new ReturnUserControl();
            //IssuePanel.Children.Add(page);
        }

        private void ListBoxItem_Selected_5(object sender, RoutedEventArgs e)
        {
            TransactionPageNavigator.Navigate(new FineUserControl());

            //IssuePanel.Children.Clear();
            //var page = new FineUserControl();
            //IssuePanel.Children.Add(page);

        }

        private void ListBoxItem_Selected_6(object sender, RoutedEventArgs e)
        {
            BooksPageNavigator.Navigate(new AddBookUserControl());

            //BookPanel.Children.Clear();
            //var page = new AddBookUserControl();
            //BookPanel.Children.Add(page);
        }

        private void ListBoxItem_Selected_7(object sender, RoutedEventArgs e)
        {
            BooksPageNavigator.Navigate(new UpBookUserControl());

            //BookPanel.Children.Clear();
            //var page = new UpBookUserControl();
            //BookPanel.Children.Add(page);
        }

        private void ListBoxItem_Selected_8(object sender, RoutedEventArgs e)
        {
            BooksPageNavigator.Navigate(new DelBookUserControl());

            //BookPanel.Children.Clear();
            //var page = new DelBookUserControl();
            //BookPanel.Children.Add(page);

        }

        private void LoadDeptName()
        {
            //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            String query = string.Format("select d_name from dept");

            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string sname = reader[0].ToString();
                    DeptnameComboBox.Items.Add(sname);


                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void ShowAllBooks()
        {
            //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            String query = string.Format("select * from book");

            SqlConnection connection = new SqlConnection(ConnectionString);



            try
            {
                connection.Open();

                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                BookCatalog.DataContext = ds.Tables[0].DefaultView; //DataGrid
                connection.Close();

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void DeptnameComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            String query = string.Format("select  b_id, b_name, b_authorname1, b_authorname2, b_authorname3, b_noofbooks, b_publishername, b_edition, b_shelfno from book where b_deptname='" +
                    DeptnameComboBox.SelectedItem + "'");

            SqlConnection connection = new SqlConnection(ConnectionString);



            try
            {
                connection.Open();

                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                BookCatalog.DataContext = ds.Tables[0].DefaultView; //DataGrid
                connection.Close();

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ListBoxItem_Selected_9(object sender, RoutedEventArgs e)
        {

            DepartmentPageNavigator.Navigate(new AddDeptUserControl());
        }

        private void ListBoxItem_Selected_10(object sender, RoutedEventArgs e)
        {
            DepartmentPageNavigator.Navigate(new AllDeptUserControl());

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            DeptnameComboBox.Text = "";
            ShowAllBooks();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Close();
        }

        private void bookNameSearchTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {

            //String query = "select * from book where b_name like '%"+searchTextBox.Text +"%'";

            String query = "select * from book where b_name like '%" + bookNameSearchTextBox.Text + "%'";

            //String query = string.Format("select * from book");


            SqlConnection connection = new SqlConnection(ConnectionString);



            try
            {
                connection.Open();

                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                BookCatalog.DataContext = ds.Tables[0].DefaultView; //DataGrid
                connection.Close();

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
 
        private void IssuBookLog_Selected(object sender, RoutedEventArgs e)
        {
            TransactionLog.Navigate(new IssuedBookLogUserControl());
        }

        private void FineLog_Selected(object sender, RoutedEventArgs e)
        {
            TransactionLog.Navigate(new FineLogUserControl());
        }

        private void QueuedBookLog_Selected(object sender, RoutedEventArgs e)
        {
            TransactionLog.Navigate(new QeuedBookUserControl());
        }



    }
}
