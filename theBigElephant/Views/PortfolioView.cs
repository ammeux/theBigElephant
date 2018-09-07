using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace theBigElephant.Views
{
    public partial class PortfolioView : Form
    {

        private DataGridView dgv;
        private Label symbolLabel;
        private TextBox symbolTextBox;
        private Label quantityLabel;
        private TextBox quantityTextBox;
        private Button buyButton;
        private Button sellButton;
        private Button dlButton;
        private Configuration.Config configFile = new Configuration.Config();

        private Portfolio.PortfolioStock stock;
        private List<Portfolio.PortfolioStock> stockPortfolioList = new List<Portfolio.PortfolioStock>();
        private Portfolio.PortfolioLoader portfolioLoader = new Portfolio.PortfolioLoader();
        private String apiAddress;
        private Portfolio.StockPortfolioUpdater stockPortfolioUpdater = new Portfolio.StockPortfolioUpdater();

        public PortfolioView()
        {
            InitializeComponent();

            this.dgv = new DataGridView();
            this.symbolLabel = new System.Windows.Forms.Label();
            this.symbolTextBox = new System.Windows.Forms.TextBox();
            this.quantityLabel = new System.Windows.Forms.Label();
            this.quantityTextBox = new System.Windows.Forms.TextBox();
            this.buyButton = new System.Windows.Forms.Button();
            this.sellButton = new System.Windows.Forms.Button();
            this.dlButton = new System.Windows.Forms.Button();

            dgv.Location = new Point(24, 50);
            dgv.Size = new Size(900, 200);

            symbolLabel.Location = new Point(24, 16);
            symbolLabel.Size = new System.Drawing.Size(50, 24);
            symbolLabel.Text = "Symbol";

            symbolTextBox.Location = new Point(80, 16);
            symbolTextBox.Size = new System.Drawing.Size(120, 24);

            quantityLabel.Location = new Point(230, 16);
            quantityLabel.Size = new System.Drawing.Size(80, 24);
            quantityLabel.Text = "Quantity";

            quantityTextBox.Location = new Point(310, 16);
            quantityTextBox.Size = new System.Drawing.Size(120, 24);

            buyButton.Location = new Point(460, 16);
            buyButton.Size = new System.Drawing.Size(120, 24);
            buyButton.Click += new System.EventHandler(buyButton_Click);
            buyButton.Text = "Buy stocks";

            sellButton.Location = new Point(610, 16);
            sellButton.Size = new System.Drawing.Size(120, 24);
            sellButton.Click += new System.EventHandler(sellButton_Click);
            sellButton.Text = "Sell stocks";

            dlButton.Location = new Point(750, 16);
            dlButton.Size = new System.Drawing.Size(120, 24);
            dlButton.Click += new System.EventHandler(dlButton_Click);
            dlButton.Text = "Load portfolio";

            this.Controls.Add(dgv);
            this.Controls.Add(symbolLabel);
            this.Controls.Add(symbolTextBox);
            this.Controls.Add(quantityLabel);
            this.Controls.Add(quantityTextBox);
            this.Controls.Add(buyButton);
            this.Controls.Add(sellButton);
            this.Controls.Add(dlButton);
        }

        private void buyButton_Click(object sender, EventArgs e)
        {
            apiAddress = "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=" + symbolTextBox.Text + "&apikey=" + configFile.APIKEY;
            var json = new WebClient().DownloadString(apiAddress);
            var myStock = JObject.Parse(json)["Global Quote"];

            stock = new Portfolio.PortfolioStock()
            {
                symbol = symbolTextBox.Text,
                purchasePrice = (float.Parse(myStock["08. previous close"].ToString().Replace(".", ",").Substring(0, myStock["08. previous close"].ToString().Replace(".", ",").Length - 2))),
                purchaseDate = DateTime.Now,
                quantity = int.Parse(quantityTextBox.Text)
            };

            stockPortfolioUpdater.stockBuy(stock);
            dlButton_Click(sender, e);
        }

        private void sellButton_Click(object sender, EventArgs e)
        {
            stockPortfolioUpdater.stockSell(symbolTextBox.Text, int.Parse(quantityTextBox.Text));
            dlButton_Click(sender, e);
        }

        private void dlButton_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            stockPortfolioList = portfolioLoader.LoadPortfolioStocks();

            DataGridViewColumn col1 = new DataGridViewColumn();
            col1.HeaderText = "Symbol";
            col1.Name = "Symbol";
            col1.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col1);

            DataGridViewColumn col2 = new DataGridViewColumn();
            col2.HeaderText = "Purchase_date";
            col2.Name = "Purchase_date";
            col2.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col2);

            DataGridViewColumn col3 = new DataGridViewColumn();
            col3.HeaderText = "Quantity";
            col3.Name = "Quantity";
            col3.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col3);

            DataGridViewColumn col4 = new DataGridViewColumn();
            col4.HeaderText = "Purchase_price";
            col4.Name = "Purchase_price";
            col4.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col4);

            DataGridViewColumn col5 = new DataGridViewColumn();
            col5.HeaderText = "Closing_price";
            col5.Name = "Closing_price";
            col5.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col5);

            DataGridViewColumn col6 = new DataGridViewColumn();
            col6.HeaderText = "%Change";
            col6.Name = "%Change";
            col6.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col6);

            DataGridViewColumn col7 = new DataGridViewColumn();
            col7.HeaderText = "Capital_gain";
            col7.Name = "Capital_gain";
            col7.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col7);

            var row = dgv.Rows[0];

            foreach (Portfolio.PortfolioStock stock in stockPortfolioList)
            {
                apiAddress = "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol="+ stock.symbol +"&apikey=" + configFile.APIKEY;
                var json = new WebClient().DownloadString(apiAddress);
                var myStock = JObject.Parse(json)["Global Quote"];

                int rowIndex = dgv.Rows.Add();
                row = dgv.Rows[rowIndex];
                row.Cells["Symbol"].Value = stock.symbol;
                row.Cells["Purchase_date"].Value = stock.purchaseDate;
                row.Cells["Quantity"].Value = stock.quantity;
                row.Cells["Purchase_price"].Value = stock.purchasePrice;
                row.Cells["Closing_price"].Value = myStock["08. previous close"];
                row.Cells["%Change"].Value = ((float.Parse(myStock["08. previous close"].ToString().Replace(".", ",").Substring(0, myStock["08. previous close"].ToString().Replace(".", ",").Length - 2)) / stock.purchasePrice - 1)*100).ToString() + "%";
                row.Cells["Capital_gain"].Value = (float.Parse(myStock["08. previous close"].ToString().Replace(".", ",").Substring(0, myStock["08. previous close"].ToString().Replace(".", ",").Length - 2)) - stock.purchasePrice) * stock.quantity;
            }
        }
    }
}
