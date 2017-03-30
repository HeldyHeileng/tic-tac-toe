using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tic_tac_toe
{
    public partial class StatsForm : Form
    {
        public StatsForm(List<GameInfo> gameInfo)
        {
            InitializeComponent();

            ganeInfoDataGrid.DataSource = gameInfo;
        }
    }
}
