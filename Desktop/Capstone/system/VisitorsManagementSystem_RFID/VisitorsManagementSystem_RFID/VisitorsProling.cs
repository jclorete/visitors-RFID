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
    public partial class VisitorsProling : Form
    {
        ConnectionClass connector = new ConnectionClass();

        string incrementvisitorid, incrementvisitingid, incrementtransactionnumber, getstatus;

        public VisitorsProling()
        {
            InitializeComponent();
            timer1.Start();
            btnsaveicon.Enabled = false;
            btnsave.Enabled = false;
            txttransactionrfid.Focus();
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

        public void IncrementVisitorID()
        {
            String query = "select count(VisitorID) from tbl_visitors";
            connector.connect.Open();
            MySqlCommand cmd = connector.connect.CreateCommand();
            cmd.CommandText = query;
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            i++;
            incrementvisitorid = "VST-ID" + i.ToString();
            connector.connect.Close();
        }

        public void IncrementVisitingID()
        {
            String query = "select count(VisitingID) from tbl_visitortransaction";
            connector.connect.Open();
            MySqlCommand cmd = connector.connect.CreateCommand();
            cmd.CommandText = query;
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            i++;
            incrementvisitingid = "VT-ID" + i.ToString();
            connector.connect.Close();

        }

        public void IncrementTransactionNumber()
        {
            String query = "select count(TransactionNumber) from tbl_transaction";
            connector.connect.Open();
            MySqlCommand cmd = connector.connect.CreateCommand();
            cmd.CommandText = query;
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            connector.connect.Close();
            i++;
            incrementtransactionnumber = "TN-ID" + i.ToString();
        }

        public void ClearAll()
        {
            ClearProfile();
            savepanel.Show();
            updatepanel.Hide();
            timer1.Start();
            txtsearchvisitor.Text = "";
            txttransactionrfid.Focus();
            txtidpresented.Enabled = true;
            txtidnumber.Enabled = true;
        }

        public void ClearProfile ()
        {
            IncrementVisitorID();
            txtvisitorid.Text = incrementvisitorid;
            txtidpresented.Text = "";
            txtidnumber.Text = "";
            txtlname.Text = "";
            txtfname.Text = "";
            txtmname.Text = "";
            txtaddress.Text = "";
            txtnumber.Text = "";

            String query = "select VisitorID, Name, RecordDate from tbl_visitors where Status = 'Active' order by VisitorID limit 10 ";
            ViewTable(query, gridVisitor);
        }

        public void ClearEnabled(bool status)
        {
            if(status == true)
            {
                txtsearchvisitorprofile.Enabled = true;
                txtpurpose.Enabled = true;
                btnadd.Enabled = true;
                btnaddclear.Enabled = true;
            }
            else if (status == false)
            {
                txtsearchvisitorprofile.Enabled = false;
                txtpurpose.Enabled = false;
                btnadd.Enabled = false;
                btnaddclear.Enabled = false;
            }
        }

        public void ClearAddTransaction()
        {
            cboffice.Text = "--Select--";
            txttovisit.Text = "";
            txtpurpose.Text = "";
            txtcompanion.Text = "";
        }

        public void ClearAllTransaction(bool state)
        {
            if (state == true)
            {
                if (gridTransaction.Rows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to remove all in the List?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        gridTransaction.Rows.Clear();

                        txttransactionrfid.Enabled = true;

                        ClearAddTransaction();
                        ClearEnabled(false);

                        txttransactionrfid.Text = "";
                        txtsearchvisitorprofile.Text = "";
                        txtprofileid.Text = "";
                        txtprofilename.Text = "";
                    }
                }
                else
                {
                    txttransactionrfid.Enabled = true;

                    ClearAddTransaction();
                    ClearEnabled(false);

                    txttransactionrfid.Text = "";
                    txtsearchvisitorprofile.Text = "";
                    txtprofileid.Text = "";
                    txtprofilename.Text = "";
                }
            }
            else if (state == false)
            {
                gridTransaction.Rows.Clear();

                txttransactionrfid.Enabled = true;

                ClearAddTransaction();
                ClearEnabled(false);

                txttransactionrfid.Text = "";
                txtsearchvisitorprofile.Text = "";
                txtprofileid.Text = "";
                txtprofilename.Text = "";
            }
            
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
            String query = "select VisitorID, Name, PhoneNumber as ContactNumber, RecordDate from tbl_visitors where Status = 'Active' order by VisitorID limit 10 ";
            ViewTable(query, gridVisitor);

            
            String querycompleteprofiling = "select * from tbl_visitors where Status = 'Active'";
            AutoComplete(querycompleteprofiling, txtsearchvisitor, "Name");

            String querycompletetetransaction = "select * from tbl_visitors where Status = 'Active'";
            AutoComplete(querycompletetetransaction, txtsearchvisitorprofile, "Name");

            String querycompletetemonitor = "select * from tbl_visitors where Status = 'Active'";
            AutoComplete(querycompletetemonitor, txtsearchmonitor, "Name");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                txtdaterecord.Text = DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt");
                txttransactiondate.Text = DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt");

                if (gridTransaction.Rows.Count > 0)
                {
                    btnsaveicon.Enabled = true;
                    btnsave.Enabled = true;
                }
                else
                {
                    btnsaveicon.Enabled = false;
                    btnsave.Enabled = false;
                }

                String querymonitor = "select a.TransactionNumber, a.VisitingID, a.VisitorName, a.Office, a.Purpose, Companion, a.Remarks, DATE_FORMAT (b.SignedIn, '" + "%b %d, %Y %l:%i %p" + "') AS SignedIn from tbl_transaction a, tbl_visitortransaction b where b.VisitingID = a.VisitingID and a.Remarks != 'Completed' and Date(a.TransactionDate) = '" + DateTime.Now.ToString("yyyy-MM-dd")+ "' order by a.TransactionNumber Desc limit 20";
                ViewTable(querymonitor, gridMonitor);
                gridMonitor.Columns[0].Visible = false;
                gridMonitor.Columns[1].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void VisitorsProling_Load(object sender, EventArgs e)
        {
            
            if(Interface.formlogusertype != "School Guard" && Interface.formlogusertype != "Administrator")
            {
                TabPage tab2 = visitortab.TabPages[1];
                visitortab.TabPages.Remove(tab2);
            }
            try
            {
                cboffice.Items.Clear();
                String query = "select * from tbl_usertype where UserID > 2";
                connector.connect.Open();
                MySqlCommand cmd = connector.connect.CreateCommand();
                cmd.CommandText = query;
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    cboffice.Items.Add(read["Office"].ToString());
                }
                connector.connect.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("System Error!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VisitorsProling_Activated(object sender, EventArgs e)
        {
            try
            {
                txtsearchvisitorprofile.Text = "";
                IncrementVisitorID();
                txtvisitorid.Text = incrementvisitorid;

                DefaultTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsavevisitor_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtidpresented.Text == "" & txtidnumber.Text == "" & txtlname.Text == "" && txtfname.Text == "" && txtmname.Text == "" && txtaddress.Text == "" && txtnumber.Text == "")
                {
                    MessageBox.Show("Please fill-up all fields.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtidpresented.Text == "")
                {
                    MessageBox.Show("Please Enter Any Valid ID.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtidpresented.Focus();
                    return;
                }
                else if (txtidnumber.Text == "")
                {
                    MessageBox.Show("Please Enter Any Valid ID.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtidnumber.Focus();
                    return;
                }
                else if (txtlname.Text == "")
                {
                    MessageBox.Show("Please Enter Last Name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtlname.Focus();
                    return;
                }
                else if (txtfname.Text == "")
                {
                    MessageBox.Show("Please Enter First Name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtfname.Focus();
                    return;
                }
                else if (txtmname.Text == "")
                {
                    MessageBox.Show("Please Enter Middle Name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmname.Focus();
                    return;
                }
                else if (txtaddress.Text == "")
                {
                    MessageBox.Show("Please Enter Address.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtaddress.Focus();
                    return;
                }
                else if (txtnumber.Text == "")
                {
                    MessageBox.Show("Please Enter Phone Number", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtnumber.Focus();
                    return;
                }
                else
                {
                    IncrementVisitorID();
                    String query = "insert into tbl_visitors (VisitorID, IDPresented, IDNumber, RecordDate, Lastname, Firstname, Middlename, Address, PhoneNumber, Status, Name) values ('" + incrementvisitorid + "', '"+txtidpresented.Text+"', '"+txtidnumber.Text+"', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"', '"+txtlname.Text+"', '"+txtfname.Text+"', '"+txtmname.Text+"', '"+txtaddress.Text+"', '"+txtnumber.Text+"', 'Active', '"+txtlname.Text + ", " + txtfname.Text + " " + txtmname.Text+"')";
                    InsertUpdateDelete(query);
                    MessageBox.Show("Successfully Saved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearProfile();

                    DefaultTable();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("System Error!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnupdatevisitor_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtlname.Text == "" && txtfname.Text == "" && txtmname.Text == "" && txtaddress.Text == "" && txtnumber.Text == "")
                {
                    MessageBox.Show("Please fill-up all fields.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtlname.Text == "")
                {
                    MessageBox.Show("Please Enter Last Name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtlname.Focus();
                    return;
                }
                else if (txtfname.Text == "")
                {
                    MessageBox.Show("Please Enter First Name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtfname.Focus();
                    return;
                }
                else if (txtmname.Text == "")
                {
                    MessageBox.Show("Please Enter Middle Name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmname.Focus();
                    return;
                }
                else if (txtaddress.Text == "")
                {
                    MessageBox.Show("Please Enter Address.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtaddress.Focus();
                    return;
                }
                else if (txtnumber.Text == "")
                {
                    MessageBox.Show("Please Enter Phone Number", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtnumber.Focus();
                    return;
                }
                else
                {
                    String query = "update tbl_visitors set Lastname = '"+txtlname.Text+"', Firstname = '"+txtfname.Text+"', Middlename = '"+txtmname.Text+"', Address = '"+txtaddress.Text+"', PhoneNumber = '"+txtnumber.Text+"', Name = '"+txtlname.Text + ", " + txtfname.Text + " " + txtmname.Text+"' where VisitorID = '"+txtvisitorid.Text+"'";
                    InsertUpdateDelete(query);
                    MessageBox.Show("Successfully Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearProfile();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("System Error!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnclearvisitor_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void txtsearchvisitorprofile_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtsearchvisitorprofile.Text))
                {
                    ClearEnabled(false);
                    ClearAddTransaction();
                    txtsearchvisitorprofile.Enabled = true;

                    txtprofileid.Text = "";
                    txtprofilename.Text = "";
                    txtsearchvisitorprofile.Focus();
                }
                else
                {
                    ClearAddTransaction();
                    ClearEnabled(true);


                    String query = "select * from tbl_visitors where Name = '" + txtsearchvisitorprofile.Text + "'";
                    connector.connect.Open();
                    MySqlCommand cmd = connector.connect.CreateCommand();
                    cmd.CommandText = query;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        txtprofileid.Text = read["VisitorID"].ToString();
                        txtprofilename.Text = read["Name"].ToString();
                    }
                    connector.connect.Close();
                }
            }
            catch (Exception ex)
            {   
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                bool notfound = false;
                foreach (DataGridViewRow row in gridTransaction.Rows)
                {
                    if ((string)row.Cells[0].Value == txttovisit.Text)
                    {
                        notfound = true;
                        MessageBox.Show("Warning! Duplicate User Choose.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txttovisit.Text = "";
                        break;
                    }
                }
                if (!notfound)
                {
                    if (txttovisit.Text == "" && txtpurpose.Text == "")
                    {
                        MessageBox.Show("Warning! Please Fill-up all the required fields.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (cboffice.Text == "")
                    {
                        MessageBox.Show("Warning! Please select your office to visit.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboffice.Focus();
                    }
                    else if (txttovisit.Text == "")
                    {
                        MessageBox.Show("Warning! Please enter your person to visit.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txttovisit.Focus();
                    }
                    else if (txtpurpose.Text == "")
                    {
                        MessageBox.Show("Warning! Please enter your Purpose.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtpurpose.Focus();
                    }
                    else
                    {
                        if (txtcompanion.Text == "")
                        {
                            gridTransaction.Rows.Add(new object[] { cboffice.Text, txttovisit.Text, txtpurpose.Text, "None" });
                            ClearAddTransaction();
                        }
                        else
                        {
                            gridTransaction.Rows.Add(new object[] { cboffice.Text, txttovisit.Text, txtpurpose.Text, txtcompanion.Text});
                            ClearAddTransaction();
                        }
                    }


                    //bool notfound = false;
                    //foreach (DataGridViewRow row in gridTransaction.Rows)
                    //{
                    //    if ((string)row.Cells[0].Value == txtusertypeid.Text)
                    //    {
                    //        notfound = true;
                    //        MessageBox.Show("Warning! Duplicate User Choose.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        txtusertypeid.Text = "";
                    //        break;
                    //    }
                    //}
                    //if (!notfound)
                    //{
                    //    if (txtusertypeid.Text == "" && cbusertype.Text == "--Select--" && cbusertypename.Text == "--Select--" && txtpurpose.Text == "")
                    //    {
                    //        MessageBox.Show("Warning! Please Fill-up all the required fields.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    }
                    //    else if (cbusertype.Text == "--Select--")
                    //    {
                    //        MessageBox.Show("Warning! Please choose a User Type.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        cbusertype.Focus();
                    //    }
                    //    else if (cbusertypename.Text == "--Select--")
                    //    {
                    //        MessageBox.Show("Warning! Please choose a User Name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        cbusertypename.Focus();
                    //    }
                    //    else if (txtpurpose.Text == "")
                    //    {
                    //        MessageBox.Show("Warning! Please enter your Purpose.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        txtpurpose.Focus();
                    //    }
                    //    else
                    //    {
                    //        gridTransaction.Rows.Add(new object[] { txtusertypeid.Text, cbusertype.SelectedItem.ToString(), cbusertypename.SelectedItem.ToString(), txtpurpose.Text });
                    //        ClearAddTransaction();
                    //    }
                    //}
                }
            }
            catch (Exception)
            {
                MessageBox.Show("System Error!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnaddclear_Click(object sender, EventArgs e)
        {
            ClearAddTransaction();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if(gridTransaction.Rows.Count < 0)
                {
                    MessageBox.Show("No data on List to be saved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (txttransactionrfid.Text == "" && txtprofileid.Text == "" && txtprofilename.Text == "")
                    {
                        MessageBox.Show("Warning! Please Fill-up all the required fields.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (txttransactionrfid.Text == "")
                    {
                        MessageBox.Show("Please Enter Transaction RFID.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txttransactionrfid.Focus();
                    }
                    else if (txtprofileid.Text == "")
                    {
                        MessageBox.Show("Please Enter Visitor ID.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtprofileid.Focus();
                    }
                    else
                    {
                        IncrementVisitingID();
                        String query = "insert into tbl_visitortransaction (VisitingID, VisitorID, VisitorName, SignedIn, Status, TransactionRFID) values ('"+incrementvisitingid+"', '"+txtprofileid.Text+"', '"+txtprofilename.Text+"', '"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"', 'On-going', '"+txttransactionrfid.Text+"')";
                        InsertUpdateDelete(query);


                        foreach (DataGridViewRow rows in gridTransaction.Rows)
                        {
                            IncrementTransactionNumber();
                            String querytransaction = "insert into tbl_transaction (TransactionNumber, TransactionRFID, TransactionDate, Office, PersonToVisit, Purpose, Companion, Remarks, VisitorID, VisitingID, VisitorName) values ('" + incrementtransactionnumber+"', '"+txttransactionrfid.Text+"', '"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', @Office, @PersonToVisit, @Purpose, @Companion, 'Pending', '" + txtprofileid.Text+"', '"+ incrementvisitingid + "', '"+txtprofilename.Text+"')";
                            connector.connect.Open();
                            MySqlCommand cmd = connector.connect.CreateCommand();
                            cmd.CommandText = querytransaction;
                            cmd.Parameters.AddWithValue("@Office", rows.Cells["Office"].Value);
                            cmd.Parameters.AddWithValue("@PersonToVisit", rows.Cells["PersonToVisit"].Value);
                            cmd.Parameters.AddWithValue("@Purpose", rows.Cells["Purpose"].Value);
                            cmd.Parameters.AddWithValue("@Companion", rows.Cells["Companion"].Value);

                            cmd.ExecuteNonQuery();
                            connector.connect.Close();
                        }
                        MessageBox.Show("Successfully Saved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearAllTransaction(false);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void visitortab_MouseClick(object sender, MouseEventArgs e)
        {
            txttransactionrfid.Focus();
        }

        private void btnsaveclear_Click(object sender, EventArgs e)
        {
            ClearAllTransaction(true);
            txttransactionrfid.Focus();
        }

        private void gridTransaction_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to remove this?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(result == DialogResult.Yes)
                {
                    gridTransaction.Rows.RemoveAt(gridTransaction.SelectedRows[0].Index);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("System Error!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearchvisitor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txtsearchvisitor.Text))
                {
                    String query = "select VisitorID, Name, PhoneNumber as ContactNumber, RecordDate from tbl_visitors where Status = 'Active' order by VisitorID limit 10 ";
                    ViewTable(query, gridVisitor);
                }
                else
                {
                    String query = "select VisitorID, Name, PhoneNumber as ContactNumber, RecordDate from tbl_visitors where Name = '" + txtsearchvisitor.Text + "' and Status = 'Active' order by VisitorID limit 10 ";
                    ViewTable(query, gridVisitor);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("System Error!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridVisitor_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                savepanel.Hide();
                updatepanel.Show();
                timer1.Stop();
                txtidpresented.Enabled = false;
                txtidnumber.Enabled = false;
                String query = "select * from tbl_visitors where VisitorID = '" + gridVisitor.SelectedRows[0].Cells["VisitorID"].Value.ToString() + "'";
                connector.connect.Open();
                MySqlCommand cmd = connector.connect.CreateCommand();
                cmd.CommandText = query;
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    DateTime daterecord = Convert.ToDateTime(read["RecordDate"]);
                    txtdaterecord.Text = daterecord.ToString("MMMM dd, yyyy hh:mm:ss tt");
                    txtvisitorid.Text = read["VisitorID"].ToString();
                    txtidpresented.Text = read["IDPresented"].ToString();
                    txtidnumber.Text = read["IDNumber"].ToString();
                    txtlname.Text = read["Lastname"].ToString();
                    txtfname.Text = read["Firstname"].ToString();
                    txtmname.Text = read["Middlename"].ToString();
                    txtaddress.Text = read["Address"].ToString();
                    txtnumber.Text = read["PhoneNumber"].ToString();
                }
                connector.connect.Close();
                visitortab.SelectedTab = profilingtab;
            }
            catch (Exception)
            {
                MessageBox.Show("System Error!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearchmonitor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txtsearchmonitor.Text))
                {
                    timer1.Start();

                    String querymonitor = "select a.TransactionNumber, a.VisitingID, a.VisitorName, a.Office, a.Purpose, a.Remarks, DATE_FORMAT (b.SignedIn, '" + "%b %d, %Y %l:%i %p" + "') AS SignedIn from tbl_transaction a, tbl_visitortransaction b where b.VisitingID = a.VisitingID and a.Remarks != 'Completed' order by a.TransactionNumber Desc limit 20";
                    ViewTable(querymonitor, gridMonitor);
                    gridMonitor.Columns[0].Visible = false;
                    gridMonitor.Columns[1].Visible = false;
                }
                else
                {
                    timer1.Stop();
                    String querymonitor = "select a.TransactionNumber, a.VisitingID, a.VisitorName, a.Office, a.Purpose, a.Remarks, DATE_FORMAT (b.SignedIn, '" + "%b %d, %Y %l:%i %p" + "') AS SignedIn from tbl_transaction a, tbl_visitortransaction b where b.VisitingID = a.VisitingID and a.Remarks != 'Completed' and a.VisitorName = '" + txtsearchmonitor.Text + "' order by a.TransactionNumber Desc limit 20";
                    ViewTable(querymonitor, gridMonitor);
                    gridMonitor.Columns[0].Visible = false;
                    gridMonitor.Columns[1].Visible = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("System Error!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridMonitor_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void txttovisit_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboffice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txttovisit.Text = "";
                String querycompleteuser = "select * from tbl_users where Office = '" + cboffice.Text + "'";
                AutoComplete(querycompleteuser, txttovisit, "Name");
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txttransactionrfid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txttransactionrfid.Text))
                {
                    txtprofileid.Text = "";
                    txtprofilename.Text = "";
                }
                else
                {
                    
                    if (txttransactionrfid.TextLength >= 10 )
                    {
                        String query = "select COUNT(VisitingID) as totalcount from tbl_visitortransaction where TransactionRFID = '" + txttransactionrfid.Text + "' and Status = 'On-going'";
                        connector.connect.Open();
                        MySqlCommand cmd = connector.connect.CreateCommand();
                        cmd.CommandText = query;
                        MySqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            if (Convert.ToInt32(read["totalcount"]) == 0)
                            {
                                timer1.Start();
                                txttransactionrfid.Enabled = false;
                                txtsearchvisitorprofile.Enabled = true;
                                txtsearchvisitorprofile.Focus();
                            }
                            else if (Convert.ToInt32(read["totalcount"]) == 1)
                            {
                                timer1.Stop();
                                MessageBox.Show("Warning! This RFID Number is still used.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txttransactionrfid.Text = "";
                                txttransactionrfid.Enabled = true;
                            }
                        }
                        connector.connect.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txttransactionrfid_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txttransactionrfid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
