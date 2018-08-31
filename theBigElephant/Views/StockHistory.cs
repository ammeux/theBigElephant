using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Configuration;
using System.Collections.Specialized;
using theBigElephant.StockHistory;

namespace theBigElephant
{
    public partial class Form1 : Form
    {
        private StockLoader stockLoader;

        private DataGridView dgv;
        private Button dlButton;
        private Button clearButton;
        private ProgressBar progBar;
        private String apiAddress;

        public Form1()
        {
            InitializeComponent();

            stockLoader = new StockLoader();

            this.dgv = new DataGridView();
            this.dlButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.progBar = new System.Windows.Forms.ProgressBar();

            dgv.Location = new Point(24, 50);
            dgv.Size = new Size(800, 200);

            dlButton.Location = new Point(24, 16);
            dlButton.Size = new System.Drawing.Size(120, 24);
            dlButton.Click += new System.EventHandler(dlButton_Click);
            dlButton.Text = "Download data";

            clearButton.Location = new Point(150, 16);
            clearButton.Size = new System.Drawing.Size(120, 24);
            clearButton.Click += new System.EventHandler(clearbutton_Click);
            clearButton.Text = "Clear";

            progBar.Visible = true;
            progBar.Minimum = 1;
            progBar.Value = 1;
            progBar.Step = 1;
            progBar.Location = new Point(500, 16);

            this.Controls.Add(dgv);
            this.Controls.Add(dlButton);
            this.Controls.Add(clearButton);
            this.Controls.Add(progBar);

        }

        private void clearbutton_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            progBar.Value = 1;
        }

        private void dlButton_Click(object sender, EventArgs e)
        {
            DataGridViewColumn col1 = new DataGridViewColumn();
            col1.HeaderText = "Date";
            col1.Name = "Date";
            col1.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col1);

            bool init = true;
            var row = dgv.Rows[0];
            List<Stock> stockList = stockLoader.LoadStocks();
            foreach(var stock in stockList)
            {
                DataGridViewColumn col2 = new DataGridViewColumn();
                col2.HeaderText = stock.Symbol;
                col2.Name = stock.Symbol;
                col2.CellTemplate = new DataGridViewTextBoxCell();
                dgv.Columns.Add(col2);

                apiAddress = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=" + stock.Symbol + "&apikey=02X9PDG7B8WP2V7C";
                var json = new WebClient().DownloadString(apiAddress);
                var myStock = JObject.Parse(json)["Time Series (Daily)"];
                int i = 0;
                foreach (JProperty prop in myStock)
                {
                    if (init)
                    {
                        int rowIndex = dgv.Rows.Add();
                        row = dgv.Rows[rowIndex];
                        row.Cells["Date"].Value = prop.Name;
                    }
                    else
                        row = dgv.Rows[i];
                    foreach (var subitem in prop)
                    {   
                        row.Cells[stock.Symbol].Value = subitem["4. close"];
                        progBar.PerformStep();
                    }
                    i++;
                }
                init = false;
            }
        }
    }
}
