using Game_of_Life.Forms;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Game_of_Life
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private int[,] arrayCell;
        private int[,] lastArrayCell;
        private int rows;
        private int columns;
        private int sizeCell;
        private int densityCell;
        private int countGeneration;
        private bool isStop = false;
        private FormHelp frmHelp = new FormHelp();
        bool doubleClick = false;
        static public List<Cell> Cells = new List<Cell>() { new Cell(1, "3", "23", Color.Lime) };
        Random random = new Random();
        public static Cell SelectedTypeCell;

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 50;
            comboBoxBrush.SelectedIndex = 0;
            SelectedTypeCell = Cells[0];
        }
        private void DrowFigures(ComboBox comboBox, int positionX, int positionY, bool brush)
        {
            if (comboBox.SelectedItem == null)
                return;
            int[,] figure = Figures.Get(comboBox.SelectedItem.ToString());
            for (int x = 0; x < figure.GetLength(0); x++)
            {
                for (int y = 0; y < figure.GetLength(1); y++)
                {
                    try
                    {
                        if (brush && figure[x, y] == 0)
                            continue;
                        arrayCell[((positionX + y) - (figure.GetLength(1) / 2)), ((positionY + x) - (figure.GetLength(0) / 2))] = figure[x, y] != 0 ? SelectedTypeCell.ID : 0;
                    }
                    catch { break; }
                }
            }
            doubleClick = false;
            PauseDrawning();
        }
        private void ShowFormHelper()
        {
            frmHelp = new FormHelp();
            frmHelp.ShowDialog();
        }
        private void ColorCell(Graphics graphics, int x, int y, int ID)
        {
            int r, g, b;
            Color color = Cells[0].color;
            SolidBrush solidBrush;

            int style = 100;
            if (comboBoxStyle.SelectedItem != null)
                style = comboBoxStyle.SelectedIndex;
            if (ID > 0 && ID < Cells.Count)
                color = Cells[ID].color;
            else
            {
                switch (style)
                {
                    case 1:
                        r = g = b = (x * 255) / columns;
                        color = Color.FromArgb(255, r, g, b);
                        break;
                    case 2:
                        r = g = b = (x * 255) / columns;
                        color = Color.FromArgb(255, r, (int)Math.Sqrt(g), b);
                        break;
                    case 3:
                        r = g = b = (x * 255) / columns;
                        color = Color.FromArgb(255, r / 2, g, b / 2);
                        break;
                    case 4:
                        r = g = b = (x * y) % 255;
                        color = Color.FromArgb(255, r, g, b);
                        break;
                    case 5:
                        r = g = b = (x * y) % 255;
                        color = Color.FromArgb(255, r, (int)Math.Sqrt(g), b);
                        break;
                    case 6:
                        r = g = b = (x * y) % 255;
                        color = Color.FromArgb(255, r / 2, g, b / 2);
                        break;
                    case 7:
                        r = (x * 255) / columns;
                        g = (int)Math.Sin(x);
                        b = (int)Math.Cos(x);
                        color = Color.FromArgb(255, r, g, b);
                        break;
                    case 8:
                        r = (int)Math.Cos(x);
                        g = (x * 255) / columns;
                        b = (int)Math.Sin(x);
                        color = Color.FromArgb(255, r, g, b);
                        break;
                    case 9:
                        r = (int)Math.Cos(x);
                        g = (int)Math.Sin(x);
                        b = (x * 255) / columns;
                        color = Color.FromArgb(255, r, g, b);
                        break;
                    case 10:
                        r = (x * 255) / columns;
                        g = (y * 255) / rows;
                        color = Color.FromArgb(255, r, g, 255);
                        break;
                    case 11:
                        r = (x * 255) / columns;
                        b = (y * 255) / rows;
                        color = Color.FromArgb(255, r, 255, b);
                        break;
                    case 12:
                        g = (x * 255) / columns;
                        b = (y * 255) / rows;
                        color = Color.FromArgb(255, 255, g, b);
                        break;
                }
            }


            solidBrush = new SolidBrush(color);
            graphics.FillRectangle(solidBrush, x * sizeCell, y * sizeCell, sizeCell, sizeCell);
        }
        private void GameStart(bool isEmpty = false, Bitmap OpenImage = null)
        {
            if (Cells.Count < 1)
                Cells.Add(new Cell(1, textBoxB.Text, textBoxS.Text, Color.Lime));
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
            arrayCell = new int[columns, rows];
            if (OpenImage == null)
            {

                for (int x = 0; x < columns; x++)
                {
                    for (int y = 0; y < rows; y++)
                    {
                        if (isEmpty)
                            arrayCell[x, y] = 0;
                        else
                            arrayCell[x, y] = (random.Next(10) < numericUpDownDensity.Value) ? Cells[random.Next(Cells.Count)].ID : 0;

                    }
                }
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                graphics = Graphics.FromImage(pictureBox1.Image);
            }
            else
            {
                LoadImage.ResizeImage(pictureBox1);


                for (int x = 0; x < columns; x++)
                {
                    for (int y = 0; y < rows; y++)
                    {
                        if (x * sizeCell >= LoadImage.Image.Width || y * sizeCell >= LoadImage.Image.Height)
                            continue;
                        if (LoadImage.Image.GetPixel(x * sizeCell, y * sizeCell).R < 150 && LoadImage.Image.GetPixel(x * sizeCell, y * sizeCell).G < 150 && LoadImage.Image.GetPixel(x * sizeCell, y * sizeCell).B < 150)
                            arrayCell[x, y] = 1;
                        else
                            arrayCell[x, y] = 0;
                    }
                }
                pictureBox1.Image = LoadImage.Image;
                graphics = Graphics.FromImage(pictureBox1.Image);
            }
            if (!isEmpty)
                timer1.Start();
        }
        private void NextGeneration()
        {
            Text = "Генерация № " + countGeneration++;
            graphics.Clear(Color.Black);
            lastArrayCell = (int[,])arrayCell.Clone();
            int[,] nextArrayCell = new int[columns, rows];
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    int[] neighbourCountsAndType = CountNeighbours(x, y);
                    int hasLife = arrayCell[x, y];
                    int indexCell = arrayCell[x, y] - 1;
                    if (indexCell < 0) indexCell = 0;
                    nextArrayCell[x, y] = CellBehaviour(hasLife, neighbourCountsAndType);
                    if (hasLife != 0)
                        ColorCell(graphics, x, y, arrayCell[x, y] - 1);
                }
            }
            arrayCell = nextArrayCell;
            pictureBox1.Refresh();
        }
        private int CellBehaviour(int hasLife, int[] neighbourCountsAndType)
        {
            //рождение клетки
            if (hasLife == 0 && Cells[neighbourCountsAndType[1]].strB.Contains(neighbourCountsAndType[0].ToString()))
                return Cells[neighbourCountsAndType[1]].ID;
            //клетка погибает
            else if (hasLife != 0 && !Cells[neighbourCountsAndType[1]].strS.Contains(neighbourCountsAndType[0].ToString()))
                return 0;
            //клетка продолжает жить
            else
                return hasLife == 0 ? 0 : Cells[neighbourCountsAndType[1]].ID;
        }
        private int[] CountNeighbours(int x, int y)
        {
            int count = 0;
            int[] arrayNeighboursCell = new int[Cells.Count];
            int indexCell = arrayCell[x, y] - 1;
            if (indexCell < 0) indexCell = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int k = -1; k < 2; k++)
                {
                    int col = x + i;
                    int row = y + k;
                    int haslife = 0;
                    if (checkBoxBorder.Checked)
                    {
                        if (col >= columns || col < 0)
                            haslife = 0;
                        else if (row >= rows || row < 0)
                            haslife = 0;
                        else
                            haslife = arrayCell[col, row];
                    }
                    else
                    {
                        col = (x + i + columns) % columns;
                        row = (y + k + rows) % rows;
                        haslife = arrayCell[col, row];
                    }
                    if (haslife != 0)
                    {
                        indexCell = haslife - 1;
                        if (indexCell >= arrayNeighboursCell.Count())
                            indexCell = 0;
                        arrayNeighboursCell[indexCell]++;
                        if (col == x && row == y)
                            continue;
                        count++;
                    }
                }
            }
            return new int[] { count, Array.IndexOf(arrayNeighboursCell, arrayNeighboursCell.Max()) };
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
            if (doubleClick)
            {
                DrowFigures(comboBoxFigures, x, y, false);
            }
            if (ModifierKeys == Keys.Alt && e.Button == MouseButtons.Left)
            {
                if (arrayCell[x, y] != 0)
                    SelectedTypeCell = Cells[arrayCell[x, y] - 1];
            }
            else if (e.Button == MouseButtons.Left)
            {
                DrowFigures(comboBoxBrush, x, y, true);
            }
            if (e.Button == MouseButtons.Middle)
            {
                DrowFigures(comboBoxFigures, x, y, true);
            }
            if (e.Button == MouseButtons.Right)
            {
                if (CheckOutRange(x, y))
                {
                    lastArrayCell[x, y] = 0;
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
                    int hasLife = lastArrayCell[x, y];
                    if (hasLife != 0)
                        ColorCell(graphics, x, y, arrayCell[x, y] - 1);
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
                    arrayCell[x, y] = 0;
                    lastArrayCell[x, y] = 0;
                }
            }

            pictureBox1.Refresh();
        }
        public void CorrectInput(TextBox textBox)
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

        private void ComboBoxSelectCellUpdate()
        {
            for (int i = 0; i < comboBoxSelectCell.Items.Count;)
                comboBoxSelectCell.Items.RemoveAt(i);
            foreach (Cell cell in Cells)
                comboBoxSelectCell.Items.Add(cell.ID);
            comboBoxSelectCell.SelectedItem = SelectedTypeCell.ID;
            comboBoxSelectCell.BackColor = Cells[comboBoxSelectCell.SelectedIndex].color;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = trackBar1.Value;
            numericUpDownUpdateTime.Value = trackBar1.Value;
        }

        private void numericUpDownUpdateTime_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)numericUpDownUpdateTime.Value;
            trackBar1.Value = timer1.Interval;
        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            CorrectInput((TextBox)sender);
            SelectedTypeCell.strB = textBoxB.Text;
            SelectedTypeCell.strS = textBoxS.Text;
            for (int i = 0; i < Cells.Count(); i++)
            {
                if (SelectedTypeCell.ID == Cells[i].ID)
                    Cells[i] = SelectedTypeCell;
            }
        }


        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (comboBoxFigures.SelectedItem != null)
                doubleClick = true;
        }
        //Button
        #region
        private void buttonStart_Click(object sender, EventArgs e)
        {
            GameStart();
        }
        private void buttonStop_Click(object sender, EventArgs e)
        {
            GameStop();
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
        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
            isStop = true;
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

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            ShowFormHelper();
        }

        private void buttonOpenImage_Click(object sender, EventArgs e)
        {
            LoadImage.OpenImage();
            GameStart(true, LoadImage.Image);
        }

        #endregion

        private void checkBoxSort_CheckedChanged(object sender, EventArgs e)
        {
            CorrectInput(textBoxB);
            CorrectInput(textBoxS);
        }
        //Mouse
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

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (graphics != null && !isStop)
                timer1.Start();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Stop();
        }

        private void buttonCreateNewCell_Click(object sender, EventArgs e)
        {
            FormCellControl frmCellControl = new FormCellControl();
            frmCellControl.ShowDialog();
            string strB = SelectedTypeCell.strB, strS = SelectedTypeCell.strS;
            textBoxB.Text = strB;
            textBoxS.Text = strS;
            ComboBoxSelectCellUpdate();
        }

        private void comboBoxSelectCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(comboBoxSelectCell.SelectedItem.ToString());
            SelectedTypeCell = Cells[ID - 1];
            string strB = SelectedTypeCell.strB, strS = SelectedTypeCell.strS;
            textBoxB.Text = strB;
            textBoxS.Text = strS;
        }

        private void comboBoxSelectCell_Click(object sender, EventArgs e)
        {
            if (Cells.Count == 1 && comboBoxSelectCell.Items.Count == 0)
                comboBoxSelectCell.Items.Add(Cells[0].ID);
        }

        private void comboBoxSelectCell_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBoxSelectCell.BackColor = Cells[comboBoxSelectCell.SelectedIndex].color;
        }

        private void numericUpDownSize_ValueChanged(object sender, EventArgs e)
        {
            if(!timer1.Enabled)
            {
                sizeCell = (int)numericUpDownSize.Value;
                densityCell = (int)numericUpDownDensity.Value;
                buttonStartEmpty_Click(null, EventArgs.Empty);
            }
        }
    }
}
