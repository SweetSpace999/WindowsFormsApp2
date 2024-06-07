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
    public partial class Form5 : Form
    {
        CL dR = new CL();
        CR fN = new CR();
        RN kN = new RN();
        public Form5()
        {
            InitializeComponent();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            dR.Read_Clients();
            fN.Read_Cars();
            kN.Read_Rents();
            dataGridView1.DataSource = dR.clients;
            dataGridView1.Columns[0].HeaderText = "Код";
            dataGridView1.Columns[1].HeaderText = "Клиент";
            dataGridView1.Columns[2].HeaderText = "Дата рождения";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            if (dataGridView1.Rows.Count > 0)
            {
                int ns = dataGridView1.CurrentRow.Index;
                int kk = int.Parse(dataGridView1.Rows[ns].Cells[0].Value.ToString());
                foreach (Rent ss in kN.rents)
                {
                    if (ss.Fk_client == kk)
                    {
                        int kt = ss.Fk_car;
                        int nt = fN.cars.FindIndex(t => t.Id_car == kt);
                        string np = fN.cars[nt].Car_model;
                        int pp = fN.cars[nt].Price_pday;
                        
                        dataGridView2.Rows.Add(ss.Rent_id.ToString(),
                            ss.Rent_date.ToString().Substring(0,10),
                            ss.Fk_car, np, pp.ToString());
                    }
                }
            }
        }
    }
}
