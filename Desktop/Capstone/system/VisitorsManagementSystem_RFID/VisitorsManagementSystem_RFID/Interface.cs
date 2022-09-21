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
    public partial class Interface : Form
    {
        ConnectionClass connector = new ConnectionClass();

        public static string formlogid = "";
        public static string formlogaccountid = "";
        public static string formlogname = "";
        public static string formlogusertype = "";

        public Interface()
        {
            InitializeComponent();
            timer1.Start();
            btnindicator.Hide();
        }

        public void InsertUpdateDelete(String query)
        {
            connector.connect.Open();
            MySqlCommand cmd = connector.connect.CreateCommand();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            connector.connect.Close();

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
            lblday.Text = DateTime.Now.ToString("dddd");
            lbltime.Text = DateTime.Now.ToString("hh:mm:ss tt");

            if (ActiveMdiChild == null)
            {
                btnindicator.Hide();
            }
        }

        private void Interface_Load(object sender, EventArgs e)
        {
            
        }

        private void Interface_Activated(object sender, EventArgs e)
        {
            formlogid = lbllogid.Text;
            formlogaccountid = lblaccountid.Text;
            formlogname = lblname.Text;
            formlogusertype = lblusertype.Text;

            if(lblusertype.Text == "Administrator")
            {
                adminpanel.Show();
                userpanel.Hide();
                guardpanel.Hide();
            }
            else if (lblusertype.Text == "School Guard")
            {
                adminpanel.Hide();
                userpanel.Hide();
                guardpanel.Show();
            }
            else
            {
                adminpanel.Hide();
                userpanel.Show();
                guardpanel.Hide();
            }
        }

        private void lbllogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Log-out?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                String query = "update tbl_userlogs set LoggedOut = '"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"', LogStatus = 'Inactive' where LogID = '"+lbllogid.Text+"'";
                InsertUpdateDelete(query);
                Login i = new Login();
                i.Show();
                this.Hide();
            }
        }

        private void btnadminvisitorprofiling_Click(object sender, EventArgs e)
        {
            if(ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            btnindicator.SetBounds(254, 327, 26, 26);
            btnindicator.BringToFront();
            btnindicator.Show();
            VisitorsProling vp = new VisitorsProling();
            vp.MdiParent = this;
            vp.Show();
        }

        private void btnadminvisitingmanagement_Click(object sender, EventArgs e)
        {
            
        }

        private void btnadminreports_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            btnindicator.SetBounds(254, 470, 26, 26);
            btnindicator.BringToFront();
            btnindicator.Show();
            Reports r = new Reports();
            r.MdiParent = this;
            r.Show();
        }

        private void btnadminusermanagement_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            btnindicator.SetBounds(254, 538, 26, 26);
            btnindicator.BringToFront();
            btnindicator.Show();
            UserManagement um = new UserManagement();
            um.MdiParent = this;
            um.Show();
        }

        private void btnguardvisitorprofiling_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            btnindicator.SetBounds(254, 380, 26, 26);
            btnindicator.BringToFront();
            btnindicator.Show();
            VisitorsProling vp = new VisitorsProling();
            vp.MdiParent = this;
            vp.Show();
        }

        private void btnguardvisitingmanagement_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            btnindicator.SetBounds(254, 466, 26, 26);
            btnindicator.BringToFront();
            btnindicator.Show();
            VisitingManagement vm = new VisitingManagement();
            vm.MdiParent = this;
            vm.Show();
        }

        private void btnuservisitorprofilnig_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            btnindicator.SetBounds(254, 365, 26, 26);
            btnindicator.BringToFront();
            btnindicator.Show();
            VisitorsProling vp = new VisitorsProling();
            vp.MdiParent = this;
            vp.Show();
        }

        private void btnuservisitingmanagement_Click(object sender, EventArgs e)
        {
            
        }

        private void btnuserreports_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            btnindicator.SetBounds(254, 523, 26, 26);
            btnindicator.BringToFront();
            btnindicator.Show();
        }

        private void lblprofile_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            MyProfile mp = new MyProfile();
            mp.txtinfoaccountid.Text = formlogaccountid;
            mp.MdiParent = this;
            mp.Show();
        }

        private void btnadminvisiting_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            btnindicator.SetBounds(254, 400, 26, 26);
            btnindicator.BringToFront();
            btnindicator.Show();
            VisitingManagement vm = new VisitingManagement();
            vm.MdiParent = this;
            vm.Show();
        }
    }
}
