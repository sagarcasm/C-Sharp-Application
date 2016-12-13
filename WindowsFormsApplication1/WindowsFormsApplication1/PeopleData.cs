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
    public partial class PeopleData : Form
    {
        public PeopleData(object sender)
        {
            InitializeComponent();
            PeopleDetails peopledata = sender as PeopleDetails;
            pictureBox1.ImageLocation = peopledata.imagePath;
            label1.Text = peopledata.name;
            label2.Text = peopledata.title;
            label3.Text = "Website: " + peopledata.website;
            label4.Text = "Office: " + peopledata.office;
            label5.Text = "Email: " + peopledata.email;
        }
    }
}
