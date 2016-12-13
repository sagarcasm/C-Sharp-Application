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
    public partial class ResearchByFaculty : Form
    {
        public ResearchByFaculty(object sender)
        {
            InitializeComponent();
            ByFaculty researchbyfaculty = sender as ByFaculty;
            label1.Text = researchbyfaculty.facultyName;
            String details = "";
            int count = 1;

            for (int i = 0; i < researchbyfaculty.citations.Count(); i++)
            {
                details += count + ": " + researchbyfaculty.citations[i] + Environment.NewLine;
                count++;
            }

            richTextBox1.Text = details;
        }
    }
}
