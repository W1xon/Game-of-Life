using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_of_Life.Forms
{
    public partial class FormCreateCell : Form
    {
        Form1 form1 = new Form1();
        Color color = Color.Lime;
        public FormCreateCell(int index = -1)
        {
            InitializeComponent();
            pictureBox1.BackColor = color;

        }

        private void buttonCreateNewCell_Click(object sender, EventArgs e)
        {
            Cell.CellAdd(Form1.Cells, textBoxB.Text, textBoxS.Text, color);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            if (colorDialog1.Color != null)
                color = colorDialog1.Color;
            pictureBox1.BackColor = color;
        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            form1.CorrectInput((TextBox)sender);
        }
    }
}
