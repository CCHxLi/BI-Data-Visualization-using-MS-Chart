using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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

                //int sum = 0;

                for (int i = 1; i <= 12; i++)
                {
                    string mnth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                    DataRow dr = dt.NewRow();
                    dr[0] = mnth;
                    dr[1] = i * 2;
                    dt.Rows.Add(dr);

                    //sum += (int)dr[1];                    
                }

                chart1.Series[0].Points.DataBind(dt.DefaultView, "Month", "Sale", null);
                chart2.Series[0].Points.DataBind(dt.DefaultView, "Month", "Sale", null);
                chart3.Series[0].Points.DataBind(dt.DefaultView, "Month", "Sale", null);
                chart4.Series[0].Points.DataBind(dt.DefaultView, "Month", "Sale", null);


                //sum = dt.Compute("Sum(Sale)", "");               

                //https://stackoverflow.com/questions/5601752/how-to-sum-columns-in-a-datatable
                //foreach (DataRow dr in dt.Rows)
                //{
                //    foreach (DataColumn dc in dt.Columns)
                //    {
                //        sum += (string)dr[dc];
                //    }
                //}

                //textBox1.Text = "sum/" + sum;
                dgv1.DataSource = dt;

                chart1.Series[0].XValueMember = "Month";
                chart1.Series[0].YValueMembers = "Sale";

                chart2.Series[0].XValueMember = "Month";
                chart2.Series[0].YValueMembers = "Sale";

                chart3.Series[0].XValueMember = "Month";
                chart3.Series[0].YValueMembers = "Sale";

                this.chart1.Titles.Add("Pie chart title");
                this.chart1.Titles.Add("Sale per month");
                this.chart2.Titles.Add("Line chart title");
                this.chart2.Titles.Add("Sale per month");


                //Series min = new Series("mininum expectation");
                //Series max = new Series("maximum expectation");
                //Series avr = new Series("average expectation");

                Series sale = new Series("sale per month");
                chart2.Series[0].LegendText = "Sale per month";

                chart3.Series[0] = sale;
                chart3.Series[0].LegendText = "Sale per month";

                this.chart3.Titles.Add("Control chart title");
                this.chart3.Titles.Add("Sale per month");
                this.chart4.Titles.Add("Pareto chart title");
                this.chart4.Titles.Add("Sale per month");

                chart1.Series[0].IsValueShownAsLabel = true;                 
                chart2.Series[0].IsValueShownAsLabel = true;
                chart3.Series[0].IsValueShownAsLabel = true;
                chart3.Series[0].MarkerStyle = MarkerStyle.Cross;
                chart4.Series[0].IsValueShownAsLabel = true;


                makeParetoChart();
                AddHorizRepeatingStripLines();                
                PopulateChart();
            }
            catch (Exception)
            {

            }


        }

        /// <summary>
        /// Pareto chart study materal reference: https://myminepapers.wordpress.com/tag/pareto-mschart/
        /// </summary>
        private void makeParetoChart()
        {

            // get name of the ChartAre of the source series

            string strChartArea = chart4.Series[0].ChartArea;

            // ensure that the source series is a column chart type

            chart4.Series[0].ChartType = SeriesChartType.Column;
            chart4.Series[0].LegendText = "Sale per month";
            // sort the data in all series by their values in descending order

            chart4.DataManipulator.Sort(PointSortOrder.Descending, chart4.Series[0]);

            // find the total of all points in the source series

            double total = 0.0;

            foreach (DataPoint pt in chart4.Series[0].Points)

                total += pt.YValues[0];

            // set the max value on the primary axis to total

            chart4.ChartAreas[strChartArea].AxisY.Maximum = total;

            // create the destination series and add it to the chart
            
            Series destSeries = new Series("Persontage");
            
            chart4.Series.Add(destSeries);
            
            // ensure that the destination series is either a Line or Spline chart type

            destSeries.ChartType = SeriesChartType.Line;

            destSeries.BorderWidth = 3;

            // assign the series to the same chart area as the column chart is assigned

            destSeries.ChartArea = chart4.Series[0].ChartArea;

            // assign this series to use the secondary axis and set it maximum to be 100%

            destSeries.YAxisType = AxisType.Secondary;

            chart4.ChartAreas[strChartArea].AxisY2.Maximum = 150;

            // locale specific percentage format with no decimals

            chart4.ChartAreas[strChartArea].AxisY2.LabelStyle.Format = "P0";

            // turn off the end point values of the primary X axis

            chart4.ChartAreas[strChartArea].AxisX.LabelStyle.IsEndLabelVisible = false;

            double percentage = 0.0;

            foreach (DataPoint pt in chart4.Series[0].Points)

            {

                percentage += (pt.YValues[0] / total * 100.0);

                destSeries.Points.Add(Math.Round(percentage, 2));

            }            
        }

        /// <summary>
        /// Reference: https://docs.microsoft.com/en-us/dotnet/api/system.web.ui.datavisualization.charting.stripline?redirectedfrom=MSDN&view=netframework-4.7.2
        /// </summary>
        private void AddHorizRepeatingStripLines()
        {



            // Instantiate max strip line  
            StripLine stripLine1 = new StripLine();
            stripLine1.StripWidth = 0;
            stripLine1.BorderColor = Color.Red;
            stripLine1.BorderWidth = 2;
            stripLine1.IntervalOffset = 24;

            //Instantiate min strip line
            StripLine stripLine2 = new StripLine();
            stripLine2.StripWidth = 0;
            stripLine2.BorderColor = Color.Yellow;
            stripLine2.BorderWidth = 2;
            stripLine2.IntervalOffset = 2;

            //Instaniate mean strip line
            StripLine stripLine3 = new StripLine();
            stripLine3.StripWidth = 0;
            stripLine3.BorderColor = Color.Green;
            stripLine3.BorderWidth = 2;
            stripLine3.IntervalOffset = 12; 

            // Add the strip line to the chart  
            chart3.ChartAreas[0].AxisY.StripLines.Add(stripLine1);
            chart3.ChartAreas[0].AxisY.StripLines.Add(stripLine2);
            chart3.ChartAreas[0].AxisY.StripLines.Add(stripLine3);

            //chart3.Series[1].MarkerColor = Color.Red;            
            chart3.Series.Add("Maximun");
            chart3.Series["Maximun"].Color = Color.Red;   
            chart3.Series.Add("Minimun");
            chart3.Series["Minimun"].Color = Color.Yellow;
            chart3.Series.Add("Average");
            chart3.Series["Average"].Color = Color.Green;


            //chart4.ChartAreas[0].AxisY.StripLines.Add(stripLine1);
            //chart4.ChartAreas[0].AxisY.StripLines.Add(stripLine2);
            //chart4.ChartAreas[0].AxisY.StripLines.Add(stripLine3);
        }

        public void PopulateChart()
        {
            //Creid: https://social.msdn.microsoft.com/Forums/en-US/34dc9031-af8b-444c-8eab-574dc4ad561e/how-to-bind-datagridview-columns-and-chart-control-in-c?forum=MSWinWebChart

            chart1.Series[0].ChartType = SeriesChartType.Pie;
            chart1.Series[0].XValueMember = dgv1.Columns[0].DataPropertyName;
            chart1.Series[0].YValueMembers = dgv1.Columns[1].DataPropertyName;
            chart1.DataSource = dgv1.DataSource;
            chart1.DataBind();

            //"Databind" Study Reference: https://zh.coursera.org/lecture/dot-net-kaifa-jishu/shu-ju-bang-ding-gai-nian-ji-yu-fa-hAbQl

            chart2.Series[0].ChartType = SeriesChartType.Line;
            chart2.Series[0].XValueMember = dgv1.Columns[0].DataPropertyName;
            chart2.Series[0].YValueMembers = dgv1.Columns[1].DataPropertyName;
            chart2.DataSource = dgv1.DataSource;
            chart2.DataBind();

            //chart3.Series[0].YValuesPerPoint = dgv1.Columns[1]
            chart3.Series[0].ChartType = SeriesChartType.Line;
            chart3.Series[0].XValueMember = dgv1.Columns[0].DataPropertyName;
            chart3.Series[0].YValueMembers = dgv1.Columns[1].DataPropertyName;
            chart3.DataSource = dgv1.DataSource;
            chart3.DataBind();

            //int avr = chart3.Series[0].YValueMembers
            chart4.Series[0].ChartType = SeriesChartType.Column;
            chart4.Series[0].XValueMember = dgv1.Columns[0].DataPropertyName;
            chart4.Series[0].YValueMembers = dgv1.Columns[1].DataPropertyName;
            chart4.DataSource = dgv1.DataSource;
            chart4.DataBind();
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
