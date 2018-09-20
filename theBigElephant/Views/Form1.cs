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
using System.Threading;
using theBigElephant.Views;

namespace theBigElephant
{
    public partial class Form1 : Form
    {
        private Button startStockHistoryButton;
        private Button startStockGraphicalButton;
        private Button startPortfolioButton;
        private Button startEventModuleButton;

        public Form1()
        {
            InitializeComponent();

            this.startStockHistoryButton = new System.Windows.Forms.Button();
            this.startStockGraphicalButton = new System.Windows.Forms.Button();
            this.startPortfolioButton = new System.Windows.Forms.Button();
            this.startEventModuleButton = new System.Windows.Forms.Button();

            startStockHistoryButton.Location = new Point(24, 42);
            startStockHistoryButton.Size = new System.Drawing.Size(120, 50);
            startStockHistoryButton.Click += new System.EventHandler(startStockHistory_Click);
            startStockHistoryButton.Text = "Start Stock History";

            startStockGraphicalButton.Location = new Point(150, 42);
            startStockGraphicalButton.Size = new System.Drawing.Size(120, 50);
            startStockGraphicalButton.Click += new System.EventHandler(startStockGraphical_Click);
            startStockGraphicalButton.Text = "Start Graphical";

            startPortfolioButton.Location = new Point(280, 42);
            startPortfolioButton.Size = new System.Drawing.Size(120, 50);
            startPortfolioButton.Click += new System.EventHandler(startPortfolio_Click);
            startPortfolioButton.Text = "Start Portfolio";

            startEventModuleButton.Location = new Point(410, 42);
            startEventModuleButton.Size = new Size(120, 50);
            startEventModuleButton.Click += new System.EventHandler(startEventModule_Click);
            startEventModuleButton.Text = "Start Event";

            this.Controls.Add(startStockHistoryButton);
            this.Controls.Add(startStockGraphicalButton);
            this.Controls.Add(startPortfolioButton);
            this.Controls.Add(startEventModuleButton);
        }

        private void startStockHistory_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(()=>Application.Run(new StockHistoryView()));
            thread.Start();
        }

        private void startStockGraphical_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => Application.Run(new CompanyGraphicalView()));
            thread.Start();
        }

        private void startPortfolio_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => Application.Run(new PortfolioView()));
            thread.Start();
        }

        private void startEventModule_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => Application.Run(new EventModuleView()));
            thread.Start();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
