using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ONE_STOP_SUPERMARKET
{
    public partial class Login : Form
    {
        Form2 form2 = Application.OpenForms["form2"] as Form2;
        DataTable dt_login = new DataTable();

        public static string enteredEmail;
        public static string enteredPassword;


        public static bool isLoggedIn { get; set; }
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        public string Hash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        private void button_login_Click(object sender, EventArgs e)
        {
            form2.query = "SELECT emailcustomer email, passwordcustomer password FROM customer order by 1 asc;";
            form2.sqlCommand = new MySqlCommand(form2.query, Form2.sqlConnection);
            form2.sqlAdapter = new MySqlDataAdapter(form2.sqlCommand);
            form2.sqlAdapter.Fill(dt_login);

            enteredEmail = textBox_email.Text;
            enteredPassword = textBox_password.Text;

            if (string.IsNullOrEmpty(enteredEmail))
            {
                MessageBox.Show("Please enter your email.", "Try again!", MessageBoxButtons.OK);
            }
            else if (string.IsNullOrEmpty(enteredPassword))
            {
                MessageBox.Show("Please enter your password.", "Try again!", MessageBoxButtons.OK);
            }
            else
            {
                isLoggedIn = false;

                using (SHA256 sha256Hash = SHA256.Create())
                {
                    for (int p = 0; p < dt_login.Rows.Count; p++)
                    {
                        string storedEmail = dt_login.Rows[p]["email"].ToString();
                        string storedPassword = dt_login.Rows[p]["password"].ToString();

                        if (enteredEmail == storedEmail)
                        {
                            string hashedPassword = Hash(enteredPassword);
                            if (enteredPassword == storedPassword)
                            {
                                isLoggedIn = true;
                                break;
                            }
                        }
                    }
                }

                if (isLoggedIn == true)
                {
                    form2.query = $"SELECT idcustomer FROM customer WHERE emailcustomer = '{enteredEmail}';";
                    form2.sqlCommand = new MySqlCommand(form2.query, Form2.sqlConnection);
                    form2.sqlAdapter = new MySqlDataAdapter(form2.sqlCommand);
                    form2.sqlAdapter.Fill(Form2.pelangganID);
                    //MessageBox.Show(Form2.pelangganID.Rows[0][0].ToString());
                    MessageBox.Show("You are logged in.", "Welcome back!", MessageBoxButtons.OK);


                    form2.query = $"SELECT namacustomer FROM customer WHERE emailcustomer = '{enteredEmail}';";
                    form2.sqlCommand = new MySqlCommand(form2.query, Form2.sqlConnection);
                    form2.sqlAdapter = new MySqlDataAdapter(form2.sqlCommand);
                    form2.sqlAdapter.Fill(Form2.custName);
                    MessageBox.Show(Form2.custName.Rows[0][0].ToString());

                    form2 = new Form2();
                    form2.Dock = DockStyle.Fill;
                    form2.FormBorderStyle = FormBorderStyle.None;
                    form2.TopLevel = false;
                    form2.ControlBox = false;

                    form2.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sorry, you have entered the wrong email or password.", "Try again.", MessageBoxButtons.OK);
                }
            }
            textBox_email.Text = "";
            textBox_password.Text = "";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
