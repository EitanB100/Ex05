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
            this.ClientSize = new Size(k_CellSize * i_GameSettings.BoardSize, k_CellSize * i_GameSettings.BoardSize);
        }

        private void buildBoard()
        {
            int boardSize = m_Game.Board.BoardSize;
            m_BoardButtons = new Button[boardSize, boardSize];

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    m_BoardButtons[col, row] = new Button();

                    m_BoardButtons[col, row].Size = new Size(k_CellSize, k_CellSize);
                    m_BoardButtons[col, row].Location = new Point(col * k_CellSize, row * k_CellSize);
                    m_BoardButtons[col, row].Text = string.Empty;
                    m_BoardButtons[col, row].Tag = new Point(col, row);
                    m_BoardButtons[col, row].Click += cellButton_Click;

                    Controls.Add(m_BoardButtons[col, row]);
                }
            }
        }

        private void cellButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            Point cell = (Point)clickedButton.Tag;
        }
    }
}