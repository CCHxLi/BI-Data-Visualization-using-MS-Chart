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

        private void Changedata()
        {
            string selectedName = listBox1.GetItemText(listBox1.SelectedItem);
            int selectedValue = Int32.Parse(textBox1.Text);
            LoadData(selectedName, selectedValue);
        }

        private void LoadData(string selectedName, int selectedValue)
        {
            string[] categories = { "EMEA", "Asia", "Americas" };
            if (categories.Contains(selectedName))
            {
                chart1.Series[0].Points.AddXY(selectedName, selectedValue);
            }


            chart1.Series[0].Points.AddXY("EMEA", 50);
            chart1.Series[0].Points.AddXY("Asia", 30);
            chart1.Series[0].Points.AddXY("Americas", 20);

            chart2.Series[0].Points.AddXY("EMEA", 50);
            chart2.Series[0].Points.AddXY("Asia", 30);
            chart2.Series[0].Points.AddXY("Ameriacas", 20);
        }

        private void InitChart()
        {
            chart1.Legends[0].Title = "Display Legend";
            chart1.Titles.Add("pie first title");
            chart1.Titles.Add("pie second title");
            chart1.Series[0].ChartType = SeriesChartType.Pie;

            chart2.Legends[0].Title = "Display Legend";
            chart2.Titles.Add("line chart title");
            chart2.Series[0].ChartType = SeriesChartType.Line;

        }
    }
}
