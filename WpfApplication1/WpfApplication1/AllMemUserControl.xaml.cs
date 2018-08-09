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
    /// Interaction logic for AllMemUserControl.xaml
    /// </summary>
    public partial class AllMemUserControl : UserControl
    {
        public AllMemUserControl()
        {
            InitializeComponent();
            loadMemberList();
        }


        string ConnectionString =
              ConfigurationManager.ConnectionStrings["LibraryConString"].ConnectionString;


        private void loadMemberList()
        {

            //String ConnectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=True";

            String query = string.Format("select * from member");
            SqlConnection connection = new SqlConnection(ConnectionString);
            DataSet ds = new DataSet();

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                MemberDataGrid.DataContext = ds.Tables[0].DefaultView; //DataGrid
                connection.Close();
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
