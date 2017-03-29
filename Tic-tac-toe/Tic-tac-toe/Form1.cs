using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace креститки_нолики
{
    public partial class Form1 : Form
    {
        public bool qwe = false;
        public bool pervhod = true;  //true говорит о том, что у компьютера нынче первый ход. первым ходом компьютер должен ходить в
                                     //центр, дальше по ситуации. значение false переправляет на путь "дальше по ситуации"
        public bool first = true;   //меняется на false сразу после первого хода юзера(играет крестиками), нужен для проверки, был ли
                                     // первый ход в центр или нет
        public int cherta = 0;   // черта для вычеркивания выигрышной комбинации
        public int x = -1;
        public int y = -1;
        public int xfir = -1; // первый ход юзера
        public int yfir = -1; // когда он играет крестиками
        public int xlast = -1;//последний
        public int ylast = -1;// ход юзера
        public int win = 0;//если 1- выиграл комп, 2-пользователь, 3- ничья
        public bool hdpc = false;//true - сейчас ход компьютера, false - ход пользователя
        public int pc = 0;     // кто ходит первым, 1 - ходит комп, 2 первый ходит юзер
        public int[,] a = new int[3, 3];   //если равен 0=пустая клетка, равен 1 - ход компа, 2 - юзер
        public Form1()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 0; j++)
                {
                    a[i, j] = 0;
                }
            }
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            qwe = true;
            if (pc == 0)  // pc равен 0 тогда, когда пользователь еще не выбрал кто будет ходить первым
            {
               label1.Text="Выберите игрока, который будет ходить первым!";
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
            Graphics gPanel = panel1.CreateGraphics();
            Pen p = new Pen(Color.Blue, 1);
            Pen p1 = new Pen(Color.Blue, 2);
            gPanel.DrawLine(p, new Point(0, 0), new Point(300, 0));
            gPanel.DrawLine(p, new Point(300, 0), new Point(300, 300));
            gPanel.DrawLine(p, new Point(0, 0), new Point(0, 300));
            gPanel.DrawLine(p, new Point(0, 300), new Point(300, 300));
            gPanel.DrawLine(p1, new Point(100,0), new Point(100, 300));
            gPanel.DrawLine(p1, new Point(200, 0), new Point(200, 300));
            gPanel.DrawLine(p1, new Point(0, 100), new Point(300, 100));
            gPanel.DrawLine(p1, new Point(0, 200), new Point(300, 200));
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

                                if (a[0, 0] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawLine(p, 2, 2, 98, 98);
                                    gPanel.DrawLine(p, 98, 2, 2, 98);
                                    a[0, 0] = 2;
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
                                if (a[1, 0] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawLine(p, 102, 2, 198, 98);
                                    gPanel.DrawLine(p, 198, 2, 102, 98);
                                    a[1, 0] = 2;
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
                                if (a[2, 0] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawLine(p, 202, 2, 298, 98);
                                    gPanel.DrawLine(p, 298, 2, 202, 98);
                                    a[2, 0] = 2;
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
                                if (a[0, 1] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawLine(p, 2, 102, 98, 198);
                                    gPanel.DrawLine(p, 98, 102, 2, 198);
                                    a[0, 1] = 2;
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
                                if (a[1, 1] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawLine(p, 102, 102, 198, 198);
                                    gPanel.DrawLine(p, 198, 102, 102, 198);
                                    a[1, 1] = 2;
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
                                if (a[2, 1] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawLine(p, 202, 102, 298, 198);
                                    gPanel.DrawLine(p, 298, 102, 202, 198);
                                    a[2, 1] = 2;
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
                                if (a[0, 2] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawLine(p, 2, 202, 98, 298);
                                    gPanel.DrawLine(p, 98, 202, 2, 298);
                                    a[0, 2] = 2;
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
                                if (a[1, 2] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawLine(p, 102, 202, 198, 298);
                                    gPanel.DrawLine(p, 198, 202, 102, 298);
                                    a[1, 2] = 2;
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
                                if (a[2, 2] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawLine(p, 202, 202, 298, 298);
                                    gPanel.DrawLine(p, 298, 202, 202, 298);
                                    a[2, 2] = 2;
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

                                if (a[0, 0] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawEllipse(p, 4, 4, 92, 92);
                                    a[0, 0] = 2;
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
                                if (a[1, 0] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawEllipse(p, 104, 4, 92, 92);
                                    a[1, 0] = 2;
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
                                if (a[2, 0] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawEllipse(p, 204, 4, 92, 92);
                                    a[2, 0] = 2;
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
                                if (a[0, 1] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawEllipse(p, 4, 104, 92, 92);
                                    a[0, 1] = 2;
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
                                if (a[1, 1] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawEllipse(p, 104, 104, 92, 92);
                                    a[1, 1] = 2;
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
                                if (a[2, 1] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawEllipse(p, 204, 104, 92, 92);
                                    a[2, 1] = 2;
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
                                if (a[0, 2] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawEllipse(p, 4, 204, 92, 92);
                                    a[0, 2] = 2;
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
                                if (a[1, 2] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawEllipse(p, 104, 204, 92, 92);
                                    a[1, 2] = 2;
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
                                if (a[2, 2] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    Pen p = new Pen(Color.Black, 3);
                                    gPanel.DrawEllipse(p, 204, 204, 92, 92);
                                    a[2, 2] = 2;
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
            if (a[1, 1] == 0)
            {
                a[1, 1] = 1;   //компьютер первым ходом всегда ходит в центр(если есть возможность), независимо от того какими он играет
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
            pobeda();    //1 правило
            if (win == 0)
            {
                zachita();  //2 правило
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
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (rand == false)
                    {
                        if (a[i, j] == 0)
                        {
                            a[i, j] = 1;
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
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (pc == 1)   //если компьютер начинал, то 1 - это крестики
                    {
                        if (a[i, j] == 1)
                        {
                            Graphics gPanel = panel1.CreateGraphics();
                            Pen p = new Pen(Color.Black, 3);
                            gPanel.DrawLine(p, 2+i*100, 2+j*100, 98+i*100, 98+j*100);
                            gPanel.DrawLine(p, 98+i*100, 2+j*100, 2+i*100, 98+j*100);
                        }
                    }
                    else   //компьютер ходил вторым, 1 - нолики
                    {
                        if (a[i, j] == 1)
                        {
                            Graphics gPanel = panel1.CreateGraphics();
                            Pen p = new Pen(Color.Black, 3);
                            gPanel.DrawEllipse(p, 4+i*100, 4+j*100, 92, 92);
                        }
                    }
                }
            }
        }            //работает
        private void winner()
        {
            Graphics gPanel = panel1.CreateGraphics();
            Pen p = new Pen(Color.Blue, 6);
            switch (cherta)
            {
                case 1: gPanel.DrawLine(p, 2, 50, 298, 50); break;
                case 2: gPanel.DrawLine(p, 2, 150, 298, 150); break;
                case 3: gPanel.DrawLine(p, 2, 250, 298, 250); break;
                case 4: gPanel.DrawLine(p, 50, 2, 50, 298); break;
                case 5: gPanel.DrawLine(p, 150, 2, 150, 298); break;
                case 6: gPanel.DrawLine(p, 250, 2, 250, 298); break;
                case 7: gPanel.DrawLine(p, 2, 2, 298, 298); break;
                case 8: gPanel.DrawLine(p, 298, 2, 2, 298); break;
            }
            if (cherta > 0)
            {
                label1.Text = "Компьютер выиграл!";
            }
            else
            {
                label1.Text = "Ничья!";
            }
        }           // работает
        private void pobeda()
        {
            //НАЧИНАЕТСЯ НАПАДЕНИЕ(ПОПЫТКА ВЫИГРАТЬ, ЕСЛИ ЕСТЬ ВОЗМОЖНОСТЬ)
            if (((a[0, 0] + a[0, 1] + a[0, 2]) == 2) && (a[0, 0] == 1 || a[0, 1] == 1 || a[0, 2] == 1))  //1-4-7  - нападение
            {
                for (int j = 0; j < 3; j++)
                {
                    if (a[0, j] == 0)
                    {
                        a[0, j] = 1;
                    }
                }
                win = 1;
                paint();
                cherta = 4;
            }
            else
            {
                if (((a[1, 0] + a[1, 1] + a[1, 2]) == 2) && (a[1, 0] == 1 || a[1, 1] == 1 || a[1, 2] == 1))   //2-5-8  - нападение
                {

                    for (int j = 0; j < 3; j++)
                    {
                        if (a[1, j] == 0)
                        {
                            a[1, j] = 1;
                        }
                    }
                    win = 1;
                    paint();
                    cherta = 5;
                }
                else
                {
                    if (((a[2, 0] + a[2, 1] + a[2, 2]) == 2) && (a[2, 0] == 1 || a[2, 1] == 1 || a[2, 2] == 1))   //3-6-9  - нападение
                    {

                        for (int j = 0; j < 3; j++)
                        {
                            if (a[2, j] == 0)
                            {
                                a[2, j] = 1;
                            }
                        }
                        win = 1;
                        paint();
                        cherta = 6;
                    }
                    else
                    {
                        if (((a[0, 0] + a[1, 0] + a[2, 0]) == 2) && (a[0, 0] == 1 || a[1, 0] == 1 || a[2, 0] == 1))   //1-2-3  -нападение
                        {

                            for (int i = 0; i < 3; i++)
                            {
                                if (a[i, 0] == 0)
                                {
                                    a[i, 0] = 1;
                                }
                            }
                            win = 1;
                            paint();
                            cherta = 1;
                        }
                        else
                        {
                            if (((a[0, 1] + a[1, 1] + a[2, 1]) == 2) && (a[0, 1] == 1 || a[1, 1] == 1 || a[2, 1] == 1))  //4-5-6  - нападение
                            {

                                for (int i = 0; i < 3; i++)
                                {
                                    if (a[i, 1] == 0)
                                    {
                                        a[i, 1] = 1;
                                    }
                                }
                                win = 1;
                                paint();
                                cherta = 2;
                            }
                            else
                            {
                                if (((a[0, 2] + a[1, 2] + a[2, 2]) == 2) && (a[0, 2] == 1 || a[1, 2] == 1 || a[2, 2] == 1))   //7-8-9  - нападение
                                {

                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (a[i, 2] == 0)
                                        {
                                            a[i, 2] = 1;
                                        }
                                    }
                                    win = 1;
                                    paint();
                                    cherta = 3;
                                }
                                else
                                {
                                    if (((a[0, 0] + a[1, 1] + a[2, 2]) == 2) && (a[0, 0] == 1 || a[1, 1] == 1 || a[2, 2] == 1))   //1-5-9  - нападение
                                    {
                                        if (a[0, 0] == 0)
                                            a[0, 0] = 1;
                                        if (a[1, 1] == 0)
                                            a[1, 1] = 1;
                                        if (a[2, 2] == 0)
                                            a[2, 2] = 1;
                                        win = 1;
                                        paint();
                                        cherta = 7;

                                    }
                                    else
                                    {
                                        if (((a[2, 0] + a[1, 1] + a[0, 2]) == 2) && (a[2, 0] == 1 || a[1, 1] == 1 || a[0, 2] == 1))   //3-5-7  - нападение
                                        {
                                            if (a[2, 0] == 0)
                                                a[2, 0] = 1;
                                            if (a[1, 1] == 0)
                                                a[1, 1] = 1;
                                            if (a[0, 2] == 0)
                                                a[0, 2] = 1;
                                            win = 1;
                                            paint();
                                            cherta = 8;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }           // работает
        private void zachita()
        {
            // защита
            if ((a[0, 0] + a[0, 1] + a[0, 2]) == 4 && a[0, 0]!=1 && a[0, 1]!=1 && a[0, 2]!=1)   //1-4-7  - защита
            {
                for (int j = 0; j < 3; j++)
                {
                    if (a[0, j] == 0)
                    {
                        a[0, j] = 1;
                        hdpc = false;
                        paint();
                    }
                }
            }
            else
            {
                if ((a[1, 0] + a[1, 1] + a[1, 2]) == 4 && a[1, 0] != 1 && a[1, 1] != 1 && a[1, 2] != 1)   //2-5-8  - защита
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (a[1, j] == 0)
                        {
                            a[1, j] = 1;
                            hdpc = false;
                            paint();
                        }
                    }
                }
                else
                {
                    if ((a[2, 0] + a[2, 1] + a[2, 2]) == 4 && a[2, 0] != 1 && a[2, 1] != 1 && a[2, 2] != 1)   //3-6-9  - защита
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (a[2, j] == 0)
                            {
                                a[2, j] = 1;
                                hdpc = false;
                                paint();

                            }
                        }
                    }
                    else
                    {
                        if ((a[0, 0] + a[1, 0] + a[2, 0]) == 4 && a[0, 0] != 1 && a[1, 0] != 1 && a[2, 0] != 1)   //1-2-3  - защита
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (a[i, 0] == 0)
                                {
                                    a[i, 0] = 1;
                                    hdpc = false;
                                    paint();
                                }
                            }
                        }
                        else
                        {
                            if ((a[0, 1] + a[1, 1] + a[2, 1]) == 4 && a[0, 1] != 1 && a[1, 1] != 1 && a[2, 1] != 1)   //4-5-6  - защита
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (a[i, 1] == 0)
                                    {
                                        a[i, 1] = 1;
                                        hdpc = false;
                                        paint();
                                    }
                                }
                            }
                            else
                            {
                                if ((a[0, 2] + a[1, 2] + a[2, 2]) == 4 && a[0, 2] != 1 && a[1, 2] != 1 && a[2, 2] != 1)   //7-8-9  - защита
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (a[i, 2] == 0)
                                        {
                                            a[i, 2] = 1;
                                            hdpc = false;
                                            paint();
                                        }
                                    }
                                }
                                else
                                {
                                    if ((a[0, 0] + a[1, 1] + a[2, 2]) == 4 && a[0, 0] != 1 && a[1, 1] != 1 && a[2, 2] != 1)   //1-5-9  - защита
                                    {
                                        if (a[0, 0] == 0)
                                            a[0, 0] = 1;
                                        if (a[1, 1] == 0)
                                            a[1, 1] = 1;
                                        if (a[2, 2] == 0)
                                            a[2, 2] = 1;
                                        hdpc = false;
                                        paint();
                                    }
                                    else
                                    {
                                        if ((a[2, 0] + a[1, 1] + a[0, 2]) == 4 && a[2, 0] != 1 && a[1, 1] != 1 && a[0, 2] != 1)   //3-5-7  - защита
                                        {
                                            if (a[2, 0] == 0)
                                                a[2, 0] = 1;
                                            if (a[1, 1] == 0)
                                                a[1, 1] = 1;
                                            if (a[0, 2] == 0)
                                                a[0, 2] = 1;
                                            hdpc = false;
                                            paint();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }       //конец защиты по 2 правилу
        }           // работает

        private void krestiki()   //противоположный ход
        {
            if (xlast == 0 && ylast == 0)  //если 0,0
            {
                if (a[2, 2] == 0)
                {
                    a[2, 2] = 1;
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
                    if (a[0, 2] == 0)
                    {
                        a[0, 2] = 1;                
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
                        if (a[2, 0] == 0)
                        {
                            a[2, 0] = 1;
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
                            if (a[0, 0] == 0)
                            {
                                a[0, 0] = 1;
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
                                if (a[2, 0] == 0)
                                {
                                    a[2, 0] = 1;
                                    hdpc = false;
                                    paint();
                                }
                                else
                                {
                                    if (a[2, 2] == 0)
                                    {
                                        a[2, 2] = 1;
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
                                    if (a[0, 2] == 0)
                                    {
                                        a[0, 2] = 1;
                                        hdpc = false;
                                        paint();
                                    }
                                    else
                                    {
                                        if (a[2, 2] == 0)
                                        {
                                            a[2, 2] = 1;
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
                                        if (a[0, 0] == 0)
                                        {
                                            a[0, 0] = 1;
                                            hdpc = false;
                                            paint();
                                        }
                                        else
                                        {
                                            if (a[0, 2] == 0)
                                            {
                                                a[0, 2] = 1;
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
                                            if (a[0, 0] == 0)
                                            {
                                                a[0, 0] = 1;
                                                hdpc = false;
                                                paint();
                                            }
                                            else
                                            {
                                                if (a[2, 0] == 0)
                                                {
                                                    a[2, 0] = 1;
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
            for (int i = 0; i < 3; i++)   //если находим 0, то получим false, то есть не ничья, можно еще ходить
            {
                for (int j = 0; j < 3; j++)
                {
                    if (nich)   
                    {
                        if (a[i, j] == 0)
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
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        a[i, j] = 0;
                    }
                }
            }
        }     //работает
        private void newgame()
        {
            qwe = false;
            label1.Text = "";
            Graphics gPanel = panel1.CreateGraphics();
            panel1.Controls.Clear();
            panel1.Invalidate();
            Pen p = new Pen(Color.Blue, 1);
            Pen p1 = new Pen(Color.Blue, 2);
            gPanel.DrawLine(p, new Point(0, 0), new Point(300, 0));
            gPanel.DrawLine(p, new Point(300, 0), new Point(300, 300));
            gPanel.DrawLine(p, new Point(0, 0), new Point(0, 300));
            gPanel.DrawLine(p, new Point(0, 300), new Point(300, 300));
            gPanel.DrawLine(p1, new Point(100, 0), new Point(100, 300));
            gPanel.DrawLine(p1, new Point(200, 0), new Point(200, 300));
            gPanel.DrawLine(p1, new Point(0, 100), new Point(300, 100));
            gPanel.DrawLine(p1, new Point(0, 200), new Point(300, 200));
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    a[i, j] = 0;
                }
            }
            first = true;
            cherta = 0;   // черта для вычеркивания выигрышной комбинации
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
            if (a[0, 0] == 0)
            {
                a[0, 0] = 1;
                hdpc = false;
                paint();
            }
            else
            {
                if (a[2, 0] == 0)
                {
                    a[2, 0] = 1;
                    hdpc = false;
                    paint();
                }
                else
                {
                    if (a[0, 2] == 0)
                    {
                        a[0, 2] = 1;
                        hdpc = false;
                        paint();
                    }
                    else
                    {
                        if (a[2, 2] == 0)
                        {
                            a[2, 2] = 1;
                            hdpc = false;
                            paint();
                        }
                        else
                        {
                            napad();
                        }
                    }
                }
            }
        }    //работает

        private void button2_Click(object sender, EventArgs e)
        {
            newgame();
        }    //работает
        private void napad()
        {
            if ((a[0, 0] + a[0, 1] + a[0, 2]) == 1)  //1-4-7  - нападение
            {
                for (int j = 0; j < 3; j++)
                {
                    if (a[0, j] == 0)
                    {
                        a[0, j] = 1;
                    }
                }
                paint();
            }
            else
            {
                if ((a[1, 0] + a[1, 1] + a[1, 2]) == 1)   //2-5-8  - нападение
                {

                    for (int j = 0; j < 3; j++)
                    {
                        if (a[1, j] == 0)
                        {
                            a[1, j] = 1;
                        }
                    }
                    paint();
                }
                else
                {
                    if ((a[2, 0] + a[2, 1] + a[2, 2]) == 1)   //3-6-9  - нападение
                    {

                        for (int j = 0; j < 3; j++)
                        {
                            if (a[2, j] == 0)
                            {
                                a[2, j] = 1;
                            }
                        }
                        paint();
                    }
                    else
                    {
                        if ((a[0, 0] + a[1, 0] + a[2, 0]) == 1)   //1-2-3  -нападение
                        {

                            for (int i = 0; i < 3; i++)
                            {
                                if (a[i, 0] == 0)
                                {
                                    a[i, 0] = 1;
                                }
                            }
                            paint();
                        }
                        else
                        {
                            if ((a[0, 1] + a[1, 1] + a[2, 1]) == 1)  //4-5-6  - нападение
                            {

                                for (int i = 0; i < 3; i++)
                                {
                                    if (a[i, 1] == 0)
                                    {
                                        a[i, 1] = 1;
                                    }
                                }
                                paint();
                            }
                            else
                            {
                                if ((a[0, 2] + a[1, 2] + a[2, 2]) == 1)   //7-8-9  - нападение
                                {

                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (a[i, 2] == 0)
                                        {
                                            a[i, 2] = 1;
                                        }
                                    }
                                    paint();
                                }
                                else
                                {
                                    if ((a[0, 0] + a[1, 1] + a[2, 2]) == 1)   //1-5-9  - нападение
                                    {
                                        if (a[0, 0] == 0)
                                        {
                                            a[0, 0] = 1;
                                        }
                                        else
                                        {
                                            if (a[1, 1] == 0)
                                            {
                                                a[1, 1] = 1;
                                            }
                                            else
                                            {
                                                if (a[2, 2] == 0)
                                                    a[2, 2] = 1;
                                            }
                                        }
                                        paint();
                                    }
                                    else
                                    {
                                        if ((a[2, 0] + a[1, 1] + a[0, 2]) == 1)   //3-5-7  - нападение
                                        {
                                            if (a[2, 0] == 0)
                                            {
                                                a[2, 0] = 1;
                                            }
                                            else
                                            {
                                                if (a[1, 1] == 0)
                                                {
                                                    a[1, 1] = 1;
                                                }
                                                else
                                                {
                                                    if (a[0, 2] == 0)
                                                    {
                                                        a[0, 2] = 1;
                                                    }
                                                }
                                            }
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
        }  //нападение - работает
    }
}
