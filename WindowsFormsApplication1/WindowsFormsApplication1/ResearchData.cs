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
    public partial class ResearchData : Form
    {
        public ResearchData(object sender)
        {
            InitializeComponent();
            ByInterestArea research = sender as ByInterestArea;
            label1.Text = research.areaName;
            String details = "";
            int count = 1;

            for (int i = 0; i < research.citations.Count(); i++)
            {
                details += count+ ": " + research.citations[i] + Environment.NewLine;
                count++;
            }

            richTextBox1.Text = details;


        }
    }
}
