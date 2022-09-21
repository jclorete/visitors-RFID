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
    public partial class UserManagement : Form
    {
        ConnectionClass connector = new ConnectionClass();

        public string accountid, getoffice, getuser, usertypeid, usersid;
        public UserManagement()
        {
            InitializeComponent();
            timer1.Start();
            maintenancepanel.Hide();
        }

        public void InsertUpdateDelete(String query)
        {
            connector.connect.Open();
            MySqlCommand cmd = connector.connect.CreateCommand();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            connector.connect.Close();
        }

        public void ViewTable(String query, DataGridView table)
        {
            connector.connect.Open();
            MySqlCommand cmd = connector.connect.CreateCommand();
            cmd.CommandText = query;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            table.DataSource = dt;
            connector.connect.Close();
        }

        public void AddUserType()
        {
            cbuser.Items.Clear();
            String query = "select * from tbl_usertype ";
            connector.connect.Open();
            MySqlCommand cmd = connector.connect.CreateCommand();
            cmd.CommandText = query;
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                cbuser.Items.Add(read["UserType"].ToString());
            }
            connector.connect.Close();
        }

        public void ClearField()
        {
            cbuser.Text = "--Select--";
            txtoffice.Text = "";
            txtlname.Text = "";
            txtfname.Text = "";
            txtmname.Text = "";
            txtnumber.Text = "";

            txtaccountid.Enabled = true;
            txtaccountid.Text = "";
            txtpass.Text = "";
            txtcpass.Text = "";
            txtsearchaccount.Text = "";
            ShowHidePass(true, txtpass, btnshowpass, btnhidepass);
            ShowHidePass(true, txtcpass, btnshowcpass, btnhidecpass);
        }

        public void ClearAll()
        {
            timer1.Start();
            ClearField();
            savepanel.Show();
            updatepanel.Hide();
            txtaccountid.Enabled = true;
            AddUserType();
        }

        public void ShowHidePass (Boolean status, TextBox txt, Button btn1, Button btn2)
        {
            txt.UseSystemPasswordChar = status;
            btn1.Show();
            btn2.Hide();
        }

        public void ClearRequest()
        {
            txtrequestaccountid.Text = "";
            txtrequestname.Text = "";
            txtrequestusertype.Text = "";
            txtrequestremarks.Text = "";
            txtsearchrequest.Text = "";

        }

        public void AutoComplete(String querycomplete, TextBox x, String attach)
        {
            connector.connect.Open();
            MySqlCommand cmdcomplete = connector.connect.CreateCommand();
            cmdcomplete.CommandText = querycomplete;
            MySqlDataReader readcomplete = cmdcomplete.ExecuteReader();
            AutoCompleteStringCollection collection1 = new AutoCompleteStringCollection();

            while (readcomplete.Read())
            {
                collection1.Add(readcomplete.GetString(attach));
                x.AutoCompleteCustomSource = collection1;
            }
            connector.connect.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                txtaccountdate.Text = DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt");
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public void ViewAllTable()
        {
            String queryview = "select AccountID, Name, UserType from tbl_useraccounts";
            ViewTable(queryview, gridAccounts);

            String querycompleteaccount = "select * from tbl_useraccounts where Status = 'Activated' and Remarks = 'Approved'";
            AutoComplete(querycompleteaccount, txtsearchaccount, "Name");

            String queryviewlog = "select b.LogID, a.AccountID, a.Name, a.UserType, b.LoggedIn, b.LogStatus from tbl_useraccounts a, tbl_userlogs b where b.AccountID = a.AccountID and b.LogStatus = 'Active Now' order by b.LogID desc limit 10 ";
            ViewTable(queryviewlog, gridLogs);
            gridLogs.Columns[0].Visible = false;

            String querycompletelog = "select * from tbl_useraccounts a, tbl_userlogs b where b.AccountID = a.AccountID and a.Status = 'Activated' and a.Remarks = 'Approved'";
            AutoComplete(querycompletelog, txtsearchlog, "Name");

            String queryviewrequest = "select AccountID, Name, UserType from tbl_useraccounts where Status = 'Activated' and Remarks = 'Pending' order by AccountDate desc limit 10 ";
            ViewTable(queryviewrequest, gridRequest);

            String querycompleterequest = "select * from tbl_useraccounts where Status = 'Activated' and Remarks = 'Pending'";
            AutoComplete(querycompleterequest, txtsearchrequest, "Name");
        }

        public void ViewMaintenanceTable()
        {
            String queryusertype = "select * from tbl_usertype where UserID > 2";
            ViewTable(queryusertype, gridUsertype);
            gridUsertype.Columns[0].Visible = false;


            String queryuser = "select * from tbl_users";
            ViewTable(queryuser, gridUsers);
            gridUsers.Columns[0].Visible = false;
        }

        private void UserManagement_Load(object sender, EventArgs e)
        {
            
        }

        private void UserManagement_Activated(object sender, EventArgs e)
        {
            try
            {
                AddUserType();

                String queryview = "select AccountID, Name, UserType from tbl_useraccounts where Status = 'Activated' and Remarks = 'Approved' order by AccountDate desc limit 10 ";
                ViewTable(queryview, gridAccounts);

                String querycompleteaccount = "select * from tbl_useraccounts where Status = 'Activated' and Remarks = 'Approved'";
                AutoComplete(querycompleteaccount, txtsearchaccount, "Name");

                String queryviewlog = "select b.LogID, a.AccountID, a.Name, a.UserType, b.LoggedIn, b.LogStatus from tbl_useraccounts a, tbl_userlogs b where b.AccountID = a.AccountID and b.LogStatus = 'Active Now' order by b.LogID desc limit 10 ";
                ViewTable(queryviewlog, gridLogs);
                gridLogs.Columns[0].Visible = false;

                String querycompletelog = "select * from tbl_useraccounts a, tbl_userlogs b where b.AccountID = a.AccountID and a.Status = 'Activated' and a.Remarks = 'Approved'";
                AutoComplete(querycompletelog, txtsearchlog, "Name");

                String queryviewrequest = "select AccountID, Name, UserType from tbl_useraccounts where Status = 'Activated' and Remarks = 'Pending' order by AccountDate desc limit 10 ";
                ViewTable(queryviewrequest, gridRequest);

                String querycompleterequest= "select * from tbl_useraccounts where Status = 'Activated' and Remarks = 'Pending'";
                AutoComplete(querycompleterequest, txtsearchrequest, "Name");

                cbaddoffice.Items.Clear();
                String query = "select * from tbl_usertype where UserID > 2";
                connector.connect.Open();
                MySqlCommand cmd = connector.connect.CreateCommand();
                cmd.CommandText = query;
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    cbaddoffice.Items.Add(read["Office"].ToString());
                }
                connector.connect.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearchaccount_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                if (String.IsNullOrWhiteSpace(txtsearchaccount.Text))
                {
                    String queryview = "select AccountID, Name, UserType from tbl_useraccounts where Status = 'Activated' and Remarks = 'Approved' order by AccountDate desc limit 10 ";
                    ViewTable(queryview, gridAccounts);
                }
                else
                {
                    String queryview = "select AccountID, Name, UserType from tbl_useraccounts where Name = '" + txtsearchaccount.Text + "' and Status = 'Activated' and Remarks = 'Approved' order by AccountDate desc limit 10 ";
                    ViewTable(queryview, gridAccounts);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnmanageaccount_Click(object sender, EventArgs e)
        {
            btnindicator.SetBounds(220, 66, 38, 26);
            btnindicator.BringToFront();
            gbmanageaccount.Show();
            gbviewlogs.Hide();
            gbmanagerequest.Hide();
        }

        private void btnviewlogs_Click(object sender, EventArgs e)
        {
            btnindicator.SetBounds(513, 66, 38, 26);
            btnindicator.BringToFront();
            gbmanageaccount.Hide();
            gbviewlogs.Show();
            gbmanagerequest.Hide();
        }

        private void btnshowpass_Click(object sender, EventArgs e)
        {
            ShowHidePass(false, txtpass, btnhidepass, btnshowpass);
        }

        private void btnhidepass_Click(object sender, EventArgs e)
        {
            ShowHidePass(true, txtpass, btnshowpass, btnhidepass);
        }

        private void btnshowcpass_Click(object sender, EventArgs e)
        {
            ShowHidePass(false, txtcpass, btnhidecpass, btnshowcpass);
        }

        private void btnhidecpass_Click(object sender, EventArgs e)
        {
            ShowHidePass(true, txtcpass, btnshowcpass, btnhidecpass);
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsaveaccount_Click(object sender, EventArgs e)
        {
            try
            {
                String queryread = "select * from tbl_useraccounts where AccountID = '" + txtaccountid.Text + "'";
                connector.connect.Open();
                MySqlCommand cmdread = connector.connect.CreateCommand();
                cmdread.CommandText = queryread;
                MySqlDataReader readread = cmdread.ExecuteReader();
                while (readread.Read())
                {
                    accountid = readread["AccountID"].ToString();
                }
                connector.connect.Close();


                if (cbuser.Text == "--Select--" && txtoffice.Text == "" && txtlname.Text == "" && txtfname.Text == "" && txtmname.Text == "" && txtnumber.Text == "" && txtaccountid.Text == "" && txtpass.Text == "" && txtcpass.Text == "")
                {
                    MessageBox.Show("Please fill-up all fields.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbuser.Text == "--Select--")
                {
                    MessageBox.Show("Please Select an User Type.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbuser.Focus();
                }
                else if (txtoffice.Text == "--Select--")
                {
                    MessageBox.Show("Please Select an Office.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtoffice.Focus();
                }
                else if (txtlname.Text == "")
                {
                    MessageBox.Show("Please Enter Last Name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtlname.Focus();
                }
                else if (txtfname.Text == "")
                {
                    MessageBox.Show("Please Enter First Name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtfname.Focus();
                }
                else if (txtmname.Text == "")
                {
                    MessageBox.Show("Please Enter Middle Name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmname.Focus();
                }
                else if (txtnumber.Text == "")
                {
                    MessageBox.Show("Please Enter Phone Number.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtnumber.Focus();
                }
                else if (txtaccountid.Text == "")
                {
                    MessageBox.Show("Please Enter Account ID. (Preferred: Employee ID.)", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtaccountid.Focus();
                }
                else if (txtpass.Text == "")
                {
                    MessageBox.Show("Please Enter Password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtpass.Focus();
                }
                else if (txtcpass.Text == "")
                {
                    MessageBox.Show("Please Confirm Password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtpass.Focus();
                }
                else
                {
                    if (txtpass.Text != txtcpass.Text)
                    {
                        MessageBox.Show("Password and Confirm Password doesn't match.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtpass.Focus();
                    }
                    else if (txtnumber.TextLength != 11)
                    {
                        MessageBox.Show("Please Enter 11-Digit Phone Number.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtnumber.Text = "";
                        txtnumber.Focus();
                    }
                    else if (accountid == txtaccountid.Text)
                    {
                        MessageBox.Show("Account ID already exist. Please Choose another.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtaccountid.Focus();
                    }
                    else
                    {
                        String query = "insert into tbl_useraccounts (AccountDate, UserType, Office, Lastname, Firstname, Middlename, PhoneNumber, AccountID, Passwords, Status, Remarks, Name) values ('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + cbuser.Text + "', '"+ txtoffice.Text+"', '" + txtlname.Text + "', '" + txtfname.Text + "', '" + txtmname.Text + "', '" + txtnumber.Text + "', '" + txtaccountid.Text + "', '" + txtpass.Text + "', 'Activated', 'Approved', '" + txtlname.Text + ", " + txtfname.Text + " " + txtmname.Text + "')";
                        InsertUpdateDelete(query);
                        MessageBox.Show("Account Successfully Saved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearAll();

                        ViewAllTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnclearaccount_Click(object sender, EventArgs e)
        {
            ClearAll();

            ViewAllTable();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbuser.Text == "")
                {
                    MessageBox.Show("Please Select an User Type.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbuser.Focus();
                }
                else if(txtoffice.Text == "")
                {
                    MessageBox.Show("Please Select an Office.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtoffice.Focus();
                }
                else if (txtlname.Text == "")
                {
                    MessageBox.Show("Please Enter Last Name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtlname.Focus();
                }
                else if (txtfname.Text == "")
                {
                    MessageBox.Show("Please Enter First Name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtfname.Focus();
                }
                else if (txtmname.Text == "")
                {
                    MessageBox.Show("Please Enter Middle Name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmname.Focus();
                }
                else if (txtnumber.Text == "")
                {
                    MessageBox.Show("Please Enter Phone Number.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtnumber.Focus();
                }
                else if (txtpass.Text == "")
                {
                    MessageBox.Show("Please Enter Password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtpass.Focus();
                }
                else
                {
                    if (txtpass.Text != txtcpass.Text)
                    {
                        MessageBox.Show("Password and Confirm Password doesn't match.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtpass.Focus();
                    }
                    else if (txtnumber.TextLength != 11)
                    {
                        MessageBox.Show("Please Enter 11-Digit Phone Number.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtnumber.Text = "";
                        txtnumber.Focus();
                    }
                    else
                    {
                        String query = "update tbl_useraccounts set UserType = '"+cbuser.Text+"', Office = '"+txtoffice.Text+"', Lastname = '"+txtlname.Text+"', Firstname = '"+txtfname.Text+"', Middlename = '"+txtmname.Text+"', PhoneNumber = '"+txtnumber.Text+"', Passwords = '"+txtpass.Text+"', Name = '"+txtlname.Text + ", " + txtfname.Text + " " + txtmname.Text+"' where AccountID = '"+txtaccountid.Text+"'";
                        InsertUpdateDelete(query);
                        MessageBox.Show("Account Successfully Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnadduser_Click(object sender, EventArgs e)
        {
            
        }

        private void btnaddoffice_Click(object sender, EventArgs e)
        {
            
        }

        private void gridAccounts_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                timer1.Stop();
                savepanel.Hide();
                updatepanel.Show();
                txtaccountid.Enabled = false;
                String query = "select * from tbl_useraccounts where AccountID = '"+ gridAccounts.SelectedRows[0].Cells["AccountID"].Value.ToString() + "' ";
                connector.connect.Open();
                MySqlCommand cmd = connector.connect.CreateCommand();
                cmd.CommandText = query;
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    DateTime dateaccount = Convert.ToDateTime(read["AccountDate"]);
                    txtaccountdate.Text = dateaccount.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    cbuser.Items.Clear();
                    cbuser.Text = read["UserType"].ToString();
                    txtoffice.Text = read["Office"].ToString();
                    txtlname.Text = read["Lastname"].ToString();
                    txtfname.Text = read["Firstname"].ToString();
                    txtmname.Text = read["Middlename"].ToString();
                    txtnumber.Text = read["PhoneNumber"].ToString();

                    txtaccountid.Text = read["AccountID"].ToString();
                    txtpass.Text = read["Passwords"].ToString();
                    txtcpass.Text = read["Passwords"].ToString();
                }
                connector.connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btncloseuser_Click(object sender, EventArgs e)
        {
            
        }

        private void btncloseoffice_Click(object sender, EventArgs e)
        {
            
        }

        private void gridLogs_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                String query = "select * from tbl_useraccounts a, tbl_userlogs b where b.AccountID = a.AccountID and b.LogID = '" + gridLogs.SelectedRows[0].Cells["LogID"].Value.ToString() + "' ";
                connector.connect.Open();
                MySqlCommand cmd = connector.connect.CreateCommand();
                cmd.CommandText = query;
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    txtlogaccountid.Text = read["AccountID"].ToString();
                    txtlogid.Text = read["LogID"].ToString();
                    txtlogname.Text = read["Name"].ToString();
                    txtlogstatus.Text = read["LogStatus"].ToString();
                    DateTime logindate = Convert.ToDateTime(read["LoggedIn"]);
                    txtlogindate.Text = logindate.ToString("MMMM dd, yyyy hh:mm:ss tt");

                    if(read["Status"].ToString() == "Inactive")
                    {
                        DateTime logoutdate = Convert.ToDateTime(read["LoggedOut"]);
                        txtlogoutdate.Text = logoutdate.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    }
                    else
                    {
                        txtlogoutdate.Text = "Not Logged-Out Yet.";
                    }
                    
                }
                connector.connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearchlog_TextChanged(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(txtsearchlog.Text))
            {
                String queryviewlog = "select b.LogID, a.AccountID, a.Name, a.UserType, b.LoggedIn, b.LogStatus from tbl_useraccounts a, tbl_userlogs b where b.AccountID = a.AccountID and b.LogStatus = 'Active Now' order by b.LogID desc limit 10 ";
                ViewTable(queryviewlog, gridLogs);
                gridLogs.Columns[0].Visible = false;
            }
            else
            {
                String queryviewlog = "select b.LogID, a.AccountID, a.Name, a.UserType, b.LoggedIn, b.LogStatus from tbl_useraccounts a, tbl_userlogs b where b.AccountID = a.AccountID and a.Name = '" + txtsearchlog.Text+"' order by b.LogID desc limit 10 ";
                ViewTable(queryviewlog, gridLogs);
                gridLogs.Columns[0].Visible = false;
            }
        }

        private void btnapproved_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txtrequestaccountid.Text))
                {
                    MessageBox.Show("No account to be approved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    String query = "update tbl_useraccounts set Remarks = 'Approved' where AccountID = '" + txtrequestaccountid.Text + "' ";
                    InsertUpdateDelete(query);
                    MessageBox.Show("Successfully Approved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearRequest();
                    ViewAllTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndisapproved_Click(object sender, EventArgs e)
        {
            try
            {
                if(String.IsNullOrWhiteSpace(txtrequestaccountid.Text))
                {
                    MessageBox.Show("No account to be disapproved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to Disapproved?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if(result == DialogResult.Yes)
                    {
                        String query = "delete from tbl_useraccounts where AccountID = '" + txtrequestaccountid.Text + "' ";
                        InsertUpdateDelete(query);
                        MessageBox.Show("Successfully Disapproved and Deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearRequest();
                        ViewAllTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearchrequest_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(String.IsNullOrWhiteSpace(txtsearchrequest.Text))
                {
                    String queryviewrequest = "select AccountID, Name, UserType from tbl_useraccounts where Status = 'Activated' and Remarks = 'Pending' order by AccountDate desc limit 10 ";
                    ViewTable(queryviewrequest, gridRequest);
                }
                else
                {
                    String queryviewrequest = "select AccountID, Name, UserType from tbl_useraccounts where Name = '"+txtsearchrequest.Text+ "' and Status = 'Activated' and Remarks = 'Pending' order by AccountDate desc limit 10 ";
                    ViewTable(queryviewrequest, gridRequest);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridRequest_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                String query = "select * from tbl_useraccounts where AccountID = '" + gridRequest.SelectedRows[0].Cells["AccountID"].Value.ToString() + "' ";
                connector.connect.Open();
                MySqlCommand cmd = connector.connect.CreateCommand();
                cmd.CommandText = query;
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    txtrequestaccountid.Text = read["AccountID"].ToString();
                    txtrequestname.Text = read["Name"].ToString();
                    txtrequestusertype.Text = read["UserType"].ToString();
                    txtrequestremarks.Text = read["Remarks"].ToString();
                }
                connector.connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearchaccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if(!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error Found!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearchrequest_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error Found!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearchlog_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error Found!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                maintenancepanel.Show();
                ViewMaintenanceTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error Found!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbuser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtoffice.Text = "";
                String query = "select * from tbl_usertype where UserType = '"+cbuser.Text+"' ";
                connector.connect.Open();
                MySqlCommand cmd = connector.connect.CreateCommand();
                cmd.CommandText = query;
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    txtoffice.Text = read["Office"].ToString();
                }
                connector.connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error Found!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnaddusers_Click(object sender, EventArgs e)
        {
            try
            {
                if(cbaddoffice.Text == "--Select--" && txtusername.Text == "")
                {
                    MessageBox.Show("Please fill-up all fields.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbaddoffice.Text == "--Select--")
                {
                    MessageBox.Show("Please Select an Office.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbaddoffice.Focus();
                }
                else if (txtusername.Text == "")
                {
                    MessageBox.Show("Please Enter User Name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtusername.Focus();
                }
                else
                {
                    String query = "insert into tbl_users (Name, Office) values ('"+txtusername.Text+"', '"+cbaddoffice.Text+"')";
                    InsertUpdateDelete(query);
                    MessageBox.Show("Successfully Added!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbaddoffice.Text = "--Select--";
                    txtusername.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error Found!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridUsertype_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                btnupdatemanintenance.Enabled = true;
                btnsavemaintenance.Enabled = false;
                usertypeid = gridUsertype.SelectedRows[0].Cells["UserID"].Value.ToString();
                txtaddoffice.Text = gridUsertype.SelectedRows[0].Cells["Office"].Value.ToString();
                txtadduser.Text = gridUsertype.SelectedRows[0].Cells["UserType"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error Found!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnupdatemanintenance_Click(object sender, EventArgs e)
        {
            try
            {
                String queryupdate = "update tbl_usertype set Office = '" + txtaddoffice.Text + "', UserType = '" + txtadduser.Text + "' where UserID = '" + usertypeid + "'";
                InsertUpdateDelete(queryupdate);
                MessageBox.Show("Successfully Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtaddoffice.Text = "";
                txtadduser.Text = "";
                btnupdatemanintenance.Enabled = false;
                btnsavemaintenance.Enabled = true;
                ViewMaintenanceTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error Found!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnclearmaintenance_Click(object sender, EventArgs e)
        {
            txtaddoffice.Text = "";
            txtadduser.Text = "";
            btnupdatemanintenance.Enabled = false;
            btnsavemaintenance.Enabled = true;
        }

        private void gridUsers_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                btnupdateusers.Enabled = true;
                btnaddusers.Enabled = false;
                usersid = gridUsers.SelectedRows[0].Cells["UserNo"].Value.ToString();
                cbaddoffice.Text = gridUsers.SelectedRows[0].Cells["Office"].Value.ToString();
                txtusername.Text = gridUsers.SelectedRows[0].Cells["Name"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error Found!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnupdateusers_Click(object sender, EventArgs e)
        {
            try
            {
                String queryupdate = "update tbl_users set Office = '" + cbaddoffice.Text + "', Name = '" + txtusername.Text + "' where UserNo = '" + usersid + "'";
                InsertUpdateDelete(queryupdate);
                MessageBox.Show("Successfully Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbaddoffice.Text = "--Select--";
                txtusername.Text = "";
                btnupdateusers.Enabled = false;
                btnaddusers.Enabled = true;
                ViewMaintenanceTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error Found!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnclearusers_Click(object sender, EventArgs e)
        {
            cbaddoffice.Text = "--Select--";
            txtusername.Text = "";
            btnupdateusers.Enabled = false;
            btnaddusers.Enabled = true;
        }

        private void btnclosemaintenance_Click(object sender, EventArgs e)
        {
            maintenancepanel.Hide();
        }

        private void btnsavemaintenance_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtaddoffice.Text == "" && txtadduser.Text == "")
                {
                    MessageBox.Show("Please fill-up all fields.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtaddoffice.Text == "")
                {
                    MessageBox.Show("Please enter an Office.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtaddoffice.Focus();
                }
                else if (txtadduser.Text == "")
                {
                    MessageBox.Show("Please enter an User Type.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtadduser.Focus();
                }
                else
                {
                    String queryread = "select * from tbl_usertype where UserType = '"+txtadduser.Text+"' and Office = '"+txtaddoffice.Text+"'";
                    connector.connect.Open();
                    MySqlCommand cmdread = connector.connect.CreateCommand();
                    cmdread.CommandText = queryread;
                    MySqlDataReader readread = cmdread.ExecuteReader();
                    while (readread.Read())
                    {
                        getoffice = readread["Office"].ToString();
                        getuser = readread["UserType"].ToString();
                    }
                    connector.connect.Close();

                    if(getoffice == txtaddoffice.Text && getuser == txtadduser.Text)
                    {
                        MessageBox.Show("User Type and Office already exist! Please Try Another.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if(txtadduser.Text == "Secretary")
                        {
                            String query = "insert into tbl_usertype (Office, UserType, Remarks) values ('" + txtaddoffice.Text + "', '" + txtadduser.Text + "', 'Secretary')";
                            InsertUpdateDelete(query);
                            MessageBox.Show("Successfully Added!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AddUserType();
                            txtaddoffice.Text = "";
                            txtadduser.Text = "";
                            ViewMaintenanceTable();

                        }
                        else
                        {
                            String query = "insert into tbl_usertype (Office, UserType, Remarks) values ('" + txtaddoffice.Text + "', '" + txtadduser.Text + "', 'None')";
                            InsertUpdateDelete(query);
                            MessageBox.Show("Successfully Added!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AddUserType();
                            txtaddoffice.Text = "";
                            txtadduser.Text = "";
                            ViewMaintenanceTable();

                        }
                    }
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show("System Error Found!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
