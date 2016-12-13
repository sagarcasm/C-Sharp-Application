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
    public partial class CourseDetails : Form
    {
        public CourseDetails(object sender)
        {
            InitializeComponent();
            MinorCourses minor = sender as MinorCourses;
            label1.Text = minor.courseID;
            label2.Text = minor.title;
            richTextBox1.Text = minor.description;

        }

        private void CourseDetails_Load(object sender, EventArgs e)
        {
            
        }
    }
}
