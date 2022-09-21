using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace VisitorsManagementSystem_RFID
{
    public partial class MyProfile : Form
    {
        ConnectionClass connector = new ConnectionClass();

        public MyProfile()
        {
            InitializeComponent();
        }

        private void MyProfile_Load(object sender, EventArgs e)
        {
            try
            {
                String query = "select * from tbl_useraccounts a, tbl_userlogs b where b.AccountID = a.AccountID and a.AccountID = '" + txtinfoaccountid.Text + "'";
                connector.connect.Open();
                MySqlCommand cmd = connector.connect.CreateCommand();
                cmd.CommandText = query;
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    txtinfoname.Text = read["Name"].ToString();
                    txtinfooffice.Text = read["Office"].ToString();
                    txtinfouser.Text = read["UserType"].ToString();

                    DateTime signedin = Convert.ToDateTime(read["LoggedIn"]);
                    txtinfosignedin.Text = signedin.ToString("MMMM dd, yyyy hh:mm:ss tt");
                }
                connector.connect.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("System Error!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btncloseinfo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
