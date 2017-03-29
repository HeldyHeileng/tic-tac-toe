using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ticTacToe
{
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
