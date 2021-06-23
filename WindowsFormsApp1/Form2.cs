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

namespace WindowsFormsApp8
{
    public partial class Form2 : Form
    {
        SqlConnection sqlcon;
        SqlCommand command;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                sqlcon = new SqlConnection(@"Data Source=DESKTOP-KUCH46Q;Initial Catalog=Stud;Integrated Security=True");
                sqlcon.Open();
                command = new SqlCommand("SELECT COUNT (Login) FROM [Curator] WHERE Login =@Login AND Password = @Password", sqlcon);
                command.Parameters.AddWithValue("Login", textBox1.Text);
                command.Parameters.AddWithValue("Password", maskedTextBox1.Text);
                int count = (int)command.ExecuteScalar();
                command.ExecuteNonQuery();
                sqlcon.Close();
            if (count == 1 || (textBox1.Text ==Program.login_admin && maskedTextBox1.Text == Program.pass_admin))
            {
                if(textBox1.Text == Program.login_admin && maskedTextBox1.Text == Program.pass_admin)
                {
                    Program.f = true;
                }
                Program.login= int.Parse(textBox1.Text); 
                this.Hide();
                Form1 forms = new Form1();
                forms.Show();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Program.f = false;
            this.ControlBox = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9')  || e.KeyChar == (char)Keys.Back)
            {

            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Используйте только арабские цифры при вводе логина", "Ошибка");
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
