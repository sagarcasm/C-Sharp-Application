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
    public partial class employmentTable : Form
    {

        public employmentTable(object sender)
        {
            InitializeComponent();
            Employment employment = sender as Employment;
            for (int i = 0; i < employment.employmentTable.professionalEmploymentInformation.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = employment.employmentTable.professionalEmploymentInformation[i].degree;
                dataGridView1.Rows[i].Cells[1].Value = employment.employmentTable.professionalEmploymentInformation[i].employer;
                dataGridView1.Rows[i].Cells[2].Value = employment.employmentTable.professionalEmploymentInformation[i].city;
                dataGridView1.Rows[i].Cells[3].Value = employment.employmentTable.professionalEmploymentInformation[i].title;
                dataGridView1.Rows[i].Cells[4].Value = employment.employmentTable.professionalEmploymentInformation[i].startDate;
            }
        }
    }
}
