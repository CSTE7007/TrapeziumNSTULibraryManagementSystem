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
    /// Interaction logic for AllDeptUserControl.xaml
    /// </summary>
    public partial class AllDeptUserControl : UserControl
    {
        public AllDeptUserControl()
        {
            InitializeComponent();
            LoadDepartmentList();
        }


        string ConnectionString =
              ConfigurationManager.ConnectionStrings["LibraryConString"].ConnectionString;


        private void LoadDepartmentList()
        {
            //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";
            String query = string.Format("select d_id, d_name from dept");
            SqlConnection connection = new SqlConnection(ConnectionString);
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                DepartementDataGrid.DataContext = ds.Tables[0].DefaultView;  // ListView

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
