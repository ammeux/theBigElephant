using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace theBigElephant.Views
{
    public partial class ThreadModuleView : Form
    {

        private DataGridView dgv;
        private Button showThreadsButton;
        private Button clearTextBoxButton;
        private Button clearButton;
        private Button startNewTaskButton;
        private Button startNewThreadButton;
        private Button goButton;
        private Button goAsyncButton;
        private RichTextBox consoleWriteLineBox;
        

        ManualResetEvent signal = new ManualResetEvent(false);


        public ThreadModuleView()
        {
            InitializeComponent();

            this.dgv = new DataGridView();
            this.showThreadsButton = new Button();
            this.clearTextBoxButton = new Button();
            this.clearButton = new Button();
            this.startNewTaskButton = new Button();
            this.startNewThreadButton = new Button();
            this.goButton = new Button();
            this.goAsyncButton = new Button();
            this.consoleWriteLineBox = new RichTextBox();

            dgv.Location = new Point(24, 100);
            dgv.Size = new Size(750, 300);

            DataGridViewColumn col0 = new DataGridViewColumn();
            col0.HeaderText = "Process / Thread";
            col0.Name = "Process / Thread";
            col0.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col0);

            DataGridViewColumn col1 = new DataGridViewColumn();
            col1.HeaderText = "ID";
            col1.Name = "ID";
            col1.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col1);

            DataGridViewColumn col2 = new DataGridViewColumn();
            col2.HeaderText = "Name";
            col2.Name = "Name";
            col2.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col2);

            DataGridViewColumn col3 = new DataGridViewColumn();
            col3.HeaderText = "Count / State";
            col3.Name = "Count / State";
            col3.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col3);

            DataGridViewColumn col4 = new DataGridViewColumn();
            col4.HeaderText = "Priority";
            col4.Name = "Priority";
            col4.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col4);

            DataGridViewColumn col5 = new DataGridViewColumn();
            col5.HeaderText = "Start Time";
            col5.Name = "Start Time";
            col5.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col5);

            DataGridViewColumn col6 = new DataGridViewColumn();
            col6.HeaderText = "CPU Time";
            col6.Name = "CPU Time";
            col6.CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns.Add(col6);

            showThreadsButton.Location = new Point(24, 50);
            showThreadsButton.Size = new Size(100, 24);
            showThreadsButton.Text = "Show Threads";
            showThreadsButton.Click += new EventHandler(showThreadsButton_Click);

            clearTextBoxButton.Location = new Point(150, 20);
            clearTextBoxButton.Size = new Size(100, 24);
            clearTextBoxButton.Text = "Clear TextBox";
            clearTextBoxButton.Click += new EventHandler(clearTextBoxButton_Click);

            clearButton.Location = new Point(150, 50);
            clearButton.Size = new Size(100, 24);
            clearButton.Text = "Clear Dgv";
            clearButton.Click += new EventHandler(clearButton_Click);

            startNewTaskButton.Location = new Point(250, 20);
            startNewTaskButton.Size = new Size(100, 24);
            startNewTaskButton.Text = "Start task";
            startNewTaskButton.Click += new EventHandler(startNewTaskButton_Click);

            startNewThreadButton.Location = new Point(250, 50);
            startNewThreadButton.Size = new Size(100, 24);
            startNewThreadButton.Text = "Start thread";
            startNewThreadButton.Click += new EventHandler(startNewThreadButton_Click);

            goButton.Location = new Point(370, 20);
            goButton.Size = new Size(100, 24);
            goButton.Text = "Go";
            goButton.Click += new EventHandler(goButton_Click);

            goAsyncButton.Location = new Point(370, 50);
            goAsyncButton.Size = new Size(100, 24);
            goAsyncButton.Text = "Go Async";
            goAsyncButton.Click += new EventHandler(goAsyncButton_Click);

            consoleWriteLineBox.Location = new Point(500, 10);
            consoleWriteLineBox.Size = new Size(400, 80);

            this.Controls.Add(dgv);
            this.Controls.Add(showThreadsButton);
            this.Controls.Add(clearTextBoxButton);
            this.Controls.Add(clearButton);
            this.Controls.Add(startNewThreadButton);
            this.Controls.Add(startNewTaskButton);
            this.Controls.Add(goButton);
            this.Controls.Add(goAsyncButton);
            this.Controls.Add(consoleWriteLineBox);
        }

        private void showThreadsButton_Click(object sender, EventArgs e)
        {

            var row = dgv.Rows[0];


            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName != "theBigElephant")
                    continue;
                else
                {
                    int rowIndex = dgv.Rows.Add();
                    row = dgv.Rows[rowIndex];
                    row.Cells["Process / Thread"].Value = "Process";
                    row.Cells["ID"].Value = p.Id;
                    row.Cells["Name"].Value = p.ProcessName;
                    row.Cells["Count / State"].Value = p.Threads.Count;

                    foreach (ProcessThread pt in p.Threads)
                    {
                        rowIndex = dgv.Rows.Add();
                        row = dgv.Rows[rowIndex];
                        row.Cells["Process / Thread"].Value = "Thread";
                        row.Cells["ID"].Value = pt.Id;
                        row.Cells["Name"].Value = pt.PriorityLevel;
                        row.Cells["Count / State"].Value = pt.ThreadState;
                        row.Cells["Priority"].Value = pt.PriorityLevel;
                        row.Cells["Start Time"].Value = pt.StartTime;
                        row.Cells["CPU Time"].Value = pt.TotalProcessorTime;
                    }
                }
            }
        }

        private void clearTextBoxButton_Click(object sender, EventArgs e)
        {
            consoleWriteLineBox.Clear();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
        }

        private void startNewTaskButton_Click(object sender, EventArgs e)
        {
            Task task = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Action action = () => consoleWriteLineBox.Text = "Task on progress \n";
                this.BeginInvoke(action);
            });
            Action action2 = () => consoleWriteLineBox.Text = "Task is not completed yet\n";
            this.BeginInvoke(action2);
            task.Wait();
            Action action3 = () => consoleWriteLineBox.Text += "Task is completed\n";
            this.BeginInvoke(action3);
        }

        private void startNewThreadButton_Click(object sender, EventArgs e)
        {
            new Thread(Work).Start();
            signal.Set();
        }

        void Work()
        {
            Thread.Sleep(1000);

            Action action = () => consoleWriteLineBox.Text = "Waiting for signal\n ";
            this.BeginInvoke(action);
            signal.WaitOne();
            signal.Dispose();
            action = () => consoleWriteLineBox.Text += "Got signal!\n";
            Thread.Sleep(2000);
            this.BeginInvoke(action);

        }

        private void goButton_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 5; i++)
            {
                consoleWriteLineBox.Text += nonAsync();
            }
        }

        private async void goAsyncButton_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 5; i++)
            {
                consoleWriteLineBox.Text += await withAsync();
            }
        }

        string nonAsync()
        {
            Thread.Sleep(2000);
            return "go under progress \n";
        }

        Task<string> withAsync()
        {

            Task<string> task = Task.Run(() => Go());
            return task;
        }

        string Go()
        {
            Thread.Sleep(2000);
            return "go Async under progress \n";
        }
    }
}
