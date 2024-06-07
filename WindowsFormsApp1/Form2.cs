using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        CL dr = new CL();
        int k;
        public Form2()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dr.Read_Clients();
            foreach (var c in dr.clients)
            {
                dataGridView1.Rows.Add(c.Id_client, c.Name, c.Birth_date.ToString().Substring(0,10));
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            k = dataGridView1.CurrentRow.Index;
            if (dataGridView1.Rows.Count > 0)
            {
                textBox1.Text = dataGridView1.Rows[k].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[k].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[k].Cells[2].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = true;
            char[] d = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            string x = textBox2.Text;
            foreach (var elem in d)
            {
                if (x.Contains(elem))
                { flag = false; break; }
            }

            if (flag && DateTime.TryParse(textBox3.Text.ToString(), out DateTime result))
            {
                dataGridView1.Rows[k].Cells[1].Value = textBox2.Text;
                dataGridView1.Rows[k].Cells[2].Value = textBox3.Text;
            }
            else MessageBox.Show("Неправильно введены данные");

            int X = dr.clients.FindIndex(p => p.Id_client == int.Parse(textBox1.Text));
            dr.clients[X].Id_client = int.Parse(textBox1.Text);
            dr.clients[X].Name = textBox2.Text;
            dr.clients[X].Birth_date = DateTime.Parse(textBox3.Text);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dr.Save_Clients();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            k = dataGridView1.CurrentRow.Index;
            int x = int.Parse(dataGridView1.Rows[k].Cells[0].Value.ToString());
            int y = dr.clients.FindIndex(p => p.Id_client == x);
            dataGridView1.Rows.RemoveAt(k);
            dr.clients.RemoveAt(y);

            dataGridView1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            button4.Visible = false;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var temp = dataGridView1.Rows.Count - 1;
            button5.Visible = false;
            button4.Visible = true;
            bool flag = true;
            char[] d = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            string x = textBox2.Text;
            foreach (var elem in d)
            {
                if (x.Contains(elem))
                { flag = false; break; }
            }

            if (flag && DateTime.TryParse(textBox3.Text.ToString(), out DateTime result))
            {
                dataGridView1.Rows.Add(int.Parse(dataGridView1.Rows[temp].Cells[0].Value.ToString()) + 1, textBox2.Text, textBox3.Text);
                Client t = new Client(int.Parse(dataGridView1.Rows[temp].Cells[0].Value.ToString()) + 1, textBox2.Text.ToString(), DateTime.Parse(textBox3.Text));
                dr.clients.Add(t);
            }
            else MessageBox.Show("Неправильно введены данные");
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
