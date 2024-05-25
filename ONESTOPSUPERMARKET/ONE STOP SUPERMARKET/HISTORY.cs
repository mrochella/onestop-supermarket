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
    public partial class HISTORY : Form
    {
        private Panel panel_main;
        Form2 form2 = Application.OpenForms["form2"] as Form2;
        DataTable history = new DataTable();
        public HISTORY(Panel panel)
        {
            InitializeComponent();
            this.panel_main = panel;
        }

        private void HISTORY_Load(object sender, EventArgs e)
        {
            form2.query = $"SELECT idtransaksi as 'TRANSAKSI ID', idcustomer as 'CUSTOMER ID', namacustomer as 'NAMA', namaproduk as 'PRODUK', hargaproduk as 'HARGA', quantity as 'QTY', totalharga as 'SUBTOTAL', statuspesanan as 'STATUS PESANAN' FROM history WHERE idcustomer = '{Form2.pelangganID.Rows[0][0]}';";
            form2.sqlCommand = new MySqlCommand(form2.query, Form2.sqlConnection);
            form2.sqlAdapter = new MySqlDataAdapter(form2.sqlCommand);
            form2.sqlAdapter.Fill(history);

            if (history.Rows.Count == 0)
            {
                this.BackColor = Color.White;

                Panel emptyCartPanel = new Panel();
                emptyCartPanel.AutoSize = true;
                emptyCartPanel.Margin = new Padding(0);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = Image.FromFile(@".\history_kosong.png");
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Size = new Size(250, 250);
                pictureBox.Location = new Point(280, 30);
                emptyCartPanel.Controls.Add(pictureBox);

                Label labelName = new Label();
                labelName.Text = "Oops! no transaction found.";
                labelName.ForeColor = Color.Orange;
                labelName.Font = new Font("Ebrima", 12, FontStyle.Bold);
                labelName.AutoSize = true;
                labelName.Location = new Point(290, 290);
                emptyCartPanel.Controls.Add(labelName);

                this.Controls.Add(emptyCartPanel);
            }
            else
            {
                foreach (DataRow row in history.Rows)
                {
                    DataGridView dgvHistory = new DataGridView();
                    dgvHistory.Dock = DockStyle.Fill;
                    dgvHistory.AutoGenerateColumns = true;
                    dgvHistory.DataSource = history;
                    this.Controls.Add(dgvHistory);

                    dgvHistory.DefaultCellStyle.SelectionBackColor = Color.Orange;
                    dgvHistory.DefaultCellStyle.SelectionForeColor = Color.White;
                }
            }
        }
    }
}
