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
using System.Windows.Forms.DataVisualization.Charting;
using theBigElephant.CompanyGraphical;

namespace theBigElephant
{
    public partial class CompanyGraphicalView : Form
    {

        private Button loadGraphButton;

        System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1;
        System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        System.Windows.Forms.DataVisualization.Charting.Legend legend1;

        private Configuration.Config configFile = new Configuration.Config();
        private String connString;
        private List<company> companyList = new List<company>();

        public CompanyGraphicalView()
        {
            InitializeComponent();

            this.loadGraphButton = new System.Windows.Forms.Button();

            this.chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();

            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 50);
            this.chart1.Name = "chart1";
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";

            loadGraphButton.Location = new Point(24, 16);
            loadGraphButton.Size = new System.Drawing.Size(120, 24);
            loadGraphButton.Click += new System.EventHandler(loadGraphButton_click);
            loadGraphButton.Text = "Load graph";

            this.Controls.Add(loadGraphButton);
            this.Controls.Add(chart1);
        }

        private void loadGraphButton_click(object sender, EventArgs e)
        {
            connString = $"Server=localhost;Port=3306;Database=the_big_elephant;user=root;password={configFile.DBPWD}";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT results.net_result, results.net_sales, results.quarter, company.company_name FROM results INNER JOIN company ON results.companyID=company.ID";
            conn.Open();
            MySqlDataReader reader = command.ExecuteReader();

            chart1.Series.Clear();
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series()
            {
                Name = "Q1",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            var series2 = new System.Windows.Forms.DataVisualization.Charting.Series()
            {
                Name = "Q2",
                Color = System.Drawing.Color.Red,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            var series3 = new System.Windows.Forms.DataVisualization.Charting.Series()
            {
                Name = "Q3",
                Color = System.Drawing.Color.Blue,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            var series4 = new System.Windows.Forms.DataVisualization.Charting.Series()
            {
                Name = "Q4",
                Color = System.Drawing.Color.Black,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);

            while (reader.Read())
            {
                companyList.Add(new company()
                {
                    name = reader["company_name"].ToString(),
                    net_result = int.Parse(reader["net_result"].ToString()),
                    net_sales = int.Parse(reader["net_sales"].ToString())
                });
            }

            var Result =
                from a in companyList
                where a.name == "BNP"
                select a;

            List < string > quarters = new List<string>() { "Q1", "Q2", "Q3", "Q4" };
            int i = 0;

            foreach (company company in Result)
            {
                chart1.Series[quarters[i]].Points.AddXY(company.name, company.net_sales);
                if (i < 3)
                    i++;
                else
                    i = 0;
            }
        }
    }
}
