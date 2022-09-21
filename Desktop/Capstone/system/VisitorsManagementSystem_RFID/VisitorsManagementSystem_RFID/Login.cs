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
    public partial class Login : Form
    {
        ConnectionClass connector = new ConnectionClass();
        public string logid, logstatus, accountid, password, usertype, office, name, status, remarks, userremarks, incrementlogid;

        public static string formlogid = "";
        public static string formlogaccountid = "";
        public static string formlogname = "";
        public static string formlogusertype = "";
        public static string formlogoffice = "";


        public Login()
        {
            InitializeComponent();
        }

        public void InsertUpdateDelete(String query)
        {
            connector.connect.Open();
            MySqlCommand cmd = connector.connect.CreateCommand();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            connector.connect.Close();
        }

        public void IncrementLogID()
        {
            String query = "select count(LogID) from tbl_userlogs";
            connector.connect.Open();
            MySqlCommand cmd = connector.connect.CreateCommand();
            cmd.CommandText = query;
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            connector.connect.Close();
            i++;
            incrementlogid = i.ToString();
        }

        private void txtpass_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnlogin.PerformClick();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("System Error Found!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtuser_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnlogin.PerformClick();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("System Error Found!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "select * from tbl_useraccounts where AccountID = '" + txtuser.Text + "' and Passwords = '" + txtpass.Text + "'";
                connector.connect.Open();
                MySqlCommand cmd = connector.connect.CreateCommand();
                cmd.CommandText = query;
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    accountid = read["AccountID"].ToString();
                    password = read["Passwords"].ToString();
                    name = read["Name"].ToString();
                    usertype = read["UserType"].ToString();
                    office = read["Office"].ToString();
                    status = read["Status"].ToString();
                    remarks = read["Remarks"].ToString();
                }
                connector.connect.Close();

                String querylog = "select * from tbl_userlogs where AccountID = '" + txtuser.Text + "' and LogStatus = 'Active Now'";
                connector.connect.Open();
                MySqlCommand cmdlog = connector.connect.CreateCommand();
                cmdlog.CommandText = querylog;
                MySqlDataReader readlog = cmdlog.ExecuteReader();
                while (readlog.Read())
                {
                    logid = readlog["LogID"].ToString();
                    logstatus = readlog["LogStatus"].ToString();
                }
                connector.connect.Close();

                String queryuser = "select * from tbl_usertype where UserType = '" + usertype + "'";
                connector.connect.Open();
                MySqlCommand cmduser = connector.connect.CreateCommand();
                cmduser.CommandText = queryuser;
                MySqlDataReader readuser = cmduser.ExecuteReader();
                while (readuser.Read())
                {
                    userremarks = readuser["Remarks"].ToString();
                }
                connector.connect.Close();

                if (accountid == txtuser.Text && password == txtpass.Text)
                {
                    if (status != "Activated")
                    {
                        MessageBox.Show("Account is not yet Activated. Please Contact your Administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtpass.Text = "";
                    }
                    else if (remarks != "Approved")
                    {
                        MessageBox.Show("Account is not yet Approved. Please Contact your Administrator", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtpass.Text = "";
                    }
                    else if (logstatus == "Active Now")
                    {
                        MessageBox.Show("Account is currently active.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtpass.Text = "";
                    }
                    else
                    {
                        if (userremarks == "Secretary")
                        {
                            IncrementLogID();
                            String queryinsertlog = "insert into tbl_userlogs (LogID, LoggedIn, LogStatus, AccountID) values ('" + Convert.ToInt32(incrementlogid) + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'Active Now', '" + accountid + "')";
                            InsertUpdateDelete(queryinsertlog);

                            formlogid = incrementlogid;
                            formlogaccountid = accountid;
                            formlogname = name;
                            formlogusertype = usertype;
                            formlogoffice = office;

                            SecretaryInterface ii = new SecretaryInterface();
                            ii.lbllogid.Text = incrementlogid;
                            ii.Show();
                            this.Hide();
                        }
                        else
                        {
                            IncrementLogID();
                            String queryinsertlog = "insert into tbl_userlogs (LogID, LoggedIn, LogStatus, AccountID) values ('" + Convert.ToInt32(incrementlogid) + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'Active Now', '" + accountid + "')";
                            InsertUpdateDelete(queryinsertlog);

                            formlogid = incrementlogid;
                            formlogaccountid = accountid;
                            formlogname = name;
                            formlogusertype = usertype;
                            formlogoffice = office;


                            Interface ii = new Interface();
                            ii.lblaccountid.Text = accountid;
                            ii.lbllogid.Text = incrementlogid;
                            ii.lblname.Text = name;
                            ii.lblusertype.Text = usertype;
                            ii.Show();
                            this.Hide();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Account is invalid.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtpass.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnregistration_Click(object sender, EventArgs e)
        {
            txtuser.Text = null;
            txtpass.Text = null;
        }

        private void btnshowpass_Click(object sender, EventArgs e)
        {
            btnshowpass.Hide();
            btnhidepass.Show();
            txtpass.UseSystemPasswordChar = false;
        }

        private void btnhidepass_Click(object sender, EventArgs e)
        {
            btnshowpass.Show();
            btnhidepass.Hide();
            txtpass.UseSystemPasswordChar = true;
        }

        private void txtuser_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("System Error Found!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //if (String.IsNullOrWhiteSpace(txtuser.Text) || txtuser.Text == null)
            //{
            //    txtuser.BackColor = Color.White;
            //    btnshowpass.BackColor = Color.White;
            //    btnhidepass.BackColor = Color.White;
            //}
            //else
            //{
            //    txtuser.BackColor = Color.PaleGreen;
            //    btnshowpass.BackColor = Color.PaleGreen;
            //    btnhidepass.BackColor = Color.PaleGreen;
            //}
        }

        private void txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("System Error Found!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //if (String.IsNullOrWhiteSpace(txtpass.Text) || txtuser.Text == null)
            //{
            //    txtpass.BackColor = Color.White;
            //    btnshowpass.BackColor = Color.White;
            //    btnhidepass.BackColor = Color.White;
            //}
            //else
            //{
            //    txtpass.BackColor = Color.PaleGreen;
            //    btnshowpass.BackColor = Color.PaleGreen;
            //    btnhidepass.BackColor = Color.PaleGreen;

            //}
        }
    }
}
