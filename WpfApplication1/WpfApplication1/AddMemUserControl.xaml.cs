using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddMemUserControl.xaml
    /// </summary>
    public partial class AddMemUserControl : UserControl
    {
        public AddMemUserControl()
        {
            InitializeComponent();

            Loadinfo();

            Dateexpiry.Text = DateTime.Now.Date.AddYears(1).ToString();
            loadCardno();
        }

        string ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True"; 

        private void loadCardno()
        {

            //string ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True"; 

            String query = string.Format("select Upper(m_cardno)  from member");

            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int sid = int.Parse(reader[0].ToString());
                    //MemberidnoTextBox.Text = (sid + 1).ToString();
                    //MemberidnoTextBox.Text = (sid).ToString();

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SavememberButton_Click(object sender, RoutedEventArgs e)
        {


           if (MemberidnoTextBox.Text== string.Empty || RollnoTextBox.Text == string.Empty)
            {
                MessageBox.Show("Both Card no and Roll no is required");
            }


           else
           {

               if (CheckMemberCardno().ToString() == MemberidnoTextBox.Text)
               {
                   MessageBox.Show("The member already exists");
               }

               else
               {

                   Member member = new Member();

                   //member.Id = int.Parse(MemberidnoTextBox.Text);
                   member.Id = MemberidnoTextBox.Text;
                   member.Name = MembernameTextBox.Text;
                   member.Address = MemberaddressTextBox.Text;
                   member.MobileNo = MemberphonenumberTextBox.Text;
                   member.Department = DeptnameComboBox.Text;
                   member.Rollno = RollnoTextBox.Text;
                   member.DateExpiry = Dateexpiry.Text;
                   member.Session = SessionTextBox.Text;

                   string ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

                   String query = string.Format("insert into member values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", member.Id, member.Name, member.Address, member.MobileNo, member.Department, member.Rollno, member.DateExpiry, member.Session);

                   SqlConnection connection = new SqlConnection(ConnectionString);

                   try
                   {
                       connection.Open();
                       SqlCommand cmd = new SqlCommand(query, connection);
                       cmd.ExecuteNonQuery();
                       MessageBox.Show("Data is inserted Successfully!!");
                       connection.Close();
                       ClearBoxes();
                   }

                   catch (Exception ex)
                   {
                       MessageBox.Show(ex.Message);
                   }

               }

             }

         }

        private void ClearBoxes()
        {
            MembernameTextBox.Text = "";
            MemberaddressTextBox.Text = "";
            MemberphonenumberTextBox.Text ="";
            DeptnameComboBox.Text = "";
            RollnoTextBox.Text = "";
            Dateexpiry.Text = "";
            SessionTextBox.Text = "";
        }

        private int CheckMemberCardno()
        {
            int cardno = 0;

            //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            String query = string.Format("select m_cardno from member where m_cardno='" + MemberidnoTextBox.Text+ "'");

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




        private void Loadinfo()
        {
            string ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True"; 

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

     }

}

