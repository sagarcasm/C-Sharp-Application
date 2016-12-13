using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Cooptable : Form
    {
        public Cooptable(object sender)
        {
            Employment employment = sender as Employment;
            InitializeComponent();
            //Console.Write(sender);
             

            for (int i = 0; i < employment.coopTable.coopInformation.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = employment.coopTable.coopInformation[i].employer;
                dataGridView1.Rows[i].Cells[1].Value = employment.coopTable.coopInformation[i].degree;
                dataGridView1.Rows[i].Cells[2].Value = employment.coopTable.coopInformation[i].city;
                dataGridView1.Rows[i].Cells[3].Value = employment.coopTable.coopInformation[i].term;
            }
            //btn_coopTable.Enabled = false;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
