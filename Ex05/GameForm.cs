using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05
{
    public partial class GameForm : Form
    {
        const int k_CellSize = 60;

        Game m_Game;
        CPU m_CPU;
        Button[,] m_BoardButtons;
        Label m_LabelScorePlayer1;
        Label m_LabelScorePlayer2;

        public GameForm()
        {
            InitializeComponent();

        }
    }
}