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

namespace Ατομικη_εργασια_2_μοναδων_Π19032
{
    public partial class Form2 : Form
    {
        bool show_f1; //this variable controls what Form is going to be shown after the current form's closure

        public String name;
        public String path;
        public String difficulty;

        String secs; //Variables that are being passed from Form1

        void changeImagelocation(String path) //A function to change the line that is one before the last line in the file (this line cointains the new user's avatar path)
        {           
            string[] filelines = File.ReadAllLines("userdata.txt"); //Save all the filelines in an array
            filelines[filelines.Length - 2] = path; //Set the line we want to change equal to the new path
            File.WriteAllLines("userdata.txt", filelines); //Rewrite the lines in the file (now the file has the new avatarpath)
            this.path = path; //set the path variable of this Form equal to the new path 
        }

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(String name, String difficulty, String secs) //Constructor to set the variables from Form1
        {
            InitializeComponent();
            
            this.name = name;
            this.difficulty = difficulty;
            this.secs = secs; 
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            show_f1 = true; //this is letting the Form1 to show when we close Form2

            label3.Text = "The rules are simple.There is a dice icon that teleports to\n" + //Show the game rules text.The seconds to show, are depending on the difficulty
                          "random points inside the window with specific frequency\n" +
                          "(frequency depends on the difficulty).You have " + secs + " seconds\n" +
                          "to click the dice as many times as you can.Every time you\n" + "" +
                          "click the dice you get a score equal to the number that the\n" +
                          "dice represents.The teleport frequency,size and teleport area\n" +
                          "of the dice depends on the difficulty.\n\n" +
                          "                                       Good Luck!\n";

            pictureBox1.ImageLocation = "images/avatar1.png";
            pictureBox2.ImageLocation = "images/avatar2.png";
            pictureBox3.ImageLocation = "images/avatar3.png";
            pictureBox4.ImageLocation = "images/avatar4.png";
            pictureBox6.ImageLocation = "images/dice.png";  //Setting some picturebox images

            label1.Text = "Welcome " + name + "! Choose your avatar:";

            path = pictureBox1.ImageLocation; //set the avatar path with the default avatarpath (the first image)

            StreamWriter sw = File.AppendText("userdata.txt");

            try
            {
                sw.WriteLine(pictureBox1.ImageLocation + Environment.NewLine); //Write the user's default avatarpath in the file
            }

            catch (IOException ex)
            {
                MessageBox.Show("An exception has occured:\n" + ex.Message);
            }
            finally
            {
                sw.Close();
            }
        }
        
        //For each radiobutton, if it is checked (the next 4 radiobuttons exist in order for the user to choose an avatar image):
        
        //1) we are setting an empty image in the picturebox5 that represents the avatar that the user choosed
        //2) we update the user's avatar path in the file, with the corresponding avatarpath depends on the radiobutton checked
        //3) we enable the PLAY button as the user choosed an avatar

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                pictureBox5.ImageLocation = "";
                changeImagelocation(pictureBox1.ImageLocation);

                button1.BackColor = System.Drawing.Color.LightGreen;
                button1.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                pictureBox5.ImageLocation = "";
                changeImagelocation(pictureBox2.ImageLocation);

                button1.BackColor = System.Drawing.Color.LightGreen;
                button1.Enabled = true;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                pictureBox5.ImageLocation = "";
                changeImagelocation(pictureBox3.ImageLocation);

                button1.BackColor = System.Drawing.Color.LightGreen;
                button1.Enabled = true;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                pictureBox5.ImageLocation = "";
                changeImagelocation(pictureBox4.ImageLocation);

                button1.BackColor = System.Drawing.Color.LightGreen;
                button1.Enabled = true;
            }

                
        }
        
        private void button1_Click(object sender, EventArgs e) //By pressing the PLAY button
        {          
                Form3 f3 = new Form3(name, "0", path, difficulty); //we show Form3 (game form) with the user's details
                f3.Show();
                show_f1 = false; //we are closing this form but we dont want Form1 to appear.So we set show_f1 = false
                this.Close();
        }

        private void button2_Click(object sender, EventArgs e) //button to choose your own avatar
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) //if we select a correct file
            {
                pictureBox5.ImageLocation = openFileDialog1.FileName; //picturebox5 is showing user's selection
                changeImagelocation(openFileDialog1.FileName); //avatar path of user is beeing updated in the file

                button1.BackColor = System.Drawing.Color.LightGreen;
                button1.Enabled = true; //enable PLAY button as the user choosed an avatar
            }
            else //else if user didnt choose a file
            {
                MessageBox.Show("Please select an avatar image!");
                pictureBox5.ImageLocation = "";//show an empty image

                button1.BackColor = System.Drawing.Color.LightGray;
                button1.Enabled = false; //disable PLAY button as the user has not choosed an avatar
            }

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false; //disable all radiobuttons in order for the user to choose again an avatar
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {            
            if (show_f1) //if we close this form from X button show_f1 is true, so we return back to Form1
                ((Form1)Application.OpenForms[0]).Show();                            
        }
    }
}
