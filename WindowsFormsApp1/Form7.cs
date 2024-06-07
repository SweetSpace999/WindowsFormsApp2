using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form7 : Form
    {
        CL dr = new CL();
        CR fn = new CR();   
        RN kn   = new RN();
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            dr.Read_Clients();
            fn.Read_Cars();
            kn.Read_Rents();

            int s1 = 0; int s2 = 0; int s3 = 0;
            foreach (var c in dr.clients)
            {
                var sv = from s in kn.rents
                         join p in fn.cars on s.Fk_car equals p.Id_car
                         where s.Fk_client == c.Id_client
                         select new
                         {
                             s1 = s.Rent_id,
                             s2 = s.Quant_days,
                             s3 = p.Price_pday,
                             s4 = p.Price_pday * (s.Quant_days)
                         };

                int kp = sv.Count();
                int kt = sv.Sum(p => p.s2);
                double sp = sv.Average(p => p.s3);
                int ss = sv.Sum(p => p.s4);

                dataGridView1.Rows.Add(c.Id_client.ToString(), c.Name,
                    kp.ToString(), kt.ToString(), sp.ToString(), ss.ToString());

                s1 += kp; s2 += kt; s3 += ss;
            }
            textBox1.Text = s1.ToString();
            textBox2.Text = s2.ToString();
            textBox3.Text = s3.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[5], ListSortDirection.Ascending);
        }
    }
}
