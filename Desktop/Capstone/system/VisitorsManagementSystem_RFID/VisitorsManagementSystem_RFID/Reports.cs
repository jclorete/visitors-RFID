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

using DGVPrinterHelper;

namespace VisitorsManagementSystem_RFID
{
    public partial class Reports : Form
    {
        ConnectionClass connector = new ConnectionClass();

        public Reports()
        {
            InitializeComponent();
            visitorspanel.Hide();
            cboffice.Hide();
            gridDisplayGeneralReport.Hide();
            gridDisplayVisitingReport.Hide();
            gridDisplayOfficeReport.Hide();
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

        public void ClearAll()
        {
            datefrom.Value = DateTime.Now;
            dateto.Value = DateTime.Now;
            datefrom.Enabled = false;
            dateto.Enabled = false;
            txttotalvisitor.Text = null;
            txttotaltransaction.Text = null;
            gridTable.Columns.Clear();
            gridTable.BringToFront();
            gridGeneralReport.SendToBack();
            gridVisitingReport.SendToBack();
            gridOfficeReport.SendToBack();
            gridGeneralReport.Rows.Clear();
            gridVisitingReport.Rows.Clear();
            gridOfficeReport.Rows.Clear();

            cbtype.Text = "--Select--";
            cboffice.Text = "--Select--";
            cboffice.Hide();
            visitorspanel.Hide();
        }

        private void Reports_Load(object sender, EventArgs e)
        {

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            datefrom.Value = DateTime.Now;
            dateto.Value = DateTime.Now;
            if (cbtype.Text == "General Report")
            {
                datefrom.Enabled = true;
                dateto.Enabled = true;
                visitorspanel.Show();
                cboffice.Hide();
                gridTable.Hide();
                gridVisitingReport.Hide();
                gridOfficeReport.Hide();
                gridDisplayGeneralReport.Hide();
                gridDisplayVisitingReport.Hide();
                gridDisplayOfficeReport.Hide();

                gridGeneralReport.Show();
            }
            else if (cbtype.Text == "Visiting Report")
            {
                datefrom.Enabled = true;
                dateto.Enabled = true;
                visitorspanel.Show();
                cboffice.Hide();
                gridTable.Hide();
                gridGeneralReport.Hide();
                gridOfficeReport.Hide();
                gridDisplayGeneralReport.Hide();
                gridDisplayVisitingReport.Hide();
                gridDisplayOfficeReport.Hide();

                gridVisitingReport.Show();
                gridVisitingReport.BringToFront();
            }
            else if (cbtype.Text == "Office Report")
            {
                datefrom.Enabled = false;
                dateto.Enabled = false;
                visitorspanel.Hide();
                cboffice.Show();
                gridTable.Hide();
                gridGeneralReport.Hide();
                gridVisitingReport.Hide();
                gridDisplayGeneralReport.Hide();
                gridDisplayVisitingReport.Hide();
                gridDisplayOfficeReport.Hide();

                gridOfficeReport.Show();
                gridOfficeReport.BringToFront();

                cboffice.Items.Clear();
                String query = "select  * from tbl_usertype where UserID > 2";
                connector.connect.Open();
                MySqlCommand cmd = connector.connect.CreateCommand();
                cmd.CommandText = query;
                MySqlDataReader read = cmd.ExecuteReader();
                while(read.Read())
                {
                    cboffice.Items.Add(read["Office"].ToString());
                }
                connector.connect.Close();
            }
        }

        private void datefrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateto_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if(cbtype.Text == "--Select--")
                {
                    MessageBox.Show("Warning! Please choose a type of report.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearAll();
                }
                else
                {
                    if (datefrom.Value > dateto.Value)
                    {
                        MessageBox.Show("Warning! Choosing of Past Dates is invalid.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        datefrom.Value = DateTime.Now;
                        dateto.Value = DateTime.Now;
                    }
                    else
                    {
                        if (cbtype.Text == "General Report")
                        {
                            gridGeneralReport.Rows.Clear();
                            gridDisplayGeneralReport.Rows.Clear();

                            String queryoffice = "select * from tbl_usertype where UserID > 2";
                            ViewTable(queryoffice, gridTable);

                            foreach (DataGridViewRow row in gridTable.Rows)
                            {
                                String offices = row.Cells[2].Value.ToString();

                                String queryofficevisit = "select count(TransactionNumber) as totalcount from tbl_transaction where Remarks = 'Completed' and Office = '" + offices + "' and TransactionDate between '" + datefrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateto.Value.ToString("yyyy-MM-dd") + "'";
                                connector.connect.Open();
                                MySqlCommand cmdcount = connector.connect.CreateCommand();
                                cmdcount.CommandText = queryofficevisit;
                                MySqlDataReader readcount = cmdcount.ExecuteReader();
                                while (readcount.Read())
                                {
                                    String count = readcount["totalcount"].ToString();

                                    gridGeneralReport.Rows.Add(new object[] { offices, count });
                                    gridDisplayGeneralReport.Rows.Add(new object[] { offices, count });

                                }
                                connector.connect.Close();
                            }

                            String queryvisitorcount = "select count(VisitingID) as totalcount from tbl_visitortransaction where Status in ('Completed', 'Cancelled') and SignedIn between '" + datefrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateto.Value.ToString("yyyy-MM-dd") + "'";
                            connector.connect.Open();
                            MySqlCommand cmdvisitorcount = connector.connect.CreateCommand();
                            cmdvisitorcount.CommandText = queryvisitorcount;
                            MySqlDataReader readvisitorcount = cmdvisitorcount.ExecuteReader();
                            while (readvisitorcount.Read())
                            {
                                txttotalvisitor.Text = readvisitorcount["totalcount"].ToString();
                            }
                            connector.connect.Close();

                            String querytransactioncount = "select count(TransactionNumber) as totalcount from tbl_transaction where Remarks in ('Completed', 'Cancelled') and TransactionDate between '" + datefrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateto.Value.ToString("yyyy-MM-dd") + "'";
                            connector.connect.Open();
                            MySqlCommand cmdtransactioncount = connector.connect.CreateCommand();
                            cmdtransactioncount.CommandText = querytransactioncount;
                            MySqlDataReader readtransactioncount = cmdtransactioncount.ExecuteReader();
                            while (readtransactioncount.Read())
                            {
                                txttotaltransaction.Text = readtransactioncount["totalcount"].ToString();
                            }
                            connector.connect.Close();
                        }
                        else if (cbtype.Text == "Visiting Report")
                        {
                            gridVisitingReport.Rows.Clear();
                            gridDisplayVisitingReport.Hide();

                            String queryvisitorcount = "select count(VisitingID) as totalcount from tbl_visitortransaction where Status in ('Completed', 'Cancelled') and SignedIn between '" + datefrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateto.Value.ToString("yyyy-MM-dd") + "'";
                            connector.connect.Open();
                            MySqlCommand cmdvisitorcount = connector.connect.CreateCommand();
                            cmdvisitorcount.CommandText = queryvisitorcount;
                            MySqlDataReader readvisitorcount = cmdvisitorcount.ExecuteReader();
                            while (readvisitorcount.Read())
                            {
                                txttotalvisitor.Text = readvisitorcount["totalcount"].ToString();
                            }
                            connector.connect.Close();

                            String querytransactioncount = "select count(TransactionNumber) as totalcount from tbl_transaction where Remarks in ('Completed', 'Cancelled') and TransactionDate between '" + datefrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateto.Value.ToString("yyyy-MM-dd") + "'";
                            connector.connect.Open();
                            MySqlCommand cmdtransactioncount = connector.connect.CreateCommand();
                            cmdtransactioncount.CommandText = querytransactioncount;
                            MySqlDataReader readtransactioncount = cmdtransactioncount.ExecuteReader();
                            while (readtransactioncount.Read())
                            {
                                txttotaltransaction.Text = readtransactioncount["totalcount"].ToString();
                            }
                            connector.connect.Close();

                            String querytable = "select a.VisitingID, a.VisitorName, b.Office, a.Status, DATE_FORMAT (a.SignedIn, '" + "%b %d, %Y %l:%i %p" + "') as SignedIn, DATE_FORMAT (a.SignedOut, '" + "%b %d, %Y %l:%i %p" + "') as SignedOut from tbl_visitortransaction a, tbl_transaction b where b.VisitingID = a.VisitingID and a.Status in ('Completed', 'Cancelled') and a.SignedOut between '" + datefrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateto.Value.ToString("yyyy-MM-dd") + "'";
                            ViewTable(querytable, gridTable);

                            foreach (DataGridViewRow rows in gridTable.Rows)
                            {
                                String visitor = rows.Cells["VisitorName"].Value.ToString();
                                String visitedoffice = rows.Cells["Office"].Value.ToString();
                                String signedin = rows.Cells["SignedIn"].Value.ToString();
                                String signedout = rows.Cells["SignedOut"].Value.ToString();
                                String status = rows.Cells["Status"].Value.ToString();

                                gridVisitingReport.Rows.Add(new object[] { visitor, visitedoffice, signedin, signedout, status });
                                gridDisplayVisitingReport.Rows.Add(new object[] { visitor, visitedoffice, status });

                            }
                        }
                        else if (cbtype.Text == "Office Report")
                        {
                            gridOfficeReport.Rows.Clear();
                            gridDisplayOfficeReport.Rows.Clear();

                            String querytable = "select a.VisitingID, a.VisitorName, b.Office, b.Remarks, DATE_FORMAT (a.SignedIn, '" + "%b %d, %Y %l:%i %p" + "') as SignedIn, DATE_FORMAT (a.SignedOut, '" + "%b %d, %Y %l:%i %p" + "') as SignedOut from tbl_visitortransaction a, tbl_transaction b where b.VisitingID = a.VisitingID and b.Office = '"+cboffice.Text+"' and b.Remarks in ('Completed', 'Cancelled') and b.TransactionDate between '" + datefrom.Value.ToString("yyyy-MM-dd") + "' and '" + dateto.Value.ToString("yyyy-MM-dd") + "'";
                            ViewTable(querytable, gridTable);

                            foreach (DataGridViewRow rows in gridTable.Rows)
                            {
                                String visitor = rows.Cells["VisitorName"].Value.ToString();
                                String remarks = rows.Cells["Remarks"].Value.ToString();
                                String signedin = rows.Cells["SignedIn"].Value.ToString();
                                String signedout = rows.Cells["SignedOut"].Value.ToString();

                                gridOfficeReport.Rows.Add(new object[] { visitor, remarks, signedin, signedout });
                                gridDisplayOfficeReport.Rows.Add(new object[] { visitor, remarks, signedin, signedout });

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btngenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if(cbtype.Text == "--Select--")
                {
                    MessageBox.Show("Warning! Please select report type.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbtype.Text == "General Report")
                {
                    DGVPrinter printer = new DGVPrinter();
                    printer.TitleSpacing = 200;
                    printer.SubTitleAlignment = StringAlignment.Center;
                    printer.SubTitleSpacing = 100;
                    printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    printer.PageNumbers = false;
                    printer.PageNumberInHeader = false;
                    printer.PorportionalColumns = true;
                    printer.HeaderCellAlignment = StringAlignment.Center;
                    printer.printDocument = this.printDocument1;
                    printer.PrintPreviewDataGridView(gridDisplayGeneralReport);
                }
                if (cbtype.Text == "Visiting Report")
                {
                    DGVPrinter printer = new DGVPrinter();
                    printer.TitleSpacing = 200;
                    printer.SubTitleAlignment = StringAlignment.Center;
                    printer.SubTitleSpacing = 100;
                    printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    printer.PageNumbers = false;
                    printer.PageNumberInHeader = false;
                    printer.PorportionalColumns = true;
                    printer.HeaderCellAlignment = StringAlignment.Center;
                    printer.printDocument = this.printDocument2;
                    printer.PrintPreviewDataGridView(gridDisplayVisitingReport);
                }
                if (cbtype.Text == "Office Report")
                {
                    DGVPrinter printer = new DGVPrinter();
                    printer.TitleSpacing = 200;
                    printer.SubTitleAlignment = StringAlignment.Center;
                    printer.SubTitleSpacing = 10;
                    printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    printer.PageNumbers = false;
                    printer.PageNumberInHeader = false;
                    printer.PorportionalColumns = true;
                    printer.HeaderCellAlignment = StringAlignment.Center;
                    printer.printDocument = this.printDocument3;
                    printer.PrintPreviewDataGridView(gridDisplayOfficeReport);
                }

                //if (cbtype.Text == "General Report")
                //{
                //    PrintReport pf = new PrintReport();
                //    pf.lblreport.Text = "General Report";
                //    pf.lbldate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
                //    pf.lblpreferred.Text = Login.formlogname;
                //    pf.lbltotalvisitor.Text = txttotalvisitor.Text;
                //    pf.lbltotaltransaction.Text = txttotaltransaction.Text;

                //    foreach (DataGridViewRow rows in gridGeneralReport.Rows)
                //    {
                //        String office = rows.Cells[0].Value.ToString();
                //        String totalvisit = rows.Cells[1].Value.ToString();

                //        pf.gridGeneralReport.Rows.Add(new object[] { office, totalvisit });
                //    }
                //    pf.Show();
                //}
                //else if (cbtype.Text == "Visiting Report")
                //{
                //    PrintReport pf = new PrintReport();
                //    pf.lblreport.Text = "Visiting Report";
                //    pf.lbldate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
                //    pf.lblpreferred.Text = Login.formlogname;
                //    pf.lbltotalvisitor.Text = txttotalvisitor.Text;
                //    pf.lbltotaltransaction.Text = txttotaltransaction.Text;

                //    foreach (DataGridViewRow rows in gridVisitingReport.Rows)
                //    {
                //        String visitor = rows.Cells[0].Value.ToString();
                //        String visitedoffice = rows.Cells[1].Value.ToString();
                //        String signedin = rows.Cells[2].Value.ToString();
                //        String signedout = rows.Cells[3].Value.ToString();
                //        String status = rows.Cells[4].Value.ToString();

                //        pf.gridVisitingReport.Rows.Add(new object[] { visitor, visitedoffice, signedin, signedout, status });
                //    }
                //    pf.Show();
                //}
                //else if (cbtype.Text == "Office Report")
                //{
                //    PrintReport pf = new PrintReport();
                //    pf.lblreport.Text = "Office Report";
                //    pf.lbldate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
                //    pf.lblpreferred.Text = Login.formlogname;
                //    pf.lbltotalvisitor.Text = txttotalvisitor.Text;
                //    pf.lbltotaltransaction.Text = txttotaltransaction.Text;

                //    foreach (DataGridViewRow rows in gridOfficeReport.Rows)
                //    {
                //        String visitor = rows.Cells[0].Value.ToString();
                //        String remarks = rows.Cells[1].Value.ToString();
                //        String signedin = rows.Cells[2].Value.ToString();
                //        String signedout = rows.Cells[3].Value.ToString();

                //        pf.gridOfficeReport.Rows.Add(new object[] { visitor, remarks, signedin, signedout });
                //    }
                //    pf.Show();
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("System Error!" + ex, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void cboffice_SelectedIndexChanged(object sender, EventArgs e)
        {
            datefrom.Enabled = true;
            dateto.Enabled = true;
            datefrom.Value = DateTime.Now;
            dateto.Value = DateTime.Now;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Pilar College of Zamboanga City, Inc.", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, new Point(160, 40));
            e.Graphics.DrawString("R.T Lim Boulevard, Zamboanga City", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(240, 74));

            e.Graphics.DrawString(DateTime.Now.ToString("MMMM dd, yyyy"), new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(343, 150));

            e.Graphics.DrawString("General Report", new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new Point(330, 220));

            e.Graphics.DrawString("Total Number of Visitor's:", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 300));
            e.Graphics.DrawString(txttotalvisitor.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(590, 300));

            e.Graphics.DrawString("Total Number of Transactions:", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 330));
            e.Graphics.DrawString(txttotaltransaction.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(590, 330));

            e.Graphics.DrawString("Prepared By: ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(120, 800));
            e.Graphics.DrawString(Login.formlogname, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(120, 850));
            e.Graphics.DrawString(Login.formlogusertype, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(120, 870));
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Pilar College of Zamboanga City, Inc.", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, new Point(160, 40));
            e.Graphics.DrawString("R.T Lim Boulevard, Zamboanga City", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(240, 74));

            e.Graphics.DrawString(DateTime.Now.ToString("MMMM dd, yyyy"), new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(343, 150));

            e.Graphics.DrawString("Visiting Report", new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new Point(320, 220));

            e.Graphics.DrawString("Total Number of Visitor's:", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 300));
            e.Graphics.DrawString(txttotalvisitor.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(590, 300));

            e.Graphics.DrawString("Total Number of Transactions:", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 330));
            e.Graphics.DrawString(txttotaltransaction.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(590, 330));

            e.Graphics.DrawString("Prepared By: ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(120, 800));
            e.Graphics.DrawString(Login.formlogname, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(120, 850));
            e.Graphics.DrawString(Login.formlogusertype, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(120, 870));
        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Pilar College of Zamboanga City, Inc.", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, new Point(160, 40));
            e.Graphics.DrawString("R.T Lim Boulevard, Zamboanga City", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(240, 74));

            e.Graphics.DrawString(DateTime.Now.ToString("MMMM dd, yyyy"), new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(343, 150));

            e.Graphics.DrawString("Office Report", new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new Point(330, 220));

            e.Graphics.DrawString("Prepared By: ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(120, 800));
            e.Graphics.DrawString(Login.formlogname, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(120, 850));
            e.Graphics.DrawString(Login.formlogusertype, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(120, 870));
        }
    }
}
