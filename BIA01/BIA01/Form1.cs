using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIA01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Month");
                dt.Columns.Add("Sale");

                dt.Rows.Add("Sping", 20);
                dt.Rows.Add("Summer", 35);
                dt.Rows.Add("Autumn", 15);
                dt.Rows.Add("winter", 30);

                dgv1.DataSource = dt;
            }
            catch (Exception)
            {
                                
            }
        }
    }
}
