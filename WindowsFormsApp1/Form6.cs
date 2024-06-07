using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form6 : Form
    {
        CL dR = new CL();
        CR fN = new CR();
        RN kN = new RN();
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            dR.Read_Clients();
            fN.Read_Cars();
            kN.Read_Rents();
            dataGridView1.DataSource = fN.cars;
            dataGridView1.Columns[0].HeaderText = "Код Авто";
            dataGridView1.Columns[1].HeaderText = "Название Авто";
            dataGridView1.Columns[2].HeaderText = "Цена за день";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            if (dataGridView1.Rows.Count > 0 )
            {
                int ns = dataGridView1.CurrentRow.Index;
                int kk = int.Parse(dataGridView1.Rows[ns].Cells[0].Value.ToString());
                var sst = from st in kN.rents
                          join pt in dR.clients
                          on st.Fk_client equals pt.Id_client
                          where st.Fk_car == kk
                          select new
                          {
                              s1 = st.Rent_id,
                              s2 = st.Rent_date,
                              s3 = st.Fk_client,
                              s4 = pt.Name,
                              s5 = pt.Birth_date,
                          };
                foreach (var sn in sst)
                {
                    dataGridView2.Rows.Add(sn.s1, sn.s2, sn.s3, sn.s4, sn.s5);
                }
            }
        }
    }
}
