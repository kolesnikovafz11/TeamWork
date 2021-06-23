using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (comboBox2.SelectedIndex == 0)
            {
                if (Program.f)
                {
                    dataGridView1.DataSource = null;
                    command = new SqlCommand("SELECT COUNT(s.Surname)FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE Su.Subject_name = N'" + comboBox4.Text + "' AND G.Group_name = N'" + comboBox3.Text + "'", sqlcon);
                    count = (int)command.ExecuteScalar();
                    command.ExecuteNonQuery();
                    if (count != 0)
                    {
                        dataGridView1.Rows.Clear();
                        adapter = new SqlDataAdapter("SELECT s.Surname,g.Group_name,su.Subject_name,m.Mark,m.Datatime FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE Su.Subject_name = N'" + comboBox4.Text + "' AND G.Group_name = N'" + comboBox3.Text + "'", sqlcon);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        Head1();
                        MessageBox.Show("Данные найдены!");
                    }
                    else
                    {
                        MessageBox.Show("Данные не найдены! Проверьте корректность заполняемых полей и то, что вы являетесь куратором данной группы", "Предупреждение");
                    }
                }
                else
                {

                    dataGridView1.DataSource = null;
                    command = new SqlCommand("SELECT COUNT(s.Surname)FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE Su.Subject_name = N'" + comboBox4.Text + "' AND G.Group_name = N'" + comboBox3.Text + "' AND c.Login = " + Program.login, sqlcon);
                    count = (int)command.ExecuteScalar();
                    command.ExecuteNonQuery();
                    if (count != 0)
                    {
                        dataGridView1.Rows.Clear();
                        adapter = new SqlDataAdapter("SELECT s.Surname,g.Group_name,su.Subject_name,m.Mark,m.Datatime FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE Su.Subject_name = N'" + comboBox4.Text + "' AND G.Group_name = N'" + comboBox3.Text + "' AND c.Login = " + Program.login, sqlcon);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        Head();
                        MessageBox.Show("Данные найдены!");
                    }
                    else
                    {
                        MessageBox.Show("Данные не найдены! Проверьте корректность заполняемых полей и то, что вы являетесь куратором данной группы", "Предупреждение");
                    }
                }
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                if (Program.f)
                {
                    dataGridView1.DataSource = null;
                    command = new SqlCommand("SELECT COUNT(s.Surname)FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE G.Group_name = N'" + comboBox3.Text + "'", sqlcon);
                    count = (int)command.ExecuteScalar();
                    command.ExecuteNonQuery();
                    if (count != 0)
                    {
                        adapter = new SqlDataAdapter("SELECT s.Surname,g.Group_name,su.Subject_name,m.Mark,m.Datatime FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE G.Group_name = N'" + comboBox3.Text + "'", sqlcon);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        Head1();
                        MessageBox.Show("Данные найдены!");
                    }
                    else
                    {
                        MessageBox.Show("Данные не найдены! Проверьте корректность заполняемых полей и то, что вы являетесь куратором данной группы", "Предупреждение");
                    }
                }
                else
                {
                    dataGridView1.DataSource = null;
                    command = new SqlCommand("SELECT COUNT(s.Surname)FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE G.Group_name = N'" + comboBox3.Text + "' AND c.Login = " + Program.login, sqlcon);
                    count = (int)command.ExecuteScalar();
                    command.ExecuteNonQuery();
                    if (count != 0)
                    {
                        adapter = new SqlDataAdapter("SELECT s.Surname,g.Group_name,su.Subject_name,m.Mark,m.Datatime FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE G.Group_name = N'" + comboBox3.Text + "' AND c.Login = " + Program.login, sqlcon);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        Head();
                        MessageBox.Show("Данные найдены!");
                    }
                    else
                    {
                        MessageBox.Show("Данные не найдены! Проверьте корректность заполняемых полей и то, что вы являетесь куратором данной группы", "Предупреждение");
                    }
                }
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                if (Program.f)
                {
                    dataGridView1.DataSource = null;
                    command = new SqlCommand("SELECT COUNT(s.Surname)FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE s.Stud_id = N'" + comboBox5.Text + "' AND su.Subject_name = N'" + comboBox4.Text + "'", sqlcon);
                    count = (int)command.ExecuteScalar();
                    command.ExecuteNonQuery();
                    if (count != 0)
                    {
                        dataGridView1.Rows.Clear();
                        adapter = new SqlDataAdapter("SELECT s.Surname,g.Group_name,su.Subject_name,m.Mark,m.Datatime FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE s.Stud_id = N'" + comboBox5.Text + "'AND su.Subject_name = N'" + comboBox4.Text + "'", sqlcon);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        Head1();
                        MessageBox.Show("Данные найдены!");
                    }
                    else
                    {
                        MessageBox.Show("Данные не найдены! Проверьте корректность заполняемых полей и то, что вы являетесь куратором данной группы", "Предупреждение");
                    }
                }
                else
                {
                    dataGridView1.DataSource = null;
                    command = new SqlCommand("SELECT COUNT(s.Surname)FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE s.Stud_id = N'" + comboBox5.Text + "' AND su.Subject_name = N'" + comboBox4.Text + "' AND c.Login = " + Program.login, sqlcon);
                    count = (int)command.ExecuteScalar();
                    command.ExecuteNonQuery();
                    if (count != 0)
                    {
                        dataGridView1.Rows.Clear();
                        adapter = new SqlDataAdapter("SELECT s.Surname,g.Group_name,su.Subject_name,m.Mark,m.Datatime FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE s.Stud_id = N'" + comboBox5.Text + "'AND su.Subject_name = N'" + comboBox4.Text + "' AND c.Login = " + Program.login, sqlcon);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        Head();
                        MessageBox.Show("Данные найдены!");
                    }
                    else
                    {
                        MessageBox.Show("Данные не найдены! Проверьте корректность заполняемых полей и то, что вы являетесь куратором данной группы", "Предупреждение");
                    }
                }
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                if (Program.f)
                {
                    dataGridView1.DataSource = null;
                    command = new SqlCommand("SELECT COUNT(s.Surname)FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE s.Stud_id = N'" + comboBox5.Text + "'", sqlcon);
                    count = (int)command.ExecuteScalar();
                    command.ExecuteNonQuery();
                    if (count != 0)
                    {
                        dataGridView1.Rows.Clear();
                        adapter = new SqlDataAdapter("SELECT s.Surname,g.Group_name,su.Subject_name,m.Mark,m.Datatime FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE s.Stud_id = N'" + comboBox5.Text + "'", sqlcon);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        Head1();
                        MessageBox.Show("Данные найдены!");
                    }
                    else
                    {
                        MessageBox.Show("Данные не найдены! Проверьте корректность заполняемых полей и то, что вы являетесь куратором данной группы", "Предупреждение");
                    }
                }
                else
                {

                    dataGridView1.DataSource = null;
                    command = new SqlCommand("SELECT COUNT(s.Surname)FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE s.Stud_id = N'" + comboBox5.Text + "'AND c.Login = " + Program.login, sqlcon);
                    count = (int)command.ExecuteScalar();
                    command.ExecuteNonQuery();
                    if (count != 0)
                    {
                        dataGridView1.Rows.Clear();
                        adapter = new SqlDataAdapter("SELECT s.Surname,g.Group_name,su.Subject_name,m.Mark,m.Datatime FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id WHERE s.Stud_id = N'" + comboBox5.Text + "' AND c.Login = " + Program.login, sqlcon);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        Head();
                        MessageBox.Show("Данные найдены!");
                    }
                    else
                    {
                        MessageBox.Show("Данные не найдены! Проверьте корректность заполняемых полей и то, что вы являетесь куратором данной группы", "Предупреждение");
                    }
                }
            }
        }

        private void Obnov()
        {
            maskedTextBox1.Clear();
            textBox6.Clear();
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!Program.f)
            {
                e.Cancel = e.TabPageIndex == 1;
                MessageBox.Show("Эта вкладка доступна только администратору", "Ошибка");
            }
            else
            {
                Obnov();
            }
        }

        private void вернутьсяВНачалоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show()
            ; this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
                if (comboBox1.SelectedIndex == 0)
                {
                    string sq = "INSERT INTO Marks (Student_id,Subject_id,Datatime,Mark) VALUES (@p1,@p2,@p3,@p4)";
                    try
                    {
                        SqlCommand comm = new SqlCommand(sq, sqlcon);
                        comm.Parameters.AddWithValue("p1", comboBox6.Text);
                        comm.Parameters.AddWithValue("p2", comboBox7.Text);
                        comm.Parameters.AddWithValue("p3", maskedTextBox1.Text);
                        comm.Parameters.AddWithValue("p4", textBox6.Text);
                        comm.ExecuteNonQuery();
                        Obn();
                    Obno();
                }
                    catch
                    {
                        MessageBox.Show("Проверьте все данные, которые вы вводите!\n Возможно,данное значение уже есть в базе данных", "Ошибка");
                    }
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    try
                    {
                        string sq1 = "DELETE FROM Marks WHERE Mark = @p4 AND Student_id =@p1 AND Subject_id = @p2 AND Datatime =@p3";
                        SqlCommand comm = new SqlCommand(sq1, sqlcon);
                        comm.Parameters.AddWithValue("p1", comboBox6.Text);
                        comm.Parameters.AddWithValue("p2", comboBox7.Text);
                        comm.Parameters.AddWithValue("p3", maskedTextBox1.Text);
                        comm.Parameters.AddWithValue("p4", textBox6.Text);
                        comm.ExecuteNonQuery();
                        Obn();
                    Obno();
                }
                    catch
                    {
                        MessageBox.Show("Неправильный ввод!Проверьте всю вводимую информацию еще раз! Возможно, строка уже удалена", "Ошибка");
                    }
            }
        }
        private void Obn()
        {
            dataGridView2.DataSource = null;
            adapter = new SqlDataAdapter("SELECT s.Surname,g.Group_name,su.Subject_name,m.Mark,m.Datatime FROM Student AS s JOIN Groups AS g ON S.Group_id = G.Group_id JOIN Marks AS m ON M.Student_id = S.Stud_id JOIN Subject AS su ON Su.Subject_id = M.Subject_id JOIN Curator c ON C.Curator_id=G.Curator_id", sqlcon);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;
            Head1();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex ==0)
            {
                label4.Visible = false;
                label3.Visible = true;
                label2.Visible = true;
                comboBox5.Visible = false;
                comboBox3.Visible = true;
                comboBox4.Visible = true;
            }
            else if(comboBox2.SelectedIndex == 1)
            {
                label2.Visible = true;
                label3.Visible = false;
                label4.Visible = false;
                comboBox5.Visible = false;
                comboBox4.Visible = false;
                comboBox3.Visible = true;
            }
            else if(comboBox2.SelectedIndex == 2)
            {
                label2.Visible = false;
                label3.Visible = true;
                label4.Visible = true;
                comboBox5.Visible = true;
                comboBox4.Visible = true;
                comboBox3.Visible = false;
            }
            else if(comboBox2.SelectedIndex == 3)
            {
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = true;
                comboBox5.Visible = true;
                comboBox3.Visible = false;
                comboBox4.Visible = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                Obno();
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                Obno();
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                Obno();
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                Obno();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        private void Obno()
        {
            textBox6.Clear();
            maskedTextBox1.Clear();
        }
    }
}
