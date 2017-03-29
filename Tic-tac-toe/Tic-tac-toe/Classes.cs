using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ticTacToe
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
        
    public class Cell
    {
        public CellType type { get; set; }

        public Cell()
        {
            type = CellType.Empty;
        }
    }

}
