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

namespace WindowsFormsApp1
{
    public partial class Form4 : Form // CAR
    {
        CR fn = new CR();
        int k;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            fn.Read_Cars();
            foreach (var c in fn.cars)
            {
                dataGridView1.Rows.Add(c.Id_car, c.Car_model, c.Price_pday, c.Gov_id);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                k = dataGridView1.CurrentRow.Index;
                if (dataGridView1.Rows.Count > 0)
                {
                    textBox1.Text = dataGridView1.Rows[k].Cells[0].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[k].Cells[1].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[k].Cells[2].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[k].Cells[3].Value.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[k].Cells[0].Value = textBox1.Text;
            dataGridView1.Rows[k].Cells[1].Value = textBox2.Text;
            dataGridView1.Rows[k].Cells[2].Value = textBox3.Text;
            dataGridView1.Rows[k].Cells[3].Value = textBox4.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            k = dataGridView1.CurrentRow.Index;     
            int x = int.Parse(dataGridView1.Rows[k].Cells[0].Value.ToString());
            int y = fn.cars.FindIndex(p => p.Id_car == x);
            dataGridView1.Rows.RemoveAt(k);
            fn.cars.RemoveAt(y);

            dataGridView1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            button4.Visible = false;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var temp = dataGridView1.Rows.Count - 1;
            button5.Visible = false;
            button4.Visible = true;
            if (int.TryParse(textBox3.Text.ToString(), out int result))
            {
                dataGridView1.Rows.Add(int.Parse(dataGridView1.Rows[temp].Cells[0].Value.ToString()) + 1, textBox2.Text, int.Parse(textBox3.Text), textBox4.Text);
                Car t = new Car(int.Parse(dataGridView1.Rows[temp].Cells[0].Value.ToString()) + 1, textBox2.Text, textBox4.Text, int.Parse(textBox3.Text));
                fn.cars.Add(t);
            }
            else MessageBox.Show("Неправильно введены данные");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (int.TryParse(textBox3.Text.ToString(), out int result))
            {
                dataGridView1.Rows[k].Cells[1].Value = textBox2.Text;
                dataGridView1.Rows[k].Cells[2].Value = textBox3.Text;
            }
            else MessageBox.Show("Неправильно введены данные");

            int X = fn.cars.FindIndex(p => p.Id_car == int.Parse(textBox1.Text));
            fn.cars[X].Id_car = int.Parse(textBox1.Text);
            fn.cars[X].Car_model = textBox2.Text;
            fn.cars[X].Price_pday = int.Parse(textBox3.Text);
            fn.cars[X].Gov_id = textBox4.Text;

            /*            foreach (Fine x in fn.fines)
                        {
                            if (x.Id_fine == int.Parse(textBox1.Text))
                            {
                                x.Id_fine = int.Parse(textBox1.Text);
                                x.CrimeName = textBox2.Text.ToString();
                                x.Price = int.Parse(textBox3.Text.ToString());
                                break;
                            }
                        }
            */
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            fn.Save_Cars();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
