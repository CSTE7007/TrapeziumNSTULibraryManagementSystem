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
    /// Interaction logic for DelMemUserControl.xaml
    /// </summary>
    public partial class DelMemUserControl : UserControl
    {
        public DelMemUserControl()
        {
            InitializeComponent();
            loadCardno();
            //loadDeptname();
        }


        string ConnectionString =
              ConfigurationManager.ConnectionStrings["LibraryConString"].ConnectionString;


        //private void loadDeptname()
        //{
        //    String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

        //    String query = string.Format("select d_name from dept");

        //    SqlConnection connection = new SqlConnection(ConnectionString);

        //    try
        //    {
        //        connection.Open();
        //        SqlCommand cmd = new SqlCommand(query, connection);

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            string sname = reader[0].ToString();
        //            DeptnameComboBox.Items.Add(sname);


        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void loadCardno()
        {
           // String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            String query = string.Format("select m_cardno from member");

            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string sname = reader[0].ToString();
                    MembercarddnoComboBox.Items.Add(sname);


                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearBoxes()
        {
            MembernameTextBox.Text = "";
            MemberaddressTextBox.Text = "";
            MemberphonenumberTextBox.Text = "";
            DeptnameComboBox.Text = "";
            RollnoTextBox.Text = "";
            Dateexpiry.Text = "";
            SessionTextBox.Text = "";
        }

        private void DeletememberButton_Click(object sender, RoutedEventArgs e)
        {

            //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            String query = string.Format("delete from member where m_cardno='" + MembercarddnoComboBox.SelectedValue + "'");

            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Ur data is deleted");
                connection.Close();
                ClearBoxes();
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MembercarddnoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string ConnectionString = @"Server=.\SQLEXPRESS;Database=Library; Integrated Security = True";

            String query = string.Format("select m_name, m_address, m_mobileno, m_deptname, m_rollno, m_dateofexpiry, m_session from member where m_cardno='" + MembercarddnoComboBox.SelectedValue + "'");

            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string sname = reader[0].ToString();
                    string saddress = reader[1].ToString();
                    string smobileno = reader[2].ToString();
                    string sdeptname = reader[3].ToString();
                    string srollno = reader[4].ToString();
                    string sdateofexpiry = reader[5].ToString();
                    string ssession = reader[6].ToString();

                    MembernameTextBox.Text = sname;
                    MemberaddressTextBox.Text = saddress;
                    MemberphonenumberTextBox.Text = smobileno;
                    DeptnameComboBox.Text = sdeptname;
                    RollnoTextBox.Text = srollno;
                    Dateexpiry.Text = sdateofexpiry;
                    SessionTextBox.Text = ssession;
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
        


    }
}
