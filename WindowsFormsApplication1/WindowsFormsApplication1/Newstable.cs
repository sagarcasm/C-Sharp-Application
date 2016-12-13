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
    public partial class Newstable : Form
    {
        public Newstable(object sender)
        {
            News newsdata = sender as News;
            InitializeComponent();
            //Console.Write(sender);


            for (int i = 0; i < newsdata.year.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = newsdata.year[i].date;
                dataGridView1.Rows[i].Cells[1].Value = newsdata.year[i].title;
                dataGridView1.Rows[i].Cells[2].Value = newsdata.year[i].description;
            }
            for (int i = 0; i < newsdata.quarter.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = newsdata.quarter[i].date;
                dataGridView1.Rows[i].Cells[1].Value = newsdata.quarter[i].title;
                dataGridView1.Rows[i].Cells[2].Value = newsdata.quarter[i].description;
            }
            for (int i = 0; i < newsdata.month.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = newsdata.month[i].date;
                dataGridView1.Rows[i].Cells[1].Value = newsdata.month[i].title;
                dataGridView1.Rows[i].Cells[2].Value = newsdata.month[i].description;
            }
            for (int i = 0; i < newsdata.older.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = newsdata.older[i].date;
                dataGridView1.Rows[i].Cells[1].Value = newsdata.older[i].title;
                dataGridView1.Rows[i].Cells[2].Value = newsdata.older[i].description;
            }
            //btn_coopTable.Enabled = false;
        }
    }
}
