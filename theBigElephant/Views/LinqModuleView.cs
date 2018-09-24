using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using theBigElephant.LinqModule;

namespace theBigElephant.Views
{
    public partial class LinqModuleView : Form
    {
        private DataGridView dgv;
        private TextBox containsBox;
        private Button containsButton;
        private Button alphabeticalOrderButton;
        private Button clearButton;
        private LocalQueries localQueries = new LocalQueries();


        public LinqModuleView()
        {
            InitializeComponent();
            this.dgv = new DataGridView();
            this.containsBox = new TextBox();
            this.containsButton = new Button();
            this.alphabeticalOrderButton = new Button();
            this.clearButton = new Button();

            dgv.Location = new Point(24, 100);
            dgv.Size = new Size(600, 300);

            containsBox.Location = new Point(24,24);
            containsBox.Size = new Size(80, 24);

            containsButton.Location = new Point(24, 60);
            containsButton.Size = new Size(80,24);
            containsButton.Text = "Contains ?";
            containsButton.Click += new EventHandler(containsButton_Click);

            alphabeticalOrderButton.Location = new Point(150, 60);
            alphabeticalOrderButton.Size = new Size(80,24);
            alphabeticalOrderButton.Text = "Alphabetical order";
            alphabeticalOrderButton.Click += new EventHandler(alphabeticalOrderButton_Click);

            clearButton.Location = new Point(280, 60);
            clearButton.Size = new Size(80, 24);
            clearButton.Text = "Clear";
            clearButton.Click += new EventHandler(clearButton_Click);

            DataGridViewColumn col1 = new DataGridViewColumn();
            col1.HeaderText = "Name";
            col1.Name = "Name";
            col1.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col1);

            DataGridViewColumn col2 = new DataGridViewColumn();
            col2.HeaderText = "Price";
            col2.Name = "Price";
            col2.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col2);

            DataGridViewColumn col3 = new DataGridViewColumn();
            col3.HeaderText = "Country";
            col3.Name = "Country";
            col3.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col3);

            this.Controls.Add(dgv);
            this.Controls.Add(containsBox);
            this.Controls.Add(containsButton);
            this.Controls.Add(alphabeticalOrderButton);
            this.Controls.Add(clearButton);
        }

        private void containsButton_Click(object sender, EventArgs e)
        {
            IEnumerable<LinqModule.Stock> stocks = localQueries.getStocksContaining(this.containsBox.Text);

            var row = dgv.Rows[0];

            foreach (LinqModule.Stock stock in stocks)
            {
                int rowIndex = dgv.Rows.Add();
                row = dgv.Rows[rowIndex];
                row.Cells["Name"].Value = stock.Stock_name;
                row.Cells["Price"].Value = stock.Stock_price;
                row.Cells["Country"].Value = stock.Stock_country;
            }
        }

        private void alphabeticalOrderButton_Click(object sender, EventArgs e)
        {
            IEnumerable<LinqModule.Stock> stocks = localQueries.orderStocksAlphabetically();

            var row = dgv.Rows[0];

            foreach (LinqModule.Stock stock in stocks)
            {
                int rowIndex = dgv.Rows.Add();
                row = dgv.Rows[rowIndex];
                row.Cells["Name"].Value = stock.Stock_name;
                row.Cells["Price"].Value = stock.Stock_price;
                row.Cells["Country"].Value = stock.Stock_country;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
        }
    }
}
