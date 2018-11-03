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

namespace Schulz_Hernandez_Ye_Old_Sudoku
{
    public partial class Form1 : Form
    {
        private bool isPaused = false;
        private bool isSaved = true;
        private bool hasCheated = false;
        Stopwatch stopwatch = Stopwatch.StartNew();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
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
            if (!isPaused)
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
            holder.BackColor = Color.LemonChiffon;
        }

        //When focus is lost, visually represent this to the user
        private void label_Leave(object sender, EventArgs e)
        {
            Label holder = (Label)sender;
            holder.BackColor = Color.FromArgb(232,232,232);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            isSaved = true;
        }

        private void openButton_Click(object sender, EventArgs e)
        {

        }

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

        private void resetButton_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Do reset the current Game? You will lose any progress made", "Reset?", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                if (isPaused)
                    pauseButton_Click(sender, e);
                else
                    isPaused = false;

                stopwatch.Restart();
                foreach(Control x in cellContainer.Controls)
                {
                    if(x is Label)
                    {
                        x.Text = string.Empty;
                    }
                }

                isSaved = true;
                hasCheated = false;
            }

        }

        private void checkButton_Click(object sender, EventArgs e)
        {

        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            hasCheated = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeElapsed.Text = string.Format("{0:hh\\:mm\\:ss}", stopwatch.Elapsed);
        }

        private void difficultyButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            var confirmResult = MessageBox.Show(string.Format("Do you want to start a new {0} game?", clickedButton.Text), string.Format("New {0} game?", clickedButton.Text), MessageBoxButtons.YesNo);
            //confirm if they want to start a new game
            if (confirmResult == DialogResult.Yes)
            {
                //if the current game is currently unsaved, ask to save it
                if (!isSaved)
                {
                    var saveResult = MessageBox.Show("Save current game?", "Save game?", MessageBoxButtons.YesNo);
                    //if they want to save the current game, save it
                    if (saveResult == DialogResult.Yes)
                    {
                        saveButton_Click(sender, e);
                    }
                }

                //Now open a new game for user

            }
        }
    }
}
