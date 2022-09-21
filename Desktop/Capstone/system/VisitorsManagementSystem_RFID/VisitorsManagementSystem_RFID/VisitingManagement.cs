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
    public partial class VisitingManagement : Form
    {
        ConnectionClass connector = new ConnectionClass();

        string formlogid = "";
        string formlogaccountid = "";
        string formlogname = "";
        string formlogusertype = "";

        public VisitingManagement()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

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

        public void DefaultTable()
        {
            if (formlogusertype == "School Guard")
            {
                String query = "select VisitingID, VisitorName, Status, TransactionRFID from tbl_visitortransaction where Status = '0' order by VisitingID desc limit 10";
                ViewTable(query, gridGuardVisitor);
                gridGuardVisitor.Columns[0].Visible = false;

                String query1 = "select TransactionNumber, PersonToVisit, Remarks from tbl_transaction where VisitingID = '0'";
                ViewTable(query1, gridGuardTransaction);
                gridGuardTransaction.Columns[0].Visible = false;

                String queryhistory = "select VisitingID, Status, TransactionRFID from tbl_visitortransaction where Status = '0' order by VisitingID desc limit 10";
                ViewTable(queryhistory, gridHistory);
                gridHistory.Columns[0].Visible = false;

                String queryhistorytransaction = "select TransactionNumber, Office, PersonToVisit, Purpose, Remarks from tbl_transaction where VisitingID = '0'";
                ViewTable(queryhistorytransaction, gridHistoryTransaction);
                gridHistoryTransaction.Columns[0].Visible = false;
            }
        }

        public void ClearField(int options, string user)
        {
            if (user == "School Guard")
            {
                guardpanel.Show();
                guardtablepanel.Show();

                if (options == 1)
                {
                    txttransactionrfid.Focus();
                    txtsearchvisitor.Enabled = false;
                    gridGuardVisitor.Enabled = false;
                    gridGuardTransaction.Enabled = false;
                    DefaultTable();
                    txtvisitingid.Text = "";
                    txtgtransactiondate.Text = "";
                    txtgvisitorid.Text = "";
                    txtgvisitorname.Text = "";
                    txtgsignedin.Text = "";
                    txtgsignedout.Text = "";
                    txtgstatus.Text = "";
                }
                else if (options == 2)
                {
                    txtsearchvisitor.Enabled = true;
                    gridGuardVisitor.Enabled = true;
                    gridGuardTransaction.Enabled = true;
                    txttransactionrfid.Text = "";
                    txtvisitingid.Text = "";
                    txtgtransactiondate.Text = "";
                    txtgvisitorid.Text = "";
                    txtgvisitorname.Text = "";
                    txtgsignedin.Text = "";
                    txtgsignedout.Text = "";
                    txtgstatus.Text = "";
                }
                else if (options == 3)
                {
                    txttransactionrfid.Text = "";
                    txtvisitingid.Text = "";
                    txtgtransactiondate.Text = "";
                    txtgvisitorid.Text = "";
                    txtgvisitorname.Text = "";
                    txtgsignedin.Text = "";
                    txtgsignedout.Text = "";
                    txtgstatus.Text = "";
                }
            }
            else
            {
                guardpanel.Hide();
                guardtablepanel.Hide();

                if (options == 1)
                {
                    txttransactionrfid.Focus();
                    txtsearchvisitor.Enabled = false;
                    DefaultTable();
                    txttransactionrfid.Text = "";
                }
                else if (options == 2)
                {
                    txtsearchvisitor.Enabled = true;
                    txttransactionrfid.Text = "";
                }
                else if (options == 3)
                {
                    txttransactionrfid.Text = "";
                    txtvisitingid.Text = "";
                }
            }


        }

        private void VisitingManagement_Load(object sender, EventArgs e)
        {
            try
            {
                Interface i = new Interface();
                formlogid = Interface.formlogid;
                formlogaccountid = Interface.formlogaccountid;
                formlogname = Interface.formlogname;
                formlogusertype = Interface.formlogusertype;

                if (formlogusertype == "School Guard")
                {
                    guardpanel.Show();
                    guardtablepanel.Show();
                }
                else
                {
                    TabPage tab2 = visitortab.TabPages[0];
                    visitortab.TabPages.Remove(tab2);
                }

                DefaultTable();

                String querycompleteprofiling = "select * from tbl_visitors where Status = 'Active'";
                AutoComplete(querycompleteprofiling, txtsearchvisitor, "Name");

                String querycompletehistory = "select * from tbl_visitors where Status = 'Active'";
                AutoComplete(querycompletehistory, txtsearchhistory, "Name");
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnmanagetransaction_Click(object sender, EventArgs e)
        {

        }

        private void btntransactionhistory_Click(object sender, EventArgs e)
        {

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txttransactionrfid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (formlogusertype == "School Guard")
                {
                    if (string.IsNullOrEmpty(txttransactionrfid.Text))
                    {
                        ClearField(1, formlogusertype);
                    }
                    else
                    {
                        txtsearchvisitor.Enabled = false;
                        DefaultTable();
                        String query = "select * from tbl_visitortransaction a, tbl_transaction b where b.VisitingID = a.VisitingID and a.Status not in ('Completed', 'Cancelled') and b.TransactionRFID = '" + txttransactionrfid.Text + "'";
                        connector.connect.Open();
                        MySqlCommand cmd = connector.connect.CreateCommand();
                        cmd.CommandText = query;
                        MySqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            DateTime datetransact = Convert.ToDateTime(read["TransactionDate"]);
                            txtgtransactiondate.Text = datetransact.ToString("MMMM dd, yyyy hh:mm tt");
                            DateTime datesignin = Convert.ToDateTime(read["SignedIn"]);
                            txtgsignedin.Text = datesignin.ToString("MMMM dd, yyyy hh:mm tt");
                            txtgsignedout.Text = "N/A";

                            txtvisitingid.Text = read["VisitingID"].ToString();
                            txtgvisitorid.Text = read["VisitorID"].ToString();
                            txtgvisitorname.Text = read["VisitorName"].ToString();
                            txtgstatus.Text = read["Status"].ToString();
                        }
                        connector.connect.Close();

                        String query1 = "select TransactionNumber, PersonToVisit, Remarks from tbl_transaction where VisitingID = '" + txtvisitingid.Text + "'";
                        ViewTable(query1, gridGuardTransaction);
                        gridGuardTransaction.Columns[0].Visible = false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txttransactionrfid.Text))
                    {
                        ClearField(1, formlogusertype);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndone_Click(object sender, EventArgs e)
        {
            try
            {
                if (formlogusertype == "School Guard")
                {
                    guardpanel.Show();

                    if (string.IsNullOrEmpty(txtvisitingid.Text))
                    {
                        MessageBox.Show("Warning! No data to be Settled.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {

                        bool notfound = false;
                        foreach (DataGridViewRow row in gridGuardTransaction.Rows)
                        {
                            if ((string)row.Cells[2].Value == "Pending")
                            {
                                notfound = true;
                                MessageBox.Show("Warning! All Transactions must be Settled. Cancelled Transactions is Acceptable", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                gridGuardTransaction.Enabled = true;
                                break;
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show("This Transaction will be marked as Completed. Click Yes to Proceed.", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    String query = "update tbl_visitortransaction set SignedOut = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', Status = 'Completed' where VisitingID = '" + txtvisitingid.Text + "'";
                                    InsertUpdateDelete(query);
                                    MessageBox.Show("Transaction Successfully Markes as Completed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ClearField(1, formlogusertype);
                                    txttransactionrfid.Text = null;
                                }
                            }
                        }
                    }
                }
                else
                {
                    guardpanel.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (formlogusertype == "School Guard")
                {
                    guardpanel.Show();

                    if (string.IsNullOrEmpty(txtvisitingid.Text))
                    {
                        MessageBox.Show("Warning! No data to be Settled.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("This Transaction will be marked as Cancelled. Click Yes to Proceed.", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            String query = "update tbl_visitortransaction set SignedOut = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', Status = 'Cancelled' where VisitingID = '" + txtvisitingid.Text + "'";
                            InsertUpdateDelete(query);
                            String query1 = "update tbl_transaction set Remarks = 'Cancelled' where VisitingID = '" + txtvisitingid.Text + "'";
                            InsertUpdateDelete(query1);
                            MessageBox.Show("Transaction Successfully Markes as Completed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearField(1, formlogusertype);
                        }
                    }
                }
                else
                {
                    guardpanel.Hide();

                    if (string.IsNullOrEmpty(txtvisitingid.Text))
                    {
                        MessageBox.Show("Warning! No data to be Settled.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            ClearField(1, formlogusertype);
        }

        private void txtsearchvisitor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (formlogusertype == "School Guard")
                {
                    if (string.IsNullOrEmpty(txttransactionrfid.Text))
                    {
                        ClearField(3, formlogusertype);
                    }
                    else
                    {
                        String query = "select VisitingID, VisitorName, Status, TransactionRFID from tbl_visitortransaction where Status != 'Completed' and VisitorName = '" + txtsearchvisitor.Text + "' order by VisitingID desc limit 10";
                        ViewTable(query, gridGuardVisitor);
                        gridGuardVisitor.Columns[0].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnload_Click(object sender, EventArgs e)
        {
            if (formlogusertype == "School Guard")
            {
                ClearField(1, formlogusertype);
                ClearField(2, formlogusertype);
                String query = "select VisitingID, VisitorName, Status, TransactionRFID from tbl_visitortransaction where Status != 'Completed' order by VisitingID desc limit 10";
                ViewTable(query, gridGuardVisitor);
                gridGuardVisitor.Columns[0].Visible = false;
            }
            else
            {
                ClearField(1, formlogusertype);
                ClearField(2, formlogusertype);
            }

        }

        private void VisitingManagement_Activated(object sender, EventArgs e)
        {
            txttransactionrfid.Focus();
        }

        private void gridGuardVisitor_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                String query = "select * from tbl_visitortransaction a, tbl_transaction b where b.VisitingID = a.VisitingID and b.VisitingID = '" + gridGuardVisitor.SelectedRows[0].Cells["VisitingID"].Value.ToString() + "'";
                connector.connect.Open();
                MySqlCommand cmd = connector.connect.CreateCommand();
                cmd.CommandText = query;
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    DateTime datetransact = Convert.ToDateTime(read["TransactionDate"]);
                    txtgtransactiondate.Text = datetransact.ToString("MMMM dd, yyyy hh:mm tt");
                    DateTime datesignin = Convert.ToDateTime(read["SignedIn"]);
                    txtgsignedin.Text = datesignin.ToString("MMMM dd, yyyy hh:mm tt");
                    txtgsignedout.Text = "N/A";

                    txtvisitingid.Text = read["VisitingID"].ToString();
                    txtgvisitorid.Text = read["VisitorID"].ToString();
                    txtgvisitorname.Text = read["VisitorName"].ToString();
                    txtgstatus.Text = read["Status"].ToString();
                }
                connector.connect.Close();

                String query1 = "select TransactionNumber, PersonToVisit, Remarks from tbl_transaction where VisitingID = '" + txtvisitingid.Text + "'";
                ViewTable(query1, gridGuardTransaction);
                gridGuardTransaction.Columns[0].Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show("System Error!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearchhistory_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtsearchhistory.Text))
                {
                    DefaultTable();
                }
                else
                {
                    String queryhistory = "select VisitingID, Status, TransactionRFID from tbl_visitortransaction where Status = '"+cbstatus.Text+"' and VisitorName = '" + txtsearchhistory.Text + "' order by VisitingID desc limit 10";
                    ViewTable(queryhistory, gridHistory);
                    gridHistory.Columns[0].Visible = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("System Error!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridHistory_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                String queryhistorytransaction = "select TransactionNumber, Office, PersonToVisit, Purpose, Companion, Remarks from tbl_transaction where VisitingID = '" + gridHistory.SelectedRows[0].Cells[0].Value.ToString() + "'";
                ViewTable(queryhistorytransaction, gridHistoryTransaction);
                gridHistoryTransaction.Columns[0].Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show("System Error!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtsearchhistory.Text = "";
            String queryhistory = "select VisitingID, Status, TransactionRFID from tbl_visitortransaction where Status = '" + cbstatus.Text + "' order by VisitingID desc limit 10";
            ViewTable(queryhistory, gridHistory);
            gridHistory.Columns[0].Visible = false;
        }
    }
}
