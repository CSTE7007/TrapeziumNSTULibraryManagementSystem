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
    /// Interaction logic for ReturnUserControl.xaml
    /// </summary>
    public partial class ReturnUserControl : UserControl
    {
        public ReturnUserControl()
        {
            InitializeComponent();
            LoadMemberCardno();
            //LoadBookID();

            BookexpiryDatePicker.Text = DateTime.Now.Date.AddDays(15).ToString();
            BookissuesDatePicker.Text = DateTime.Now.Date.ToString();
            TodaysDate.Text = DateTime.Now.Date.ToString();
        }


        string ConnectionString =
              ConfigurationManager.ConnectionStrings["LibraryConString"].ConnectionString;




        //private void LoadBookID()
        //{
        //    String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

        //    String query = string.Format("select b_id from book");

        //    SqlConnection connection = new SqlConnection(ConnectionString);

        //    try
        //    {
        //        connection.Open();
        //        SqlCommand cmd = new SqlCommand(query, connection);

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            int sid = int.Parse(reader[0].ToString());
        //            //Membercardno.Items.Add(sid);

        //            BookIDComboBox.Items.Add(sid);

        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);


        //    }

        //}


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

                    }
                }
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        //private void BookIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{


        //    String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

        //    String query = string.Format("select b_name from book where b_id='" + BookIDComboBox.SelectedValue + "'");

        //    SqlConnection connection = new SqlConnection(ConnectionString);

        //    try
        //    {
        //        connection.Open();
        //        SqlCommand cmd = new SqlCommand(query, connection);

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {

        //            string sbookname = reader[0].ToString();
        //            BookNameTextBox.Text = sbookname;


        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);


        //    }

        //}


        private void Clear()
        {

            //MembercardnoComboBox.Text = "";
            MembernameTextBox.Text = "";
            BookIDTextBox.Text = "";
            BookNameTextBox.Text = "";
           // BookissuesDatePicker.Text = "";
           // BookexpiryDatePicker.Text = "";

        }


        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {

            DateTime todaysdate = DateTime.Today;

            DateTime expirydate = BookexpiryDatePicker.DisplayDate;

            DateTime issuedate = BookissuesDatePicker.DisplayDate;

            TimeSpan span = todaysdate.Subtract(issuedate);

            //TimeSpan span = expirydate.Date - issuedate.Date;

            //if(span.TotalDays<=15)  
            if(span.TotalDays <= 15)
            {

                int totalbooks = Counttotalbooks() + 1;

                //string ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

                //string query = string.Format("update book set b_noofbooks ='" + totalbooks + "' where b_id ='" + BookIDComboBox.Text + "'");

                string query = string.Format("update book set b_noofbooks ='" + totalbooks + "' where b_id ='" + BookIDTextBox.Text + "'");


                string query1 = string.Format("delete  from returnbook where r_mcardno='" + MembercardnoComboBox.SelectedItem + "'");

                SqlConnection connection = new SqlConnection(ConnectionString);

                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlCommand cmd1 = new SqlCommand(query1, connection);
                    cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    
                    MessageBox.Show("Book is updated and cleared from returning!!");
                    connection.Close();
                    Clear();
                    


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            else
            {
                MessageBox.Show("U are fined ");
               Clear();
            }
        }


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

        }

 }


