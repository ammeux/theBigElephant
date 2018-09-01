using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace theBigElephant
{
    public partial class StockGraphicalView : Form
    {

        private Button loadGraphButton;

        public StockGraphicalView()
        {
            InitializeComponent();

            this.loadGraphButton = new System.Windows.Forms.Button();

            loadGraphButton.Location = new Point(24, 16);
            loadGraphButton.Size = new System.Drawing.Size(120, 24);
            loadGraphButton.Click += new System.EventHandler(loadGraphButton_click);
            loadGraphButton.Text = "Load graph";

            this.Controls.Add(loadGraphButton);
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void loadGraphButton_click(object sender, EventArgs e)
        {
            this.chart1.Series["Value"].Points.AddXY("19/12/1988", 25);
            this.chart1.Series["Value"].Points.AddXY("20/12/1988", 32);
            this.chart1.Series["Value"].Points.AddXY("21/12/1988", 40);
        }
    }
}
