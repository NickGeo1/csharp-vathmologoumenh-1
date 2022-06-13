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
    public partial class Form5 : Form
    {
        bool show_f1; //this variable controls what Form is going to be shown after the current form's closure

        String name;
        String path;
        String highscore;
        String difficulty; //user and game details

        void changeFileLine(String find, String replace_with, bool replace_name) //this function replaces "find" line in file with "replace_with" string 
                                                                                //if replace_name is true.Else it replaces the line that is 2 lines bellow "find"
        {
            String[] filelines = File.ReadAllLines("userdata.txt"); //save file lines in filelines

            for(int i=0; i<filelines.Length; i++) //in a loop for all lines
            {
                if (filelines[i].Equals(find) && replace_name) //in case we want to replace the name, we are executing this(must replace_name = true)
                {
                    filelines[i] = replace_with;
                    break;

                }else if(filelines[i].Equals(find) && !replace_name)//in case we want to replace the avatarpath, we are executing this(must replace_name = false)
                {
                    filelines[i+2] = replace_with;
                    break;
                }
            }

            File.WriteAllLines("userdata.txt", filelines); //Write changes back to file
        }

        public Form5()
        {
            InitializeComponent();
        }

        public Form5(String name, String highscore , String path, String difficulty) //Constructor to pass user and game details
        {
            InitializeComponent();

            this.name = name;
            this.highscore = highscore;
            this.path = path;
            this.difficulty = difficulty;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "images/avatar1.png";
            pictureBox2.ImageLocation = "images/avatar2.png";
            pictureBox3.ImageLocation = "images/avatar3.png";
            pictureBox4.ImageLocation = "images/avatar4.png";
            pictureBox5.ImageLocation = path; 
            pictureBox6.ImageLocation = "images/i.png"; //set some paths for images in pictureboxes.picturebox5 is always representing the user's current avatar

            //At the beginning we check the radiobutton that corresponds to user's avatar

            if (path.Equals("images/avatar1.png"))
            {
                radioButton1.Checked =  true;

            }else if (path.Equals("images/avatar2.png"))
            {
                radioButton2.Checked = true;
            }
            else if (path.Equals("images/avatar3.png"))
            {
                radioButton3.Checked = true;
            }
            else if (path.Equals("images/avatar4.png"))
            {
                radioButton4.Checked = true;
            }

            show_f1 = true; //this is letting the Form1 to show when we close Form5

            label1.Text = name;
            label5.Text = highscore;
        }

        //For each radiobutton(if it is checked):

        //1)We are showing user's avatar choice that corresponds to the checked radiobutton in picturebox5
        //2)We are changing the user's avatarpath in file using changeFileLine function
        //3)We are setting the new user's avatar path 
        //4)We enable the PLAY button as the user choosed an avatar

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                pictureBox5.ImageLocation = pictureBox1.ImageLocation;
                changeFileLine(name, pictureBox1.ImageLocation, false);

                path = pictureBox1.ImageLocation;

                button3.BackColor = System.Drawing.Color.LightGreen;
                button3.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                pictureBox5.ImageLocation = pictureBox2.ImageLocation;
                changeFileLine(name, pictureBox2.ImageLocation, false);

                path = pictureBox2.ImageLocation;

                button3.BackColor = System.Drawing.Color.LightGreen;
                button3.Enabled = true;
            }
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                pictureBox5.ImageLocation = pictureBox3.ImageLocation;
                changeFileLine(name, pictureBox3.ImageLocation, false);

                path = pictureBox3.ImageLocation;

                button3.BackColor = System.Drawing.Color.LightGreen;
                button3.Enabled = true;
            }
        }

        private void radioButton4_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                pictureBox5.ImageLocation = pictureBox4.ImageLocation;
                changeFileLine(name, pictureBox4.ImageLocation, false);

                path = pictureBox4.ImageLocation;

                button3.BackColor = System.Drawing.Color.LightGreen;
                button3.Enabled = true;
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e) //As the text change in textbox
        {
            //We check if it is valid and if it is not equal to the previous name

            if (!textBox1.Text.Equals("") && !textBox1.Text.StartsWith(" ") && !textBox1.Text.All(Char.IsDigit) && !textBox1.Text.Contains(".") && !textBox1.Text.Equals(name))
            {
                button1.BackColor = System.Drawing.Color.LightGreen; 
                button1.Enabled = true; //if everything from the above is true, enable the button
            }
            else
            {
                button1.BackColor = System.Drawing.Color.LightGray;
                button1.Enabled = false; //else dissable the button
            }
        }
        private void button1_Click(object sender, EventArgs e) //change name
        {
            changeFileLine(name, textBox1.Text, true); //function responsible for the name change in file
            MessageBox.Show("Name succesfully changed from " + name + " to " + textBox1.Text);
            name = textBox1.Text;
            label1.Text = textBox1.Text;

            button1.BackColor = System.Drawing.Color.LightGray;
            button1.Enabled = false; //disable the button until the user change the textbox input
        }

        private void button2_Click(object sender, EventArgs e) //load local avatar image
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) //if load successfull
            {
                pictureBox5.ImageLocation = openFileDialog1.FileName; //Show user's choice in picturebox5
                changeFileLine(name, openFileDialog1.FileName, false); //we change the user's avatarpath in file using changeFileLine function
                path = openFileDialog1.FileName; //we set the new avatarimage 

                button3.BackColor = System.Drawing.Color.LightGreen;
                button3.Enabled = true; //enable PLAY button as the user choosed an avatar image
            }
            else //else load was not successfull 
            {
                MessageBox.Show("Please select an avatar image!");
                pictureBox5.ImageLocation = ""; //show an empty avatar image in picturebox5

                button3.BackColor = System.Drawing.Color.LightGray;
                button3.Enabled = false; //disable PLAY button
            }

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false; //uncheck all radiobuttons in order for the user to choose avatar image again
        }

        private void button3_Click(object sender, EventArgs e) //PLAY button
        {
            Form3 f3 = new Form3(name, highscore, path, difficulty); //Opening Form3 (game) with user and game details
            f3.Show();
            show_f1 = false; //we are closing this form but we dont want Form1 to appear.So we set show_f1 = false
            this.Close();
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (show_f1) //if we close this form from X button show_f1 is true, so we return back to Form1
                ((Form1)Application.OpenForms[0]).Show();
            
        }

        private void pictureBox6_Click(object sender, EventArgs e) //change name info button text
        {
            MessageBox.Show("Input your new name in the textbox in order to change your old name.\n\n" +
                            "Your name must cointain the most 10 characters.\nYour name cant contain any fullstops.\nYour name cant be only digits.\n" +
                            "Your name cant start with space.\nYour new name cant be the same as your old name.");
        }
    }
}
