using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_of_Life
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        bool[,] arrayCell;
        bool[,] lastArrayCell;
        int rows;
        int columns;
        int sizeCell;
        int densityCell;
        int countGeneration;
        bool isStop = false;

        FormHelp frmHelp = new FormHelp();
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 50;
        }


        private void ShowFormHelper()
        {
            frmHelp = new FormHelp();
            frmHelp.Show();
        }
        private void ColorCell(Graphics graphics, int x, int y)
        {
            Random rand = new Random();
            if (checkBoxColor.Checked == false)
            {
                graphics.FillRectangle(Brushes.Lime, x * sizeCell, y * sizeCell, sizeCell, sizeCell);
                return;
            }
            int r, g, b;

            Color color = new Color();
            SolidBrush solidBrush;
            int style = 100;

            if (comboBoxStyle.SelectedItem != null)
                style = comboBoxStyle.SelectedIndex;
            color = Color.Lime;
            switch (style)
            {
                case 0:
                    r = g = b = (x * 255) / columns;
                    color = Color.FromArgb(255, r, g, b);
                    break;
                case 1:
                    r = g = b = (x * 255) / columns;
                    color = Color.FromArgb(255, r, (int)Math.Sqrt(g), b);
                    break;
                case 2:
                    r = g = b = (x * 255) / columns;
                    color = Color.FromArgb(255, r / 2, g, b / 2);
                    break;
                case 3:
                    r = g = b = (x * y) % 255;
                    color = Color.FromArgb(255, r, g, b);
                    break;
                case 4:
                    r = g = b = (x * y) % 255;
                    color = Color.FromArgb(255, r, (int)Math.Sqrt(g), b);
                    break;
                case 5:
                    r = g = b = (x * y) % 255;
                    color = Color.FromArgb(255, r / 2, g, b / 2);
                    break;
                case 6:
                    r = (x * 255) / columns;
                    g = (int)Math.Sin(x);
                    b = (int)Math.Cos(x);
                    color = Color.FromArgb(255, r, g, b);
                    break;
                case 7:
                    r = (int)Math.Cos(x);
                    g = (x * 255) / columns;
                    b = (int)Math.Sin(x);
                    color = Color.FromArgb(255, r, g, b);
                    break;
                case 8:
                    r = (int)Math.Cos(x);
                    g = (int)Math.Sin(x);
                    b = (x * 255) / columns;
                    color = Color.FromArgb(255, r, g, b);
                    break;
                case 9:
                    r = (x * 255) / columns;
                    g = (y * 255) / rows;
                    color = Color.FromArgb(255, r, g, 255);
                    break;
                case 10:
                    r = (x * 255) / columns;
                    b = (y * 255) / rows;
                    color = Color.FromArgb(255, r, 255, b);
                    break;
                case 11:
                    g = (x * 255) / columns;
                    b = (y * 255) / rows;
                    color = Color.FromArgb(255, 255, g, b);
                    break;
                case 12:
                    g = (x * 255) / columns;
                    b = (y * 255) / rows;
                    color = Color.FromArgb(255, rand.Next(0, 255), g, b);
                    break;
            }



            solidBrush = new SolidBrush(color);
            graphics.FillRectangle(solidBrush, x * sizeCell, y * sizeCell, sizeCell, sizeCell);
        }
        private void GameStart(bool isEmpty = false)
        {
            if (timer1.Enabled)
                GameStop();
            isStop = false;
            buttonStart.Text = "Рестарт";
            numericUpDownDensity.Enabled = false;
            numericUpDownSize.Enabled = false;
            buttonNextStep.Enabled = true;
            buttonClear.Enabled = true;

            countGeneration = 0;
            sizeCell = (int)numericUpDownSize.Value;
            densityCell = (int)numericUpDownDensity.Value;
            rows = pictureBox1.Height / sizeCell;
            columns = pictureBox1.Width / sizeCell;
            arrayCell = new bool[columns, rows];

            Random rand = new Random();

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (isEmpty)
                        arrayCell[x, y] = false;
                    else
                        arrayCell[x, y] = (rand.Next((int)numericUpDownDensity.Value) != 0) ? true : false;
                }
            }
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            if (!isEmpty)
                timer1.Start();
        }
        private void NextGeneration()
        {
            Text = "Генерация № " + countGeneration++;
            graphics.Clear(Color.Black);
            lastArrayCell = arrayCell;
            bool[,] nextArrayCell = new bool[columns, rows];
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    int neigboursCount = CountNeighbours(x, y);
                    bool hasLife = arrayCell[x, y];

                    if (!hasLife && textBoxB.Text.Contains(neigboursCount.ToString()))// ==3 B 3 S 23
                        nextArrayCell[x, y] = true;
                    else if (hasLife && !textBoxS.Text.Contains(neigboursCount.ToString()))//<2   >3
                        nextArrayCell[x, y] = false;
                    else
                        nextArrayCell[x, y] = arrayCell[x, y];
                    if (hasLife)
                        ColorCell(graphics, x, y);
                }
            }
            arrayCell = nextArrayCell;
            pictureBox1.Refresh();
        }
        private int CountNeighbours(int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int k = -1; k < 2; k++)
                {
                    int col;
                    int row;
                    bool haslife;
                    if (checkBoxBorder.Checked)
                    {
                        col = x + i;
                        row = y + k;
                        if (col >= columns || col < 0)
                            haslife = false;
                        else if (row >= rows || row < 0)
                            haslife = false;
                        else
                            haslife = arrayCell[col, row];
                    }
                    else
                    {

                        col = (x + i + columns) % columns;
                        row = (y + k + rows) % rows;
                        haslife = arrayCell[col, row];
                    }
                    bool isSelfChecking = col == x && row == y;
                    if (haslife && !isSelfChecking)
                        count++;
                }
            }

            return count;
        }
        private void GameStop()
        {
            isStop = true;
            numericUpDownDensity.Enabled = true;
            numericUpDownSize.Enabled = true;
            buttonProceed.Enabled = true;
            if (!timer1.Enabled)
                return;
            timer1.Stop();
        }
        private void DrawForMouse(MouseEventArgs e)
        {
            if (arrayCell == null)
                return;
            int x = e.X / sizeCell;
            int y = e.Y / sizeCell;
            if (e.Button == MouseButtons.Left)
            {
                if (CheckOutRange(x, y))
                {
                    lastArrayCell[x, y] = true;
                    PauseDrawning();
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (CheckOutRange(x, y))
                {
                    lastArrayCell[x, y] = false;
                    PauseDrawning();
                }
            }

        }
        private void PauseDrawning()
        {
            graphics.Clear(Color.Black);
            arrayCell = lastArrayCell;
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    int neigboursCount = CountNeighbours(x, y);
                    bool hasLife = lastArrayCell[x, y];
                    if (hasLife)
                        ColorCell(graphics, x, y);
                }
            }
            pictureBox1.Refresh();
        }
        private bool CheckOutRange(int x, int y)
        {
            return (x < arrayCell.GetLength(0) && y < arrayCell.GetLength(1)) && (x >= 0 && y >= 0);
        }
        private void Clear()
        {
            timer1.Stop();
            buttonProceed.Enabled = true;

            graphics.Clear(Color.Black);
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    arrayCell[x, y] = false;
                    lastArrayCell[x, y] = false;
                }
            }

            pictureBox1.Refresh();
        }
        private void CorrectInput(TextBox textBox)
        {
            if (Regex.IsMatch(textBox.Text, "[^0-9]"))
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                textBox.SelectionStart = textBox.TextLength;
            }
            if (checkBoxSort.Checked)
            {
                int[] array = textBox.Text.Select(x => Convert.ToInt32(x.ToString())).OrderBy(x => x).ToArray();
                textBox.Text = String.Join("", array);
            }
            textBox.SelectionStart = textBox.TextLength;
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            GameStart();
        }
        private void buttonStop_Click(object sender, EventArgs e)
        {
            GameStop();
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = trackBar1.Value;
            numericUpDownUpdateTime.Value = trackBar1.Value;
        }
        private void buttonProceed_Click(object sender, EventArgs e)
        {
            NextGeneration();
            isStop = false;
            timer1.Start();
            numericUpDownDensity.Enabled = false;
            numericUpDownSize.Enabled = false;

            numericUpDownDensity.Value = densityCell;
            numericUpDownSize.Value = sizeCell;
            buttonProceed.Enabled = false;
        }
        private void buttonNextStep_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                timer1.Stop();
            buttonProceed.Enabled = true;
            NextGeneration();
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (graphics != null)
                DrawForMouse(e);
            else
                buttonStartEmpty.PerformClick();
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            DrawForMouse(e);
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
            isStop = true;
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (graphics != null && !isStop)
                timer1.Start();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Stop();
        }

        private void buttonStartEmpty_Click(object sender, EventArgs e)
        {
            countGeneration = 0;
            Text = "Генерация № 0";
            GameStart(true);
            buttonStart.Text = "Старт";
            GameStop();
            lastArrayCell = arrayCell;
            graphics.Clear(Color.Black);
        }

        private void numericUpDownUpdateTime_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)numericUpDownUpdateTime.Value;
            trackBar1.Value = timer1.Interval;
        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            CorrectInput(textBoxB);
            CorrectInput(textBoxS);
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            ShowFormHelper();
        }

        private void checkBoxColor_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxStyle.Visible = !comboBoxStyle.Visible;
        }

        private void checkBoxSort_CheckedChanged(object sender, EventArgs e)
        {
            CorrectInput(textBoxB);
            CorrectInput(textBoxS);
        }
    }
}
