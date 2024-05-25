using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ONE_STOP_SUPERMARKET
{
    public partial class Form2 : Form
    {
        public static string connectionstring = "server=localhost;uid=root;pwd=Meganmegan2009;database=db_supermarket";
        public static MySqlConnection sqlConnection = new MySqlConnection(connectionstring);
        public MySqlConnection sqlconnect;
        public MySqlCommand sqlCommand;
        public MySqlDataAdapter sqlAdapter;
        public string query;
        private Timer timer;
        private int currentImageIndex;
        private List<string> imagePaths;
        private bool isOpenFromOtherForm = false;

        public static string email = Login.enteredEmail;
        public static string password = Login.enteredPassword;

        public static DataTable pelangganID = new DataTable();
        public static DataTable custName = new DataTable();

        public Form2()
        {
            InitializeComponent();

        }
        public class CustomColorTable : ProfessionalColorTable
        {
            public override Color MenuItemBorder
            {
                get { return Color.White; }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.FromArgb(255, 158, 1); }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.FromArgb(255, 158, 1); }
            }
            public override Color MenuItemSelected
            {
                get { return Color.FromArgb(255, 158, 1); }
            }
            public override Color MenuBorder
            {
                get { return Color.White; }
            }

            public override Color MenuItemPressedGradientBegin
            {
                get { return Color.FromArgb(255, 158, 1); }
            }

            public override Color MenuItemPressedGradientEnd
            {
                get { return Color.FromArgb(255, 158, 1); }
            }

            public override Color MenuItemPressedGradientMiddle
            {
                get { return Color.FromArgb(255, 158, 1); }
            }
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            isOpenFromOtherForm = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowNextImage();
        }
        private void Form2_Load_1(object sender, EventArgs e)
        {
            imagePaths = new List<string>
            {
                @".\promo_oranges.jpg",
                @".\promo_jackdaniels.jpg",
                @".\promo_sale.jpg",
            };

            timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += timer1_Tick;

            if (!isOpenFromOtherForm)
            {
                timer.Enabled = true;
                timer.Start();
            }

            ShowNextImage();

            menuStrip1.BackColor = Color.Orange;
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());
        }
        private void ShowNextImage()
        {
            panel_main.Controls.Clear();

            //gambar berikutnya
            PictureBox pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Size = new Size(799, 348);
            pictureBox.Location = new Point(0, 0);
            pictureBox.Image = Image.FromFile(imagePaths[currentImageIndex]);
            pictureBox.Dock = DockStyle.Fill;
            panel_main.Controls.Add(pictureBox);

            //nambah index untuk gambar berikutnya
            currentImageIndex++;
            if (currentImageIndex >= imagePaths.Count)
            {
                currentImageIndex = 0; //kembali ke gambar pertama kalau mencapai akhir list
            }
        }
        private void pictureBox_logo_Click(object sender, EventArgs e)
        {
            panel_main.Controls.Clear();
        }
        private void FruitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Ketika membuka form dari tempat lain
            isOpenFromOtherForm = true;
            timer.Stop();

            FRUIT fruit = new FRUIT(panel_main);
            fruit.Dock = DockStyle.Fill;
            fruit.FormBorderStyle = FormBorderStyle.None;
            fruit.TopLevel = false;
            fruit.ControlBox = false;
            panel_main.AutoScroll = false;
            this.panel_main.Controls.Clear();
            this.panel_main.Controls.Add(fruit);
            fruit.Show();
        }

        private void VegetablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isOpenFromOtherForm = true;
            timer.Stop();

            VEGERATBLES vegetables = new VEGERATBLES(panel_main);
            vegetables.Dock = DockStyle.Fill;
            vegetables.FormBorderStyle = FormBorderStyle.None;
            vegetables.TopLevel = false;
            vegetables.ControlBox = false;
            panel_main.AutoScroll= false;
            this.panel_main.Controls.Clear();
            this.panel_main.Controls.Add(vegetables);
            vegetables.Show();
        }

        private void dairyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isOpenFromOtherForm = true;
            timer.Stop();

            DAIRY dairy = new DAIRY(panel_main);
            dairy.Dock = DockStyle.Fill;
            dairy.FormBorderStyle = FormBorderStyle.None;
            dairy.TopLevel = false;
            dairy.ControlBox = false;
            panel_main.AutoScroll = false;
            this.panel_main.Controls.Clear();
            this.panel_main.Controls.Add(dairy);
            dairy.Show();
        }

        private void sodaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isOpenFromOtherForm = true;
            timer.Stop();

            SODA soda = new SODA(panel_main);
            soda.Dock = DockStyle.Fill;
            soda.FormBorderStyle = FormBorderStyle.None;
            soda.TopLevel = false;
            soda.ControlBox = false;
            panel_main.AutoScroll = false;
            this.panel_main.Controls.Clear();
            this.panel_main.Controls.Add(soda);
            soda.Show();
        }

        private void alcoholToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isOpenFromOtherForm = true;
            timer.Stop();

            this.panel_main.Controls.Clear();
            ALCOHOL alkohol = new ALCOHOL(panel_main);
            alkohol.Dock = DockStyle.Fill;
            alkohol.FormBorderStyle = FormBorderStyle.None;
            alkohol.TopLevel = false;
            alkohol.ControlBox = false;
            panel_main.AutoScroll = false;
            this.panel_main.Controls.Add(alkohol);
            alkohol.Show();
        }

        private void meatAndFishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isOpenFromOtherForm = true;
            timer.Stop();

            MEAT_AND_FISH meatfish = new MEAT_AND_FISH(panel_main);
            meatfish.Dock = DockStyle.Fill;
            meatfish.FormBorderStyle = FormBorderStyle.None;
            meatfish.TopLevel = false;
            meatfish.ControlBox = false;
            panel_main.AutoScroll = false;
            this.panel_main.Controls.Clear();
            this.panel_main.Controls.Add(meatfish);
            meatfish.Show();
        }

        private void snacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isOpenFromOtherForm = true;
            timer.Stop();

            SNACK snack = new SNACK(panel_main);
            snack.Dock = DockStyle.Fill;
            snack.FormBorderStyle = FormBorderStyle.None;
            snack.TopLevel = false;
            snack.ControlBox = false;
            panel_main.AutoScroll = false;
            this.panel_main.Controls.Clear();
            this.panel_main.Controls.Add(snack);
            snack.Show();
        }

        private void cleanersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isOpenFromOtherForm = true;
            timer.Stop();

            CLEANERS cleaners = new CLEANERS(panel_main);
            cleaners.Dock = DockStyle.Fill;
            cleaners.FormBorderStyle = FormBorderStyle.None;
            cleaners.TopLevel = false;
            cleaners.ControlBox = false;
            panel_main.AutoScroll = false;
            this.panel_main.Controls.Clear();
            this.panel_main.Controls.Add(cleaners);
            cleaners.Show();
        }

        private void personalCareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isOpenFromOtherForm = true;
            timer.Stop();

            PERSONALCARE personalcare = new PERSONALCARE(panel_main);
            personalcare.Dock = DockStyle.Fill;
            personalcare.FormBorderStyle = FormBorderStyle.None;
            personalcare.TopLevel = false;
            personalcare.ControlBox = false;
            panel_main.AutoScroll = false;
            this.panel_main.Controls.Clear();
            this.panel_main.Controls.Add(personalcare);
            personalcare.Show();
        }

        private void paperGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isOpenFromOtherForm = true;
            timer.Stop();

            PAPERGOODS papergoods = new PAPERGOODS(panel_main);
            papergoods.Dock = DockStyle.Fill;
            papergoods.FormBorderStyle = FormBorderStyle.None;
            papergoods.TopLevel = false;
            papergoods.ControlBox = false;
            panel_main.AutoScroll = false;
            this.panel_main.Controls.Clear();
            this.panel_main.Controls.Add(papergoods);
            papergoods.Show();
        }
     
        private void signInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isOpenFromOtherForm = true;
            timer.Stop();

            Signin signin = new Signin(panel_main);
            signin.Dock = DockStyle.Fill;
            signin.FormBorderStyle = FormBorderStyle.None;
            signin.TopLevel = false;
            signin.ControlBox = false;
            this.panel_main.Controls.Clear();
            this.panel_main.Controls.Add(signin);
            signin.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login.isLoggedIn)
            {
                HISTORY history = new HISTORY(panel_main);
                history.Dock = DockStyle.Fill;
                history.FormBorderStyle = FormBorderStyle.None;
                history.TopLevel = false;
                history.ControlBox = false;
                this.panel_main.Controls.Clear();
                this.panel_main.Controls.Add(history);
                history.Show();
            }
            else
            {
                MessageBox.Show("No history yet.", "Sign in now!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login.isLoggedIn)
            {
                MessageBox.Show("You are about to log out.", "Good bye!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CART.x.Clear(); //sampai sini
                Login.isLoggedIn = false;
            }
            else
            {
                MessageBox.Show("No account logged in.", "Sorry!",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void panel_main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void beverageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
