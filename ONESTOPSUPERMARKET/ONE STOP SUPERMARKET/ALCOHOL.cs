using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static ONE_STOP_SUPERMARKET.CART;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static ONE_STOP_SUPERMARKET.FAVOURITE;
using System.Data.SqlClient;
using System.IO;

namespace ONE_STOP_SUPERMARKET
{
  
    public partial class ALCOHOL : Form
    {
        private Panel panel_main;
        Form2 form2 = Application.OpenForms["form2"] as Form2;
        public static List<Label> labelList = new List<Label>();
        private List<AlcoholClass> alcohol = new List<AlcoholClass>();
        private List<LikeItem> likeItems = new List<LikeItem>();

        DataTable alcohols = new DataTable();
        DataTable promo = new DataTable();
        DataTable promoprice = new DataTable();

        private FAVOURITE favorit;
        public MySqlCommand SqlCommand;
        public MySqlDataAdapter SqlAdapter;
        string sqlQuery;

        Button buttonmin = new Button();
        Button buttonplus = new Button();
        DataTable gambar = new DataTable(); //INI SEMUA
        PictureBox[] picturebox;

        public static string ImagePath, project;
        public static Label[] labelharga; //YANG PUBLIC STATIC NANTI TINGGAL TAMBAH FRUIT. DEPANNYA

        public ALCOHOL(Panel panel)
        {
            InitializeComponent();
            this.panel_main = panel;
        }

        private void ALCOHOL_Load(object sender, EventArgs e)
        {
            //DI QUERY WHERE KATEGORI LIKE BISA DIHAPUS AJA
            labelList.Clear(); //TAMBAH INI LABELLIST CLEAR
            form2.query = "SELECT p.idproduk, p.namaproduk, p.hargaproduk, p.deskripsi, IF(pp.statuspromo = 'Aktif', IFNULL(pp.hargapromo, 0), 0) AS hargapromo, IF(pp.statuspromo = 'Aktif' AND IFNULL(pp.hargapromo, 0) <> 0, IFNULL(pp.hargapromo, 0), p.hargaproduk) AS hargacampur FROM produk p LEFT JOIN promoproduk pp ON p.idproduk = pp.idproduk;";
            form2.sqlCommand = new MySqlCommand(form2.query, Form2.sqlConnection);
            form2.sqlAdapter = new MySqlDataAdapter(form2.sqlCommand);
            form2.sqlAdapter.Fill(alcohols);

            //TAMBAH INI
            form2.query = "SELECT gambarproduk from produk;";
            form2.sqlCommand = new MySqlCommand(form2.query, Form2.sqlConnection);
            form2.sqlAdapter = new MySqlDataAdapter(form2.sqlCommand);
            form2.sqlAdapter.Fill(gambar);
            picturebox = new PictureBox[gambar.Rows.Count];
            labelharga = new Label[alcohols.Rows.Count]; //GANTI KE DATATABLE MASING2

            for (int i = 25; i <= 29; i++)
            {
                string working = Environment.CurrentDirectory; //TAMBAH INI SAMPE ADD PICTUREBOX[i]
                FRUIT.project = Directory.GetParent(working).Parent.FullName;

                FRUIT.ImagePath = FRUIT.project + gambar.Rows[i][0].ToString();

                picturebox[i] = new PictureBox();
                picturebox[i].ImageLocation = FRUIT.ImagePath;
                picturebox[i].Size = new Size(250, 250);
                picturebox[i].SizeMode = PictureBoxSizeMode.Zoom;
                picturebox[i].Location = new Point(50, (i - 25) * 300 + 80);
                this.Controls.Add(picturebox[i]);

                Label idproduk = new Label();
                Label labelalcohol = new Label();

                labelalcohol.Text = alcohols.Rows[i]["namaproduk"].ToString(); // ROWS NYA [i] AJA GA PERLU DI KURANG2 LAGI
                labelalcohol.Tag = alcohols.Rows[i]["idproduk"].ToString();
                labelalcohol.AutoSize = true;
                labelalcohol.Font = new Font("Ebrima", 15, FontStyle.Bold);
                labelalcohol.Location = new Point(325, (i - 25) * 300 + 80);
                this.Controls.Add(labelalcohol);

                Label labelrp = new Label();
                labelrp.Text = "Rp.";
                labelrp.AutoSize = true;
                labelrp.Font = new Font("Ebrima", 12, FontStyle.Bold);
                labelrp.Location = new Point(325, (i - 25) * 300 + 120);
                this.Controls.Add(labelrp);

                labelharga[i] = new Label(); //INI DIUBAH JADI ADA [i]
                int harga = Convert.ToInt32(alcohols.Rows[i]["hargacampur"].ToString());
                labelharga[i].Text = harga.ToString();
                labelharga[i].AutoSize = true;
                labelharga[i].Font = new Font("Ebrima", 12, FontStyle.Bold);
                labelharga[i].Location = new Point(355, (i -  25) * 300 + 120);
                this.Controls.Add(labelharga[i]);

                for (int y = 0; y < alcohols.Rows.Count; y++)
                {
                    int promosi = Convert.ToInt32(alcohols.Rows[y]["hargapromo"]);

                    if (promosi != 0)
                    {
                        if (labelalcohol.Text == alcohols.Rows[y]["namaproduk"].ToString())
                        {
                            labelharga[i].ForeColor = Color.Red;
                            Label labelhargapromo = new Label();
                            int hargapromo = Convert.ToInt32(alcohols.Rows[y]["hargaproduk"]);
                            labelhargapromo.Text = hargapromo.ToString();
                            labelhargapromo.AutoSize = true;
                            labelhargapromo.Font = new Font("Ebrima", 12, FontStyle.Bold | FontStyle.Strikeout);
                            labelhargapromo.Location = new Point(420, (i - 25) * 300 + 120);
                            this.Controls.Add(labelhargapromo);
                        }
                    }
                }

                buttonmin = new Button();
                buttonmin.Text = "-";
                buttonmin.Size = new Size(25, 20);
                buttonmin.AutoSize = true;
                buttonmin.Tag = i;
                buttonmin.Location = new Point(325, (i - 25) * 300 + 155);
                buttonmin.Click += Buttonmin_Click;
                this.Controls.Add(buttonmin);

                Label labelnol = new Label();
                labelnol.Text = "0";
                labelnol.AutoSize = true;
                labelnol.Location = new Point(360, (i - 25) * 300 + 160);
                labelList.Add(labelnol);
                this.Controls.Add(labelnol);

                buttonplus = new Button();
                buttonplus.Text = "+";
                buttonplus.Size = new Size(25, 20);
                buttonplus.AutoSize = true;
                buttonplus.Tag = i;
                buttonplus.Location = new Point(labelnol.Right + 20, buttonmin.Top);
                buttonplus.Click += Buttonplus_Click;
                this.Controls.Add(buttonplus);

                Button buttonbuy = new Button();
                buttonbuy.Text = "Buy";
                buttonbuy.Tag = ""+labelalcohol.Text.ToString();
                buttonbuy.Size = new Size(100, 35);
                buttonbuy.AutoSize = true;
                buttonbuy.Location = new Point(325, (i - 25) * 300 + 290);
                buttonbuy.Click += Buttonbuy_Click;
                this.Controls.Add(buttonbuy);
                labelnol.Tag = buttonbuy.Tag.ToString();

                Button buttonfav = new Button();
                buttonfav.Text = "+Favourite";
                buttonfav.Size = new Size(100, 35);
                buttonfav.AutoSize = true;
                buttonfav.Location = new Point(450, (i - 25) * 300 + 290);
                buttonfav.Tag = i-25;
                buttonfav.Click += Buttonfav_Click;
                this.Controls.Add(buttonfav);

                Label labeldeskripsi = new Label();
                labeldeskripsi.Text = alcohols.Rows[i]["deskripsi"].ToString();
                labeldeskripsi.AutoSize = true;
                labeldeskripsi.Font = new Font("Serif", 10, FontStyle.Regular);
                labeldeskripsi.Location = new Point(325, (i-25) * 300 + 200);
                labeldeskripsi.MaximumSize = new Size(350, 200);
                this.Controls.Add(labeldeskripsi);

                alcohol.Add(new AlcoholClass()
                {
                    AlcoholImage = $@".\FOTO PRODUK\{i}.jpg",
                    AlcoholName = labelalcohol.Text,
                    AlcoholPrice = Convert.ToDecimal(labelharga[i].Text),
                    AlcoholQuantity = 0,
                    AlcoholTotal = 0,
                    AlcoholID = alcohols.Rows[i]["idproduk"].ToString()
                });
            }
        }

        private void Buttonfav_Click(object sender, EventArgs e)
        {
            if (Login.isLoggedIn)
            {
                Button buttonfav = (Button)sender;
                int index = Convert.ToInt32(buttonfav.Tag);

                try
                {
                    sqlQuery = $"INSERT INTO fav VALUES ('E001', {Form2.pelangganID.Rows[0][0]})"; //JANGAN LUPA IDPRODUKNYA GANTI
                    SqlCommand = new MySqlCommand(sqlQuery, form2.sqlconnect);
                }
                catch
                {

                    MessageBox.Show(e.ToString());
                }
          
                if (index >= 0 && index < alcohol.Count)
                {
                    AlcoholClass selectedalcohol = alcohol[index];
                    bool isAlreadyLiked = false;

                    foreach (LikeItem likedItem in likeItems)
                    {
                        if (likedItem.Name == selectedalcohol.AlcoholName)
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
                            ID = selectedalcohol.AlcoholID,
                            Name = selectedalcohol.AlcoholName,
                            ImagePath = project + selectedalcohol.AlcoholImage,
                            Price = selectedalcohol.AlcoholPrice
                        };
                        try
                        {
                            Form2.sqlConnection.Open();

                            sqlQuery = $"INSERT INTO fav VALUES ('{Form2.pelangganID.Rows[0][0]}', '{likedItem.ID}', '0')";
                            SqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
                            SqlAdapter = new MySqlDataAdapter(SqlCommand);
                            SqlCommand.ExecuteNonQuery();

                            sqlQuery = $"SELECT * FROM fav WHERE status_del = '0' AND idcustomer = '{Form2.pelangganID.Rows[0][0]}'";
                            SqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
                            SqlAdapter = new MySqlDataAdapter(SqlCommand);
                            SqlAdapter.Fill(favItemsTable);

                            foreach (DataRow item in favItemsTable.Rows)
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
                                    break;
                                }
                            }

                            Form2.sqlConnection.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }

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
                    AlcoholClass selectedalcohol = null;

                    foreach (AlcoholClass alcohol in alcohol)
                    {
                        if (alcohol.AlcoholName == tags)
                        {
                            selectedalcohol = alcohol;
                            break;
                        }
                    }

                    if (selectedalcohol != null)
                    {
                        CartItem cartItem = new CartItem()
                        {
                            ID = selectedalcohol.AlcoholID,
                            ImagePath = selectedalcohol.AlcoholImage,
                            Name = selectedalcohol.AlcoholName,
                            Price = selectedalcohol.AlcoholPrice,
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

                                    sqlQuery = $"UPDATE cart SET quantity = '{newQuantity}' WHERE idproduk = '{cartItem.ID}' AND idcustomer = '{Form2.pelangganID.Rows[0][0]}'";
                                    SqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
                                    SqlAdapter = new MySqlDataAdapter(SqlCommand);
                                    SqlCommand.ExecuteNonQuery();
                                    samaID = true;
                                    break;

                                }
                          
                            }
                            if (samaID==false)
                            {

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
                    else
                    {
                        MessageBox.Show("Buy at least 1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
            int index = Convert.ToInt32(button.Tag);
            Label label = labelList[index-25];
            int value = Convert.ToInt32(labelList[index-25].Text);

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
                    alcohol[index - 25].tambahQuantity();
                }
                else
                {
                    label.Text = "100";
                }
            }

            int harga = Convert.ToInt32(alcohols.Rows[index]["hargacampur"]);
            labelharga[index].Text = (harga * value).ToString();
        }
        private void Buttonmin_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int index = Convert.ToInt32(button.Tag);
            Label label = labelList[index - 25];
            int value = Convert.ToInt32(labelList[index - 25].Text);

            if (value <= 0)
            {
                //MessageBox.Show("Buy at least 1.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonmin.Enabled = false;
            }
            else
            {
                buttonmin.Enabled = true;
                MessageBox.Show(value.ToString());
                value = value - 1;
                label.Text = value.ToString();
                alcohol[index - 25].tambahQuantity();
            }

            int harga = Convert.ToInt32(alcohols.Rows[index]["hargacampur"]);
            if (value == 0)
            {
                labelharga[index].Text = harga.ToString();
            }
            else
            {
                labelharga[index].Text = (harga * value).ToString();
            }
        }
        internal class AlcoholClass
        {
            public string AlcoholName { get; set; }
            public decimal AlcoholPrice { get; set; }
            public int AlcoholQuantity { get; set; }
            public decimal AlcoholTotal { get; set; }
            public string AlcoholImage { get; set; }
            public string AlcoholID { get; set; }

            public void tambahQuantity()
            {
                AlcoholQuantity++;
            }
        }
        //private string FormatCurrency(decimal value)
        //{
        //    string strValue = value.ToString("N0", new CultureInfo("id-ID"));
        //    return strValue;
        //}

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
