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
            target = new Bitmap(paintPanel.ClientSize.Width, paintPanel.ClientSize.Height);
            paintPanel.Image = target;
            toolBox.SelectedIndex = 0;
            color1Box.BackColor = color1;
            sizeTextbox.Text = pencilSize.Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            undoStack.Push(new Bitmap(target));
            debug.Text = undoStack.Count.ToString();
        }


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
                    //GFX.SmoothingMode = SmoothingMode.HighQuality;

                    //pencil
                    if (toolBox.SelectedIndex == 0)
                    {
                        //antialiansed looks like garbage for the thin lines for some reason, so we set it back to normal for the pencil
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
            if (isPainting == true)
            {
            }
        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }

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

        private void sizeTextbox_TextChanged(object sender, EventArgs e)
        {

            //fix temp to only allow numerical values within range
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

        private void pencilSize_ValueChanged(object sender, EventArgs e)
        {
            sizeTextbox.Text = pencilSize.Value.ToString();
        }

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

        private void swap_Click(object sender, EventArgs e)
        {
            Color temp = color1;
            color1 = color2;
            color2 = temp;
            color1Box.BackColor = color1;
            color2Box.BackColor = color2;
        }

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

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // maybe set a flag when image is changed and then check for the flag before saving?
            if (string.IsNullOrEmpty(filePath))
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                //Handle saving file here
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                isSaved = true;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Filter = "png files (*.png)|*.png";
                saveFileDialog1.InitialDirectory = "c:\\desktop";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog1.FileName;
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);

                    isSaved = true;
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isSaved == false)
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
                    //Handle openning the file here. clear everything
                    undoStack.Clear();
                    redoStack.Clear();
                    target = new Bitmap(filePath);
                    paintPanel.Image = target;
                    redoStack.Push(target);
                    isSaved = true;
                }
            }
        }
    }
}
