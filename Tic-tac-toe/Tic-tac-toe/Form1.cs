using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExtensionMethods;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace Tic_tac_toe
{
    public partial class Form1 : Form
    {
        public bool isGameStarted = false;
        public CellPointer lastMove = new CellPointer(-1, -1); //последний ход 
        public PlayerType win = PlayerType.None;//если 1- выиграл комп, 2-пользователь, 3- ничья
        public int moveCounter = 0; //счетчик ходов
        public PlayerType currentMove = PlayerType.None;//true - сейчас ход компьютера, false - ход пользователя
        public PlayerType firstMove = PlayerType.None;     // кто ходит первым, 1 - ходит комп, 2 первый ходит юзер
        public Requester requester;
        //Инициализируем классы объектов
        Grid grid = new Grid(); // сетка
        Dash dash = new Dash(); // черта для вычеркивания выигрышной комбинации
        Cross cross = new Cross(); // крестик
        Nought nought = new Nought(); // нолик

        //Инициализируем массив данных о текущем состоянии игры, все клетки пустые
        public Cell[,] cells = new Cell[Settings.GRID_SIZE, Settings.GRID_SIZE].Populate(() => new Cell());

        public Form1()
        {
            InitializeComponent();

            //Настраиваем внешний вид игры
            DisplaySettings.panel = panel1.CreateGraphics();
            DisplaySettings.gridBorderPen = new Pen(Color.Blue, 1);
            DisplaySettings.gridPen = new Pen(Color.Blue, 2);
            DisplaySettings.crossPen = new Pen(Color.Black, 3);
            DisplaySettings.noughtPen = new Pen(Color.Black, 3);
            DisplaySettings.dashPen = new Pen(Color.Blue, 6);
            requester = new Requester();
        }

        public Form1(Requester requester)
            : this()
        {
            this.requester = requester;
        }

        private void startGameBtn_Click(object sender, EventArgs e)
        {
            if (firstMove == PlayerType.None)  // pc равен 0 тогда, когда пользователь еще не выбрал кто будет ходить первым
            {
                label1.Text = "Выберите игрока, который будет ходить первым!";
            }
            else if (String.IsNullOrWhiteSpace(userNameTextBox.Text))
            {
                label1.Text = "Введите имя игрока!";
            }
            else
            {
                isGameStarted = true;
                //скрываем выбор первого ходящего
                computerFirstMove.Visible = false;
                playerFirstMove.Visible = false;
                userNameTextBox.ReadOnly = true;
                startGameBtn.Enabled = false;

                if (firstMove == PlayerType.PC) //первый ход компа
                {
                    //делаем ход
                    PCMoveToCenter();
                }
                else //первый ход пользователя
                {
                    label1.Text = "Ваш ход!";
                }

                //ждем ход пользователя
                currentMove = PlayerType.User;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            grid.Draw();
        }

        private void d(int locationX, int locationY)
        {
            label1.Text = "Ваш ход!";
            if (currentMove == PlayerType.User)   //ход пользователя
            {
                //Вычисляем на какую ячейку кликнули 
                int row = locationX / 100;
                int col = locationY / 100;

                //Если ячейка пустая
                if (cells[col, row].type == CellType.Empty)
                {
                    //Запоминаем ход
                    lastMove.Move(col, row);
                    cells[col, row].type = CellType.User;
                    moveCounter++;

                    //Отрисовываем
                    if (firstMove == PlayerType.User)    //первый ход пользователя, значит он играет крестиками
                    {
                        cross.Draw(lastMove);
                    }
                    else
                    {
                        nought.Draw(lastMove);
                    }

                    currentMove = PlayerType.PC;   //след ход компьютера
                    PCMove();
                }
                else
                {
                    throw new CellNotEmptyException();
                }
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (firstMove == PlayerType.None)  //если пользователь сразу нажимает на доску, не выбрав кто будет ходить первым
                {
                    label1.Text = "Выберите игрока, который будет ходить первым!";
                }
                if (isGameStarted)
                {
                    d(e.Location.X, e.Location.Y);
                }
            }
            catch (Exception x)
            {
                label1.Text = "Ячейка уже занята";
            }
        }

        private void computerFirstMove_CheckedChanged(object sender, EventArgs e)
        {
            if (computerFirstMove.Checked)
            {
                firstMove = PlayerType.PC;   //первый ход компьютера
            }
        }

        private void playerFirstMove_CheckedChanged(object sender, EventArgs e)
        {
            if (playerFirstMove.Checked)
            {
                firstMove = PlayerType.User;    //первый ход человека
            }
        }

        private void PCMoveToCenter()
        {
            cells[1, 1].type = CellType.PC;   //компьютер первым ходом всегда ходит в центр(если есть возможность), независимо от того какими он играет
            paint();
            moveCounter++;
        }
        private void PCMove()
        {
            if (checkIfDraw())  //есть ли свободное поле
            {
                return; //если ничья то выходим из функции
            }
            tryToWin();    //1 правило
            if (win == 0)
            {
                tryToProtect();  //2 правило
                if (currentMove == PlayerType.PC)   //если 1,2 правила невыполнены, то есть до сих пор ход компа, он должен сделать либо противоположный
                {                              // либо любой ход
                    if (firstMove == PlayerType.PC)    //если первый сходил комп, значит по тактике нужно ходить точно противоположно ходу юзера
                    {
                        oppositeMove();//ход противоположный
                    }
                    else  //комп ходит вторым
                    {
                        if (cells[1, 1].type == CellType.Empty) //если центральная клетка еще не занята, то ходим в центр
                        {
                            PCMoveToCenter();   //ходим в центр
                        }
                        else
                        {
                            ugol();
                        }
                    }
                    currentMove = PlayerType.User;
                    moveCounter++;
                }    //конец противоположного хода
                checkIfDraw();
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
                            currentMove = PlayerType.User;
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
                    if (firstMove == PlayerType.PC)   //если компьютер начинал, то 1 - это крестики
                    {
                        if (cells[i, j].type == CellType.PC)
                        {
                            cross.Draw(new CellPointer(i, j));
                        }
                    }
                    else   //компьютер ходил вторым, 1 - нолики
                    {
                        if (cells[i, j].type == CellType.PC)
                        {
                            nought.Draw(new CellPointer(i, j));
                        }
                    }
                }
            }
        }            //работает
        private void winner()
        {
            dash.Draw();
            isGameStarted = false;
            if (dash.type != DashType.Empty)
            {
                label1.Text = "Компьютер выиграл!";
            }
            else
            {
                label1.Text = "Ничья!";
            }
            SaveGameInfo();
        }           // работает

        //Проверяем, можно ли выиграть на данном наборе ячеек
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
                win = PlayerType.PC;
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
                win = PlayerType.PC;
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
                win = PlayerType.PC;
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
                win = PlayerType.PC;
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
                win = PlayerType.PC;
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
                win = PlayerType.PC;
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
                win = PlayerType.PC;
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
                win = PlayerType.PC;
                paint();
                dash.type = DashType.DiagonalLeftBottom;
            }
        }           // работает

        //Проверяем, нужно ли защищаться на данном наборе ячеек
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
                        currentMove = PlayerType.User;
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
                        currentMove = PlayerType.User;
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
                        currentMove = PlayerType.User;
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
                        currentMove = PlayerType.User;
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
                        currentMove = PlayerType.User;
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
                        currentMove = PlayerType.User;
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
                currentMove = PlayerType.User;
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
                currentMove = PlayerType.User;
                paint();

            }       //конец защиты по 2 правилу
        }           // работает

        private void oppositeMove()   //противоположный ход
        {
            if (lastMove.row == 0 && lastMove.col == 0)  //если 0,0
            {
                if (cells[2, 2].type == CellType.Empty)
                {
                    cells[2, 2].type = CellType.PC;
                    currentMove = PlayerType.User;
                    paint();
                }
                else
                {
                    random();
                }
            }
            else
            {
                if (lastMove.row == 2 && lastMove.col == 0)   //2.0
                {
                    if (cells[0, 2].type == CellType.Empty)
                    {
                        cells[0, 2].type = CellType.PC;
                        currentMove = PlayerType.User;
                        paint();
                    }
                    else
                    {
                        random();
                    }
                }
                else
                {
                    if (lastMove.row == 0 && lastMove.col == 2)   //0.2
                    {
                        if (cells[2, 0].type == CellType.Empty)
                        {
                            cells[2, 0].type = CellType.PC;
                            currentMove = PlayerType.User;
                            paint();
                        }
                        else
                        {
                            random();
                        }
                    }
                    else
                    {
                        if (lastMove.row == 2 && lastMove.col == 2)   //2.2
                        {
                            if (cells[0, 0].type == CellType.Empty)
                            {
                                cells[0, 0].type = CellType.PC;
                                currentMove = PlayerType.User;
                                paint();
                            }
                            else
                            {
                                random();
                            }
                        }
                        else
                        {
                            if (lastMove.row == 0 && lastMove.col == 1)   //0.1
                            {
                                if (cells[2, 0].type == CellType.Empty)
                                {
                                    cells[2, 0].type = CellType.PC;
                                    currentMove = PlayerType.User;
                                    paint();
                                }
                                else
                                {
                                    if (cells[2, 2].type == CellType.Empty)
                                    {
                                        cells[2, 2].type = CellType.PC;
                                        currentMove = PlayerType.User;
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
                                if (lastMove.row == 1 && lastMove.col == 0)   //1.0
                                {
                                    if (cells[0, 2].type == CellType.Empty)
                                    {
                                        cells[0, 2].type = CellType.PC;
                                        currentMove = PlayerType.User;
                                        paint();
                                    }
                                    else
                                    {
                                        if (cells[2, 2].type == CellType.Empty)
                                        {
                                            cells[2, 2].type = CellType.PC;
                                            currentMove = PlayerType.User;
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
                                    if (lastMove.row == 2 && lastMove.col == 1)   //2.1
                                    {
                                        if (cells[0, 0].type == CellType.Empty)
                                        {
                                            cells[0, 0].type = CellType.PC;
                                            currentMove = PlayerType.User;
                                            paint();
                                        }
                                        else
                                        {
                                            if (cells[0, 2].type == CellType.Empty)
                                            {
                                                cells[0, 2].type = CellType.PC;
                                                currentMove = PlayerType.User;
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
                                        if (lastMove.row == 1 && lastMove.col == 2)   //1.2
                                        {
                                            if (cells[0, 0].type == CellType.Empty)
                                            {
                                                cells[0, 0].type = CellType.PC;
                                                currentMove = PlayerType.User;
                                                paint();
                                            }
                                            else
                                            {
                                                if (cells[2, 0].type == CellType.Empty)
                                                {
                                                    cells[2, 0].type = CellType.PC;
                                                    currentMove = PlayerType.User;
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
        public bool checkIfDraw()
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
                isGameStarted = false;
                label1.Text = "Ничья";
                win = PlayerType.None;
                SaveGameInfo();
                for (int i = 0; i < Settings.GRID_SIZE; i++)
                {
                    for (int j = 0; j < Settings.GRID_SIZE; j++)
                    {
                        cells[i, j].type = CellType.Empty;
                    }
                }
            }
            return nich;
        }     //работает
        private void newgame()
        {
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

            dash.type = DashType.Empty;   // черта для вычеркивания выигрышной комбинации
            lastMove.row = -1;//последний
            lastMove.col = -1;// ход юзера
            win = 0;//если 1- выиграл комп, 2-пользователь, 3- ничья
            moveCounter = 0;
            currentMove = PlayerType.User;//true - сейчас ход компьютера, false - ход пользователя
            firstMove = 0;     // кто ходит первым, 1 - ходит комп, 2 первый ходит юзер
            computerFirstMove.Visible = true;
            playerFirstMove.Visible = true;
            userNameTextBox.ReadOnly = false;
            computerFirstMove.Checked = false;
            playerFirstMove.Checked = false;
            startGameBtn.Enabled = true;
        }     //работает
        private void ugol()
        {
            if (cells[0, 0].type == CellType.Empty)
            {
                cells[0, 0].type = CellType.PC;
                currentMove = PlayerType.User;
                paint();
            }
            else
            {
                if (cells[2, 0].type == CellType.Empty)
                {
                    cells[2, 0].type = CellType.PC;
                    currentMove = PlayerType.User;
                    paint();
                }
                else
                {
                    if (cells[0, 2].type == CellType.Empty)
                    {
                        cells[0, 2].type = CellType.PC;
                        currentMove = PlayerType.User;
                        paint();
                    }
                    else
                    {
                        if (cells[2, 2].type == CellType.Empty)
                        {
                            cells[2, 2].type = CellType.PC;
                            currentMove = PlayerType.User;
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

        private void clearPanelBtn_Click(object sender, EventArgs e)
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

        public void SaveGameInfo()
        {
            var gameInfo = new GameInfo()
            {
                UserName = userNameTextBox.Text,
                FirstMove = firstMove.ToString(),
                MoveCount = moveCounter,
                Winner = win.ToString(),
                CreateDate = DateTime.Now
            };

            //кодируем данные
            byte[] byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(gameInfo));

            //делаем запрос
            requester.PostRequest("http://localhost:58108/Home/SaveGameInfo", byteArray);
        }

        public List<GameInfo> GetGameInfo()
        {
            //делаем запрос
            var response = requester.GetRequest("http://localhost:58108/Home/GetGameInfo");

            //десериализуем ответ
            return JsonConvert.DeserializeObject<List<GameInfo>>(response);

        }

        private void statsButton_Click(object sender, EventArgs e)
        {
            var gameInfoList = GetGameInfo();

            if (gameInfoList != null)
            {
                StatsForm statsForm = new StatsForm(gameInfoList);
                statsForm.Show();
            }
        }
    }
}
