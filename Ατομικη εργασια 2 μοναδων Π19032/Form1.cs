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
    public partial class Form1 : Form
    {
        String difficulty;
        String secs;    //variables that are going to be passed in other forms depending on the difficulty the user choosed

        public Form1()
        {
            InitializeComponent();
        }
        public void enablerb3() //this function is being called when a user reach a score of 200 or more
        {
            radioButton3.Enabled = true;
            radioButton3.Text = "Hard";  //Enables the hard difficulty
        }

        private void button1_Click(object sender, EventArgs e) //if the user is able to press the button, this means that the name is valid.
                                                               //If so, we are checking if it is an existing name or a new name by searching the file
        {
            String name = textBox1.Text;  
            String highscore;
            String avatarpath;

            StreamReader sr = new StreamReader("userdata.txt");

            try
            {
                while (!sr.EndOfStream) //while we are not at the end of the file
                {
                    if (sr.ReadLine().Equals(name)) //we are checking if each line is equal to the name that the user gave
                    {
                        highscore = sr.ReadLine();
                        avatarpath = sr.ReadLine(); //if it is, we are getting the values of highscore and avatarpath of this user, by reading the next 2 lines

                        MessageBox.Show("Welcome back " + name + "!");

                        sr.Close();
                        Form5 f5 = new Form5(name, highscore, avatarpath, difficulty); //Opening Form5 (login form) with the values we found and with the diffculty 
                        f5.Show();                                                     //the user setted
                        this.Hide();

                        return;
                    }
                }
            }
            catch (IOException ex)
            {
                sr.Close();
                MessageBox.Show("An exception has occured:\n" + ex.Message);
                return;
            }

            sr.Close();
            StreamWriter sw = File.AppendText("userdata.txt");

            try  //if there is not that name in the file, we appending it also with a highscore 0
            {
                
                sw.WriteLine(name);
                sw.WriteLine(0);
                sw.Close();

                Form2 f2 = new Form2(name, difficulty, secs); //Opening Form2 (new user form) with the new name,difficulty and seconds to show on the rules of the game
                f2.Show();
                this.Hide();

            }
            catch (IOException ex)
            {
                sw.Close();
                MessageBox.Show("An exception has occured:\n" + ex.Message);
                
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "images/i.png";
            pictureBox2.ImageLocation = "images/dice.png";
            pictureBox3.ImageLocation = "images/dice.png"; //setting the paths of picturebox images 

            secs = "30";
            difficulty = "Easy"; //The default difficulty is easy, so we are setting the corresponding values
            
            StreamReader sr = new StreamReader("userdata.txt");

            try
            {
                String line;

                while (!sr.EndOfStream) //while we are not at the end of the file
                {
                    line = sr.ReadLine();

                    if (line.All(Char.IsDigit) && !line.Equals("") && int.Parse(line) >= 200) //we are reading each line to confirm if there is any highscore >=200
                    {                                                                        
                        radioButton3.Text = "Hard";
                        radioButton3.Enabled = true;  //if there is, we are enabling the hard difficulty
                        break;
                    }
                        
                }
            }catch(IOException ex)
            {
                MessageBox.Show("An exception has occured: \n" + ex.Message);
            }
            finally
            {
                sr.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //every time the text of the name changes, check if it is valid

            if (textBox1.Text.Equals("") || textBox1.Text.StartsWith(" ") || textBox1.Text.All(Char.IsDigit) || textBox1.Text.Contains("."))
            {
                button1.BackColor = System.Drawing.Color.LightGray;
                button1.Enabled = false; //if it is not, disable the button
            }
            else
            {
                button1.BackColor = System.Drawing.Color.LightGreen;
                button1.Enabled = true; //if it is, enable the button               
            }
        }

        //For each radiobutton, if it is checked, we are setting the corresponding difficulty values

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {               
                secs = "30";
                difficulty = "Easy";
            }               
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                secs = "20";
                difficulty = "Medium";
            }
                
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                secs = "15";
                difficulty = "Hard";
            }
                
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            Form4 f4 = new Form4(); //Show leaderboard form
            f4.Show(); 
        }


        private void pictureBox1_Click(object sender, EventArgs e) //Text to show, when we press the name info icon
        {
            MessageBox.Show("Input your name in the textbox in order to login with an existing profile or make a new profile.\n\n" +
                            "Your name must cointain the most 10 characters.\nYour name cant contain any fullstops.\nYour name cant be only digits.\n" +
                            "Your name cant start with space.");
        }
    }
}
