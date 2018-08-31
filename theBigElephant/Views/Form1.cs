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

namespace theBigElephant
{
    public partial class Form1 : Form
    {
        private Button startStockHistoryButton;

        public Form1()
        {
            InitializeComponent();

            this.startStockHistoryButton = new System.Windows.Forms.Button();

            startStockHistoryButton.Location = new Point(24, 42);
            startStockHistoryButton.Size = new System.Drawing.Size(120, 50);
            startStockHistoryButton.Click += new System.EventHandler(startStockHistory_Click);
            startStockHistoryButton.Text = "Start Stock History";

            this.Controls.Add(startStockHistoryButton);
        }

        private void startStockHistory_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(()=>Application.Run(new StockHistoryView()));
            thread.Start();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
