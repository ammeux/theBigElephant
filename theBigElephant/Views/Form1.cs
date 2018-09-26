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
using Point = System.Drawing.Point;

namespace theBigElephant
{
    public partial class Form1 : Form
    {
        private Button startStockHistoryButton;
        private Button startStockGraphicalButton;
        private Button startPortfolioButton;
        private Button startEventModuleButton;
        private Button startLinqModuleButton;
        private Button startThreadModuleButton;

        public Form1()
        {
            InitializeComponent();

            this.startStockHistoryButton = new System.Windows.Forms.Button();
            this.startStockGraphicalButton = new System.Windows.Forms.Button();
            this.startPortfolioButton = new System.Windows.Forms.Button();
            this.startEventModuleButton = new System.Windows.Forms.Button();
            this.startLinqModuleButton = new System.Windows.Forms.Button();
            this.startThreadModuleButton = new System.Windows.Forms.Button();

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

            startLinqModuleButton.Location = new Point(540, 42);
            startLinqModuleButton.Size = new Size(120, 50);
            startLinqModuleButton.Click += new System.EventHandler(startLinqModule_Click);
            startLinqModuleButton.Text = "Start LINQ";

            startThreadModuleButton.Location = new Point(670, 42);
            startThreadModuleButton.Size = new Size(120, 50);
            startThreadModuleButton.Click += new System.EventHandler(startThreadModule_Click);
            startThreadModuleButton.Text = "Start Thread";

            this.Controls.Add(startStockHistoryButton);
            this.Controls.Add(startStockGraphicalButton);
            this.Controls.Add(startPortfolioButton);
            this.Controls.Add(startEventModuleButton);
            this.Controls.Add(startLinqModuleButton);
            this.Controls.Add(startThreadModuleButton);
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

        private void startLinqModule_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => Application.Run(new LinqModuleView()));
            thread.Start();
        }

        private void startThreadModule_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => Application.Run(new ThreadModuleView()));
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
