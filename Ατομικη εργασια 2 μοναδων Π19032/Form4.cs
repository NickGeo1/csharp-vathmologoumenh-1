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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            label1.Text = "Leaderboard";

            pictureBox6.ImageLocation = "images/dice.png";
            pictureBox7.ImageLocation = "images/dice.png";
            pictureBox8.ImageLocation = "images/dice.png"; //set image paths for some pictureboxes 

            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;
            pictureBox8.Visible = true; //make user avatars in leaderboard visible(they are hiding when we choose to see all users created)

            StreamReader sr = new StreamReader("userdata.txt");

            List<int> hs = new List<int>(); //An integer list where we save all highscores in file
            String line;

            try
            {
                while (!sr.EndOfStream) //while we are not at the end of the file
                {
                    line = sr.ReadLine(); //read line
                    if (line.All(Char.IsDigit) && !line.Equals("")) //if line represents a highscore
                    {
                        hs.Add(int.Parse(line)); //add it to the list
                    }
                        
                }

                sr.Close();                
            }
            catch (IOException ex)
            {
                MessageBox.Show("An exception has occured: " + ex.Message);
                sr.Close();
                this.Close();
                return;
            }

            hs.Sort(); //sort highscore list(now our highscores are on ascending order)

            String[] best_high_scores = new String[5] { "", "", "", "", "" }; //Array to save the best 5 or less highscores

            int i = hs.Count - 1; //At the beginning, i has the index of the last list element
            int j = 0;

            while (i>=0 && i>= hs.Count - 5) //while index has not reached the beginning of the list and also has not reached the element that is 4 elements
                                             //before the last
            {
                best_high_scores[j] = hs[i].ToString(); //set in descending order the 5 or less best highscores to array
                j++;
                i--;
            }
            
            String[] best_names = new String[5] { "", "", "", "", "" }; //Array to save the 5 or less best names

            String[] filelines = File.ReadAllLines("userdata.txt"); //Array to save file's lines

            bool found_0 = false , found_1 = false, found_2 = false, found_3 = false, found_4 = false; //boolean variables in order to control loop bellow

            for (int k = 0; k < filelines.Length; k++) //We are checking each file line in this loop
            {
                if (!best_high_scores[0].Equals("") && filelines[k].Equals(best_high_scores[0]) && !found_0) //if you find a best highscore and found_x is false
                {
                   best_names[0] = filelines[k - 1] + ": "; //save that highscore's username in the corresponding point in best_names array
                   pictureBox1.ImageLocation = filelines[k + 1]; //set that player's avatar in leaderboard
                   found_0 = true; //in case we have 2 same best highscores, we set these variables true in order to avoid the second best user to replace the first
                }
                else if (!best_high_scores[1].Equals("") && filelines[k].Equals(best_high_scores[1]) && !found_1)
                {
                   best_names[1] = filelines[k - 1] + ": ";
                   pictureBox2.ImageLocation = filelines[k + 1];
                   found_1 = true;
                }
                else if (!best_high_scores[2].Equals("") && filelines[k].Equals(best_high_scores[2]) && !found_2)
                {
                   best_names[2] = filelines[k - 1] + ": ";
                   pictureBox3.ImageLocation = filelines[k + 1];
                   found_2 = true;
                }
                else if (!best_high_scores[3].Equals("") && filelines[k].Equals(best_high_scores[3]) && !found_3)
                {
                   best_names[3] = filelines[k - 1] + ": ";
                   pictureBox4.ImageLocation = filelines[k + 1];
                   found_3 = true;
                }
                else if (!best_high_scores[4].Equals("") && filelines[k].Equals(best_high_scores[4]) && !found_4)
                {
                   best_names[4] = filelines[k - 1] +": ";
                   pictureBox5.ImageLocation = filelines[k + 1];
                   found_4 = true;
                }
            }

            richTextBox1.Text = "1. " + best_names[0] + best_high_scores[0] + Environment.NewLine + Environment.NewLine  //show results 
                               + "2. " + best_names[1]  + best_high_scores[1] + Environment.NewLine + Environment.NewLine
                               + "3. " + best_names[2]  + best_high_scores[2] + Environment.NewLine + Environment.NewLine
                               + "4. " + best_names[3]  + best_high_scores[3] + Environment.NewLine + Environment.NewLine
                               + "5. " + best_names[4]  + best_high_scores[4]; 
        }

        private void button2_Click(object sender, EventArgs e) //reload best users
        {
            this.Form4_Load(sender,e); 
        }

        private void button1_Click(object sender, EventArgs e) //Show all users created
        {
            richTextBox1.Text = ""; //at the beginning set richtextbox empty  
            label1.Text = "Total users created";

            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox8.Visible = false; //hide all the best user avatars


            StreamReader sr = new StreamReader("userdata.txt");

            String s;
            int i = 0;

            while (!sr.EndOfStream) //while we are not at the end of the file
            {               
                s = sr.ReadLine(); //read line
                if(!s.All(Char.IsDigit) && !s.Equals("") && !s.Contains(".")) //if line equals to a name
                {
                    i++;
                    richTextBox1.AppendText(i.ToString() + ". " + s); //Append i-name to richtextbox

                }else if(s.All(Char.IsDigit) && !s.Equals("")) //else if line equals to i-user's highscore 
                {
                    richTextBox1.AppendText(": " + s);
                    richTextBox1.AppendText(Environment.NewLine); //append it in richtextbox
                }           
            }

            sr.Close();
        }
    }
}
