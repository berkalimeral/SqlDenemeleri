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

namespace SqlDenemeleri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-E35HS2M;Initial Catalog=testDatabase;Integrated Security=True");
        SqlCommand command;
        SqlDataReader reader;
        public void listele()
        {
            listView1.Items.Clear();
            connection.Open();
            command = new SqlCommand("SELECT * FROM student", connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                ListViewItem item = new ListViewItem();
                item.Text = reader["id"].ToString();
                item.SubItems.Add(reader["name"].ToString());
                item.SubItems.Add(reader["surname"].ToString());
                item.SubItems.Add(reader["number"].ToString());
                item.SubItems.Add(reader["phone_number"].ToString());
                item.SubItems.Add(reader["course"].ToString());

                listView1.Items.Add(item);
            }
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            command = new SqlCommand("INSERT INTO student (name,surname,number,phone_number,course) VALUES ('"+textBox1.Text.ToString()+ "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "','" + comboBox1.Text.ToString()+"')",connection);
            command.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("Öğrenci Veri Tabanına Kaydedildi.");

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listele();
        }

        int id = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            command = new SqlCommand("DELETE FROM student WHERE id=(" + id + ")", connection);
            command.ExecuteNonQuery();
            connection.Close();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.Text = "";

            MessageBox.Show("Öğrenci Veri Tabanından Silindi.");
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[4].Text;
            comboBox1.Text = listView1.SelectedItems[0].SubItems[5].Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            command = new SqlCommand("UPDATE student SET name='" + textBox1.Text.ToString() + "',surname='"+textBox2.Text.ToString()+ "',number='"+textBox3.Text.ToString()+ "',phone_number='"+textBox4.Text.ToString()+ "',course='" + comboBox1.Text.ToString() + "'where id = " +id+ "", connection);
            command.ExecuteNonQuery();
            connection.Close();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.Text = "";

            MessageBox.Show("Öğrenci Güncellendi.");
        }
    }
}
