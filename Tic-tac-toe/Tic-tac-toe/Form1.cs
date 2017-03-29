using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExtensionMethods;
using static ticTacToe.GameObjects;

namespace ticTacToe
{
    public partial class Form1 : Form
    {
        public bool qwe = false;
        public bool pervhod = true;  //true говорит о том, что у компьютера нынче первый ход. первым ходом компьютер должен ходить в
                                     //центр, дальше по ситуации. значение false переправляет на путь "дальше по ситуации"
        public bool first = true;   //меняется на false сразу после первого хода юзера(играет крестиками), нужен для проверки, был ли
                                    // первый ход в центр или нет
        Dash dash = new Dash();  // черта для вычеркивания выигрышной комбинации
        public int x = -1;
        public int y = -1;
        public int xfir = -1; // первый ход юзера
        public int yfir = -1; // когда он играет крестиками
        public int xlast = -1;//последний
        public int ylast = -1;// ход юзера
        public int win = 0;//если 1- выиграл комп, 2-пользователь, 3- ничья
        public bool hdpc = false;//true - сейчас ход компьютера, false - ход пользователя
        public int pc = 0;     // кто ходит первым, 1 - ходит комп, 2 первый ходит юзер

        Grid grid = new Grid();
        Cross cross = new Cross();
        Nought nought = new Nought();

        public Cell[,] cells = new Cell[Settings.GRID_SIZE, Settings.GRID_SIZE].Populate(() => new Cell());
        public Form1()
        {
            InitializeComponent();

            GridSettings.panel = panel1.CreateGraphics();
            GridSettings.gridBorderPen = new Pen(Color.Blue, 1);
            GridSettings.gridPen = new Pen(Color.Blue, 2);
            GridSettings.crossPen = new Pen(Color.Black, 3);
            GridSettings.noughtPen = new Pen(Color.Black, 3);
            GridSettings.dashPen = new Pen(Color.Blue, 6);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            qwe = true;
            if (pc == 0)  // pc равен 0 тогда, когда пользователь еще не выбрал кто будет ходить первым
            {
                label1.Text = "Выберите игрока, который будет ходить первым!";
            }
            else
            {
                if (pc == 1)  //первый ход компа
                {
                    hdpc = true;
                    hod1();
                    radioButton1.Visible = false;
                    radioButton2.Visible = false;
                }
                else     //первый ход пользователя
                {
                    hdpc = false;
                    label1.Text = "Ваш ход!";
                    radioButton1.Visible = false;
                    radioButton2.Visible = false;
                }
            }
        }    //работает

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            grid.Draw();
        }  //работает

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (qwe)
            {
                if (pc == 0)  //если пользователь сразу нажимает на доску, не выбрав кто будет ходить первым
                {
                    label1.Text = "Выберите игрока, который будет ходить первым!";
                }
                else
                {
                    if (hdpc == false)   //ход пользователя
                    {
                        if (pc == 2)    //первый ход пользователя, значит он играет крестиками
                        {
                            if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 0 && e.Location.Y < 100)  //1 ячейка
                            {

                                if (cells[0, 0].type == CellType.Empty)
                                {
                                    cross.Draw(0, 0);
                                    cells[0, 0].type = CellType.User;
                                    xlast = 0;
                                    ylast = 0;
                                    first = false;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }

                            if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 0 && e.Location.Y < 100)//2 ячейка
                            {
                                if (cells[1, 0].type == CellType.Empty)
                                {
                                    cross.Draw(1, 0);
                                    cells[1, 0].type = CellType.User;
                                    xlast = 1;
                                    ylast = 0;
                                    first = false;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                            if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 0 && e.Location.Y < 100)//3 ячейка
                            {
                                if (cells[2, 0].type == CellType.Empty)
                                {
                                    cross.Draw(2, 0);
                                    cells[2, 0].type = CellType.User;
                                    xlast = 2;
                                    ylast = 0;
                                    first = false;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                            if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 100 && e.Location.Y < 200)//4 ячейка
                            {
                                if (cells[0, 1].type == CellType.Empty)
                                {
                                    cross.Draw(0, 1);
                                    cells[0, 1].type = CellType.User;
                                    xlast = 0;
                                    ylast = 1;
                                    first = false;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                            if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 100 && e.Location.Y < 200)//5 ячейка
                            {
                                if (cells[1, 1].type == CellType.Empty)
                                {
                                    cross.Draw(1, 1);
                                    cells[1, 1].type = CellType.User;
                                    xlast = 1;
                                    ylast = 1;
                                    if (first)
                                    {
                                        xfir = 1;
                                        yfir = 1;
                                        first = false;
                                    }
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                            if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 100 && e.Location.Y < 200)//6 ячейка
                            {
                                if (cells[2, 1].type == CellType.Empty)
                                {
                                    cross.Draw(2, 1);
                                    cells[2, 1].type = CellType.User;
                                    xlast = 2;
                                    ylast = 1;
                                    first = false;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                            if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 200 && e.Location.Y < 300)//7 ячейка
                            {
                                if (cells[0, 2].type == CellType.Empty)
                                {
                                    cross.Draw(0, 2);
                                    cells[0, 2].type = CellType.User;
                                    xlast = 0;
                                    ylast = 2;
                                    first = false;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                            if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 200 && e.Location.Y < 300)//8 ячейка
                            {
                                if (cells[1, 2].type == CellType.Empty)
                                {
                                    cross.Draw(1, 2);
                                    cells[1, 2].type = CellType.User;
                                    xlast = 1;
                                    ylast = 2;
                                    first = false;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                            if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 200 && e.Location.Y < 300)//8 ячейка
                            {
                                if (cells[2, 2].type == CellType.Empty)
                                {
                                    cross.Draw(2, 2);
                                    cells[2, 2].type = CellType.User;
                                    xlast = 2;
                                    ylast = 2;
                                    first = false;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                        }
                        else   // пользователь играет ноликами!!!
                        {
                            if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 0 && e.Location.Y < 100)  //1 ячейка
                            {

                                if (cells[0, 0].type == CellType.Empty)
                                {
                                    nought.Draw(0, 0);
                                    cells[0, 0].type = CellType.User;
                                    xlast = 0;
                                    ylast = 0;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }

                            if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 0 && e.Location.Y < 100)//2 ячейка
                            {
                                if (cells[1, 0].type == CellType.Empty)
                                {
                                    nought.Draw(1, 0);
                                    cells[1, 0].type = CellType.User;
                                    xlast = 1;
                                    ylast = 0;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                            if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 0 && e.Location.Y < 100)//3 ячейка
                            {
                                if (cells[2, 0].type == CellType.Empty)
                                {
                                    nought.Draw(2, 0);
                                    cells[2, 0].type = CellType.User;
                                    xlast = 2;
                                    ylast = 0;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                            if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 100 && e.Location.Y < 200)//4 ячейка
                            {
                                if (cells[0, 1].type == CellType.Empty)
                                {
                                    nought.Draw(0, 1);
                                    cells[0, 1].type = CellType.User;
                                    xlast = 0;
                                    ylast = 1;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                            if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 100 && e.Location.Y < 200)//5 ячейка
                            {
                                if (cells[1, 1].type == CellType.Empty)
                                {
                                    nought.Draw(1, 1);
                                    cells[1, 1].type = CellType.User;
                                    xlast = 1;
                                    ylast = 1;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                            if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 100 && e.Location.Y < 200)//6 ячейка
                            {
                                if (cells[2, 1].type == CellType.Empty)
                                {
                                    nought.Draw(2, 1);
                                    cells[2, 1].type = CellType.User;
                                    xlast = 2;
                                    ylast = 1;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                            if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 200 && e.Location.Y < 300)//7 ячейка
                            {
                                if (cells[0, 2].type == CellType.Empty)
                                {
                                    nought.Draw(0, 2);
                                    cells[0, 2].type = CellType.User;
                                    xlast = 0;
                                    ylast = 2;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                            if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 200 && e.Location.Y < 300)//8 ячейка
                            {
                                if (cells[1, 2].type == CellType.Empty)
                                {
                                    nought.Draw(1, 2);
                                    cells[1, 2].type = CellType.User;
                                    xlast = 1;
                                    ylast = 2;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                            if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 200 && e.Location.Y < 300)//9 ячейка
                            {
                                if (cells[2, 2].type == CellType.Empty)
                                {
                                    nought.Draw(2, 2);
                                    cells[2, 2].type = CellType.User;
                                    xlast = 2;
                                    ylast = 2;
                                }
                                else
                                {
                                    label1.Text = "Ячейка уже занята";
                                }
                            }
                        }
                        hdpc = true;   //след ход компьютера
                        hod();
                    }
                }
            }
        }   //рабоатет

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                pc = 1;   //первый ход компьютера
            }
        }  //работает

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                pc = 2;    //первый ход человека
            }
        }    // работает

        private void hod1()
        {
            if (cells[1, 1].type == CellType.Empty)
            {
                cells[1, 1].type = CellType.PC;   //компьютер первым ходом всегда ходит в центр(если есть возможность), независимо от того какими он играет
            }
            else
            {
                random();
            }
            paint();
            hdpc = false;
            pervhod = false;
        }    //работает
        private void hod()
        {
            nichia();    //есть ли свободное поле
            tryToWin();    //1 правило
            if (win == 0)
            {
                tryToProtect();  //2 правило
                if (hdpc == true)   //если 1,2 правила невыполнены, то есть до сих пор ход компа, он должен сделать либо противоположный
                {                              // либо любой ход
                    if (pc == 1)    //если первый сходил комп, значит по тактике нужно ходить точно противоположно ходу юзера
                    {
                        krestiki();//ход противоположный
                    }
                    else  //комп ходит вторым
                    {
                        if (xfir == 1 && yfir == 1)   //ходят в центр - ходим в углы
                        {
                            ugol();
                        }
                        else   //если ходят не в центр
                        {
                            if (pervhod)
                            {
                                hod1();   //ходим в центр
                                pervhod = false;
                            }
                            else
                            {
                                ugol();
                            }
                        }
                    }
                }    //конец противоположного хода
                nichia();
            }
            else
            {
                winner();
            }
        }   // ход компьютера, когда он ходит первый
        private void random()   //заполняем любую пустую клетку, для компьютера
        {
            bool rand = false;
            for (int i = 0; i < Settings.GRID_SIZE; i++)
            {
                for (int j = 0; j < Settings.GRID_SIZE; j++)
                {
                    if (rand == false)
                    {
                        if (cells[i, j].type == CellType.Empty)
                        {
                            cells[i, j].type = CellType.PC;
                            rand = true;
                            hdpc = false;
                            paint();
                        }
                    }
                }
            }
        }   // работает
        private void paint()
        {
            for (int i = 0; i < Settings.GRID_SIZE; i++)
            {
                for (int j = 0; j < Settings.GRID_SIZE; j++)
                {
                    if (pc == 1)   //если компьютер начинал, то 1 - это крестики
                    {
                        if (cells[i, j].type == CellType.PC)
                        {
                            cross.Draw(i, j);
                        }
                    }
                    else   //компьютер ходил вторым, 1 - нолики
                    {
                        if (cells[i, j].type == CellType.PC)
                        {
                            nought.Draw(i, j);
                        }
                    }
                }
            }
        }            //работает
        private void winner()
        {
            dash.Draw();

            if (dash.type != DashType.Empty)
            {
                label1.Text = "Компьютер выиграл!";
            }
            else
            {
                label1.Text = "Ничья!";
            }
        }           // работает

        private bool checkWinability(params Cell[] cellsToCheck)
        {
            var typeSum = cellsToCheck.Sum(x => (int)x.type);
            var anyPCCell = cellsToCheck.Any(x => x.type == CellType.PC);

            return typeSum == 2 && anyPCCell;
        }

        private void tryToWin()
        {
            //НАЧИНАЕТСЯ НАПАДЕНИЕ(ПОПЫТКА ВЫИГРАТЬ, ЕСЛИ ЕСТЬ ВОЗМОЖНОСТЬ)
            if (checkWinability(cells[0, 0], cells[0, 1], cells[0, 2])) //1-4-7  - нападение
            {
                for (int j = 0; j < Settings.GRID_SIZE; j++)
                {
                    if (cells[0, j].type == CellType.Empty)
                    {
                        cells[0, j].type = CellType.PC;
                    }
                }
                win = 1;
                paint();
                dash.type = DashType.HorizontalTop;
            }
            else if (checkWinability(cells[1, 0], cells[1, 1], cells[1, 2])) //2-5-8  - нападение
            {
                for (int j = 0; j < Settings.GRID_SIZE; j++)
                {
                    if (cells[1, j].type == CellType.Empty)
                    {
                        cells[1, j].type = CellType.PC;
                    }
                }
                win = 1;
                paint();
                dash.type = DashType.HorizontalMiddle;
            }
            else if (checkWinability(cells[2, 0], cells[2, 1], cells[2, 2])) //3-6-9  - нападение
            {

                for (int j = 0; j < Settings.GRID_SIZE; j++)
                {
                    if (cells[2, j].type == CellType.Empty)
                    {
                        cells[2, j].type = CellType.PC;
                    }
                }
                win = 1;
                paint();
                dash.type = DashType.HorizontalBottom;
            }
            else if (checkWinability(cells[0, 0], cells[1, 0], cells[2, 0])) //1-2-3  -нападение
            {

                for (int i = 0; i < Settings.GRID_SIZE; i++)
                {
                    if (cells[i, 0].type == CellType.Empty)
                    {
                        cells[i, 0].type = CellType.PC;
                    }
                }
                win = 1;
                paint();
                dash.type = DashType.VerticalLeft;
            }
            else if (checkWinability(cells[0, 1], cells[1, 1], cells[2, 1])) //4-5-6  - нападение
            {

                for (int i = 0; i < Settings.GRID_SIZE; i++)
                {
                    if (cells[i, 1].type == CellType.Empty)
                    {
                        cells[i, 1].type = CellType.PC;
                    }
                }
                win = 1;
                paint();
                dash.type = DashType.VerticalMiddle;
            }
            else if (checkWinability(cells[0, 2], cells[1, 2], cells[2, 2])) //7-8-9  - нападение
            {

                for (int i = 0; i < Settings.GRID_SIZE; i++)
                {
                    if (cells[i, 2].type == CellType.Empty)
                    {
                        cells[i, 2].type = CellType.PC;
                    }
                }
                win = 1;
                paint();
                dash.type = DashType.VerticalRight;
            }
            else if (checkWinability(cells[0, 0], cells[1, 1], cells[2, 2])) //1-5-9  - нападение
            {
                if (cells[0, 0].type == CellType.Empty)
                    cells[0, 0].type = CellType.PC;
                if (cells[1, 1].type == CellType.Empty)
                    cells[1, 1].type = CellType.PC;
                if (cells[2, 2].type == CellType.Empty)
                    cells[2, 2].type = CellType.PC;
                win = 1;
                paint();
                dash.type = DashType.DiagonalLeftTop;

            }
            else if (checkWinability(cells[2, 0], cells[1, 1], cells[0, 2])) //3-5-7  - нападение
            {
                if (cells[2, 0].type == CellType.Empty)
                    cells[2, 0].type = CellType.PC;
                if (cells[1, 1].type == CellType.Empty)
                    cells[1, 1].type = CellType.PC;
                if (cells[0, 2].type == CellType.Empty)
                    cells[0, 2].type = CellType.PC;
                win = 1;
                paint();
                dash.type = DashType.DiagonalLeftBottom;
            }
        }           // работает

        private bool checkNeedProtection(params Cell[] cells)
        {
            var typeSum = cells.Sum(x => (int)x.type);
            var allNotPC = cells.All(x => x.type != CellType.PC);

            return typeSum == 4 && allNotPC;
        }

        private void tryToProtect()
        {
            // защита
            if (checkNeedProtection(cells[0, 0], cells[0, 1], cells[0, 2])) //1-4-7  - защита
            {
                for (int j = 0; j < Settings.GRID_SIZE; j++)
                {
                    if (cells[0, j].type == CellType.Empty)
                    {
                        cells[0, j].type = CellType.PC;
                        hdpc = false;
                        paint();
                    }
                }
            }
            else if (checkNeedProtection(cells[1, 0], cells[1, 1], cells[1, 2])) //2-5-8  - защита
            {
                for (int j = 0; j < Settings.GRID_SIZE; j++)
                {
                    if (cells[1, j].type == CellType.Empty)
                    {
                        cells[1, j].type = CellType.PC;
                        hdpc = false;
                        paint();
                    }
                }
            }
            else if (checkNeedProtection(cells[2, 0], cells[2, 1], cells[2, 2])) //3-6-9  - защита
            {
                for (int j = 0; j < Settings.GRID_SIZE; j++)
                {
                    if (cells[2, j].type == CellType.Empty)
                    {
                        cells[2, j].type = CellType.PC;
                        hdpc = false;
                        paint();

                    }
                }
            }
            else if (checkNeedProtection(cells[0, 0], cells[1, 0], cells[2, 0])) //1-2-3  - защита
            {
                for (int i = 0; i < Settings.GRID_SIZE; i++)
                {
                    if (cells[i, 0].type == CellType.Empty)
                    {
                        cells[i, 0].type = CellType.PC;
                        hdpc = false;
                        paint();
                    }
                }
            }
            else if (checkNeedProtection(cells[0, 1], cells[1, 1], cells[2, 1])) //4-5-6  - защита
            {
                for (int i = 0; i < Settings.GRID_SIZE; i++)
                {
                    if (cells[i, 1].type == CellType.Empty)
                    {
                        cells[i, 1].type = CellType.PC;
                        hdpc = false;
                        paint();
                    }
                }
            }
            else if (checkNeedProtection(cells[0, 2], cells[1, 2], cells[2, 2])) //7-8-9  - защита
            {
                for (int i = 0; i < Settings.GRID_SIZE; i++)
                {
                    if (cells[i, 2].type == CellType.Empty)
                    {
                        cells[i, 2].type = CellType.PC;
                        hdpc = false;
                        paint();
                    }
                }
            }
            else if (checkNeedProtection(cells[0, 0], cells[1, 1], cells[2, 2]))  //1-5-9  - защита
            {
                if (cells[0, 0].type == CellType.Empty)
                    cells[0, 0].type = CellType.PC;
                if (cells[1, 1].type == CellType.Empty)
                    cells[1, 1].type = CellType.PC;
                if (cells[2, 2].type == CellType.Empty)
                    cells[2, 2].type = CellType.PC;
                hdpc = false;
                paint();
            }

            else if (checkNeedProtection(cells[2, 0], cells[1, 1], cells[0, 2]))   //3-5-7  - защита
            {
                if (cells[2, 0].type == CellType.Empty)
                    cells[2, 0].type = CellType.PC;
                if (cells[1, 1].type == CellType.Empty)
                    cells[1, 1].type = CellType.PC;
                if (cells[0, 2].type == CellType.Empty)
                    cells[0, 2].type = CellType.PC;
                hdpc = false;
                paint();

            }       //конец защиты по 2 правилу
        }           // работает

        private void krestiki()   //противоположный ход
        {
            if (xlast == 0 && ylast == 0)  //если 0,0
            {
                if (cells[2, 2].type == CellType.Empty)
                {
                    cells[2, 2].type = CellType.PC;
                    hdpc = false;
                    paint();
                }
                else
                {
                    random();
                }
            }
            else
            {
                if (xlast == 2 && ylast == 0)   //2.0
                {
                    if (cells[0, 2].type == CellType.Empty)
                    {
                        cells[0, 2].type = CellType.PC;
                        hdpc = false;
                        paint();
                    }
                    else
                    {
                        random();
                    }
                }
                else
                {
                    if (xlast == 0 && ylast == 2)   //0.2
                    {
                        if (cells[2, 0].type == CellType.Empty)
                        {
                            cells[2, 0].type = CellType.PC;
                            hdpc = false;
                            paint();
                        }
                        else
                        {
                            random();
                        }
                    }
                    else
                    {
                        if (xlast == 2 && ylast == 2)   //2.2
                        {
                            if (cells[0, 0].type == CellType.Empty)
                            {
                                cells[0, 0].type = CellType.PC;
                                hdpc = false;
                                paint();
                            }
                            else
                            {
                                random();
                            }
                        }
                        else
                        {
                            if (xlast == 0 && ylast == 1)   //0.1
                            {
                                if (cells[2, 0].type == CellType.Empty)
                                {
                                    cells[2, 0].type = CellType.PC;
                                    hdpc = false;
                                    paint();
                                }
                                else
                                {
                                    if (cells[2, 2].type == CellType.Empty)
                                    {
                                        cells[2, 2].type = CellType.PC;
                                        hdpc = false;
                                        paint();
                                    }
                                    else
                                    {
                                        random();
                                    }
                                }
                            }
                            else
                            {
                                if (xlast == 1 && ylast == 0)   //1.0
                                {
                                    if (cells[0, 2].type == CellType.Empty)
                                    {
                                        cells[0, 2].type = CellType.PC;
                                        hdpc = false;
                                        paint();
                                    }
                                    else
                                    {
                                        if (cells[2, 2].type == CellType.Empty)
                                        {
                                            cells[2, 2].type = CellType.PC;
                                            hdpc = false;
                                            paint();
                                        }
                                        else
                                        {
                                            random();
                                        }
                                    }
                                }
                                else
                                {
                                    if (xlast == 2 && ylast == 1)   //2.1
                                    {
                                        if (cells[0, 0].type == CellType.Empty)
                                        {
                                            cells[0, 0].type = CellType.PC;
                                            hdpc = false;
                                            paint();
                                        }
                                        else
                                        {
                                            if (cells[0, 2].type == CellType.Empty)
                                            {
                                                cells[0, 2].type = CellType.PC;
                                                hdpc = false;
                                                paint();
                                            }
                                            else
                                            {
                                                random();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (xlast == 1 && ylast == 2)   //1.2
                                        {
                                            if (cells[0, 0].type == CellType.Empty)
                                            {
                                                cells[0, 0].type = CellType.PC;
                                                hdpc = false;
                                                paint();
                                            }
                                            else
                                            {
                                                if (cells[2, 0].type == CellType.Empty)
                                                {
                                                    cells[2, 0].type = CellType.PC;
                                                    hdpc = false;
                                                    paint();
                                                }
                                                else
                                                {
                                                    random();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }   //работает
        private void nichia()
        {
            bool nich = true;   //если true - то ничья
            for (int i = 0; i < Settings.GRID_SIZE; i++)   //если находим 0, то получим false, то есть не ничья, можно еще ходить
            {
                for (int j = 0; j < Settings.GRID_SIZE; j++)
                {
                    if (nich)
                    {
                        if (cells[i, j].type == CellType.Empty)
                        {
                            nich = false;
                        }
                    }
                }
            }
            if (nich)
            {
                label1.Text = "Ничья";
                win = 3;
                for (int i = 0; i < Settings.GRID_SIZE; i++)
                {
                    for (int j = 0; j < Settings.GRID_SIZE; j++)
                    {
                        cells[i, j].type = CellType.Empty;
                    }
                }
            }
        }     //работает
        private void newgame()
        {
            qwe = false;
            label1.Text = "";
            panel1.Controls.Clear();
            panel1.Invalidate();
            grid.Draw();

            for (int i = 0; i < Settings.GRID_SIZE; i++)
            {
                for (int j = 0; j < Settings.GRID_SIZE; j++)
                {
                    cells[i, j].type = CellType.Empty;
                }
            }

            first = true;
            dash.type = DashType.Empty;   // черта для вычеркивания выигрышной комбинации
            x = -1;
            y = -1;
            xfir = -1; // первый ход юзера
            yfir = -1; // когда он играет крестиками
            xlast = -1;//последний
            ylast = -1;// ход юзера
            win = 0;//если 1- выиграл комп, 2-пользователь, 3- ничья
            hdpc = false;//true - сейчас ход компьютера, false - ход пользователя
            pc = 0;     // кто ходит первым, 1 - ходит комп, 2 первый ходит юзер
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }     //работает
        private void ugol()
        {
            if (cells[0, 0].type == CellType.Empty)
            {
                cells[0, 0].type = CellType.PC;
                hdpc = false;
                paint();
            }
            else
            {
                if (cells[2, 0].type == CellType.Empty)
                {
                    cells[2, 0].type = CellType.PC;
                    hdpc = false;
                    paint();
                }
                else
                {
                    if (cells[0, 2].type == CellType.Empty)
                    {
                        cells[0, 2].type = CellType.PC;
                        hdpc = false;
                        paint();
                    }
                    else
                    {
                        if (cells[2, 2].type == CellType.Empty)
                        {
                            cells[2, 2].type = CellType.PC;
                            hdpc = false;
                            paint();
                        }
                        else
                        {
                            attack();
                        }
                    }
                }
            }
        }    //работает

        private void button2_Click(object sender, EventArgs e)
        {
            newgame();
        }    //работает

        private bool checkIfAttack(params Cell[] cells)
        {
            return cells.Sum(x => (int)x.type) == 1;
        }
        private void attack()
        {
            if (checkIfAttack(cells[0, 0], cells[0, 1], cells[0, 2])) //1-4-7  - нападение
            {
                for (int j = 0; j < Settings.GRID_SIZE; j++)
                {
                    if (cells[0, j].type == CellType.Empty)
                    {
                        cells[0, j].type = CellType.PC;
                    }
                }
                paint();
            }
            else if (checkIfAttack(cells[1, 0], cells[1, 1], cells[1, 2]))   //2-5-8  - нападение
            {

                for (int j = 0; j < Settings.GRID_SIZE; j++)
                {
                    if (cells[1, j].type == CellType.Empty)
                    {
                        cells[1, j].type = CellType.PC;
                    }
                }
                paint();
            }
            else if (checkIfAttack(cells[2, 0], cells[2, 1], cells[2, 2]))  //3-6-9  - нападение
            {

                for (int j = 0; j < Settings.GRID_SIZE; j++)
                {
                    if (cells[2, j].type == CellType.Empty)
                    {
                        cells[2, j].type = CellType.PC;
                    }
                }
                paint();
            }
            else if (checkIfAttack(cells[0, 0], cells[1, 0], cells[2, 0])) //1-2-3  -нападение
            {

                for (int i = 0; i < Settings.GRID_SIZE; i++)
                {
                    if (cells[i, 0].type == CellType.Empty)
                    {
                        cells[i, 0].type = CellType.PC;
                    }
                }
                paint();
            }
            else if (checkIfAttack(cells[0, 1], cells[1, 1], cells[2, 1])) //4-5-6  - нападение
            {

                for (int i = 0; i < Settings.GRID_SIZE; i++)
                {
                    if (cells[i, 1].type == CellType.Empty)
                    {
                        cells[i, 1].type = CellType.PC;
                    }
                }
                paint();
            }
            else if (checkIfAttack(cells[0, 2], cells[1, 2], cells[2, 2]))  //7-8-9  - нападение
            {

                for (int i = 0; i < Settings.GRID_SIZE; i++)
                {
                    if (cells[i, 2].type == CellType.Empty)
                    {
                        cells[i, 2].type = CellType.PC;
                    }
                }
                paint();
            }
            else if (checkIfAttack(cells[0, 0], cells[1, 1], cells[2, 2]))  //1-5-9  - нападение
            {
                if (cells[0, 0].type == CellType.Empty)
                {
                    cells[0, 0].type = CellType.PC;
                }
                else
                {
                    if (cells[1, 1].type == CellType.Empty)
                    {
                        cells[1, 1].type = CellType.PC;
                    }
                    else
                    {
                        if (cells[2, 2].type == CellType.Empty)
                            cells[2, 2].type = CellType.PC;
                    }
                }
                paint();
            }
            else if (checkIfAttack(cells[2, 0], cells[1, 1], cells[0, 2]))   //3-5-7  - нападение
            {
                if (cells[2, 0].type == CellType.Empty)
                {
                    cells[2, 0].type = CellType.PC;
                }
                else
                {
                    if (cells[1, 1].type == CellType.Empty)
                    {
                        cells[1, 1].type = CellType.PC;
                    }
                    else
                    {
                        if (cells[0, 2].type == CellType.Empty)
                        {
                            cells[0, 2].type = CellType.PC;
                        }
                    }
                }
                paint();
            }
            else
            {
                random();
            }
        }  //нападение - работает
    }
}
