using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing;
using static ONE_STOP_SUPERMARKET.FAVOURITE;
using static ONE_STOP_SUPERMARKET.CART;
using System.Data.SqlClient;
using MySqlX.XDevAPI.Relational;

namespace ONE_STOP_SUPERMARKET
{
    public partial class CART : Form
    {
        public MySqlCommand sqlCommand;
        public MySqlDataAdapter sqlAdapter;
        public MySqlDataReader sqlReader;
        public string sqlQuery;

        public static DataTable cartItemsTable = new DataTable();
        public static DataTable selectedPhoto = new DataTable();
        //public static DataTable a = new DataTable();
        public static DataTable x = new DataTable();

        Panel emptyCartPanel = new Panel();
        Label labelQuantity = new Label();
        int panelTop = 30;
        int panelHeight = 300;

        Button button_checkout = new Button();
        public CART()
        {
            InitializeComponent();
        }
        private void CART_Load(object sender, EventArgs e)
        {
            cartItemsTable.Clear();
            cartItemsTable.Columns.Clear();
            sqlQuery = $"select p.idproduk, p.namaproduk, p.hargaproduk, (select p.hargaproduk * c.quantity) as 'subtotal', c.quantity, p.gambarproduk FROM cart c, produk p WHERE c.idproduk = p.idproduk AND status_del = '0' AND c.idcustomer = '{Form2.pelangganID.Rows[0][0]}';";
            sqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(cartItemsTable);

            

            if (cartItemsTable.Rows.Count == 0)
            {
                this.BackColor = Color.White;

                emptyCartPanel.AutoSize = true;
                emptyCartPanel.Margin = new Padding(0);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = Image.FromFile(@".\cart_kosong.png");
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Size = new Size(250, 250);
                pictureBox.Location = new Point(280, 30);
                emptyCartPanel.Controls.Add(pictureBox);

                Label labelNamee = new Label();
                labelNamee.Text = "Looks like you have not added anything to your cart..";
                labelNamee.ForeColor = Color.Orange;
                labelNamee.Font = new Font("Ebrima", 12, FontStyle.Bold);
                labelNamee.AutoSize = true;
                labelNamee.Location = new Point(200, 290);
                emptyCartPanel.Controls.Add(labelNamee);

                emptyCartPanel.Location = new Point((panelcartitem.Width - emptyCartPanel.Width) / 2, (panelcartitem.Height - emptyCartPanel.Height) / 2);
                panelcartitem.Controls.Add(emptyCartPanel);
            }
            else
            {
                items();
            }
        }
        private void items()
        {
            this.panelcartitem.Controls.Clear();

            button_checkout = new Button();
            button_checkout.Text = "Check out";
            button_checkout.Size = new Size(100, 25);
            button_checkout.Location = new Point(650, 20);
            button_checkout.Click += Button_checkout_Click;
            panelcartitem.Controls.Add(button_checkout);
            button_checkout.BringToFront();

            MessageBox.Show(cartItemsTable.Rows.Count.ToString());

            int panelTop = 0;  // Lokasi vertikal panel pertama
            int panelSpacing = 10;  // Jarak antara panel
            int i = 0;  // Variabel bantu untuk indeks

            foreach (DataRow item in cartItemsTable.Rows)
            {
                Panel itemPanel = new Panel();
                itemPanel.Width = 800;
                itemPanel.Height = panelHeight;
                itemPanel.AutoScroll = true;

                PictureBox pictureBox = new PictureBox();
                pictureBox.ImageLocation = FRUIT.project + item["gambarproduk"].ToString();
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Size = new Size(250, 250);
                pictureBox.Location = new Point(30, 30);
                itemPanel.Controls.Add(pictureBox);

                Label labelName = new Label();
                labelName.Text = item["namaproduk"].ToString();
                labelName.Font = new Font("Ebrima", 14, FontStyle.Bold);
                labelName.Tag = item["idproduk"].ToString();
                labelName.AutoSize = true;
                labelName.Location = new Point(pictureBox.Right + 20, pictureBox.Top);
                itemPanel.Controls.Add(labelName);

                Label labelPrice = new Label();
                labelPrice.Text = "Rp. " + item["subtotal"].ToString();
                labelPrice.Font = new Font("Ebrima", 14);
                labelPrice.AutoSize = true;
                labelPrice.Location = new Point(pictureBox.Right + 20, labelName.Bottom + 10);
                itemPanel.Controls.Add(labelPrice);

                Label labelQuantity = new Label();
                labelQuantity.Text = item["quantity"].ToString();
                labelQuantity.AutoSize = true;
                labelQuantity.Location = new Point(pictureBox.Right + 20, labelPrice.Bottom + 15);
                itemPanel.Controls.Add(labelQuantity);

                Button buttondelete = new Button();
                buttondelete.Text = "Delete";
                buttondelete.Size = new Size(70, 40);
                buttondelete.AutoSize = true;
                buttondelete.Location = new Point(pictureBox.Right + 20, labelQuantity.Bottom + 110);
                buttondelete.Click += Buttondelete_Click;
                itemPanel.Controls.Add(buttondelete);

                int panelY = panelTop + (i * (panelHeight + panelSpacing));
                itemPanel.Location = new Point(0, panelY);

                panelcartitem.Controls.Add(itemPanel);

                i++;
            }
        }
    
        private void Button_checkout_Click(object sender, EventArgs e)
        {
            if (cartItemsTable.Rows.Count == 0)
            {
                button_checkout.Enabled = false; 
            }
            else
            {
                button_checkout.Enabled = true;
                    sqlQuery = $"select sum(p.hargaproduk * c.quantity) as 'hargatotal' from cart c, produk p where c.idproduk = p.idproduk and idcustomer = '{Form2.pelangganID.Rows[0][0].ToString()}' and status_del = '0'";
                    sqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
                    sqlAdapter = new MySqlDataAdapter(sqlCommand);
                    DataTable resultTable = new DataTable();
                    sqlAdapter.Fill(resultTable);
                   
                    sqlQuery = "select idtransaksi from transaksi";
                    sqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
                    sqlAdapter = new MySqlDataAdapter(sqlCommand);
                    DataTable transaksi = new DataTable();
                    sqlAdapter.Fill(transaksi);

                    sqlQuery = $"select namaproduk p, hargaproduk p from produk p, cart c where p.idproduk = c.idproduk and idcustomer = '{Form2.pelangganID.Rows[0][0].ToString()}'";
                    sqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
                    sqlAdapter = new MySqlDataAdapter(sqlCommand);
                    DataTable dthistory = new DataTable();
                    sqlAdapter.Fill(dthistory);


                    if (resultTable.Rows.Count > 0)
                    {
                        double hargatotal = Convert.ToDouble(resultTable.Rows[0]["hargatotal"]);
                        DialogResult result = MessageBox.Show($"Total harga : Rp. {hargatotal}\nPay Now??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            int count = transaksi.Rows.Count + 1;
                            string transaksiID = "T" + count.ToString("000");
                            string tanggal = DateTime.Now.ToString("yyyy-MM-dd");
                            string command = $"INSERT into transaksi values ('{transaksiID}', '{tanggal}', {hargatotal}, '{Form2.pelangganID.Rows[0][0].ToString()}', 'Sedang diproses')";
                            try
                            {
                                Form2.sqlConnection.Open();

                                sqlCommand = new MySqlCommand(command, Form2.sqlConnection);
                                sqlReader = sqlCommand.ExecuteReader();
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                Form2.sqlConnection.Close();
                            }

                            int counter = 0;
                            foreach (DataRow row in cartItemsTable.Rows)
                            {
                            string command2 = $"INSERT into `history` values ('{transaksiID}', '{Form2.pelangganID.Rows[0][0].ToString()}', '{Form2.custName.Rows[0][0].ToString()}', '{cartItemsTable.Rows[counter][1].ToString()}', {Convert.ToInt32(cartItemsTable.Rows[counter][2].ToString())}, {Convert.ToInt32(cartItemsTable.Rows[counter][4].ToString())}, {hargatotal}, 'Sedang diproses')";

                            try
                            {
                                Form2.sqlConnection.Open();

                                sqlCommand = new MySqlCommand(command2, Form2.sqlConnection);
                                sqlReader = sqlCommand.ExecuteReader();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                Form2.sqlConnection.Close();
                            }
                                counter++;
                            }
                        cartItemsTable.Rows.Clear();

                        PROSESPEMBAYARAN pembayaran = new PROSESPEMBAYARAN();
                            pembayaran.Dock = DockStyle.Fill;
                            pembayaran.FormBorderStyle = FormBorderStyle.None;
                            pembayaran.TopLevel = false;
                            pembayaran.ControlBox = false;
                            panelcartitem.AutoScroll = true;
                            this.panelcartitem.Controls.Clear();
                            this.panelcartitem.Controls.Add(pembayaran);

                            pembayaran.Show();

                        }
                    }
                    else
                    {
                        MessageBox.Show("Cart is empty.");
                    }
            }
        }

        public class CartItem
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public string ImagePath { get; set; }
        }
        private void Buttondelete_Click(object sender, EventArgs e)
        {
            Button buttondelete = (Button)sender;
            Panel itemPanel = (Panel)buttondelete.Parent;

            Label labelName = (Label)itemPanel.Controls[1];
            string productName = labelName.Text;
            MessageBox.Show(productName);

            MessageBox.Show(labelName.Tag.ToString());

            string updateDel = $"UPDATE cart SET status_del = '1' WHERE idproduk = '{labelName.Tag.ToString()}'";

            DataRow[] rowsToDelete = cartItemsTable.Select($"namaproduk = '{productName}'");
            foreach (DataRow row in rowsToDelete)
            {
                cartItemsTable.Rows.Remove(row);
            }

            panelcartitem.Controls.Remove(itemPanel);

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
            sqlQuery = $"SELECT * FROM cart WHERE status_del = '0' AND idcustomer = '{Form2.pelangganID.Rows[0][0]}'";
            sqlCommand = new MySqlCommand(sqlQuery, Form2.sqlConnection);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(x);

            if (x.Rows.Count == 0)
            {
                this.BackColor = Color.White;

                emptyCartPanel.AutoSize = true;
                emptyCartPanel.Margin = new Padding(0);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = Image.FromFile(@".\cart_kosong.png");
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Size = new Size(250, 250);
                pictureBox.Location = new Point(280, 30);
                emptyCartPanel.Controls.Add(pictureBox);

                Label labelNamee = new Label();
                labelNamee.Text = "Looks like you have not added anything to your cart..";
                labelNamee.ForeColor = Color.Orange;
                labelNamee.Font = new Font("Ebrima", 12, FontStyle.Bold);
                labelNamee.AutoSize = true;
                labelNamee.Location = new Point(200, 290);
                emptyCartPanel.Controls.Add(labelNamee);

                emptyCartPanel.Location = new Point((panelcartitem.Width - emptyCartPanel.Width) / 2, (panelcartitem.Height - emptyCartPanel.Height) / 2);
                panelcartitem.Controls.Add(emptyCartPanel);
            }
            else
            {
                panelcartitem.Controls.Add(itemPanel);
                items();
            }
        }
        public void updatecart()
        {
            cartItemsTable.Clear();

            string keranjang = $"SELECT * FROM cart";
            sqlCommand = new MySqlCommand(keranjang, Form2.sqlConnection);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(cartItemsTable);
        }

        private void panelcartitem_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
