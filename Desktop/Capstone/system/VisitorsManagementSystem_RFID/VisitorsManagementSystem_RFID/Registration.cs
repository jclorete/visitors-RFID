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
    public partial class Registration : Form
    {
        ConnectionClass connector = new ConnectionClass();

        public string accountid;

        public Registration()
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

        private void Registration_Load(object sender, EventArgs e)
        {
            try
            {
                String query = "select * from tbl_usertype where UserID > 1 ";
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
            catch (Exception)
            {
                MessageBox.Show("System Error!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnshowcpass_Click(object sender, EventArgs e)
        {
            btnshowcpass.Hide();
            btnhidecpass.Show();
            txtcpass.UseSystemPasswordChar = false;
        }

        private void btnhidecpass_Click(object sender, EventArgs e)
        {
            btnshowcpass.Show();
            btnhidecpass.Hide();
            txtcpass.UseSystemPasswordChar = true;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                String queryread = "select * from tbl_useraccounts where AccountID = '"+txtuser.Text+"'";
                connector.connect.Open();
                MySqlCommand cmdread = connector.connect.CreateCommand();
                cmdread.CommandText = queryread;
                MySqlDataReader readread = cmdread.ExecuteReader();
                while(readread.Read())
                {
                    accountid = readread["AccountID"].ToString();
                }
                connector.connect.Close();


                if (cbuser.Text == "--Select--" && txtlname.Text == "" && txtfname.Text == "" && txtmname.Text == "" && txtnumber.Text == "" && txtuser.Text == "" && txtpass.Text == "" && txtcpass.Text == "")
                {
                    MessageBox.Show("Please fill-up all fields.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbuser.Text == "--Select--")
                {
                    MessageBox.Show("Please Select an User Type.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbuser.Focus();
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
                else if (txtuser.Text == "")
                {
                    MessageBox.Show("Please Enter Account ID. (Preferred: Employee ID.)", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtuser.Focus();
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
                    if(txtpass.Text != txtcpass.Text)
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
                    else if (accountid == txtuser.Text)
                    {
                        MessageBox.Show("Account ID already exist. Please Choose another.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtuser.Focus();
                    }
                    else
                    {
                        String query = "insert into tbl_useraccounts (AccountDate, UserType, Office, Lastname, Firstname, Middlename, PhoneNumber, AccountID, Passwords, Status, Remarks, Name) values ('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '"+cbuser.Text+"', '"+txtoffice.Text+"', '" + txtlname.Text + "', '" + txtfname.Text + "', '" + txtmname.Text + "', '" + txtnumber.Text + "', '" + txtuser.Text + "', '" + txtpass.Text + "', 'Activated', 'Pending', '" + txtlname.Text+ ", " + txtfname.Text + " " + txtmname.Text +"')";
                        InsertUpdateDelete(query);
                        MessageBox.Show("Account Successfully Saved! Please Wait for the Approval of the Administrator (ITRC). Thank you!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            cbuser.Text = "--Select--";
            txtoffice.Text = null;
            txtlname.Text = null;
            txtfname.Text = null;
            txtmname.Text = null;
            txtnumber.Text = null;
            txtuser.Text = null;
            txtpass.Text = null;
            txtcpass.Text = null;

            btnshowpass.Show();
            btnhidepass.Hide();
            txtpass.UseSystemPasswordChar = true;

            btnshowcpass.Show();
            btnhidecpass.Hide();
            txtcpass.UseSystemPasswordChar = true;
        }

        private void txtnumber_TextChanged(object sender, EventArgs e)
        {
            if (txtnumber.TextLength > 11)
            {
                MessageBox.Show("Please Enter 11-Digit Phone Number.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnumber.Text = "";
                txtnumber.Focus();
            }
        }

        private void cbuser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtoffice.Text = "";
                String query = "select * from tbl_usertype where UserType = '" + cbuser.Text + "' ";
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
            catch (Exception)
            {
                MessageBox.Show("System Error Found!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
