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
using static ONE_STOP_SUPERMARKET.FAVOURITE;
using static ONE_STOP_SUPERMARKET.CART;
using System.Xml;

namespace ONE_STOP_SUPERMARKET
{
    public partial class FAVOURITE : Form
    {
        public MySqlCommand sqlCommand;
        public MySqlDataAdapter sqlAdapter;
        public string sqlQuery;
        public MySqlDataReader sqlReader;

        Panel emptyCartPanel = new Panel();
        public static DataTable favID = new DataTable();

        public static DataTable favItemsTable = new DataTable();
        public FAVOURITE()
        {
            InitializeComponent();
        }
   
        private void Buttondelete_Click(object sender, EventArgs e)
        {
            Button buttondelete = (Button)sender;
            Panel itemPanel = (Panel)buttondelete.Parent;

            Label labelName = (Label)itemPanel.Controls[1];
            string productName = labelName.Text;

            string updateDel = $"UPDATE fav SET status_del = '1' WHERE idproduk = '{labelName.Tag.ToString()}'";

            DataRow[] rowsToDelete = favItemsTable.Select($"namaproduk = '{productName}'");
            foreach (DataRow row in rowsToDelete)
            {
                favItemsTable.Rows.Remove(row);
            }

            panel_like.Controls.Remove(itemPanel);

            try
            {
                Form2.sqlConnection.Open();
                sqlCommand = new MySqlCommand(updateDel, Form2.sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
            finally
            {
                Form2.sqlConnection.Close();
            }

            x.Clear();
            sqlQuery = $"SELECT * FROM fav WHERE status_del = '0' AND idcustomer = '{Form2.pelangganID.Rows[0][0]}'";
            sqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(x);

            if (x.Rows.Count == 0)
            {
                this.BackColor = Color.White;

                emptyCartPanel.AutoSize = true;
                emptyCartPanel.Margin = new Padding(0);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = Image.FromFile(@".\fav_kosong.png");
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Size = new Size(250, 250);
                pictureBox.Location = new Point(280, 30);
                emptyCartPanel.Controls.Add(pictureBox);

                Label labelNamee = new Label();
                labelNamee.Text = "No favorites yet! Try clicking the favorite button to add.";
                labelNamee.ForeColor = Color.Orange;
                labelNamee.Font = new Font("Ebrima", 12, FontStyle.Bold);
                labelNamee.AutoSize = true;
                labelNamee.Location = new Point(200, 290);
                emptyCartPanel.Controls.Add(labelNamee);

                emptyCartPanel.Location = new Point((panel_like.Width - emptyCartPanel.Width) / 2, (panel_like.Height - emptyCartPanel.Height) / 2);
                panel_like.Controls.Add(emptyCartPanel);
            }
            else
            {
                panel_like.Controls.Add(itemPanel);
                items();
            }
        }
        private void updatefav()
        {
            favItemsTable.Clear();

            string keranjang = $"SELECT * FROM fav";
            sqlCommand = new MySqlCommand(keranjang, Form2.sqlConnection);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(favItemsTable);
        }

        private void FAVOURITE_Load(object sender, EventArgs e)
        {
            favItemsTable.Clear();
            sqlQuery = $"SELECT p.idproduk, p.namaproduk, p.hargaproduk, p.gambarproduk FROM produk p, fav f WHERE f.idproduk = p.idproduk AND status_del = '0' AND f.idcustomer = '{Form2.pelangganID.Rows[0][0]}';";
            sqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(favItemsTable);

            if (favItemsTable.Rows.Count == 0)
            {
                this.BackColor = Color.White;

                emptyCartPanel.AutoSize = true;
                emptyCartPanel.Margin = new Padding(0);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = Image.FromFile(@".\fav_kosong.png");
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Size = new Size(250, 250);
                pictureBox.Location = new Point(280, 30);
                emptyCartPanel.Controls.Add(pictureBox);

                Label labelNamee = new Label();
                labelNamee.Text = "No favorites yet! Try clicking the favorite button to add.";
                labelNamee.ForeColor = Color.Orange;
                labelNamee.Font = new Font("Ebrima", 12, FontStyle.Bold);
                labelNamee.AutoSize = true;
                labelNamee.Location = new Point(200, 290);
                emptyCartPanel.Controls.Add(labelNamee);

                emptyCartPanel.Location = new Point((panel_like.Width - emptyCartPanel.Width) / 2, (panel_like.Height - emptyCartPanel.Height) / 2);
                panel_like.Controls.Add(emptyCartPanel);
            }
            else
            {
                items();
            }
        }

        private void items()
        {
            this.panel_like.Controls.Clear();

            int itemOffsetX = 30;
            int itemOffsetY = 30;
            int itemSpacing = 50;

            MessageBox.Show(favItemsTable.Rows.Count.ToString());
            foreach (DataRow item in favItemsTable.Rows)
            {
                Panel itemPanel = new Panel();
                itemPanel.AutoSize = true;
                itemPanel.Margin = new Padding(0);
                panel_like.Controls.Add(itemPanel);

                PictureBox pictureBox = new PictureBox();
                pictureBox.ImageLocation = FRUIT.project + item["gambarproduk"].ToString();
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Size = new Size(250, 250);
                pictureBox.Location = new Point(20, 20);
                itemPanel.Controls.Add(pictureBox);

                Label labelName = new Label();
                labelName.Text = item["namaproduk"].ToString();
                labelName.Tag = item["idproduk"].ToString();
                labelName.Font = new Font("Ebrima", 14, FontStyle.Bold);
                labelName.AutoSize = false;
                labelName.Size = new Size(pictureBox.Width, 30);
                labelName.TextAlign = ContentAlignment.MiddleCenter;
                labelName.Location = new Point(pictureBox.Left, pictureBox.Bottom + 20);
                itemPanel.Controls.Add(labelName);

                Label labelPrice = new Label();
                labelPrice.Text = "Rp. " + item["hargaproduk"].ToString();
                labelPrice.Font = new Font("Ebrima", 14);
                labelPrice.AutoSize = false;
                labelPrice.Size = labelName.Size;
                labelPrice.TextAlign = ContentAlignment.MiddleCenter;
                labelPrice.Location = new Point(labelName.Left, labelName.Bottom + 15);
                itemPanel.Controls.Add(labelPrice);

                Button buttondelete = new Button();
                buttondelete.Text = "Delete";
                buttondelete.Size = new Size(70, 40);
                buttondelete.AutoSize = false;
                buttondelete.Size = new Size(labelPrice.Width, 30);
                buttondelete.TextAlign = ContentAlignment.MiddleCenter;
                buttondelete.Location = new Point(labelPrice.Left, labelPrice.Bottom + 10);
                buttondelete.Click += Buttondelete_Click;
                itemPanel.Controls.Add(buttondelete);

                itemPanel.Location = new Point(itemOffsetX, itemOffsetY);

                itemOffsetX += itemPanel.Width + itemSpacing;

                if (itemOffsetX + itemPanel.Width > panel_like.Width)
                {
                    itemOffsetX = 30;
                    itemOffsetY += itemPanel.Height + itemSpacing;
                }
            }
            panel_like.AutoScroll = true;
            panel_like.HorizontalScroll.Enabled = false;
            panel_like.HorizontalScroll.Visible = false;
            panel_like.HorizontalScroll.Maximum = 0;
            panel_like.AutoScrollMargin = new Size(0, 20);
            panel_like.VerticalScroll.SmallChange = 20;
            panel_like.VerticalScroll.LargeChange = 20;
        }
        public class LikeItem
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string ImagePath { get; set; }
        }

        private void panel_like_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    
}

