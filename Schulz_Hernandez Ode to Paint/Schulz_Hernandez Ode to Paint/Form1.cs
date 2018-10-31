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
    public struct Coordinates{
        public int x;
        public int y;
    }
    

    public partial class Form1 : Form
    {
        private Graphics g;
        private bool isPainting = false;
        private Color color1 = Color.Black;
        private Color color2 = Color.White;
        private List<Coordinates> coords = new List<Coordinates>();
        private Stack<List<Coordinates>> undoableActions = new Stack<List<Coordinates>>();
        private Stack<List<Coordinates>> redoableActions = new Stack<List<Coordinates>>();
        private Stack<GraphicsState> undoActions = new Stack<GraphicsState>();
        private Stack<GraphicsState> redoActions = new Stack<GraphicsState>();

        public Form1()
        {
            InitializeComponent();
            g = paintPanel.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            toolBox.SelectedIndex = 3;
            color1Box.BackColor = color1;
            sizeTextbox.Text = pencilSize.Value.ToString();

        }

        int? initX = null;
        int? initY = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            GraphicsState currentState = g.Save();
            undoActions.Push(currentState);

        }

        private void colorBox_Enter(object sender, EventArgs e)
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
            else if(e.Button == MouseButtons.Right)
            {
                color2 = colorPicker.BackColor;
                color2Box.BackColor = color2;
            }
        }

        private void paintPanel_MouseDown(object sender, MouseEventArgs e)
        {

            

            Coordinates temp;
            temp.x = e.X;
            temp.y = e.Y;
            coords.Add(temp);
            isPainting = true;

            if(toolBox.SelectedIndex != 3)
            {
                drawing(e);
            }
        }

        private void PaintPanel_MouseUp(object sender, MouseEventArgs e)
        {
            Coordinates temp;
            temp.x = e.X;
            temp.y = e.Y;
            coords.Add(temp);

            //line
            if (toolBox.SelectedIndex == 3 && coords.Count > 1)
            {
                Pen p = new Pen(color1, pencilSize.Value);
                g.DrawLine(p, coords[1].x, coords[1].y, coords[0].x, coords[0].y);
            }

            undoableActions.Push(coords);
            coords.Clear();
            isPainting = false;

            initX = null;
            initY = null;

            GraphicsState currentState = g.Save();
            undoActions.Push(currentState);
            debug.Text = undoActions.Count.ToString();
        }

        private void paintPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPainting == true && toolBox.SelectedIndex != 3)
            {
                Coordinates temp;
                temp.x = e.X;
                temp.y = e.Y;
                coords.Add(temp);

                drawing(e);
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

            if (undoActions.Count > 0)
            {
                //g.Clear(Color.Transparent);
                g.Restore(undoActions.Peek());
                redoActions.Push(undoActions.Pop());
                debug.Text = undoActions.Count.ToString();
                //redoableActions.Push(undoableActions.Pop());
            }

        }

        private void redoButton_MouseClick(object sender, MouseEventArgs e)
        {

            if (redoActions.Count > 0)
            {
                //g.Clear(Color.Transparent);
                g.Restore(redoActions.Peek());
                undoActions.Push(redoActions.Pop());
                //undoableActions.Push(redoableActions.Pop());
            }

        }

        private void drawing(MouseEventArgs e)
        {
            //pencil
            if (toolBox.SelectedIndex == 0)
            {
                Pen p = new Pen(color1, pencilSize.Value);
                g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                initX = e.X;
                initY = e.Y;
            }
            //brush
            if (toolBox.SelectedIndex == 1)
            {
                //Pen p = new Pen(color1, pencilSize.Value);
                //g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                
                SolidBrush brush = new SolidBrush(color1);
                g.FillEllipse(brush, new Rectangle(e.X - (pencilSize.Value / 2), e.Y - (pencilSize.Value / 2), pencilSize.Value, pencilSize.Value));
                initX = e.X;
                initY = e.Y;
            }
            //erasor
            if (toolBox.SelectedIndex == 2)
            {
                SolidBrush brush = new SolidBrush(color2);
                g.FillEllipse(brush, e.X - (pencilSize.Value / 2), e.Y - (pencilSize.Value / 2), pencilSize.Value, pencilSize.Value);
            }

            initX = e.X;
            initY = e.Y;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            undoActions.Clear();
            redoActions.Clear();
        }

        private void paintPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
