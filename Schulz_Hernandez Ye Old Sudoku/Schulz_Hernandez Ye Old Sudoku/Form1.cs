using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Schulz_Hernandez_Ye_Old_Sudoku
{
    public partial class Form1 : Form
    {
        private bool isPaused = false;
        private bool isSaved = true;
        private bool hasCheated = false;
        private bool cellSelected = false;
        private const string directoryFilePath = @"..\\..\\Puzzles\\directory.txt";
        private string[] lines;
        public string gameFilePath = string.Empty;
        private List<Control> incorrectOrEmpty;
        Stopwatch stopwatch = Stopwatch.StartNew();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            cellContainer.BackColor = Color.FromArgb(40, 40, 40);
            incorrectOrEmpty = new List<Control>();
        }

        //KeyDown easily checks for backspace and clears the active cell if down
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isPaused)
            {
                if (e.KeyCode == Keys.Back)
                {
                    Label holder = (Label)this.ActiveControl;
                    holder.Text = string.Empty;
                }
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    pauseButton_Click(sender, e);
                }
            }
        }

        //KeyPress checks to make sure there is a number from 1 to 9 being entered and enters it
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isPaused && cellSelected)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "[1-9]"))
                {
                    Label holder = (Label)this.ActiveControl;
                    holder.Text = e.KeyChar.ToString();
                    isSaved = false;
                }
            }
        }


        //Gives focus to a label when clicked to allow input to change
        private void label_Click(object sender, EventArgs e)
        {
                Label holder = (Label)sender;
                holder.Focus();
                cellSelected = true;
                holder.BackColor = Color.LemonChiffon;
        }

        //Cells defined by game file will be disabled from user changing the values. That would be too easy!
        //NOTE: This will have to be reset if a new game is loaded up, or code it to set the click event when loaded
        private void labelDisabled_Click(object sender, EventArgs e)
        {
            //Does nothing. That is the point.
        }

        //When focus is lost, visually represent this to the user
        private void label_Leave(object sender, EventArgs e)
        {
            Label holder = (Label)sender;
            holder.BackColor = Color.FromArgb(232,232,232);
            cellSelected = false;
        }


        //
        private void saveButton_Click(object sender, EventArgs e)
        {
            if(!isSaved && !string.IsNullOrEmpty(gameFilePath)){
                isSaved = true;

                //Saves current time
                string[] directory = System.IO.File.ReadAllLines(directoryFilePath);
                var cTime =
                    from T in directory
                    where T.Contains(gameFilePath.Substring(gameFilePath.Count() - 6) + "{TIME}")
                    select T;


                //Trying to get the saving features to work, starting with saving the current game times.
                
                //overwriting an existing save
                if (cTime.Count() > 0)
                {
                    debugBox.Text += string.Format("\r\nFound: {0}", cTime.First());
                    
                }
                //file has never been saved before
                else
                {
                    debugBox.Text += string.Format("\r\n{0} - {1}", gameFilePath.Substring(gameFilePath.Count() - 6) + "{TIME}", stopwatch.Elapsed.ToString());
                    using (StreamWriter sw = File.AppendText(directoryFilePath))
                    {
                        sw.WriteLine(string.Format("\r\n{0} - {1}", gameFilePath.Substring(gameFilePath.Count() - 6) + "{TIME}", stopwatch.Elapsed.ToString()));
                    }
                    using (StreamWriter sw = File.AppendText(gameFilePath))
                    {
                        sw.Write("\n\n");
                        int cell = 0;
                        int row = 0;
                        foreach (var x in cellContainer.Controls.OfType<Control>().OrderBy(x => x.TabIndex))
                        {
                            if (string.IsNullOrEmpty(x.Text))
                            {
                                sw.Write('0');
                            }
                            else
                            {
                                sw.Write(x.Text[0]);
                            }
                            cell++;

                            if (cell % 9 == 0)
                            {
                                cell = 0;
                                row++;
                                sw.Write('\n');
                            }
                        }
                    }
                }
            }
        }

        //Opens a form to be able to choose what game you want to pick
        private void openButton_Click(object sender, EventArgs e)
        {
            string[] dir = System.IO.File.ReadAllLines(directoryFilePath);
            var cTime =
                from T in dir
                where T.Contains("txt") && !T.Contains("TIME") && !T.Contains("COMPLETED")
                select T;
            Form2 form2 = new Form2(cTime, this);
            form2.ShowDialog();
                

        }


        //toggles the covering of the board and pauses the clock
        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (isPaused)
            {
                isPaused = false;
                pauseButton.Text = "Pause";
                coverPanel.SendToBack();
                pauseMenu.Visible = false;
                stopwatch.Start();
            }
            else
            {
                isPaused = true;
                pauseButton.Text = "{ Resume }";
                coverPanel.BringToFront();
                pauseMenu.Visible = true;
                stopwatch.Stop();
            }
        }

        //reset the game to the original before any user input. Resets flags.
        private void resetButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(gameFilePath))
            {
                var confirmResult = MessageBox.Show("Do reset the current Game? You will lose any progress made", "Reset?", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    if (isPaused)
                        pauseButton_Click(sender, e);
                    else
                        isPaused = false;

                    stopwatch.Restart();
                    generateGame(gameFilePath);

                    isSaved = true;
                    hasCheated = false;
                }
            }
            else
            {
                var confirmResult = MessageBox.Show("No Game is currently loaded", "No Game Loaded", MessageBoxButtons.OK);
            }
        }

        //Checks the board for mistakes, and changes the background color to display the incorrect values. Only displays one wrong cell at a time, otherwise it would be too easy!
        private void checkButton_Click(object sender, EventArgs e)
        {
            incorrectOrEmpty.Clear();
            int row = 10;
            int cell = 0;
            Random rand = new Random();

            if (!string.IsNullOrEmpty(gameFilePath))
            {
                //populates list of empty or otherwise incorrect cells
                foreach (var x in cellContainer.Controls.OfType<Control>().OrderBy(x => x.TabIndex))
                {
                    if (string.IsNullOrEmpty(x.Text) || lines[row][cell] != x.Text[0])
                    {
                        incorrectOrEmpty.Add(x);
                    }
                    cell++;

                    if (cell % 9 == 0)
                    {
                        cell = 0;
                        row++;
                    }
                }
                //randomly picks an incorrect cell to notify the player that the specific cell is incorrect. 
                //Can pick the same cell more than once and multiple cells can be selected at once, thats ok.
                var incorrect =
                    from X in incorrectOrEmpty
                    where !string.IsNullOrEmpty(X.Text)
                    select X;
                if (incorrect.Count() > 0)
                {
                    int index = rand.Next() % incorrect.Count();
                    incorrect.ToArray()[index].BackColor = Color.FromArgb(215, 244, 78, 66);   
                }
                else
                {
                    var progressPopup = MessageBox.Show("They're all correct so far! Way to go!", "So far, so good", MessageBoxButtons.OK);
                }

                int correct = 81 - incorrectOrEmpty.Count();
                debugBox.Text += string.Format("\r\n{0} out of 81 cells correct", correct);
                if (correct == 81)
                    stopwatch.Stop();
            }
        }

        //the user has cheated. Reveal a random empty or incorrect cell and set hasCheated flag to true
        private void helpButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(gameFilePath))
            {
                incorrectOrEmpty.Clear();
                int row = 10;
                int cell = 0;
                Random rand = new Random();

                //populates list of empty or otherwise incorrect cells
                foreach (var x in cellContainer.Controls.OfType<Control>().OrderBy(x => x.TabIndex))
                {
                    if (string.IsNullOrEmpty(x.Text) || lines[row][cell] != x.Text[0])
                    {
                        incorrectOrEmpty.Add(x);
                    }
                    cell++;

                    if (cell % 9 == 0)
                    {
                        cell = 0;
                        row++;
                    }
                }

                //grabs the correct value for a random cell that is either empty or incorrect and sets the value to the correct value and sets the cheated flag
                if (incorrectOrEmpty.Count > 0)
                {
                    int index = rand.Next() % incorrectOrEmpty.Count();
                    incorrectOrEmpty.ToArray()[index].BackColor = Color.FromArgb(215, 66, 210, 210);

                    row = ((incorrectOrEmpty.ToArray()[index].TabIndex - 1) / 9) + 10;
                    cell = ((incorrectOrEmpty.ToArray()[index].TabIndex - 1) % 9);
                    incorrectOrEmpty.ToArray()[index].Text = lines[row][cell].ToString();
                    hasCheated = true;
                }
            }
        }

        //timer for current game; output current elapsed time to the screen
        private void timer1_Tick(object sender, EventArgs e)
        {
            timeElapsed.Text = string.Format("{0:hh\\:mm\\:ss}", stopwatch.Elapsed);
        }

        //opens a game of specified difficulty
        private void difficultyButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            var confirmResult = MessageBox.Show(string.Format("Do you want to start a new {0} game?", clickedButton.Text), string.Format("New {0} game?", clickedButton.Text), MessageBoxButtons.YesNo);
            //confirm if they want to start a new game
            if (confirmResult == DialogResult.Yes)
            {
                //if the current game is currently unsaved, ask to save it
                if (!isSaved && !string.IsNullOrEmpty(gameFilePath))
                {
                    var saveResult = MessageBox.Show("Save current game?", "Save game?", MessageBoxButtons.YesNo);
                    //if they want to save the current game, save it
                    if (saveResult == DialogResult.Yes)
                    {
                        saveButton_Click(sender, e);
                    }
                }
                //Now open a new game for user
                string[] directory = System.IO.File.ReadAllLines(directoryFilePath);
                var result =
                    from G in directory
                    where G.StartsWith(clickedButton.Text.ToLower()) && !G.EndsWith("{COMPLETED}")
                    select G;
                debugBox.Text = "Loaded: " + result.FirstOrDefault();
                gameFilePath = @"..\\..\\Puzzles\\" + result.FirstOrDefault();
                generateGame(gameFilePath);
            }
        }

        public void generateGame(string filePath)
        {
            lines = System.IO.File.ReadAllLines(gameFilePath);
            int cell = 0;
            int row = 0;

            //assuming the saved data was put in correctly, this little bit of code loads the game from the last saved state
            if(lines.Count() > 21)
            {
                row = 20;
            }

            //Had to make sure the loop iterated by tabindex and not whatever weird ordering system it was using by default.
            foreach (var x in cellContainer.Controls.OfType<Control>().OrderBy(x => x.TabIndex))
            {

                if (lines[row][cell] != '0')
                {
                    x.Text = lines[row][cell].ToString();
                    x.BackColor = Color.FromArgb(210, 210, 210);
                    x.Click += new EventHandler(labelDisabled_Click);
                    x.Click -= new EventHandler(label_Click);
                    x.Cursor = Cursors.Arrow;
                }
                else
                {
                    x.Text = string.Empty;
                    x.BackColor = Color.FromArgb(232, 232, 232);
                    x.Click -= new EventHandler(labelDisabled_Click);
                    x.Click += new EventHandler(label_Click);
                    x.Cursor = Cursors.Hand;
                }

                cell++;

                if (cell % 9 == 0)
                {
                    cell = 0;
                    row++;
                }
            }

            isPaused = false;
            stopwatch.Restart();
            isSaved = true;
            hasCheated = false;
        }
    }
}
