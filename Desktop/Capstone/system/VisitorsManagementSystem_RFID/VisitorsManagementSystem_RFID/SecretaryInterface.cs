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
    public partial class SecretaryInterface : Form
    {
        ConnectionClass connector = new ConnectionClass();
        String transnumber;

        public SecretaryInterface()
        {
            InitializeComponent();
            timer1.Start();
            infopanel.Hide();
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

        public void ClearFiltering()
        {
            txttransactionid.Focus();
            txttransactionid.Text = "";
            txtvisitorid.Text = "";
            txtvisitorname.Text = "";
            txtpurpose.Text = "";
            txtstatus.Text = "";

            String query1 = "select TransactionNumber, PersonToVisit, Purpose, Remarks from tbl_transaction where VisitorID = '0' order by TransactionNumber desc";
            ViewTable(query1, gridList);
            gridList.Columns[0].Visible = false;
        }

        public void ClearTable()
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                lbldatetime.Text = DateTime.Now.ToString("MMMM dd, yyyy") + " | " + DateTime.Now.ToString("dddd");

                String query = "select count(a.VisitingID) as totalcount from tbl_visitortransaction a, tbl_transaction b where b.VisitingID = a.VisitingID and b.Remarks not in ('Completed', 'Cancelled') and a.Status = 'On-going' and b.Office = '" + Login.formlogoffice + "' ";
                connector.connect.Open();
                MySqlCommand cmd = connector.connect.CreateCommand();
                cmd.CommandText = query;
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    txtvisitorcount.Text = read["totalcount"].ToString();
                }
                connector.connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SecretaryInterface_Load(object sender, EventArgs e)
        {
            ClearFiltering();
        }

        private void SecretaryInterface_Activated(object sender, EventArgs e)
        {
            txttransactionid.Focus();
            txtinfoaccountid.Text = Login.formlogaccountid;
            txtinfoname.Text = Login.formlogname;
            txtinfouser.Text = Login.formlogusertype;
            txtinfooffice.Text = Login.formlogoffice;
        }

        private void txttransactionid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txttransactionid.Text))
                {
                    ClearFiltering();
                }
                else
                {
                    String query = "select * from tbl_visitortransaction a, tbl_transaction b where b.VisitingID = a.VisitingID and b.Remarks not in ('Completed', 'Cancelled') and a.Status = 'On-going' and b.TransactionRFID = '" + txttransactionid.Text + "' and b.Office = '"+Login.formlogoffice+ "' ";
                    connector.connect.Open();
                    MySqlCommand cmd = connector.connect.CreateCommand();
                    cmd.CommandText = query;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        txtvisitingid.Text = read["VisitingID"].ToString();
                        txtvisitorid.Text = read["VisitorID"].ToString();
                        txtvisitorname.Text = read["VisitorName"].ToString();
                        txtstatus.Text = read["Status"].ToString();
                        txttransactionnumber.Text = read["TransactionNumber"].ToString();
                        transnumber = read["TransactionNumber"].ToString();

                    }
                    connector.connect.Close();

                    //MessageBox.Show("!");

                    String query1 = "select TransactionNumber, PersonToVisit, Remarks, Purpose from tbl_transaction where VisitingID = '" + txtvisitingid.Text+ "' and Office = '" + Login.formlogoffice + "' order by TransactionNumber desc";
                    ViewTable(query1, gridList);
                    gridList.Columns[0].Visible = false;
                    gridList.Columns[3].Visible = false;


                    String queryupdate = "update tbl_transaction set TransactionStart = '"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"', Remarks = '" + "Visitor is at " + txtinfooffice.Text + "' where TransactionNumber = '" + transnumber + "' and TransactionRFID = '" + txttransactionid.Text + "'";
                    InsertUpdateDelete(queryupdate);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            ClearFiltering();
        }

        private void btnprofileicon_Click(object sender, EventArgs e)
        {
            infopanel.Show();
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to Log-out?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    String query = "update tbl_userlogs set LoggedOut = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', LogStatus = 'Inactive' where LogID = '" + lbllogid.Text + "'";
                    InsertUpdateDelete(query);
                    Login i = new Login();
                    i.Show();
                    this.Hide();
                }
                else if (result == DialogResult.No)
                {
                    txttransactionid.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (gridList.SelectedRows[0].Cells["Remarks"].Value.ToString() == "Completed")
                {
                    txtpurpose.Text = gridList.SelectedRows[0].Cells["Purpose"].Value.ToString();
                    MessageBox.Show("Transaction already completed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    String value = gridList.SelectedRows[0].Cells["TransactionNumber"].Value.ToString();
                    txtpurpose.Text = gridList.SelectedRows[0].Cells["Purpose"].Value.ToString();

                    DialogResult result = MessageBox.Show("Are you sure you want to proceed this Transaction?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        String queryupdate = "update tbl_transaction set TransactionEnd = '"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"', Remarks = 'Completed' where TransactionNumber = '" + value + "'";
                        InsertUpdateDelete(queryupdate);
                        MessageBox.Show("Transaction Successfully Proceeded!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        String query1 = "select TransactionNumber, PersonToVisit, Purpose, Remarks from tbl_transaction where VisitingID = '" + txtvisitingid.Text + "' and Office = '" + Login.formlogoffice + "' order by TransactionNumber desc";
                        ViewTable(query1, gridList);
                        gridList.Columns[0].Visible = false;

                        ClearFiltering();

                        //String query1 = "select TransactionNumber, PersonToVisit, Purpose, Remarks from tbl_transaction where VisitorID = '" + txtvisitorid.Text + "' order by TransactionNumber desc";
                        //ViewTable(query1, gridList);
                        //gridList.Columns[0].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txttransactionid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btncloseinfo_Click(object sender, EventArgs e)
        {
            infopanel.Hide();
        }

        private void txttransactionnumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception)
            {
                MessageBox.Show("System Error!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
