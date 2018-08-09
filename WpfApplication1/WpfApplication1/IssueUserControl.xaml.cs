using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for IssueUserControl.xaml
    /// </summary>
    public partial class IssueUserControl : UserControl
    {
        public IssueUserControl()
        {
            InitializeComponent();
            LoadMemberCardno();
            LoadBookID();

            BookexpiryDatePicker.Text = DateTime.Now.Date.AddDays(15).ToString();
            BookissuesDatePicker.Text = DateTime.Now.Date.ToString();
            
        }


        string ConnectionString =
              ConfigurationManager.ConnectionStrings["LibraryConString"].ConnectionString;



        private void LoadBookID()
        {
           // String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            String query = string.Format("select b_id from book");

            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int sid = int.Parse(reader[0].ToString());
                    //Membercardno.Items.Add(sid);
                    BookIDComboBox.Items.Add(sid);

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }

        }


        private void LoadMemberCardno()
        {

            //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            String query = string.Format("select m_cardno from member");

            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int sid = int.Parse(reader[0].ToString());
                    //Membercardno.Items.Add(sid);
                    MembercardnoComboBox.Items.Add(sid);

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }
        }


        private void Clear()
        {

            MembercardnoComboBox.Text = "";
            MembernameTextBox.Text = "";
            BookIDComboBox.Text = "";
            BookNameTextBox.Text = "";
            BookissuesDatePicker.Text = "";
            BookexpiryDatePicker.Text = "";

        }


        private void IssueButton_Click(object sender, RoutedEventArgs e)
        {
            var bookIssues = new BookIssues();

            bookIssues.MemberCardNo =  int.Parse( MembercardnoComboBox.Text);
            bookIssues.BookID = BookIDComboBox.Text;

            bookIssues.BookName = BookNameTextBox.Text;
            bookIssues.MemberName = MembernameTextBox.Text;

            bookIssues.DateIssues = BookissuesDatePicker.Text;
            bookIssues.DateExpiry = BookexpiryDatePicker.Text;

            int totalbooks = Counttotalbooks() - 1;

            if (CheckMemberCardno() != bookIssues.MemberCardNo)
            {

                if (totalbooks <= 0)
                {
                    MessageBox.Show("Sorry,U can't have this book because it is confiend copy.");
                    Clear();
                }
                else
                {
                    //string ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

                    string query = string.Format("insert into issuesbook values('{0}','{1}','{2}','{3}','{4}','{5}')",
                        bookIssues.MemberCardNo, bookIssues.MemberName, bookIssues.BookID, bookIssues.BookName, bookIssues.DateIssues,
                        bookIssues.DateExpiry);

                    string query1 =
                        string.Format("update book set b_noofbooks='" + totalbooks + "' where b_id='" +
                                      BookIDComboBox.Text+ "'");

                    string query2 = string.Format("insert into returnbook values('{0}','{1}','{2}','{3}','{4}','{5}')",
                        bookIssues.MemberCardNo, bookIssues.MemberName,bookIssues.BookID, bookIssues.BookName, bookIssues.DateIssues,
                        bookIssues.DateExpiry);

                    SqlConnection connection = new SqlConnection(ConnectionString);

                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(query, connection);
                        SqlCommand cmd1 = new SqlCommand(query1, connection);
                        SqlCommand cmd2 = new SqlCommand(query2, connection);
                        cmd.ExecuteNonQuery();
                        cmd1.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Thank you!!!! for issuing book \n Data is inserted Successfully and Book is updated!!");
                        connection.Close();
                        Clear();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            else
            {
                MessageBox.Show("First, return your last borrowed book !!");
                Clear();
            }


        }


        private int Counttotalbooks()
        {
            int i = 0;
            //var ConnectionString = @"server=.\SQLEXPRESS;Database=Library;Integrated Security=true";
            String query = string.Format("select b_noofbooks from book where b_id='" + BookIDComboBox.SelectedItem + "'");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    i = int.Parse(reader[0].ToString());


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return i;
        }


        private int CheckMemberCardno()
        {
            int cardno = 0;

            //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            String query = string.Format("select r_mcardno from returnbook where r_mcardno='" + MembercardnoComboBox.SelectedValue + "'");

            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cardno = int.Parse(reader[0].ToString());

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }

            return cardno;
        }

    

        private void MembercardnoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            String query = string.Format("select m_name from member where m_cardno='" + MembercardnoComboBox.SelectedValue + "'");


            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                     string smemname = reader[0].ToString();
                     MembernameTextBox.Text = smemname;


                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }
        }

        

        private void BookIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

             
           // String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            String query = string.Format("select b_name from book where b_id='" +  BookIDComboBox.SelectedValue + "'");

            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    string sbookname = reader[0].ToString();
                    BookNameTextBox.Text=sbookname;


                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }

        }

    }
}
