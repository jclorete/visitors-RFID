using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisitorsManagementSystem_RFID
{
    public partial class PrintReport : Form
    {
        Bitmap bm;

        public PrintReport()
        {
            InitializeComponent();
        }

        private void PrintReport_Load(object sender, EventArgs e)
        {
            if (lblreport.Text == "General Report")
            {
                generalpanel.Show();
                visitingpanel.Hide();
                officepanel.Hide();
            }
            else if (lblreport.Text == "Visiting Report")
            {
                generalpanel.Hide();
                visitingpanel.Show();
                officepanel.Hide();
            }
            else if (lblreport.Text == "Office Report")
            {
                generalpanel.Hide();
                visitingpanel.Hide();
                officepanel.Show();
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                btnprint.Hide();
                printicon.Hide();
                btnclose.Hide();
                closeicon.Hide();

                Graphics g = this.CreateGraphics();
                bm = new Bitmap(this.Size.Width, this.Size.Height, g);
                Graphics mg = Graphics.FromImage(bm);
                mg.CopyFromScreen(this.Location.X, this.Location.Y, 10, 20, this.Size);
                printPreviewDialog1.ShowDialog();

                this.Close();
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bm, 0, 0);
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
    }
}
