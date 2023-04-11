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
    public partial class FormCellControl : Form
    {
        Form1 form = new Form1();
        List<Button> buttonsSelect = new List<Button>();
        List<Button> buttonsDelete = new List<Button>();
        int distance = 0;
        public FormCellControl()
        {
            InitializeComponent();
            RefreshUIListCell();
        }

        private void buttonCreateNewCell_Click(object sender, EventArgs e)
        {
            CreateNewCell();
        }
        private void CreateNewCell()
        {
            FormCreateCell formCreateCell = new FormCreateCell();
            int countCell = Form1.Cells.Count;
            formCreateCell.ShowDialog();
            if (countCell == Form1.Cells.Count()) return;
            DeleteUIListCell();
            RefreshUIListCell();

        }
        private void CreateUICell(int ID, string strB, string strS, Color color)
        {
            GroupBox groupBox = new GroupBox() { Size = new Size(480, 120), Location = new Point(0, distance) };
            Font font = new Font("Microsoft Sans Serif", 9);

            groupBox.Controls.Add(new Label() { Location = new Point(3, 15), Text = "Рождается если рядом есть:\n" + strB, Size = new Size(190, 30), Font = font });

            groupBox.Controls.Add(new Label() { Location = new Point(3, 65), Text = "Живет если  рядом есть:\n" + strS, Size = new Size(190, 30), Font = font });

            PictureBox pictureBox = new PictureBox()
            {
                Size = new Size(100, 85),
                Location = new Point(215, 20),
                BackColor = color,
                BorderStyle = BorderStyle.Fixed3D,
            };


            groupBox.Controls.Add(pictureBox);
            Button buttonSelect = new Button()
            {
                Text = "Выбрать",
                Location = new Point(320, 20),
                Size = new Size(75, 85),
            };
            Button buttonDelete = new Button()
            {
                Text = "Удалить",
                Location = new Point(400, 20),
                Size = new Size(75, 85),
            };
            buttonDelete.Click += ButtonDelete_Click;
            buttonSelect.Click += ButtonSelect_Click;

            buttonsSelect.Add(buttonSelect);
            buttonsDelete.Add(buttonDelete);

            groupBox.Controls.Add(buttonSelect);
            groupBox.Controls.Add(buttonDelete);
            splitContainer1.Panel2.Controls.Add(groupBox);

            distance += 120;
        }


        private void DeleteUIListCell()
        {
            for (int i = 0; i < splitContainer1.Panel2.Controls.Count;)
            {
                splitContainer1.Panel2.Controls.RemoveAt(i);
                buttonsDelete.RemoveAt(i);
                buttonsSelect.RemoveAt(i);
            }
            distance = 0;
            splitContainer1.Panel2.Update();

        }
        private void RefreshUIListCell()
        {
            foreach (Cell cell in Form1.Cells)
            {
                CreateUICell(cell.ID, cell.strB, cell.strS, cell.color);
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (Form1.Cells.Count == 1)
                return;
            int i = 0;
            foreach (Button button in buttonsDelete)
            {
                if (button == sender)
                {
                    Cell.CellDelete(Form1.Cells, i);
                    DeleteUIListCell();
                    RefreshUIListCell();
                    return;
                }
                i++;
            }
        }

        private void ButtonSelect_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (Button button in buttonsSelect)
            {
                if (button == sender)
                {
                    Form1.SelectedTypeCell = Form1.Cells[i];
                    return;
                }
                i++;
            }
        }

    }
}