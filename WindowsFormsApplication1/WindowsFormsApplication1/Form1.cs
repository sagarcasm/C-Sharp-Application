using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //class variables


        public Form1()
        {
            InitializeComponent();
            //get the about data
            //getabout();
            //get the hone page data
            getHome();

            //get the undergraduate degress details
            getUndergraddetails();

            //get the undergraduate degress details
            getGraddetails();

            //get the ug minors data for the minors tabs
            getugminors();

            //get the employment data
            getemploymentdata();

            //get people data 
            getpeopledata();

            //get the research data api
            getreasearch();

            //get social links
            getsociallinks();

            //get the contact us form
            webBrowser2.Navigate("http://ist.rit.edu/api/contactForm/");

            //get the map api data and how it in the web browser
            webBrowser1.Navigate("http://ist.rit.edu/api/map/");

            //webBrowser3.Navigate("http://people.rit.edu/sds8231/");



        }

        private string getRestData(string url)
        {
            string baseUri = "http://ist.rit.edu/api";

            // connect to the API
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUri + url);
            try
            {
                WebResponse response = request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException we)
            {
                // Something goes wrong, get the error response, then do something with it
                WebResponse err = we.Response;
                using (Stream responseStream = err.GetResponseStream())
                {
                    StreamReader r = new StreamReader(responseStream, Encoding.UTF8);
                    string errorText = r.ReadToEnd();
                    // display or log error
                    Console.WriteLine(errorText);
                }
                throw;
            }
        }//end of method


        //getting the home tab for the application

        private void getHome()
        {
            string about = getRestData("/about/");
            About aboutdata = JToken.Parse(about).ToObject<About>();
            label4.Text = aboutdata.title;
            homedescription.Text = aboutdata.description;
            richTextBox18.Text = aboutdata.quote;
            quoteAuthorlabel.Text = "- " + aboutdata.quoteAuthor;

        }

        //getting the undregrad details for the tab
        public void getUndergraddetails()
        {
            string degree = getRestData("/degrees/");
            Degree degreedata = JToken.Parse(degree).ToObject<Degree>();
            //creating three button array for the three degreess
            string[] ugbuttonvalue = new string[3];
            string[] ugdescpvalue = new string[3];
            string[] ugtags = new string[3];

            int i = 0;
            //pushing the json value to the degree array
            foreach (Undergraduate ug in degreedata.undergraduate)
            {
                ugbuttonvalue[i] = ug.title;
                ugdescpvalue[i] = ug.description;
                ugtags[i] = ug.degreeName;
                i++;
            }//end of foreach

            //adding the button values
            button1.Text = ugbuttonvalue[0];
            button2.Text = ugbuttonvalue[1];
            button5.Text = ugbuttonvalue[2];

            button1.Tag = ugtags[0];
            button2.Tag = ugtags[1];
            button5.Tag = ugtags[2];

            richTextBox2.Text = ugdescpvalue[0];
            richTextBox3.Text = ugdescpvalue[1];
            richTextBox4.Text = ugdescpvalue[2];

            //adding the button click event for the first load of the button
            button1_Click(button1, null);
        }//end of method

        //getting the grad tab
        public void getGraddetails()
        {
            string degree = getRestData("/degrees/");
            Degree degreedata = JToken.Parse(degree).ToObject<Degree>();

            //init the button array for the degree details
            string[] gbuttonvalue = new string[3];
            string[] gdescpvalue = new string[3];
            string[] gtags = new string[3];

            int i = 0;
            string gradAdvCert = "";
            string gradAdvCertDescrip = "";
            //pushing the json value to the degree array
            foreach (Graduate g in degreedata.graduate)
            {
                if (i < 3)
                {
                    gbuttonvalue[i] = g.title;
                    gdescpvalue[i] = g.description;
                    gtags[i] = g.degreeName;
                    i++;
                }//if (i < 3)
                else
                {
                    gradAdvCert = g.degreeName;

                    for (int j = 0; j < g.availableCertificates.Count; j++)
                    {
                        gradAdvCertDescrip += g.availableCertificates[j] + Environment.NewLine;
                    }//for (int j = 0; j < g.availableCertificates.Count; j++)
                }//else

            }//foreach (Graduate g in degreedata.graduate)

            button6.Text = gbuttonvalue[0];
            button7.Text = gbuttonvalue[1];
            button8.Text = gbuttonvalue[2];

            button6.Tag = gtags[0];
            button7.Tag = gtags[1];
            button8.Tag = gtags[2];

            richTextBox6.Text = gdescpvalue[0];
            richTextBox7.Text = gdescpvalue[1];
            richTextBox8.Text = gdescpvalue[2];

            label5.Text = gradAdvCert.ToUpper();
            richTextBox9.Text = gradAdvCertDescrip;

            button6_Click(button6, null);
        }//end of method

        //event for the button click
        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string btnTag = (string)btn.Tag;
            string degree = getRestData("/degrees/undergraduate/degreeName=" + btnTag);
            Undergraduate degreedata = JToken.Parse(degree).ToObject<Undergraduate>();

            string details = "";
            for (int i = 0; i < degreedata.concentrations.Count; i++)
            {
                details += degreedata.concentrations[i] + Environment.NewLine;
            }//for (int i = 0; i < degreedata.concentrations.Count; i++)
            richTextBox1.Text = details;
        }//private void button1_Click(object sender, EventArgs e)

        //button click for the graduate programs
        private void button6_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string btnTag = (string)btn.Tag;
            //Console.Write(btnTag);
            string degree = getRestData("/degrees/graduate/degreeName=" + btnTag);
            Graduate degreedata = JToken.Parse(degree).ToObject<Graduate>();

            string details = "";
            for (int i = 0; i < degreedata.concentrations.Count; i++)
            {
                details += degreedata.concentrations[i] + Environment.NewLine;
            }//for (int i = 0; i < degreedata.concentrations.Count; i++)
            richTextBox5.Text = details;
        }//private void button6_Click(object sender, EventArgs e)

        //get the cootable on the button click
        private void button3_Click(object sender, EventArgs e)
        {
            getCoopDetails();
        }//private void button3_Click(object sender, EventArgs e)

        //button click for the employment table
        private void button4_Click(object sender, EventArgs e)
        {
            getEmplyomentTable();
        }//private void button4_Click(object sender, EventArgs e)

        //get coopdetails for the table layout
        public void getCoopDetails()
        {
            //get data for employment
            string employment = getRestData("/employment/");
            Employment employmentdata = JToken.Parse(employment).ToObject<Employment>();

            Cooptable cooptable = new Cooptable(employmentdata);
            //calling the coop dialog class
            cooptable.ShowDialog();

        }//public void getCoopDetails()

        //get employment tablefor after the click of the button
        public void getEmplyomentTable()
        {
            //get data for employment
            string employment = getRestData("/employment/");
            Employment employmentdata = JToken.Parse(employment).ToObject<Employment>();
            //class to show the cooptable
            employmentTable cooptable = new employmentTable(employmentdata);
            cooptable.ShowDialog();

        }//public void getEmplyomentTable()

        //get ug minors data
        public void getugminors()
        {
            string minors = getRestData("/minors/");
            Minors minordata = JToken.Parse(minors).ToObject<Minors>();

            //get the button data and add the text tag
            string[] mbuttonname = new string[8];
            string[] mdescptitle = new string[8];
            string[] mdescrp = new string[8];

            int i = 0;
            string courses = "";
            foreach (UgMinor mi in minordata.UgMinors)
            {
                mbuttonname[i] = mi.name;
                mdescptitle[i] = mi.title;
                mdescrp[i] = mi.description;
                for (int j = 0; j < mi.courses.Count; j++)
                {
                    courses += mi.courses[j] + Environment.NewLine;
                }
                //richTextBox5.Text = details;
                i++;
            }//foreach (UgMinor mi in minordata.UgMinors)

            button9.Text = mdescptitle[0];
            button10.Text = mdescptitle[1];
            button11.Text = mdescptitle[2];
            button12.Text = mdescptitle[3];
            button13.Text = mdescptitle[4];
            button14.Text = mdescptitle[5];
            button15.Text = mdescptitle[6];
            button16.Text = mdescptitle[7];

            //setting the tag name for buttons of them minors
            button9.Tag = mbuttonname[0];
            button10.Tag = mbuttonname[1];
            button11.Tag = mbuttonname[2];
            button12.Tag = mbuttonname[3];
            button13.Tag = mbuttonname[4];
            button14.Tag = mbuttonname[5];
            button15.Tag = mbuttonname[6];
            button16.Tag = mbuttonname[7];

            //button click event for the inital click event
            button9_Click(button9, null);
        }//public void getugminors()


        private void button9_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("button9.Click was raised.");
            Button btn = sender as Button;
            string btnTag = (string)btn.Tag;
            //Console.Write(btnTag);
            string minors = getRestData("/minors/UgMinors/name=" + btnTag);
            UgMinor minorsdata = JToken.Parse(minors).ToObject<UgMinor>();

            label6.Text = minorsdata.title;
            label7.Text = minorsdata.name;
            richTextBox10.Text = minorsdata.description;

            //remove all the list items every time a click event occurs
            listBox1.Items.Clear();

            string courses = "";
            for (int i = 0; i < minorsdata.courses.Count; i++)
            {
                //appending the data to the newline
                courses += minorsdata.courses[i] + Environment.NewLine;
                listBox1.Items.Add(minorsdata.courses[i]);
            }//for (int i = 0; i < minorsdata.courses.Count; i++)
        }// private void button9_Click(object sender, EventArgs e)


        //get tge data for the eompyment tab
        public void getemploymentdata()
        {
            string employment = getRestData("/employment/");
            Employment employmentdata = JToken.Parse(employment).ToObject<Employment>();

            label9.Text = employmentdata.introduction.title;

            for (int i = 0; i < employmentdata.introduction.content.Count; i++)
            {
                if (i == 0)
                {
                    label10.Text = employmentdata.introduction.content[i].title;
                    richTextBox12.Text = employmentdata.introduction.content[i].description;
                }//if (i == 0)
                else
                {
                    label11.Text = employmentdata.introduction.content[i].title;
                    richTextBox13.Text = employmentdata.introduction.content[i].description;
                }//else
            }//for (int i=0; i < employmentdata.introduction.content.Count; i++ )

            label12.Text = employmentdata.degreeStatistics.title;

            for (int i = 0; i < employmentdata.degreeStatistics.statistics.Count; i++)
            {
                //swtich case to change the text description according to the value
                switch (i)
                {
                    case 0:
                        label13.Text = employmentdata.degreeStatistics.statistics[i].value;
                        richTextBox14.Text = employmentdata.degreeStatistics.statistics[i].description;
                        break;
                    case 1:
                        label14.Text = employmentdata.degreeStatistics.statistics[i].value;
                        richTextBox15.Text = employmentdata.degreeStatistics.statistics[i].description;
                        break;
                    case 2:
                        label15.Text = employmentdata.degreeStatistics.statistics[i].value;
                        richTextBox16.Text = employmentdata.degreeStatistics.statistics[i].description;
                        break;
                    case 3:
                        label16.Text = employmentdata.degreeStatistics.statistics[i].value;
                        richTextBox17.Text = employmentdata.degreeStatistics.statistics[i].description;
                        break;
                    default:
                        break;
                }//switch (i)
            }//for (int i = 0; i < employmentdata.degreeStatistics.statistics.Count; i++)
            //onselectindexchange
        }//public void getemploymentdata()

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listbx = sender as ListBox;
            string listText = (string)listbx.Text;
            Console.Write(listText);
            string courseData = getRestData("/course/courseID=" + listText);
            MinorCourses minorsdata = JToken.Parse(courseData).ToObject<MinorCourses>();

            //adding the class for the form CourseDetails
            CourseDetails coursedetails = new CourseDetails(minorsdata);
            coursedetails.ShowDialog();
        }// private void listBox1_SelectedIndexChanged(object sender, EventArgs e)


        //get the peoples tab data
        public void getpeopledata()
        {
            string faculty = getRestData("/people/");
            People facultydata = JToken.Parse(faculty).ToObject<People>();
            PictureBox[] pics = new PictureBox[50];
            int brh = 0;

            //dynamic addition of the buttons to the flowlayout
            for (int i = 0; i < facultydata.faculty.Count(); i++)
            {
                Button button = new Button();
                button.Name = facultydata.faculty[i].username;
                button.Text = facultydata.faculty[i].name;
                button.Margin = new Padding(10);
                button.TabIndex = 0;
                button.Click += new EventHandler(getfacultydata);
                button.Location = new Point(953, 95 + brh);
                button.Size = new Size(100, 60);
                //flowpanel name
                flowLayoutPanel1.Controls.Add(button);
            }//for (int i = 0; i< facultydata.faculty.Count(); i++)

            //dynamic addition of the buttons to the flow layout for staff
            for (int i = 0; i < facultydata.staff.Count(); i++)
            {
                Button button = new Button();
                button.Name = facultydata.staff[i].username;
                button.Text = facultydata.staff[i].name;
                button.Margin = new Padding(10);
                button.TabIndex = 0;
                button.Location = new Point(953, 95 + brh);
                button.Click += new EventHandler(getstaffdata);
                button.Size = new Size(100, 60);
                flowLayoutPanel2.Controls.Add(button);
            }//for (int i = 0; i < facultydata.staff.Count(); i++)


        }//end of method

        //function to add the event on the button click for the facultydata
        void getfacultydata(Object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string username = clickedButton.Name;
            string faculty = getRestData("/people/faculty/username=" + username);
            PeopleDetails facultydata = JToken.Parse(faculty).ToObject<PeopleDetails>();
            PeopleData peopledetails = new PeopleData(facultydata);
            peopledetails.ShowDialog();
        }//void getfacultydata(Object sender, EventArgs e)

        //function to add the event on the button click for the staffdata
        void getstaffdata(Object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string username = clickedButton.Name;
            string faculty = getRestData("/people/staff/username=" + username);
            PeopleDetails facultydata = JToken.Parse(faculty).ToObject<PeopleDetails>();
            PeopleData peopledetails = new PeopleData(facultydata);
            peopledetails.ShowDialog();
        }//void getstaffdata(Object sender, EventArgs e)

        //get research data
        public void getreasearch()
        {
            string research = getRestData("/research/");
            Research researchdata = JToken.Parse(research).ToObject<Research>();

            int brh = 0;
            //adding the dynamic data for the research buttons set
            for (int i = 0; i < researchdata.byInterestArea.Count(); i++)
            {
                Button button = new Button();
                button.Name = researchdata.byInterestArea[i].areaName;
                button.Text = researchdata.byInterestArea[i].areaName;
                button.Margin = new Padding(10);
                button.TabIndex = 0;
                button.Click += new EventHandler(getresearchdatabyinterest);
                button.Location = new Point(953, 95 + brh);
                button.Size = new Size(100, 60);
                //flowpanel  data appending
                flowLayoutPanel3.Controls.Add(button);
            }//for (int i = 0; i < researchdata.byInterestArea.Count(); i++)

            //adding the button into the flow layout panel by faculty name
            for (int i = 0; i < researchdata.byFaculty.Count(); i++)
            {
                Button button = new Button();
                button.Name = researchdata.byFaculty[i].username;
                button.Text = researchdata.byFaculty[i].facultyName;
                button.Margin = new Padding(10);
                button.TabIndex = 0;
                button.Click += new EventHandler(getresearchdatabyfaculty);
                button.Location = new Point(953, 95 + brh);
                button.Size = new Size(100, 60);
                //flowpanel name
                flowLayoutPanel4.Controls.Add(button);
            }//for (int i = 0; i < researchdata.byFaculty.Count(); i++)
        }//end of public void getreasearch()

        

        //get the data for research by interest button event
        void getresearchdatabyinterest(Object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string researchname = clickedButton.Name;

            string researchinterest = getRestData("/research/byInterestArea/areaName=" + researchname);
            ByInterestArea researchdata = JToken.Parse(researchinterest).ToObject<ByInterestArea>();
            ResearchData researchdailog = new ResearchData(researchdata);
            researchdailog.ShowDialog();
        }//void getresearchdatabyinterest(Object sender, EventArgs e)

        //get the research data by faculty
        void getresearchdatabyfaculty(Object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string researchname = clickedButton.Name;

            string researchinterest = getRestData("/research/byFaculty/username=" + researchname);
            ByFaculty researchdata = JToken.Parse(researchinterest).ToObject<ByFaculty>();
            //class object to pass the data to another view
            ResearchByFaculty researchdailog = new ResearchByFaculty(researchdata);
            researchdailog.ShowDialog();
        }// void getresearchdatabyfaculty(Object sender, EventArgs e)

        //getting the social links to for last tab
        public void getsociallinks()
        {
            string footer = getRestData("/footer/");
            Footer footerdata = JToken.Parse(footer).ToObject<Footer>();

            label27.Text = footerdata.social.title;
            richTextBox11.Text = footerdata.social.tweet;
            label29.Text = footerdata.social.by;
        }//public void getsociallinks()

        //button click event to show the news
        private void button17_Click(object sender, EventArgs e)
        {
            string news = getRestData("/news/");
            News newsdata = JToken.Parse(news).ToObject<News>();
            //get the class object to open the news on the other view
            Newstable coursedetails = new Newstable(newsdata);
            coursedetails.ShowDialog();

        }// private void button17_Click(object sender, EventArgs e)
    }
}
