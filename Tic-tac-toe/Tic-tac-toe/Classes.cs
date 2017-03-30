using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Tic_tac_toe
{
    public static class Settings
    {
        public const int GRID_SIZE = 3;
        public const int CELL_SIZE = 100;
        public const int MARGIN = 2;
    }

    public enum CellType
    {
        Empty = 0,
        PC = 1,
        User = 2
    }
    public enum PlayerType
    {
        None = 0,
        PC = 1,
        User = 2
    }

    public class Cell
    {
        public CellType type { get; set; }

        public Cell()
        {
            type = CellType.Empty;
        }
    }

    //указатель на ячейку
    public class CellPointer
    {
        public int col { get; set; }
        public int row { get; set; }

        public CellPointer(int col, int row)
        {
            this.col = col;
            this.row = row;
        }

        //перемещаем указатель 
        public void Move(int col, int row) {
            this.col = col;
            this.row = row;
        }
    }

    public class GameInfo
    {
        public string UserName { get; set; }

        public string FirstMove { get; set; }

        public int MoveCount { get; set; }

        public string Winner { get; set; }

        public DateTime CreateDate { get; set; }
    }

}
