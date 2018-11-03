using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


//Created by Benjamin Schulz and Alejandro Hernandez

namespace Schulz_Hernandez_Ode_to_Paint
{
    public partial class Form1 : Form
    {
        private Bitmap target;
        private bool isPainting = false;
        private bool isSaved = true;
        private Color color1 = Color.Black;
        private Color color2 = Color.White;
        private string filePath = string.Empty;
        private List<PointF> coordinates = new List<PointF>();
        private Stack<Image> undoStack = new Stack<Image>();
        private Stack<Image> redoStack = new Stack<Image>();

        public Form1()
        {
            InitializeComponent();
        }


        //initial load of the form
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filePath))
                target = new Bitmap(filePath);
            else
                target = new Bitmap(paintPanel.ClientSize.Width, paintPanel.ClientSize.Height);

            paintPanel.Image = target;
            toolBox.SelectedIndex = 0;
            color1Box.BackColor = color1;
            sizeTextbox.Text = pencilSize.Value.ToString();
            undoStack.Push(new Bitmap(target));
            debug.Text = undoStack.Count.ToString();

            //populate recently opened items
            string[] lines = System.IO.File.ReadAllLines(@"RecentlyOpened.txt");
            recentlyOpened.DropDownItems.Clear();
            foreach (string s in lines)
            {
                recentlyOpened.DropDownItems.Add(s);
            }
            foreach(ToolStripMenuItem m in recentlyOpened.DropDownItems)
            {
                m.Click += new EventHandler(menuClick);
            }
        }

        //User has selected a color
        private void colorBox_MouseClick(object sender, MouseEventArgs e)
        {
            Label colorPicker = (Label)sender;

            if (e.Button == MouseButtons.Left)
            {
                color1 = colorPicker.BackColor;
                color1Box.BackColor = color1;
            }
            else if (e.Button == MouseButtons.Right)
            {
                color2 = colorPicker.BackColor;
                color2Box.BackColor = color2;
            }
        }

        //user pressed down on the mouse button
        private void paintPanel_MouseDown(object sender, MouseEventArgs e)
        {
            isPainting = true;
            isSaved = false;
            coordinates.Add(e.Location);

            using (Graphics GFX = Graphics.FromImage(paintPanel.Image))
            {
                //Draw here
                paintPanel_MouseMove(sender, e);
            }

            debug.Text = undoStack.Count.ToString();
            

        }
        //user is moving mouse
        private void paintPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPainting == true)
            {
                float iX = (coordinates.Last().X + e.X) / 2;
                float iY = (coordinates.Last().Y + e.Y) / 2;
                coordinates.Add(e.Location);

                using (Graphics GFX = Graphics.FromImage(paintPanel.Image))
                {
                    //pencil
                    if (toolBox.SelectedIndex == 0)
                    {
                        //antialiased looks like garbage for the thin lines for some reason, so we set it back to normal for the pencil
                        GFX.SmoothingMode = SmoothingMode.Default;  
                        coordinates.Add(e.Location);
                        GFX.DrawLines(new Pen(color1, pencilSize.Value), coordinates.ToArray());
                    }
                    //brush or erasor   - they're functionally similar, except the erasor is using the foreground color (specified by 'Color 2')
                    if (toolBox.SelectedIndex == 1 || toolBox.SelectedIndex == 2)
                    {
                        GFX.SmoothingMode = SmoothingMode.HighQuality;
                        SolidBrush brush;
                        if (toolBox.SelectedIndex == 2)
                            brush = new SolidBrush(color2);
                        else
                            brush = new SolidBrush(color1);

                        //averages some points along the path to smooth things out when mousespeed is high. 
                        float dX = (iX + e.X) / 2;
                        float dY = (iY + e.Y) / 2;
                        float fX = coordinates.Last().X + (dX - iX);
                        float fY = coordinates.Last().Y + (dY - iY);

                        //draws points of pencilSize along the path
                        GFX.FillEllipse(brush, new Rectangle(e.X - (pencilSize.Value / 2), e.Y - (pencilSize.Value / 2), pencilSize.Value, pencilSize.Value));
                        GFX.FillEllipse(brush, new Rectangle(((int)iX - (pencilSize.Value / 2)), ((int)iY - (pencilSize.Value / 2)), pencilSize.Value, pencilSize.Value));
                        GFX.FillEllipse(brush, new Rectangle(((int)dX - (pencilSize.Value / 2)), ((int)dY - (pencilSize.Value / 2)), pencilSize.Value, pencilSize.Value));
                        GFX.FillEllipse(brush, new Rectangle(((int)fX - (pencilSize.Value / 2)), ((int)fY - (pencilSize.Value / 2)), pencilSize.Value, pencilSize.Value));
                    }
                }
                paintPanel.Refresh();
            }
        }

        //user lifted up off the mouse button
        private void paintPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (isPainting == true)
            {
                coordinates.Add(e.Location);

                using (Graphics GFX = Graphics.FromImage(paintPanel.Image))
                {
                    //GFX.SmoothingMode = SmoothingMode.HighQuality;

                    //lines
                    if (toolBox.SelectedIndex == 3)
                    {
                        PointF[] lineSegment = { coordinates.First(), coordinates.Last() };
                        GFX.DrawLines(new Pen(color1, pencilSize.Value), lineSegment);
                    }
                }
                paintPanel.Refresh();
            }
            isPainting = false;

            //push the new image that was just drawn onto the stack and clear redoStack to fix weird stacking issues.
            //NOTE: If you draw stuff after undoing, it erases anything done after your current position. It's a feature, not a bug.
            undoStack.Push(new Bitmap(target));
            redoStack.Clear();

            debug.Text = undoStack.Count.ToString();
            coordinates.Clear();

        }

        private void paintPanel_Paint(object sender, PaintEventArgs e)
        {  

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }

        //Sets up the different tools to be used
        private void toolBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(toolBox.SelectedIndex == 0)
            {
                pencilSize.Maximum = 3;
                pencilSize.Minimum = 1;
                sizeTextbox.Text = pencilSize.Value.ToString();
            }
            else if(toolBox.SelectedIndex == 1)
            {
                pencilSize.Maximum = 25;
                pencilSize.Minimum = 5;
                sizeTextbox.Text = pencilSize.Value.ToString();
            }
            else if(toolBox.SelectedIndex == 2)
            {
                pencilSize.Maximum = 50;
                pencilSize.Minimum = 1;
            }
            else if (toolBox.SelectedIndex == 3)
            {
                pencilSize.Maximum = 10;
                pencilSize.Minimum = 1;
            }
            else
            {

            }
        }

        //sets pencil/brush size by typing in a number in the text box
        private void sizeTextbox_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(sizeTextbox.Text);
            if (temp >= pencilSize.Minimum && temp <= pencilSize.Maximum)
            {
                pencilSize.Value = temp;       
            }
            else
            {
                pencilSize.Value = pencilSize.Minimum;
                sizeTextbox.Text = pencilSize.Value.ToString();

                if (temp < pencilSize.Minimum)
                    pencilSize.Value = pencilSize.Minimum;
                if (temp > pencilSize.Maximum)
                    pencilSize.Value = pencilSize.Maximum;
            }
        }

        //sets pencil/brush size by sliding a bar. Changes the text of the bar to display its current value
        private void pencilSize_ValueChanged(object sender, EventArgs e)
        {
            sizeTextbox.Text = pencilSize.Value.ToString();
        }

        //undo button. Can also use CTRL + Z. Peeks at top image on undo stack, pastes it to paintPanel then pops it off and into redo
        private void undoButton_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 1)
            {
                paintPanel.Image = null;

                redoStack.Push(undoStack.Pop());
                target = new Bitmap(undoStack.Peek());
                paintPanel.Image = target;
            }
        }

        //redo nutton. Can also use CTRL + X. Peeks at the top image on redo stack, pastes it to paintPanel then pops it off and into undo
        private void redoButton_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                paintPanel.Image = null;
                target = new Bitmap(redoStack.Peek());
                undoStack.Push(redoStack.Pop());
                paintPanel.Image = target;
            }
        }

        //Just a typical color selector, setting it to 'Color 1'. The user can swap later if they want it to be in the background position instead.
        private void colorPicker_MouseClick(object sender, MouseEventArgs e)
        {
            colorDialog1.AllowFullOpen = false;
            colorDialog1.Color = color1;
            colorDialog1.AllowFullOpen = true;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                color1 = colorDialog1.Color;
                color1Box.BackColor = color1;
            }
        }

        //This swaps the forground color with the background color and the other way around.
        private void swap_Click(object sender, EventArgs e)
        {
            Color temp = color1;
            color1 = color2;
            color2 = temp;
            color1Box.BackColor = color1;
            color2Box.BackColor = color2;
        }

        // key shortcut input for undo and redo
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //undo
            if (e.KeyCode == Keys.Z && e.Modifiers == Keys.Control)
            {
                undoButton_Click(sender, e);
            }
            //redo
            if (e.KeyCode == Keys.X && e.Modifiers == Keys.Control)
            {
                redoButton_Click(sender, e);
            }
        }

        private void menuClick(object sender, EventArgs e)
        {
            if (!isSaved)
            {
                var confirmResult = MessageBox.Show("Do you want to save changes?", "Unsaved", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    saveAsToolStripMenuItem_Click(sender, e);
                }
            }
            ToolStripMenuItem m = (ToolStripMenuItem)sender;
            filePath = m.Text;
            //Handle openning the file here. clear everything and set up to draw
            undoStack.Clear();
            redoStack.Clear();
            paintPanel.Image.Dispose();
            Form1_Load(sender, e);
            isSaved = true;
        }

        // new paintPanel
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isSaved)
            {
                var confirmResult = MessageBox.Show("Do you want to save changes?", "Unsaved", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    saveAsToolStripMenuItem_Click(sender, e);
                }
            }
            target = new Bitmap(paintPanel.Width, paintPanel.Height);
            paintPanel.Image = target;
            paintPanel.Invalidate();
            undoStack.Clear();
            redoStack.Clear();
        }

        // Saves an image to a file
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap saveImg = new Bitmap(paintPanel.Image);
            if (string.IsNullOrEmpty(filePath))
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                //Handle saving file here
                //clear usage of file to be able to overwrite.
                if (System.IO.File.Exists(filePath))
                {
                    paintPanel.Image.Dispose();
                    target.Dispose();
                    System.IO.File.Delete(filePath);
                }

                saveImg.Save(filePath);
                saveImg.Dispose();
                //reload form
                Form1_Load(sender, e);
                isSaved = true;
            }
        }

        //Save as above, but unconditionally lets you set a pathname
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap saveImg = new Bitmap(paintPanel.Image);
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Filter = "png files (*.png)|*.png";
                saveFileDialog1.InitialDirectory = "c:\\desktop";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog1.FileName;
                    //clear usage of file to be able to overwrite.
                    if (System.IO.File.Exists(filePath)) {
                        paintPanel.Image.Dispose();
                        target.Dispose();
                        System.IO.File.Delete(filePath);
                    }

                    saveImg.Save(filePath);
                    saveImg.Dispose();
                    //reload form
                    Form1_Load(sender, e);
                    isSaved = true;
                }
            }
        }

        //opens up a png file and pastes it to the screen, then calls form1_load to get everything ready to draw
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isSaved)
            {
                var confirmResult = MessageBox.Show("Do you want to save changes?", "Unsaved", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    saveAsToolStripMenuItem_Click(sender, e);
                }
            }

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = "c:\\desktop";
                openFileDialog1.Filter = "png files (*.png)|*.png";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog1.FileName;
                    using(StreamWriter sw = System.IO.File.AppendText(@"RecentlyOpened.txt"))
                    {
                        sw.WriteLine(filePath);
                    }
                    //System.IO.File.AppendText(@"RecentlyOpened.txt") = filePath;
                    //System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.txt", lines);
                    //Handle openning the file here. clear everything and set up to draw
                    undoStack.Clear();
                    redoStack.Clear();
                    paintPanel.Image.Dispose();
                    Form1_Load(sender, e);
                    isSaved = true;
                }
            }
            
        }
    }
}
