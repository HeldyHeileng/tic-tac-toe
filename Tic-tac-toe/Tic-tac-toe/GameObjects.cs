using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ticTacToe
{
    interface INonPlayableObject
    {
        void Draw();
    }
    interface IPlayableObject
    {
        void Draw(int row, int col);
    }

    class GameObjects
    {
        public static class GridSettings
        {
            public static Graphics panel { get; set; }
            public static Pen gridBorderPen { get; set; }
            public static Pen gridPen { get; set; }
            public static Pen crossPen { get; set; }
            public static Pen noughtPen { get; set; }
            public static Pen dashPen { get; set; }

        }

        public enum DashType
        {
            Empty = 0,
            VerticalLeft = 1,
            VerticalMiddle = 2,
            VerticalRight = 3,
            HorizontalTop = 4,
            HorizontalMiddle = 5,
            HorizontalBottom = 6,
            DiagonalLeftTop = 7,
            DiagonalLeftBottom = 8
        }

        public class Grid : INonPlayableObject
        {
            public void Draw()
            {
                for (int i = 0; i <= Settings.GRID_SIZE; i++)
                {
                    var pen = i == 0 || i == Settings.GRID_SIZE ? GridSettings.gridBorderPen : GridSettings.gridPen;
                    var pos = i * Settings.CELL_SIZE;
                    GridSettings.panel.DrawLine(pen, new Point(0, pos), new Point(300, pos));
                    GridSettings.panel.DrawLine(pen, new Point(pos, 0), new Point(pos, 300));
                }
            }
        }

        public class Dash : INonPlayableObject
        {
            public DashType type { get; set; }

            public Dash()
            {
                type = DashType.Empty;
            }

            public void Draw()
            {
                switch ((int)type)
                {
                    case 1: GridSettings.panel.DrawLine(GridSettings.dashPen, 2, 50, 298, 50); break;
                    case 2: GridSettings.panel.DrawLine(GridSettings.dashPen, 2, 150, 298, 150); break;
                    case 3: GridSettings.panel.DrawLine(GridSettings.dashPen, 2, 250, 298, 250); break;
                    case 4: GridSettings.panel.DrawLine(GridSettings.dashPen, 50, 2, 50, 298); break;
                    case 5: GridSettings.panel.DrawLine(GridSettings.dashPen, 150, 2, 150, 298); break;
                    case 6: GridSettings.panel.DrawLine(GridSettings.dashPen, 250, 2, 250, 298); break;
                    case 7: GridSettings.panel.DrawLine(GridSettings.dashPen, 2, 2, 298, 298); break;
                    case 8: GridSettings.panel.DrawLine(GridSettings.dashPen, 298, 2, 2, 298); break;
                }
            }
        }

        public class Cross : IPlayableObject
        {
            public void Draw(int row, int col)
            {
                var xPos = row * Settings.CELL_SIZE + Settings.MARGIN;
                var yPos = col * Settings.CELL_SIZE + Settings.MARGIN;

                var delta = Settings.CELL_SIZE - 2 * Settings.MARGIN;

                GridSettings.panel.DrawLine(GridSettings.crossPen, xPos, yPos, xPos + delta, yPos + delta);
                GridSettings.panel.DrawLine(GridSettings.crossPen, xPos + delta, yPos, xPos, yPos + delta);
            }
        }

        public class Nought : IPlayableObject
        {
            public void Draw(int row, int col)
            {
                var xPos = row * Settings.CELL_SIZE + Settings.MARGIN;
                var yPos = col * Settings.CELL_SIZE + Settings.MARGIN;

                var delta = Settings.MARGIN;
                var ellipseSize = Settings.CELL_SIZE - 4 * delta;

                GridSettings.panel.DrawEllipse(GridSettings.noughtPen, xPos + delta, yPos + delta, ellipseSize, ellipseSize);
            }
        }
    }
}
