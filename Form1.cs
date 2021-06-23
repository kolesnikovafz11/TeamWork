using System;
using System.Data;
using System.Windows.Forms;
using System.Data. SqlClient;

namespace WindowsFormsApp8
{
    public partial class Form1 : Form
    {
        SqlConnection sqlcon;
        SqlDataAdapter adapter;
        SqlCommand command;
        DataTable dt;
        public Form1()
        {
            InitializeComponent();
            //comboBox3.Visible = false;
            //comboBox4.Visible = false;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            label4.Visible = false;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if (Program.f)
            {
                sqlcon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; Initial Catalog = Stud; Integrated Security = True");
                sqlcon.Open();
                this.ControlBox = false;
                adapter = new SqlDataAdapter("SELECT g.Group_name FROM Groups AS g", sqlcon);
                dt = new DataTable();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox3.Items.Add(dt.Rows[i]["Group_name"]);
                }
                adapter = new SqlDataAdapter("SELECT Subject_name FROM Subject", sqlcon);
                dt = new DataTable();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox4.Items.Add(dt.Rows[i]["Subject_name"]);
                }
                adapter = new SqlDataAdapter("SELECT Stud_id FROM Student", sqlcon);
                dt = new DataTable();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox5.Items.Add(dt.Rows[i]["Stud_id"]);
                }
                adapter = new SqlDataAdapter("SELECT Subject_id FROM Subject", sqlcon);
                dt = new DataTable();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox7.Items.Add(dt.Rows[i]["Subject_id"]);
                }
                adapter = new SqlDataAdapter("SELECT Stud_id FROM Student", sqlcon);
                dt = new DataTable();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox6.Items.Add(dt.Rows[i]["Stud_id"]);
                }
            }
            else
            {
                sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Stud;Integrated Security=True");
                sqlcon.Open();
                this.ControlBox = false;
                adapter = new SqlDataAdapter("SELECT g.Group_name FROM Groups as g, Curator as c WHERE G.Curator_id = c.Curator_id AND c.Login = " + Program.login, sqlcon);
                dt = new DataTable();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox3.Items.Add(dt.Rows[i]["Group_name"]);
                }
                adapter = new SqlDataAdapter("SELECT Subject_name FROM Subject", sqlcon);
                dt = new DataTable();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox4.Items.Add(dt.Rows[i]["Subject_name"]);
                }
                adapter = new SqlDataAdapter("SELECT s.Stud_id FROM Student AS s,Groups as g,Curator as c WHERE s.Group_id = g.Group_id AND g.Curator_id = c.Curator_id  AND c.Login = " + Program.login, sqlcon);
                dt = new DataTable();
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox5.Items.Add(dt.Rows[i]["Stud_id"]);
                }
            }         
        }
        private void Head()
        {
            dataGridView1.Columns[0].HeaderText = "Фамилия";
            dataGridView1.Columns[1].HeaderText = "Группа";
            dataGridView1.Columns[2].HeaderText = "Предмет";
            dataGridView1.Columns[4].HeaderText = "Дата";
            dataGridView1.Columns[3].HeaderText = "Оценка";
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sqlcon.Close();
            Application.Exit();
        }
        private void Head1()
        {
            dataGridView2.Columns[0].HeaderText = "Фамилия";
            dataGridView2.Columns[1].HeaderText = "Группа";
            dataGridView2.Columns[2].HeaderText = "Предмет";
            dataGridView2.Columns[4].HeaderText = "Дата";
            dataGridView2.Columns[3].HeaderText = "Оценка";
        }

       
}
