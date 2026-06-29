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

        public GameForm(GameSettings i_GameSettings) : this()
        {
            Player player1 = new Player(i_GameSettings.Player1Name, ePlayerSymbol.X, false);

            bool isPlayer2CPU = (i_GameSettings.GameMode == eGameMode.PlayerVsCPU);
            Player player2 = new Player(i_GameSettings.Player2Name, ePlayerSymbol.O, isPlayer2CPU);

            m_Game = new Game(i_GameSettings.BoardSize, player1, player2, 0);

            m_CPU = isPlayer2CPU ? new CPU(player2, m_Game) : null;

            buildBoard();
        }

        private void buildBoard()
        {
            int boardSize = m_Game.Board.BoardSize;
            m_BoardButtons = new Button[boardSize, boardSize];

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    m_BoardButtons[i, j] = new Button();

                    m_BoardButtons[i, j].Size = new Size(k_CellSize, k_CellSize);
                    m_BoardButtons[i, j].Location = new Point(i * k_CellSize, j * k_CellSize);
                    m_BoardButtons[i, j].Text = ePlayerSymbol.None.ToString();
                    m_BoardButtons[i, j].Tag = new Point(i, j);
                    m_BoardButtons[i, j].Click += cellButton_Click;
                }
            }
        }

        private void cellButton_Click(object sender, EventArgs e)
        {
            Point buttonLocation = new Point((sender as Button).Tag);
        }
    }
}