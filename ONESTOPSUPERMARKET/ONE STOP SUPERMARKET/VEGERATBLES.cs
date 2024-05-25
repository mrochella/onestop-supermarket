using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static ONE_STOP_SUPERMARKET.ALCOHOL;
using static ONE_STOP_SUPERMARKET.CART;
using static ONE_STOP_SUPERMARKET.FAVOURITE;

namespace ONE_STOP_SUPERMARKET
{
   
    public partial class VEGERATBLES : Form
    {
        private Panel panel_main;
        Form2 form2 = Application.OpenForms["form2"] as Form2;
        private List<Label> labelList = new List<Label>();
        private List<VegetablesClass> vegetables = new List<VegetablesClass>();
        private List<CartItem> cartItems = new List<CartItem>();
        private List<LikeItem> likeItems = new List<LikeItem>();
        DataTable vegetable = new DataTable();
        DataTable promo = new DataTable();
        DataTable promoprice = new DataTable();
        public MySqlCommand SqlCommand;
        public MySqlDataAdapter SqlAdapter;
        string sqlQuery;
        private FAVOURITE favorit;
        public VEGERATBLES(Panel panel)
        {
            InitializeComponent();
            this.panel_main = panel;
        }
        private void VEGERATBLES_Load(object sender, EventArgs e)
        {
            form2.query = "SELECT p.idproduk, p.namaproduk, p.hargaproduk, p.deskripsi, \r\n IF(pp.statuspromo = 'Aktif', IFNULL(pp.hargapromo, 0), 0) AS hargapromo,\r\n IF(pp.statuspromo = 'Aktif' AND IFNULL(pp.hargapromo, 0) <> 0, IFNULL(pp.hargapromo, 0), p.hargaproduk) AS hargacampur\r\n FROM produk p \r\n LEFT JOIN promoproduk pp ON p.idproduk = pp.idproduk WHERE p.kategori = 'Vegetables';";
            //form2.sqlconnect = new MySqlConnection(form2.connectionstring);
            form2.sqlCommand = new MySqlCommand(form2.query, Form2.sqlConnection);
            form2.sqlAdapter = new MySqlDataAdapter(form2.sqlCommand);
            form2.sqlAdapter.Fill(vegetable);

            for (int i = 6; i <= 11; i++)
            {
                PictureBox picturebox = new PictureBox();
                picturebox.Image = Image.FromFile($@".\FOTO PRODUK\{i}.jpg");
                picturebox.Size = new Size(250, 250);
                picturebox.SizeMode = PictureBoxSizeMode.Zoom;
                picturebox.Location = new Point(50, (i - 6) * 300 + 80);
                this.Controls.Add(picturebox);

                Label labelvegetables = new Label();
                labelvegetables.Text = vegetable.Rows[i - 6]["namaproduk"].ToString();
                labelvegetables.AutoSize = true;
                labelvegetables.Font = new Font("Ebrima", 15, FontStyle.Bold);
                labelvegetables.Location = new Point(325, (i - 6) * 300 + 80);
                this.Controls.Add(labelvegetables);

                Label labelrp = new Label();
                labelrp.Text = "Rp.";
                labelrp.AutoSize = true;
                labelrp.Font = new Font("Ebrima", 12, FontStyle.Bold);
                labelrp.Location = new Point(325, (i - 6) * 300 + 120);
                this.Controls.Add(labelrp);

                Label labelharga = new Label();
                decimal harga = Convert.ToDecimal(vegetable.Rows[i - 6]["hargacampur"].ToString());
                string hargaFormatted = FormatCurrency(harga);
                labelharga.Text = hargaFormatted;
                labelharga.AutoSize = true;
                labelharga.Font = new Font("Ebrima", 12, FontStyle.Bold);
                labelharga.Location = new Point(355, (i - 6) * 300 + 120);
                this.Controls.Add(labelharga);

                for (int y = 0; y < vegetable.Rows.Count; y++)
                {
                    int promosi = Convert.ToInt32(vegetable.Rows[y]["hargapromo"]);

                    if (promosi != 0)
                    {
                        if (labelvegetables.Text == vegetable.Rows[y]["namaproduk"].ToString())
                        {
                            labelharga.ForeColor = Color.Red;
                            Label labelhargapromo = new Label();
                            decimal hargapromo = Convert.ToDecimal(vegetable.Rows[y]["hargaproduk"]);
                            hargaFormatted = FormatCurrency(hargapromo);
                            labelhargapromo.Text = hargaFormatted;
                            labelhargapromo.AutoSize = true;
                            labelhargapromo.Font = new Font("Ebrima", 12, FontStyle.Bold | FontStyle.Strikeout);
                            labelhargapromo.Location = new Point(420, (i - 6) * 300 + 120);
                            this.Controls.Add(labelhargapromo);
                        }
                    }
                }


                Button buttonmin = new Button();
                buttonmin.Text = "-";
                buttonmin.Size = new Size(25, 20);
                buttonmin.AutoSize = true;
                int indexmin = i - 6;
                buttonmin.Tag = indexmin;
                buttonmin.Location = new Point(325, (i-6) * 300 + 155);
                buttonmin.Click += Buttonmin_Click;
                this.Controls.Add(buttonmin);

                Label labelnol = new Label();
                labelnol.Text = "0";
                labelnol.AutoSize = true;
                labelnol.Location = new Point(360, (i - 6) * 300 + 160);
                labelList.Add(labelnol);
                this.Controls.Add(labelnol);

                Button buttonplus = new Button();
                buttonplus.Text = "+";
                buttonplus.Size = new Size(25, 20);
                buttonplus.AutoSize = true;
                int indexplus = i - 6;
                buttonplus.Tag = indexplus;
                buttonplus.Location = new Point(labelnol.Right + 20, buttonmin.Top);
                buttonplus.Click += Buttonplus_Click;
                this.Controls.Add(buttonplus);

                Button buttonbuy = new Button();
                buttonbuy.Text = "Buy";
                buttonbuy.Size = new Size(100, 35);
                buttonbuy.AutoSize = true;
                buttonbuy.Tag = "" + labelvegetables.Text.ToString();
                buttonbuy.Location = new Point(325, (i-6) * 300 + 290);
                buttonbuy.Click += Buttonbuy_Click;
                this.Controls.Add(buttonbuy);
                labelnol.Tag = buttonbuy.Tag.ToString();

                Button buttonfav = new Button();
                buttonfav.Text = "+Favourite";
                buttonfav.Size = new Size(100, 35);
                buttonfav.AutoSize = true;
                buttonfav.Location = new Point(450, (i - 6) * 300 + 290);
                buttonfav.Tag = i-6;
                buttonfav.Click += Buttonfav_Click;
                this.Controls.Add(buttonfav);

                Label labeldeskripsi = new Label();
                labeldeskripsi.Text = vegetable.Rows[i-6]["deskripsi"].ToString();
                labeldeskripsi.AutoSize = true;
                labeldeskripsi.Font = new Font("Serif", 10, FontStyle.Regular);
                labeldeskripsi.Location = new Point(325, (i-6) * 300 + 200);
                labeldeskripsi.MaximumSize = new Size(350, 200);
                this.Controls.Add(labeldeskripsi);

                vegetables.Add(new VegetablesClass()
                {
                    VegetablesImage = $@".\FOTO PRODUK\{i}.jpg",
                    VegetablesName = labelvegetables.Text,
                    VegetablesPrice = Convert.ToDecimal(labelharga.Text),
                    VegetablesQuantity = 0,
                    VegetablesTotal = 0,
                    VegetablesID = vegetable.Rows[i-6]["idproduk"].ToString()
                });
            }
        }

        private void Buttonfav_Click(object sender, EventArgs e)
        {
            if (Login.isLoggedIn)
            {
                Button buttonfav = (Button)sender;
                int index = Convert.ToInt32(buttonfav.Tag);

                if (index >= 0 && index < vegetables.Count)
                {
                    VegetablesClass selectedvegetable = vegetables[index];
                    bool isAlreadyLiked = false;

                    foreach (LikeItem likedItem in likeItems)
                    {
                        if (likedItem.Name == selectedvegetable.VegetablesName)
                        {
                            isAlreadyLiked = true;
                            break;
                        }
                    }

                    if (isAlreadyLiked)
                    {
                        MessageBox.Show("This product is already in your favorite.", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        LikeItem likedItem = new LikeItem()
                        {
                            Name = selectedvegetable.VegetablesName,
                            ImagePath = selectedvegetable.VegetablesImage,
                            Price = selectedvegetable.VegetablesPrice
                        };

                        likeItems.Add(likedItem);
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to log in first", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Buttonbuy_Click(object sender, EventArgs e)
        {
            if (Login.isLoggedIn)
            {
                Button buy = (Button)sender;
                string tags = buy.Tag.ToString();
                int qty = 0;
                foreach (Label labelnol in labelList)
                {
                    qty += Convert.ToInt32(labelnol.Text);
                    if (labelnol.Tag.ToString() == tags)
                    {
                        qty = Convert.ToInt32(labelnol.Text);
                        labelnol.Text = "0";
                    }
                }
                if (qty > 0)
                {
                    VegetablesClass selectedvegetables = null;

                    foreach (VegetablesClass vegetables in vegetables)
                    {
                        if (vegetables.VegetablesName == tags)
                        {
                            selectedvegetables = vegetables;
                            break;
                        }
                    }

                    if (selectedvegetables != null)
                    {
                        CartItem cartItem = new CartItem()
                        {
                            ID = selectedvegetables.VegetablesID,
                            ImagePath = selectedvegetables.VegetablesImage,
                            Name = selectedvegetables.VegetablesName,
                            Price = selectedvegetables.VegetablesPrice,
                            Quantity = qty
                        };
                        try
                        {
                            CART.cartItemsTable.Clear();
                            Form2.sqlConnection.Open();
                            sqlQuery = $"SELECT * FROM cart WHERE idcustomer = '{Form2.pelangganID.Rows[0][0]}'";
                            SqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
                            SqlAdapter = new MySqlDataAdapter(SqlCommand);
                            SqlAdapter.Fill(CART.cartItemsTable);
                            bool samaID = false;
                            foreach (DataRow item in CART.cartItemsTable.Rows)
                            {

                                if (item[1].ToString() == cartItem.ID)
                                {
                                    int existingQuantity = Convert.ToInt32(item["quantity"]);
                                    int newQuantity = existingQuantity + cartItem.Quantity;

                                    sqlQuery = $"UPDATE cart SET quantity = '{newQuantity}' WHERE idproduk = '{cartItem.ID}'";
                                    SqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
                                    SqlAdapter = new MySqlDataAdapter(SqlCommand);
                                    SqlCommand.ExecuteNonQuery();
                                    samaID = true;
                                    break;

                                }

                            }
                            if (samaID == false)
                            {

                                sqlQuery = $"INSERT INTO cart VALUES ('{Form2.pelangganID.Rows[0][0]}', '{cartItem.ID}', '{cartItem.Quantity}')";
                                SqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
                                SqlAdapter = new MySqlDataAdapter(SqlCommand);
                                SqlCommand.ExecuteNonQuery();


                            }


                            Form2.sqlConnection.Close();
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message.ToString());
                        }

                    }

                }
                else
                {
                    MessageBox.Show("Buy at least 1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You have to log in first.", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Buttonplus_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int index = (int)button.Tag;

            if (index >= 0 && index < labelList.Count)
            {
                Label label = labelList[index];
                int value = Convert.ToInt32(label.Text);
                value++;
                label.Text = value.ToString();
                vegetables[index].tambahQuantity();
            }

        }
        private void Buttonmin_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int index = (int)button.Tag;

            if (index >= 0 && index < labelList.Count)
            {
                Label label = labelList[index];
                int value = Convert.ToInt32(label.Text);

                if (value > 0)
                {
                    value--;
                    label.Text = value.ToString();
                    vegetables[index].tambahQuantity();
                }
            }
        }
        internal class VegetablesClass
        {
            public string VegetablesName { get; set; }
            public decimal VegetablesPrice { get; set; }
            public int VegetablesQuantity { get; set; }
            public decimal VegetablesTotal { get; set; }
            public string VegetablesImage { get; set; }
            public string VegetablesID { get; set; }
            public void tambahQuantity()
            {
                VegetablesQuantity++;
            }
        }
        private string FormatCurrency(decimal value)
        {
            string strValue = value.ToString("N0", new CultureInfo("id-ID"));
            return strValue;
        }

        private void button_cart_Click(object sender, EventArgs e)
        {
            CART cart = new CART();
            cart.Dock = DockStyle.Fill;
            cart.FormBorderStyle = FormBorderStyle.None;
            cart.TopLevel = false;
            cart.ControlBox = false;
            panel_main.AutoScroll = false;
            this.panel_main.Controls.Clear();
            this.panel_main.Controls.Add(cart);
            cart.Show();
        }

        private void button_like_Click(object sender, EventArgs e)
        {
            favorit = new FAVOURITE(panel_main, likeItems);
            favorit.Dock = DockStyle.Fill;
            favorit.FormBorderStyle = FormBorderStyle.None;
            favorit.TopLevel = false;
            favorit.ControlBox = false;
            panel_main.AutoScroll = false;
            this.panel_main.Controls.Clear();
            this.panel_main.Controls.Add(favorit);

            favorit.Show();
            favorit.AddItemToFav(likeItems);
        }
    }
}
