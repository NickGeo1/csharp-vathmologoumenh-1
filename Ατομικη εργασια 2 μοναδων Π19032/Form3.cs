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
    public partial class Form3 : Form
    {

        public String name;
        public String highscore;
        public String path;
        public String difficulty; //user and game details

        static int count; //count variable for the 3 second countdown before the game begin
        static int countdown; //countdown variable for the time remaining
        static int randomimage; //a variable to set a random number between 1-6 to load the corresppnding dice image and add the corresponding score if you click it
        int score; //a variable for the current score

        String[] filelines; //an array in which we are going to save the file's lines

        Random r; //random object
        
        void prepareGame(String difficulty) //this function is taking the difficulty as a parameter and makes the corresponding settings to the game
        {
            pictureBox3.Enabled = false;
            pictureBox3.Visible = false; //disable and hide dice image

            timer1.Enabled = false;
            timer3.Enabled = false; //beggin with the time remaining timer and dice timer off

            count = 0;
            score = 0; //on every load of the form we are setting score and count 0           

            switch (difficulty)
            {
                //For each difficulty we are making the corresponding changes for:

                //1) the countdown (time remaining)
                //2) the panel location
                //3) the panel dimensions
                //4) the dice timer interval (the frequency with which the dice teleports)

                case "Easy":
                    countdown = 30;
                    timer1.Interval = 2000;
                    panel1.Location = new Point(236, 104);
                    panel1.Width = 489;
                    panel1.Height = 438;
                    label5.Text = "Time: 30";
                    break;

                case "Medium":
                    countdown = 20;
                    timer1.Interval = 1500;
                    panel1.Location = new Point(103, 104);
                    panel1.Width = 778;
                    panel1.Height = 438;
                    label5.Text = "Time: 20";
                    break;

                case "Hard":
                    countdown = 15;
                    timer1.Interval = 1000;
                    panel1.Location = new Point(16, 104);
                    panel1.Width = 976;
                    panel1.Height = 459;
                    label5.Text = "Time: 15";
                    break;
            }

            int best_hs = 0;
            String best_name = "";
            String best_path = ""; //variables to set the best user's details

            filelines = File.ReadAllLines("userdata.txt"); //save the file's lines in filelines array

            for (int i = 0; i < filelines.Length; i++) //in a loop that executes as many times as the fileline's length
            {
                if (filelines[i].All(Char.IsDigit) && !filelines[i].Equals("") && int.Parse(filelines[i]) > best_hs) //if we find a better highscore from the previous
                {
                    best_hs = int.Parse(filelines[i]); //set that highscore as the new best highscore
                    best_name = filelines[i - 1]; 
                    best_path = filelines[i + 1]; //get also the username and avatar path of that highscore (the name line is one line before the highscore line and
                                                 //the avatarpath line is one after the highscore line)   
                }
            }
                                
            label1.Text = "Your name:\n" + name;
            label2.Text = "Your highscore:\n" + highscore;
            label3.Text = "Best player:\n" + best_name;
            label4.Text = "Best highscore:\n" + best_hs.ToString();
            label6.ForeColor = Color.LawnGreen;
            label6.Text = "3";
            label7.Text = "Current score:\n0"; //Set the corresponding text in the game's labels

            pictureBox1.ImageLocation = path;
            pictureBox2.ImageLocation = best_path;
            pictureBox3.ImageLocation = "images/3.png"; //Set the corresponding paths of images in the game's pictureboxes (image of dice at the begining is 3)

            pictureBox3.Location = new Point((panel1.Width / 2) - (pictureBox3.Width / 2), (panel1.Height / 2) - (pictureBox3.Height / 2)); //Set the dice image
                                                                                                       //location that depends on the panel size in each difficulty
                                                                                                       //(we want the dice image to be at the center each time)

            label6.Location = new Point(pictureBox3.Location.X + 16, pictureBox3.Location.Y); //Set the label6 location depend on the dice image location
                                                                                             //(label6 represents the countdown pre-game)
            timer2.Enabled = true; //enable the pre-game 3 second countdown
        }

        public Form3()
        {
            InitializeComponent();
        }

        public Form3(String name, String highscore, String path, String difficulty) //Constructor to set the user and game details
        {
            InitializeComponent();

            this.name = name;
            this.highscore = highscore;
            this.path = path;
            this.difficulty = difficulty;

        }
        
        private void Form3_Load(object sender, EventArgs e)
        {
            r = new Random(); //on each Form3 load we are making a new random object

            switch (difficulty) //Set game with the corresponding difficulty
            {
                case "Easy":
                    prepareGame("Easy");
                    break;

                case "Medium":
                    prepareGame("Medium");
                    break;

                case "Hard":
                    prepareGame("Hard");
                    break;
            }            
        }

        private void timer1_Tick(object sender, EventArgs e) //Timer code for dice teleportation
        {
            int x, y; //variables to set the random coordinates of dice image

            randomimage = r.Next(1, 7); //set randomimage equal to a random number between 1-6 
            pictureBox3.ImageLocation = "images/" + randomimage.ToString() + ".png"; //the dice image to show is the corresponding randomimage value that we got

            x = r.Next(panel1.Width - pictureBox3.Width + 1);
            y = r.Next(panel1.Height - pictureBox3.Height + 1); //the coordinates are getting values that make the dice image to teleport only inside the panel's bounds

            pictureBox3.Location = new Point(x, y); //Change the dice image location (teleport dice)                      
        }
        private void timer2_Tick(object sender, EventArgs e) //Timer code for 3 second countdown pre-game
        {
            count++; //at each tick, we are getting a different number in the screen depends on the case we are in (count is responsible for the case)  

            switch (count)
            {
                case 1:
                    label6.ForeColor = System.Drawing.Color.Gold;
                    label6.Text = "2";
                    break;

                case 2:
                    label6.ForeColor = System.Drawing.Color.Red;
                    label6.Text = "1";
                    break;

                case 3:
                    label6.Location = new Point(label6.Location.X - 30, label6.Location.Y); //changing the location for "GO!" label in order for it to appear at the center
                    label6.Text = "GO!";
                    break;

                case 4: //when the countdown finishes
                    label6.Text = ""; //hide label6 text
                    timer2.Enabled = false; //disable this timer 
                    timer1.Enabled = true;
                    timer3.Enabled = true; //enable dice and time remaining timer 
                    pictureBox3.Enabled = true;
                    pictureBox3.Visible = true; //enable and show dice image
                    randomimage = 3; //add 3 to score if you click the dice (at the beginning the dice is always 3)
                    break;
            }
        }

        private void timer3_Tick(object sender, EventArgs e) //Timer code for time remaining
        {
            countdown--; //substract 1 second
            label5.Text = "Time: " + countdown; //update the time remaining

            if (countdown == 0) //if time is up 
            {
                timer3.Enabled = false;
                timer1.Enabled = false; //disable dice and time remaining timers

                bool exists = false; //boolean variable that tells if there is already a score >=200 in file or not

                filelines = File.ReadAllLines("userdata.txt"); //Save lines in filelines 

                foreach (string line in filelines) //check each line in filelines
                {
                    if (line.All(Char.IsDigit) && !line.Equals("") && int.Parse(line) >= 200) //if we find a score >=200
                    {
                        exists = true; //set exists = true
                        break;
                    }

                }
                if (score >= 200 && !exists) //if your score is >=200 and there is not any other score >=200 in file (so its the first)
                {
                    MessageBox.Show("You unlocked the 'Hard' difficulty!"); //inform the user that the hard difficulty has been unlocked
                    ((Form1)Application.OpenForms[0]).enablerb3(); //call the enablerb3 function of Form1 in order to unlock the hard difficulty
                }

                if (score > int.Parse(highscore)) //if your score is greater than your highscore
                {                   
                    for (int i = 0; i < filelines.Length; i++)
                    {
                        if (filelines[i].Equals(name)) //search your name in filelines and when you find it
                        {
                            filelines[i + 1] = score.ToString(); //set your old highscore equals to your new highscore(which is your current score) in filelines 
                            highscore = score.ToString(); //set the highscore variable equals to the new highscore
                            break;
                        }

                    }

                    File.WriteAllLines("userdata.txt", filelines); //Rewrite the file in order to save changes
                    MessageBox.Show("Congratulations " + name + "!\nYou achieved a new highscore: " + score);
                }

                //ask user if he/she wants to play again
                if (MessageBox.Show("Game over!\n" + "Your score is: " + score + "\nDo you want to play again;", "Play again?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Form3_Load(e, e); //if yes, run Form3_Load function again(play again)
                }
                else
                {
                    this.Close(); //if not, close this form
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e) //if you click the dice image
        {
            score += randomimage; //Add randomimage value(which corresponds to the dice image) in your scoore
            label7.Text = "Current score:\n" + score.ToString(); //change score text
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form5 f5 = new Form5(name, highscore, path, difficulty); //At form's closure, open Form5(login form)
            f5.Show();
        }
    }
}
