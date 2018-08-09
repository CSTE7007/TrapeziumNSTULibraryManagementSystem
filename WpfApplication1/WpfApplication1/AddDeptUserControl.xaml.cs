using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
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
    /// Interaction logic for AddDeptUserControl.xaml
    /// </summary>
    public partial class AddDeptUserControl : UserControl
    {
        public AddDeptUserControl()
        {
            InitializeComponent();
            LoadDeptId();
        }


        string ConnectionString =
              ConfigurationManager.ConnectionStrings["LibraryConString"].ConnectionString;


        //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

        private void insertdeptButton_Click(object sender, RoutedEventArgs e)
        {
            Department department = new Department();

            department.Dept_id = int.Parse(DeptidTextBox.Text);
            department.Name = DeptnameTextBox.Text;


            //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            String query = string.Format("insert into dept values('{0}','{1}')", department.Dept_id, department.Name);

            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                //  int n = Convert.ToInt32(cmd.ExecuteScalar());
                // if (n == 1)
                // {
                MessageBox.Show("Data is inserted Successfully!!");
                connection.Close();
                // }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        private void LoadDeptId()
        {

            //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            String query = string.Format("select Upper(d_id) from dept");

            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int sid = int.Parse(reader[0].ToString());
                    DeptidTextBox.Text = (sid + 1).ToString(CultureInfo.InvariantCulture);

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
