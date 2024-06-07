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
    public partial class Form3 : Form
    {
        RN kn = new RN();
        CR fn = new CR();
        CL dr = new CL();
        int k;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            kn.Read_Rents();
            fn.Read_Cars();
            dr.Read_Clients();

            foreach (Rent kind in kn.rents)
            {
                int fk_d = kind.Fk_client;
                int n_d = dr.clients.FindIndex(p => p.Id_client == fk_d);
                int fk_f = kind.Fk_car;
                int n_f = fn.cars.FindIndex(p => p.Id_car == fk_f);
                            

                if (n_f != -1 && n_d != -1)
                    dataGridView1.Rows.Add(
                        kind.Rent_id, kind.Rent_date.ToString().Substring(0, 10), kind.Fk_client, dr.clients[n_d].Name, kind.Fk_car, fn.cars[n_f].Car_model, kind.Quant_days);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            k = dataGridView1.CurrentRow.Index;
            int fk_d = int.Parse(dataGridView1.Rows[k].Cells[2].Value.ToString());
            int n_d = dr.clients.FindIndex(p => p.Id_client == fk_d);

            int fk_f = int.Parse(dataGridView1.Rows[k].Cells[4].Value.ToString());
            int n_f = fn.cars.FindIndex(p => p.Id_car == fk_f);

            if (n_d != -1)
                comboBox1.DataSource = dr.clients;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Id_client";
                comboBox1.SelectedIndex = n_d;
            if (n_f != -1)
                comboBox2.DataSource = fn.cars;
                comboBox2.DisplayMember = "Car_model";
                comboBox2.ValueMember = "Id_car";
                comboBox2.SelectedIndex = n_f;

            textBox1.Text = dataGridView1.Rows[k].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[k].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[k].Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.Rows[k].Cells[6].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e) // Удаление строки
        {
            k = dataGridView1.CurrentRow.Index;
            int x = int.Parse(dataGridView1.Rows[k].Cells[0].Value.ToString());
            int y = kn.rents.FindIndex(p => p.Rent_id == x);
            dataGridView1.Rows.RemoveAt(k);
            kn.rents.RemoveAt(y);
            dataGridView1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e) //Создание новой строки
        {
            button5.Visible = true;
            button4.Visible = false;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button5_Click(object sender, EventArgs e) // Добавление новой строки
        {
            var temp = dataGridView1.Rows.Count - 1;
            if (DateTime.TryParse(textBox2.Text, out DateTime result1))
            {
                int nd = comboBox1.SelectedIndex;
                int kd = dr.clients[nd].Id_client;
                int nf = comboBox2.SelectedIndex;
                int kf = fn.cars[nf].Id_car;

                dataGridView1.Rows.Add(int.Parse(dataGridView1.Rows[temp].Cells[0].Value.ToString()) + 1,
                    textBox2.Text, kd.ToString(), comboBox1.Text, kf.ToString(), comboBox2.Text, textBox5.Text);
                button5.Visible = false;
                button4.Visible = true;

                Rent t = new Rent(int.Parse(dataGridView1.Rows[temp].Cells[0].Value.ToString()) + 1,
                    DateTime.Parse(textBox2.Text), kd, kf, int.Parse(textBox5.Text));
                kn.rents.Add(t);
            }
            else
            {
                MessageBox.Show("Неправильный формат ввода");
            }
        
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
  /*          for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[4].Value.ToString() == comboBox1.Text.ToString())
                {
                    textBox7.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    textBox6.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    textBox1.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
                }
            }
  */
        }

        private void button1_Click_1(object sender, EventArgs e) // Изменение строки
        {
            int nd = comboBox1.SelectedIndex;
            int kd = dr.clients[nd].Id_client;
            int nf = comboBox2.SelectedIndex;
            int kf = fn.cars[nf].Id_car;

            if (DateTime.TryParse(textBox2.Text, out DateTime result1))
                dataGridView1.Rows[k].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[k].Cells[1].Value = textBox2.Text;
                dataGridView1.Rows[k].Cells[2].Value = kd.ToString();
                dataGridView1.Rows[k].Cells[3].Value = comboBox1.Text;
                dataGridView1.Rows[k].Cells[4].Value = kf.ToString();
                dataGridView1.Rows[k].Cells[5].Value = comboBox2.Text;
                dataGridView1.Rows[k].Cells[6].Value = textBox5.Text;

            int x = kn.rents.FindIndex(p => p.Rent_id == int.Parse(textBox1.Text));
            kn.rents[x].Rent_id = int.Parse(textBox1.Text);
            kn.rents[x].Rent_date = DateTime.Parse(textBox2.Text);
            kn.rents[x].Fk_client = kd;
            kn.rents[x].Fk_car = kf;
            kn.rents[x].Quant_days = int.Parse(textBox5.Text);
            /*
                        foreach (Kind_of_crime x in kn.kinds_of_crimes)
                        {
                            if (x.Id_crime == int.Parse(textBox7.Text))
                            {
                                x.Id_crime = int.Parse(textBox7.Text);
                                x.Data_crime = DateTime.Parse(textBox6.Text);
                                x.Fk_driver = kv;
                                x.Fk_fine = int.Parse(textBox3.Text);
                                x.Check = bool.Parse(textBox4.Text);
                                break;
                            }
                        }
            */
        }

        private void button2_Click_1(object sender, EventArgs e) // Сохрание в файл
        {
            kn.Save_Rent();
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
