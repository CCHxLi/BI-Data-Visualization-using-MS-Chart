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
            LoadData();            
        }

        private void LoadData()
        {
            chart1.Series[0].Points.AddXY("EMEA",50.00);
            chart1.Series[0].Points.AddXY("Asia",30.00);
            chart1.Series[0].Points.AddXY("Americas", 20.00);
        }

        private void InitChart()
        {
            chart1.Legends[0].Title = "Display Legends";
            chart1.Titles.Add("pie first title");
            chart1.Titles.Add("pie second title");
            chart1.Series[0].ChartType = SeriesChartType.Pie;

        }

        private void chart1_Click(object sender, EventArgs e)
        {
           
        }
        
    }
}
