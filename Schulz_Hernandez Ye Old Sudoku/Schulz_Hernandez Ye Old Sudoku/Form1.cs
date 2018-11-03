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
            Label holder = (Label)this.ActiveControl;
            if (e.KeyCode == Keys.Back)
            {
                holder.Text = string.Empty;
            }
        }

        //KeyPress checks to make sure there is a number from 1 to 9 being entered and enters it
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Label holder = (Label)this.ActiveControl;
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "[1-9]"))
            {
                holder.Text = e.KeyChar.ToString();
            }
        }


        //Gives focus to a label when clicked to allow input to change
        private void label_Click(object sender, EventArgs e)
        {
            Label holder = (Label)sender;
            holder.Focus();
            holder.BackColor = Color.LemonChiffon;
        }

        //When focus is lost, represent this to the user
        private void label_Leave(object sender, EventArgs e)
        {
            Label holder = (Label)sender;
            holder.BackColor = Color.FromArgb(232,232,232);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

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
            var confirmResult = MessageBox.Show("Do resent the current Game? You will lose any progress made", "Reset?", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if (isPaused)
                    pauseButton_Click(sender, e);
                else
                    isPaused = false;

                stopwatch.Restart();
            }

        }

        private void checkButton_Click(object sender, EventArgs e)
        {

        }

        private void helpButton_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeElapsed.Text = string.Format("{0:hh\\:mm\\:ss}", stopwatch.Elapsed);
        }
    }
}
