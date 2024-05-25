using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONE_STOP_SUPERMARKET
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer_progressbar.Start();
        }

        private void timer_progressbar_Tick(object sender, EventArgs e)
        {
            progressBar1.Value++;

            if (progressBar1.Value == progressBar1.Maximum)
            {
                timer_progressbar.Stop();
                progressBar1.Visible = false;

                Form2 form2 = new Form2();
                form2.Dock = DockStyle.Fill;
                form2.FormBorderStyle = FormBorderStyle.None;
                form2.TopLevel = false;
                form2.ControlBox = false;

                this.panel1.Controls.Clear();
                this.panel1.Controls.Add(form2);
                form2.Show();
            }
        }
    }
}
