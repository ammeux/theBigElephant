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
using System.IO;

namespace theBigElephant
{
    public partial class StockHistoryView : Form
    {
        private StockLoader stockLoader;
        private StockListUpdater stockListUpdater;

        private DataGridView dgv;
        private Button dlButton;
        private Button clearButton;
        private Button addRemoveStockButton;
        private ProgressBar progBar;
        private Label symbolLabel;
        private TextBox symbolTextBox;
        private Label nameLabel;
        private TextBox nameTextBox;
        private String apiAddress;
        private Stock stockToAddOrRemove;
        private String error;

        public StockHistoryView()
        {
            InitializeComponent();

            stockLoader = new StockLoader();
            stockListUpdater = new StockListUpdater();

            this.dgv = new DataGridView();
            this.dlButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.symbolLabel = new System.Windows.Forms.Label();
            this.symbolTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.addRemoveStockButton = new System.Windows.Forms.Button();
            this.progBar = new System.Windows.Forms.ProgressBar();

            dgv.Location = new Point(24, 50);
            dgv.Size = new Size(1000, 200);

            dlButton.Location = new Point(24, 16);
            dlButton.Size = new System.Drawing.Size(120, 24);
            dlButton.Click += new System.EventHandler(dlButton_Click);
            dlButton.Text = "Download data";

            clearButton.Location = new Point(150, 16);
            clearButton.Size = new System.Drawing.Size(120, 24);
            clearButton.Click += new System.EventHandler(clearButton_Click);
            clearButton.Text = "Clear";

            symbolLabel.Location = new Point(300, 16);
            symbolLabel.Size = new System.Drawing.Size(50, 24);
            symbolLabel.Text = "Symbol";

            symbolTextBox.Location = new Point(350, 16);
            symbolTextBox.Size = new System.Drawing.Size(120, 24);

            nameLabel.Location = new Point(500, 16);
            nameLabel.Size = new System.Drawing.Size(50, 24);
            nameLabel.Text = "Name";

            nameTextBox.Location = new Point(550, 16);
            nameTextBox.Size = new System.Drawing.Size(120, 24);

            addRemoveStockButton.Location = new Point(700, 16);
            addRemoveStockButton.Size = new System.Drawing.Size(120, 24);
            addRemoveStockButton.Click += new System.EventHandler(addRemoveStockButton_Click);
            addRemoveStockButton.Text = "Add/Remove stock";

            progBar.Location = new Point(850, 16);
            progBar.Visible = true;
            progBar.Minimum = 1;
            progBar.Value = 1;
            progBar.Step = 1;

            this.Controls.Add(dgv);
            this.Controls.Add(dlButton);
            this.Controls.Add(clearButton);
            this.Controls.Add(symbolLabel);
            this.Controls.Add(symbolTextBox);
            this.Controls.Add(nameLabel);
            this.Controls.Add(nameTextBox);
            this.Controls.Add(addRemoveStockButton);
            this.Controls.Add(progBar);
        }

        private void clearButton_Click(object sender, EventArgs e)
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
            foreach (var stock in stockList)
            {
                DataGridViewColumn col2 = new DataGridViewColumn();
                col2.HeaderText = stock.Name;
                col2.Name = stock.Name;
                col2.CellTemplate = new DataGridViewTextBoxCell();
                dgv.Columns.Add(col2);

                apiAddress = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=" + stock.Symbol + "&apikey=02X9PDG7B8WP2V7C";
                var json = new WebClient().DownloadString(apiAddress);
                var myStock = JObject.Parse(json)["Time Series (Daily)"];
                int i = 0;
                try
                {
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
                            row.Cells[stock.Name].Value = subitem["4. close"];
                            progBar.PerformStep();
                        }
                        i++;
                    }
                    init = false;
                }
                catch(NullReferenceException ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("Sorry, API only accepts 5 requests per minute");
                    error = ex.Message;
                    LogMsg();
                }
            }
        }

        private void addRemoveStockButton_Click(object sender, EventArgs e)
        {
            stockToAddOrRemove = new Stock()
            {
                Symbol = symbolTextBox.Text,
                Name = nameTextBox.Text
            };
            stockListUpdater.Save(stockToAddOrRemove);
            clearButton_Click(sender, e);
            dlButton_Click(sender, e);
        }

        private void LogMsg()
        {
            StreamWriter sw = new StreamWriter(@"log.txt", true);
            sw.WriteLine($"Error Message:({error}\tErrorTime:{DateTime.Now})");
            sw.Close();
        }
    }
}