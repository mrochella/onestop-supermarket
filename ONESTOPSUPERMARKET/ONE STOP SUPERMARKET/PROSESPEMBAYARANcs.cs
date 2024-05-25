using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONE_STOP_SUPERMARKET
{
    public partial class PROSESPEMBAYARAN : Form
    {
        public PROSESPEMBAYARAN()
        {
            InitializeComponent();
        }

        private void PROSESPEMBAYARAN_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;

            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = Image.FromFile(@".\order_complete.png");
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Size = new Size(250, 250);
            pictureBox.Location = new Point(280, 30);
            this.Controls.Add(pictureBox);

            Label labelName = new Label();
            labelName.Text = "Thank you! Your order is complete.";
            labelName.ForeColor = Color.Orange;
            labelName.Font = new Font("Ebrima", 12, FontStyle.Bold);
            labelName.AutoSize = true;
            labelName.Location = new Point(250, 300);
            this.Controls.Add(labelName);
        }
    }
}
