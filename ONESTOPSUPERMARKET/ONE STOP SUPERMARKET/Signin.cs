using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace ONE_STOP_SUPERMARKET
{
    public partial class Signin : Form
    {
        private Panel panel_main;
        Form2 form2 = Application.OpenForms["form2"] as Form2;
        DataTable customer = new DataTable();
        public Signin(Panel panel)
        {
            InitializeComponent();
            this.panel_main = panel;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox_email.Text == "" || textBox_pass.Text == "" || textBox_nama.Text == "" || textBox_telp.Text == "" || textBox_alamat.Text == "")
            {
                MessageBox.Show("These cannot be empty! ");

            }
            else
            {
                string password = textBox_pass.Text;

                bool isPasswordValid = Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$");

                if (isPasswordValid && password.Length >= 8)
                {
                    MessageBox.Show("Password created.", "Thank you!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Password needs to include a capital letter, a small letter, and a digit.", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                form2.query = $"select emailcustomer, count(*) \r\nfrom customer \r\nwhere emailcustomer = '{textBox_email.Text}'\r\ngroup by emailcustomer;";
                form2.sqlconnect = new MySqlConnection(Form2.connectionstring);
                form2.sqlconnect.Open();
                form2.sqlCommand = new MySqlCommand(form2.query, form2.sqlconnect);
                form2.sqlAdapter = new MySqlDataAdapter(form2.sqlCommand);
                form2.sqlAdapter.Fill(customer);

                if (customer.Rows[0]["emailcustomer"].ToString() == textBox_email.Text)
                {
                    MessageBox.Show("You already have account.","Please go to log in page!");
                }
                else
                {
                    try
                    {
                        string idgenerate = "C";
                        int jumlahbelakang = customer.Rows.Count + 1;
                        jumlahbelakang++;
                        if (jumlahbelakang < 10)
                        {
                            idgenerate += jumlahbelakang.ToString("00");
                        }
                        else if (jumlahbelakang < 100)
                        {
                            idgenerate += "0";
                        }
                        idgenerate += jumlahbelakang;
                        form2.query = $"insert into customer values ('{idgenerate}', '{textBox_nama.Text}', '{textBox_telp.Text}', '{textBox_alamat.Text}', '{textBox_email.Text}', '{textBox_pass.Text}');";
                        //form2.sqlconnect = new MySqlConnection(form2.connectionstring);
                        form2.sqlCommand = new MySqlCommand(form2.query, Form2.sqlConnection);
                        form2.sqlAdapter = new MySqlDataAdapter(form2.sqlCommand);
                        form2.sqlAdapter.Fill(customer);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        MessageBox.Show("Signed Up Successfully.");
                        form2.sqlconnect.Close();
                        form2.sqlconnect.Dispose();


                        form2 = new Form2();
                        form2.Dock = DockStyle.Fill;
                        form2.FormBorderStyle = FormBorderStyle.None;
                        form2.TopLevel = false;
                        form2.ControlBox = false;

                        form2.Show();
                        this.Close();
                    }
                }
            }
        }

        private void Signin_Load(object sender, EventArgs e)
        {
            
        }

        private void label_clickHere_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.Dock = DockStyle.Fill;
            login.FormBorderStyle = FormBorderStyle.None;
            login.TopLevel = false;
            login.ControlBox = false;
            panel_main.AutoScroll = false;
            this.panel_main.Controls.Add(login);
            login.Show();
        }

        private void textBox_telp_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
