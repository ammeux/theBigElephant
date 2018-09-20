using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using theBigElephant.EventModule;

namespace theBigElephant.Views
{
    public partial class EventModuleView : Form
    {

        private Label crudeOilBrentPriceLabel;
        private TextBox crudeOilBrentPriceTextBox;
        private Button updatePriceButtton;
        private Label currentBrentPriceLabel;

        private Brent brent;

        public EventModuleView()
        {
            InitializeComponent();
            this.currentBrentPriceLabel = new Label();
            this.crudeOilBrentPriceLabel = new Label();
            this.crudeOilBrentPriceTextBox = new TextBox();
            this.updatePriceButtton = new Button();
            this.brent = new Brent();

            brent.CrudeOilBrentPrice = 86;
            brent.BrentPriceChanged += brent_PriceChanged;

            currentBrentPriceLabel.Location = new Point(80, 24);
            currentBrentPriceLabel.Size = new Size(120, 24);
            currentBrentPriceLabel.Text = "Current Price: " + brent.CrudeOilBrentPrice.ToString();

            crudeOilBrentPriceLabel.Location = new Point(24,50);
            crudeOilBrentPriceLabel.Size = new Size(80, 24);
            crudeOilBrentPriceLabel.Text = "Brent Price";

            crudeOilBrentPriceTextBox.Location = new Point(120, 50);
            crudeOilBrentPriceTextBox.Size = new Size(80, 24);
            
            updatePriceButtton.Location = new Point(50, 80);
            updatePriceButtton.Size = new Size(120, 40);
            updatePriceButtton.Text = "Update Crude Oil Price";
            updatePriceButtton.Click += new EventHandler(updatePriceButtton_Click);

            this.Controls.Add(currentBrentPriceLabel);
            this.Controls.Add(crudeOilBrentPriceLabel);
            this.Controls.Add(crudeOilBrentPriceTextBox);
            this.Controls.Add(updatePriceButtton);
        }

        private void updatePriceButtton_Click(object sender, EventArgs e)
        {
            brent.CrudeOilBrentPrice = Int32.Parse(crudeOilBrentPriceTextBox.Text);
            currentBrentPriceLabel.Text = "Current Price: " + brent.CrudeOilBrentPrice.ToString();
        }

        static void brent_PriceChanged(object sender, BrentPriceChangeEventArgs e)
        {
            if (e.NewBrentPrice - e.LastBrentPrice > 10)
                MessageBox.Show("Price increased by more than 10");
        }
    }
}
