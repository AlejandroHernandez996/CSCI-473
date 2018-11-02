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
        private bool undo = false;
        private bool redo = false;
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
            coordinates.Add(e.Location);

            using (Graphics GFX = Graphics.FromImage(paintPanel.Image))
            {
                //Draw here
                paintPanel_MouseMove(sender, e);
            }
            

            debug.Text = undoStack.Count.ToString();
            undoStack.Push(new Bitmap(target));

        }
        //user is moving mouse
        private void paintPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPainting == true)
            {
                coordinates.Add(e.Location);

                using (Graphics GFX = Graphics.FromImage(paintPanel.Image))
                {
                    GFX.SmoothingMode = SmoothingMode.HighQuality;

                    //pencil
                    if (toolBox.SelectedIndex == 0)
                    {
                        GFX.DrawLines(new Pen(color1, pencilSize.Value), coordinates.ToArray());
                    }
                    //brush
                    if (toolBox.SelectedIndex == 1)
                    {
                        SolidBrush brush = new SolidBrush(color1);
                        GFX.FillEllipse(brush, new Rectangle(e.X - (pencilSize.Value / 2), e.Y - (pencilSize.Value / 2), pencilSize.Value, pencilSize.Value));
                    }
                }
                paintPanel.Invalidate();
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
                paintPanel.Invalidate();
            }
            isPainting = false;
            debug.Text = undoStack.Count.ToString();
            coordinates.Clear();

        }

        private void paintPanel_Paint(object sender, PaintEventArgs e)
        {
            
            if (isPainting == true)
            {
                /*
                
                    Pen p = new Pen(color1, pencilSize.Value);
                    e.Graphics.DrawLines(p, coordinates.ToArray());
                }
                //brush
                if (toolBox.SelectedIndex == 1)
                {
                    SolidBrush brush = new SolidBrush(color1);
                    //e.Graphics.FillEllipse(brush, new Rectangle(e.X - (pencilSize.Value / 2), e.Y - (pencilSize.Value / 2), pencilSize.Value, pencilSize.Value));
                }
                //erasor
                if (toolBox.SelectedIndex == 2)
                {
                    SolidBrush brush = new SolidBrush(color2);
                    //e.Graphics.FillEllipse(brush, e.X - (pencilSize.Value / 2), e.Y - (pencilSize.Value / 2), pencilSize.Value, pencilSize.Value);
                }

               */
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

        private void undoButton_MouseClick(object sender, MouseEventArgs e)
        {

                paintPanel.Invalidate();
        }

        private void redoButton_MouseClick(object sender, MouseEventArgs e)
        {
/*
            if (redoActions.Count > 0)
            {
                undoActions.Push(redoActions.Pop());
                undoableActions.Push(redoableActions.Pop());
            }
*/
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
                using (MemoryStream memory = new MemoryStream())
                {
                    using(FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        paintPanel.Image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
                
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "png files (*.png)|*.png";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog1.FileName;
                //Handle saving the file here
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
                paintPanel.Image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                //NOTE: Will need to fix how the originial graphics is being drawn, as its currently wrong.

            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = "c:\\desktop";
                openFileDialog1.Filter = "png files (*.png)|*.png";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog1.FileName;
                    // do stuff with path file
                    target = new Bitmap(filePath);
                    paintPanel.Image = target;
                }
            }
        }

        private void colorPicker_MouseClick(object sender, MouseEventArgs e)
        {
            colorDialog1.AllowFullOpen = false;
            colorDialog1.Color = color1;
            colorDialog1.AllowFullOpen = true;
            
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                //Label clicked = (Label)sender;

                    color1 = colorDialog1.Color;
                    color1Box.BackColor = color1;
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Color temp = color1;
            color1 = color2;
            color2 = temp;
            color1Box.BackColor = color1;
            color2Box.BackColor = color2;
        }
    }
}
