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
    /// Interaction logic for FineUserControl.xaml
    /// </summary>
    public partial class FineUserControl : UserControl
    {
        public FineUserControl()
        {
            InitializeComponent();
            LoadMemberCardno();
        }

        string ConnectionString =
              ConfigurationManager.ConnectionStrings["LibraryConString"].ConnectionString;



        public class Fine
        {
            public double finetopay { get; set; }
            public string today { get; set; }
            public string amount { get; set; }
        }       


        //private void CalculateFine()
        //{
        //    Fine f = new Fine();

        //    DateTime todaysdate = DateTime.Today;

        //    DateTime expirydate = BookexpiryDatePicker.DisplayDate;

        //    TimeSpan span = todaysdate.Subtract(expirydate);

        //    f.finetopay = span.TotalDays * 5;
        //}

        private int Counttotalbooks()
        {
            int i = 0;
            //var ConnectionString = @"server=.\SQLEXPRESS;Database=Library;Integrated Security=true";

            //String query = string.Format("select b_noofbooks from book where b_id='" + BookIDComboBox.SelectedItem + "'");
            String query = string.Format("select b_noofbooks from book where b_id='" + BookIDTextBox.Text + "'");
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



        private void PayFineButton_Click(object sender, RoutedEventArgs e)
        {

            var bookIssues = new BookIssues();
            Fine fine = new Fine();

            bookIssues.MemberCardNo = int.Parse(MembercardnoComboBox.Text);
            bookIssues.BookID = BookIDTextBox.Text;

            bookIssues.BookName = BookNameTextBox.Text;
            bookIssues.MemberName = MembernameTextBox.Text;

            bookIssues.DateIssues = BookissuesDatePicker.Text;
            bookIssues.DateExpiry = BookexpiryDatePicker.Text;

            fine.today= DateTime.Today.ToString();
            fine.amount = FineAmountTextBox.Text;


            int totalbooks = Counttotalbooks() + 1;

            //string ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            //string query = string.Format("update book set b_noofbooks ='" + totalbooks + "' where b_id ='" + BookIDComboBox.Text + "'");

            string query = string.Format("update book set b_noofbooks ='" + totalbooks + "' where b_id ='" + BookIDTextBox.Text + "'");
           
            string query1 = string.Format("delete  from returnbook where r_mcardno='" + MembercardnoComboBox.SelectedItem + "'");

            string query2 = string.Format("insert into finebook values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                        bookIssues.MemberCardNo, bookIssues.MemberName, bookIssues.BookID, bookIssues.BookName,bookIssues.DateIssues,
                        bookIssues.DateExpiry,fine.today,fine.amount);
          
            
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

                MessageBox.Show(" thank you!!! You paid the accumulate fine.\n Book is updated and cleared from returning!!");
                connection.Close();

                MembernameTextBox.Text = "";
                BookIDTextBox.Text = "";
                BookNameTextBox.Text = "";
                FineAmountTextBox.Text = "";

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

        private void MembercardnoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            String query = string.Format("select * from returnbook where r_mcardno='" + MembercardnoComboBox.SelectedValue + "'");

            //String query = string.Format("select r_mname,r_bookid,r_bookname,r_dateofissue,r_dateofexpiry from returnbook where r_mcardno='"
            //    + MembercardnoComboBox.SelectedValue + "'");

            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    MessageBox.Show("Member has not taken any book or he has already submitted his books");

                    //Clear();
                }
                else
                {
                    while (reader.Read())
                    {
                        string smemname = reader[2].ToString();
                        string sbookid = reader[3].ToString();
                        string sbookname = reader[4].ToString();
                        string sissudate = reader[5].ToString();
                        string sexpiredate = reader[6].ToString();

                       
                        MembernameTextBox.Text = smemname;
                        //BookIDComboBox.Text = sbookid;
                        BookIDTextBox.Text = sbookid;
                        BookNameTextBox.Text = sbookname;
                        BookissuesDatePicker.Text = sissudate;
                        BookexpiryDatePicker.Text = sexpiredate;


                        Fine f = new Fine();

                        DateTime todaysdate = DateTime.Today;

                        TodaysDate.Text = DateTime.Today.ToString();
                        DateTime expirydate = DateTime.Parse(BookexpiryDatePicker.Text);

                        //TimeSpan span = expirydate.Subtract(todaysdate);

                        TimeSpan span = todaysdate.Subtract(expirydate);


                        f.finetopay = span.TotalDays * 3.00;

                        FineAmountTextBox.Text = f.finetopay.ToString();

                    }
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
