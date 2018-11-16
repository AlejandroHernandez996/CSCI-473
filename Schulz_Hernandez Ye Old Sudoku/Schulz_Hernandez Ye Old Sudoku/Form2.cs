using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schulz_Hernandez_Ye_Old_Sudoku
{
    public partial class Form2 : Form
    {
        Form1 form1;

        //Initiaze combo box with directories
        public Form2(IEnumerable<string> items, Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();
            foreach(String item in items)
            {
                comboBox1.Items.Add(item);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //Set the gameFilePath then generate a game with it and close the dialog
        private void button1_Click(object sender, EventArgs e)
        {
            if (!comboBox1.Text.Equals(String.Empty))
            {
                form1.gameFilePath = @"..\..\puzzles\" + comboBox1.Text;
                form1.getBox().Text = "Loaded: " + comboBox1.Text;
                form1.generateGame(form1.gameFilePath);
                this.Close();
            }
        }
    }
}
