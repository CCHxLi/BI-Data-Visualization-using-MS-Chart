using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BIA01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitChart();
        }      

        private void InitChart()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Month");
                dt.Columns.Add("Sale");

                dt.Rows.Add("Sping", 20);
                dt.Rows.Add("Summer", 35);
                dt.Rows.Add("Autumn", 15);
                dt.Rows.Add("Winter", 30);

                dgv1.DataSource = dt;

                chart1.Series[0].XValueMember = "Month";
                chart1.Series[0].YValueMembers = "Sale";
                this.chart1.Titles.Add("Pie chart title");
                chart1.Series[0].ChartType = SeriesChartType.Pie;
                chart1.Series[0].IsValueShownAsLabel = true;
                PopulateChart();
            }
            catch (Exception)
            {

            }


        }
        public void PopulateChart()
        {
            //Creid: https://social.msdn.microsoft.com/Forums/en-US/34dc9031-af8b-444c-8eab-574dc4ad561e/how-to-bind-datagridview-columns-and-chart-control-in-c?forum=MSWinWebChart
            
            chart1.Series[0] = new Series();
            chart1.Series[0].XValueMember = dgv1.Columns[0].DataPropertyName;
            chart1.Series[0].YValueMembers = dgv1.Columns[1].DataPropertyName;
            chart1.DataSource = dgv1.DataSource;


            chart2.Series[0] = new Series();
            chart2.Series[0].XValueMember = dgv1.Columns[0].DataPropertyName;
            chart2.Series[0].YValueMembers = dgv1.Columns[1].DataPropertyName;
            chart2.DataSource = dgv1.DataSource;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            PopulateChart();
        }

        public class DataCollection
        {
            public string X { get; set; }

            public int Y { get; set; }
        }
    }
}
