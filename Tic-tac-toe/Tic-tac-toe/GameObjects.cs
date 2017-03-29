using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ticTacToe
{
    //Объекты интерфейса
    interface INonPlayableObject
    {
        void Draw();
    }

    //Игровые объекты
    interface IPlayableObject
    {
        void Draw(int row, int col);
    }

    //Типы линий, которые отрисовываются при выигрыше
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

    //Настройки отображения
    public static class DisplaySettings
    {
        public static Graphics panel { get; set; }
        public static Pen gridBorderPen { get; set; }
        public static Pen gridPen { get; set; }
        public static Pen crossPen { get; set; }
        public static Pen noughtPen { get; set; }
        public static Pen dashPen { get; set; }

    }

    //Сетка
    public class Grid : INonPlayableObject
    {
        public void Draw()
        {
            for (int i = 0; i <= Settings.GRID_SIZE; i++)
            {
                //Является ли текущая линия границей
                var isBorder = i == 0 || i == Settings.GRID_SIZE;
                var pen = isBorder ? DisplaySettings.gridBorderPen : DisplaySettings.gridPen;
                
                var pos = i * Settings.CELL_SIZE;
                //Отрисовываем вертикальную линию
                DisplaySettings.panel.DrawLine(pen, new Point(0, pos), new Point(300, pos));

                //Отрисовываем горизонтальную линию
                DisplaySettings.panel.DrawLine(pen, new Point(pos, 0), new Point(pos, 300));
            }
        }
    }

    //Черта выигрыша
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
                case 1: DisplaySettings.panel.DrawLine(DisplaySettings.dashPen, 2, 50, 298, 50); break;
                case 2: DisplaySettings.panel.DrawLine(DisplaySettings.dashPen, 2, 150, 298, 150); break;
                case 3: DisplaySettings.panel.DrawLine(DisplaySettings.dashPen, 2, 250, 298, 250); break;
                case 4: DisplaySettings.panel.DrawLine(DisplaySettings.dashPen, 50, 2, 50, 298); break;
                case 5: DisplaySettings.panel.DrawLine(DisplaySettings.dashPen, 150, 2, 150, 298); break;
                case 6: DisplaySettings.panel.DrawLine(DisplaySettings.dashPen, 250, 2, 250, 298); break;
                case 7: DisplaySettings.panel.DrawLine(DisplaySettings.dashPen, 2, 2, 298, 298); break;
                case 8: DisplaySettings.panel.DrawLine(DisplaySettings.dashPen, 298, 2, 2, 298); break;
            }
        }
    }

    //Крестик
    public class Cross : IPlayableObject
    {
        public void Draw(int row, int col)
        {
            //Вычисление координат левой верхней точки крестика
            var xPos = row * Settings.CELL_SIZE + Settings.MARGIN;
            var yPos = col * Settings.CELL_SIZE + Settings.MARGIN;

            //Вычисление размера крестика
            var size = Settings.CELL_SIZE - 2 * Settings.MARGIN;

            DisplaySettings.panel.DrawLine(DisplaySettings.crossPen, xPos, yPos, xPos + size, yPos + size);
            DisplaySettings.panel.DrawLine(DisplaySettings.crossPen, xPos + size, yPos, xPos, yPos + size);
        }
    }

    //Нолик
    public class Nought : IPlayableObject
    {
        public void Draw(int row, int col)
        {
            //Вычисление координат левой верхней точки прямоугольника, в котором будет располагаться нолик
            var xPos = row * Settings.CELL_SIZE + Settings.MARGIN;
            var yPos = col * Settings.CELL_SIZE + Settings.MARGIN;

            //Вычисление размера нолика
            var ellipseSize = Settings.CELL_SIZE - 2 * Settings.MARGIN;

            DisplaySettings.panel.DrawEllipse(DisplaySettings.noughtPen, xPos, yPos, ellipseSize, ellipseSize);
        }
    }

}
