using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static ONE_STOP_SUPERMARKET.CART;
using static ONE_STOP_SUPERMARKET.FAVOURITE;

namespace ONE_STOP_SUPERMARKET
{
    
    public partial class FRUIT : Form
    {
        private Panel panel_main;
        Form2 form2 = Application.OpenForms["form2"] as Form2;
        public static  List<Label> labelList = new List<Label>();
        private List<FruitClass> fruits = new List<FruitClass>();
        private List<LikeItem> likeItems = new List<LikeItem>();
        private List<string> gambarproduk = new List<string>();

        public static DataTable fruit = new DataTable();
        DataTable promo = new DataTable();
        DataTable promoprice = new DataTable();
        DataTable gambar = new DataTable();

        PictureBox[] picturebox;

        private FAVOURITE favorit;
        public MySqlCommand SqlCommand;
        public MySqlDataAdapter SqlAdapter;
        public static string ImagePath, project;
        string sqlQuery;


        Button buttonplus = new Button();
        Button buttonmin = new Button();
        public static Label[] labelharga;
        public FRUIT(Panel panel)
        {
            InitializeComponent();
            this.panel_main = panel;
        }
       
        private void FRUIT_Load(object sender, EventArgs e)
        {
            labelList.Clear();
            form2.query = "SELECT p.idproduk, p.namaproduk, p.hargaproduk, p.deskripsi, \r\n IF(pp.statuspromo = 'Aktif', IFNULL(pp.hargapromo, 0), 0) AS hargapromo,\r\n IF(pp.statuspromo = 'Aktif' AND IFNULL(pp.hargapromo, 0) <> 0, IFNULL(pp.hargapromo, 0), p.hargaproduk) AS hargacampur\r\n FROM produk p \r\n LEFT JOIN promoproduk pp ON p.idproduk = pp.idproduk;";
            form2.sqlCommand = new MySqlCommand(form2.query, Form2.sqlConnection);
            form2.sqlAdapter = new MySqlDataAdapter(form2.sqlCommand);
            form2.sqlAdapter.Fill(fruit);

            form2.query = "SELECT gambarproduk from produk;";
            form2.sqlCommand = new MySqlCommand(form2.query, Form2.sqlConnection);
            form2.sqlAdapter = new MySqlDataAdapter(form2.sqlCommand);
            form2.sqlAdapter.Fill(gambar);

            picturebox = new PictureBox[gambar.Rows.Count];

            labelharga = new Label[fruit.Rows.Count];

            for (int i = 0; i <= 5; i++)
            {
                string working = Environment.CurrentDirectory;
                project = Directory.GetParent(working).Parent.FullName;

                ImagePath = project + gambar.Rows[i][0].ToString();

                picturebox[i] = new PictureBox();
                picturebox[i].ImageLocation = ImagePath;
                picturebox[i].Size = new Size(250, 250);
                picturebox[i].SizeMode = PictureBoxSizeMode.Zoom;
                picturebox[i].Location = new Point(50, i * 300 + 80);
                this.Controls.Add(picturebox[i]);

                Label idproduk = new Label();
                Label labelfruit = new Label();

                labelfruit.Text = fruit.Rows[i]["namaproduk"].ToString();
                labelfruit.Tag = fruit.Rows[i]["idproduk"].ToString();
                labelfruit.AutoSize = true;
                labelfruit.Font = new Font("Ebrima", 15, FontStyle.Bold);
                labelfruit.Location = new Point(325, i * 300 + 80);
                this.Controls.Add(labelfruit);

                Label labelrp = new Label();
                labelrp.Text = "Rp.";
                labelrp.AutoSize = true;
                labelrp.Font = new Font("Ebrima", 12, FontStyle.Bold);
                labelrp.Location = new Point(325, i * 300 + 120);
                this.Controls.Add(labelrp);

                labelharga[i] = new Label();
                int harga = Convert.ToInt32(fruit.Rows[i]["hargacampur"].ToString());
                labelharga[i].Text = harga.ToString();
                labelharga[i].AutoSize = true;
                labelharga[i].Font = new Font("Ebrima", 12, FontStyle.Bold);
                labelharga[i].Location = new Point(355, i * 300 + 120);
                this.Controls.Add(labelharga[i]);

                for (int y = 0; y < fruit.Rows.Count; y++)
                {
                    int promosi = Convert.ToInt32(fruit.Rows[y]["hargapromo"]);

                    if (promosi != 0)
                    {
                        if (labelfruit.Text == fruit.Rows[y]["namaproduk"].ToString())
                        {
                            labelharga[i].ForeColor = Color.Red;
                            Label labelhargapromo = new Label();
                            int hargapromo = Convert.ToInt32(fruit.Rows[y]["hargaproduk"]);
                            labelhargapromo.Text = hargapromo.ToString();
                            labelhargapromo.AutoSize = true;
                            labelhargapromo.Font = new Font("Ebrima", 12, FontStyle.Bold | FontStyle.Strikeout);
                            labelhargapromo.Location = new Point(420, i * 300 + 120);
                            this.Controls.Add(labelhargapromo);
                        }
                    }
                }

                buttonmin = new Button();
                buttonmin.Text = "-";
                buttonmin.Size = new Size(25, 20);
                buttonmin.AutoSize = true;
                buttonmin.Tag = i;
                buttonmin.Location = new Point(325, i * 300 + 155);
                buttonmin.Click += Buttonmin_Click;
                this.Controls.Add(buttonmin);

                Label labelnol = new Label();
                labelnol.Text = "0";
                labelnol.AutoSize = true;
                labelnol.Location = new Point(360, i * 300 + 160);
                labelList.Add(labelnol);
                this.Controls.Add(labelnol);

                buttonplus = new Button();
                buttonplus.Text = "+";
                buttonplus.Size = new Size(25, 20);
                buttonplus.AutoSize = true;
                buttonplus.Tag = i;
                buttonplus.Location = new Point(labelnol.Right + 20, buttonmin.Top); ;
                buttonplus.Click += Buttonplus_Click;
                this.Controls.Add(buttonplus);

                Button buttonbuy = new Button();
                buttonbuy.Text = "Buy";
                buttonbuy.Size = new Size(100, 35);
                buttonbuy.Tag = "" + labelfruit.Text.ToString();
                buttonbuy.AutoSize = true;
                buttonbuy.Location = new Point(325, i * 300 + 290);
                buttonbuy.Click += Buttonbuy_Click1;
                this.Controls.Add(buttonbuy);
                labelnol.Tag = buttonbuy.Tag.ToString();

                Button buttonfav = new Button();
                buttonfav.Text = "+Favourite";
                buttonfav.Size = new Size(100, 35);
                buttonfav.AutoSize = true;
                buttonfav.Tag = i;
                buttonfav.Location = new Point(450, i * 300 + 290);
                buttonfav.Click += Buttonfav_Click;
                this.Controls.Add(buttonfav);

                Label labeldeskripsi = new Label();
                labeldeskripsi.Text = fruit.Rows[i]["deskripsi"].ToString();
                labeldeskripsi.AutoSize = true;
                labeldeskripsi.Font = new Font("Serif", 10, FontStyle.Regular);
                labeldeskripsi.Location = new Point(325, i * 300 + 200);
                labeldeskripsi.MaximumSize = new Size(350, 200);
                this.Controls.Add(labeldeskripsi);

                fruits.Add(new FruitClass()
                {
                    FruitImage = $@".\FOTO PRODUK\{i}.jpg",
                    FruitName = labelfruit.Text,
                    FruitPrice = Convert.ToDecimal(labelharga[i].Text),
                    FruitQuantity = 0,
                    FruitTotal = 0,
                    FruitID = fruit.Rows[i]["idproduk"].ToString()
                });
            }

        }

        private void Buttonfav_Click(object sender, EventArgs e)
        {
            //if (Login.isLoggedIn)
            //{
            //    Button buttonfav = (Button)sender;
            //    int index = Convert.ToInt32(buttonfav.Tag);


            //    if (index >= 0 && index < fruits.Count)
            //    {
            //        FruitClass selectedFruit = fruits[index];
            //        bool isAlreadyLiked = false;

            //        foreach (LikeItem likedItem in likeItems)
            //        {
            //            if (likedItem.Name == selectedFruit.FruitName)
            //            {
            //                isAlreadyLiked = true;
            //                break;
            //            }
            //        }

            //        if (isAlreadyLiked)
            //        {
            //            MessageBox.Show("This product is already in your favorite.", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        else
            //        {
            //            LikeItem likedItem = new LikeItem()
            //            {
            //                ID = selectedFruit.FruitID,
            //                Name = selectedFruit.FruitName,
            //                ImagePath = project + selectedFruit.FruitImage,
            //                Price = selectedFruit.FruitPrice
            //            };
            //            try
            //            {
            //                Form2.sqlConnection.Open();
            //                sqlQuery = $"INSERT INTO fav VALUES ('{Form2.pelangganID.Rows[0][0]}', '{likedItem.ID}', '0')";
            //                SqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
            //                SqlAdapter = new MySqlDataAdapter(SqlCommand);
            //                SqlCommand.ExecuteNonQuery();
            //                Form2.sqlConnection.Close();
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message.ToString());
            //            }

            //            likeItems.Add(likedItem);
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("You have to log in first", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

            if (Login.isLoggedIn)
            {
                Button fav = (Button)sender;
                string tags = fav.Tag.ToString();
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
                    FruitClass selectedFruit = null;

                    foreach (FruitClass fruit in fruits)
                    {
                        if (fruit.FruitName == tags)
                        {
                            gambarproduk.Add(tags);
                            selectedFruit = fruit;
                            break;
                        }
                    }

                    if (selectedFruit != null)
                    {
                        LikeItem likedItem = new LikeItem()
                        {
                            ID = selectedFruit.FruitID,
                            Name = selectedFruit.FruitName,
                            ImagePath = project + selectedFruit.FruitImage,
                            Price = selectedFruit.FruitPrice
                        };
                        try
                        {
                            Form2.sqlConnection.Open();

                            sqlQuery = $"SELECT * FROM fav WHERE idcustomer = '{Form2.pelangganID.Rows[0][0]}'";
                            SqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
                            SqlAdapter = new MySqlDataAdapter(SqlCommand);
                            SqlAdapter.Fill(CART.cartItemsTable);
                            bool samaID = false;
                            foreach (DataRow item in CART.cartItemsTable.Rows)
                            {
                                if (item[1].ToString() == likedItem.ID)
                                {
                                    if (item["status_del"].ToString() == "1")
                                    {
                                        sqlQuery = $"UPDATE fav SET status_del = '0' WHERE idproduk = '{likedItem.ID}' AND idcustomer = '{Form2.pelangganID.Rows[0][0]}'";
                                    }

                                    SqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
                                    SqlAdapter = new MySqlDataAdapter(SqlCommand);
                                    SqlCommand.ExecuteNonQuery();
                                    bool samaID2 = true;
                                    break;
                                }
                            }
                            if (samaID == false)
                            {
                                sqlQuery = $"INSERT INTO fav VALUES ('{Form2.pelangganID.Rows[0][0]}', '{likedItem.ID}', '0')";
                                SqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
                                SqlAdapter = new MySqlDataAdapter(SqlCommand);
                                SqlCommand.ExecuteNonQuery();
                                Form2.sqlConnection.Close();
                            }

                            Form2.sqlConnection.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to log in first", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Buttonbuy_Click1(object sender, EventArgs e)
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
                    FruitClass selectedFruit = null;

                    foreach (FruitClass fruit in fruits)
                    {
                        if (fruit.FruitName == tags)
                        {
                            gambarproduk.Add(tags);
                            selectedFruit = fruit;
                            break;
                        }
                    }

                    if (selectedFruit != null)
                    {
                        CartItem cartItem = new CartItem()
                        {
                            ID = selectedFruit.FruitID,
                            ImagePath = selectedFruit.FruitImage,
                            Name = selectedFruit.FruitName,
                            Price = selectedFruit.FruitPrice,
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
                                    int newQuantity;

                                    if (item["status_del"].ToString() == "1")
                                    {
                                        newQuantity = 1;
                                        sqlQuery = $"UPDATE cart SET quantity = '{newQuantity}', status_del = '0' WHERE idproduk = '{cartItem.ID}' AND idcustomer = '{Form2.pelangganID.Rows[0][0]}'";
                                    }
                                    else
                                    {
                                        newQuantity = existingQuantity + cartItem.Quantity;
                                        sqlQuery = $"UPDATE cart SET quantity = '{newQuantity}' WHERE idproduk = '{cartItem.ID}' AND idcustomer = '{Form2.pelangganID.Rows[0][0]}'";
                                    }

                                    SqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
                                    SqlAdapter = new MySqlDataAdapter(SqlCommand);
                                    SqlCommand.ExecuteNonQuery();
                                    samaID = true;
                                    break;
                                }


                            }
                            if (samaID == false)
                            {
                                MessageBox.Show(cartItem.Quantity.ToString());
                                sqlQuery = $"INSERT INTO cart VALUES ('{Form2.pelangganID.Rows[0][0]}', '{cartItem.ID}', '{cartItem.Quantity}', '0')";
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
                MessageBox.Show("You have to log in first", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Buttonplus_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int index = Convert.ToInt32(button.Tag);
            Label label = labelList[index];
            int value = Convert.ToInt32(labelList[index].Text);

            if (value >= 100)
            {
                //MessageBox.Show("Stok barang habis!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonplus.Enabled = false;
            }
            else
            {
                buttonplus.Enabled = true;
                value = value + 1;

                if (value <= 100)
                {
                    MessageBox.Show(value.ToString());
                    label.Text = value.ToString();
                    fruits[index].tambahQuantity();
                }
                else
                {
                    label.Text = "100";
                }
            }

            int harga = Convert.ToInt32(fruit.Rows[index]["hargacampur"]);
            labelharga[index].Text = (harga * value).ToString();

        }

        private void Buttonmin_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int index = Convert.ToInt32(button.Tag);
            Label label = labelList[index];
            int value = Convert.ToInt32(labelList[index].Text);

            if (value <= 0)
            {
                //MessageBox.Show("Buy at least 1.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonmin.Enabled = false;
            }
            else
            {
                buttonmin.Enabled = true;
                value = value - 1;
                label.Text = value.ToString();
                fruits[index].tambahQuantity();
            }

            int harga = Convert.ToInt32(fruit.Rows[index]["hargacampur"]);
            if (value == 0)
            {
                labelharga[index].Text = harga.ToString();
            }
            else
            {
                labelharga[index].Text = (harga * value).ToString();
            }
        }
        private void button_cart_Click(object sender, EventArgs e)
        {
            if (Login.isLoggedIn == false)
            {
                Signin signin = new Signin(panel_main);
                signin.Dock = DockStyle.Fill;
                signin.FormBorderStyle = FormBorderStyle.None;
                signin.TopLevel = false;
                signin.ControlBox = false;
                this.panel_main.Controls.Clear();
                this.panel_main.Controls.Add(signin);
                signin.Show();
            }
            else
            {
                
                CART cart = new CART();
                updatecart();
                cart.Dock = DockStyle.Fill;
                cart.FormBorderStyle = FormBorderStyle.None;
                cart.TopLevel = false;
                cart.ControlBox = false;
                //panel_main.AutoScroll = false;
                this.panel_main.Controls.Clear();
                this.panel_main.Controls.Add(cart);
                cart.Show();
            }
        }
        public void updatecart()
        {
            cartItemsTable.Clear();
            string keranjang = $"SELECT * FROM cart WHERE status_del = '0' AND idcustomer = '{Form2.pelangganID.Rows[0][0]}'";
            form2.sqlCommand = new MySqlCommand(keranjang, Form2.sqlConnection);
            form2.sqlAdapter = new MySqlDataAdapter(form2.sqlCommand);
            form2.sqlAdapter.Fill(cartItemsTable);
            //dataGridView1.DataSource = cartItemsTable;
        }
        public class FruitClass
        {
            public string FruitID { get; set; }
            public string FruitName { get; set; }
            public decimal FruitPrice { get; set; }
            public int FruitQuantity { get; set; }
            public decimal FruitTotal { get; set; }
            public string FruitImage { get; set; }
            public void tambahQuantity()
            {
                FruitQuantity++;
            }
        }
       /* private string FormatCurrency(decimal value)
        {
            string strValue = value.ToString("N0", new CultureInfo("id-ID"));
            return strValue;
        }*/

        private void button_like_Click(object sender, EventArgs e)
        {
            if (Login.isLoggedIn == false)
            {
                Signin signin = new Signin(panel_main);
                signin.Dock = DockStyle.Fill;
                signin.FormBorderStyle = FormBorderStyle.None;
                signin.TopLevel = false;
                signin.ControlBox = false;
                this.panel_main.Controls.Clear();
                this.panel_main.Controls.Add(signin);
                signin.Show();
            }
            else
            {
                favorit = new FAVOURITE();
                favorit.Dock = DockStyle.Fill;
                favorit.FormBorderStyle = FormBorderStyle.None;
                favorit.TopLevel = false;
                favorit.ControlBox = false;
                panel_main.AutoScroll = false;
                this.panel_main.Controls.Clear();
                this.panel_main.Controls.Add(favorit);
                favorit.Show();
            }
        }
    }
}
