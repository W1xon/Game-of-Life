using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life
{
    public class Cell
    {
        public int ID { get; set; }
        public string strB;
        public string strS;
        public Color color;

        public Cell(int ID, string strB, string strS, Color color)
        {
            this.ID = ID;
            this.strB = strB;
            this.strS = strS;
            this.color = color;
        }
        public static void CellAdd(List<Cell> cells, string strB, string strS, Color color)
        {
            int maxID = 0;
            foreach(Cell cell in cells)
                if (maxID < cell.ID) maxID = cell.ID;
            cells.Add(new Cell(++maxID, strB, strS, color));
        }
        public static void CellDelete(List<Cell> cells, int index)
        {
            cells.RemoveAt(index);
            for (int i = 0; i < cells.Count; i++)
                cells[i].ID = i + 1;

            Form1.SelectedTypeCell = Form1.Cells[0];
        }
    }
}
