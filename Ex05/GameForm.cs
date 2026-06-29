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
        const int k_ScoreBarSize = 20;
        const int k_Margin = 40;

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
            int boardSize = i_GameSettings.BoardSize;

            Player player1 = new Player(i_GameSettings.Player1Name, ePlayerSymbol.X, false);

            bool isPlayer2CPU = (i_GameSettings.GameMode == eGameMode.PlayerVsCPU);
            Player player2 = new Player(i_GameSettings.Player2Name, ePlayerSymbol.O, isPlayer2CPU);

            m_Game = new Game(i_GameSettings.BoardSize, player1, player2, 0);
            m_CPU = isPlayer2CPU ? new CPU(player2, m_Game) : null;

            buildBoard(boardSize);
            buildScoreLabels(boardSize);

            this.ClientSize = new Size(k_CellSize * boardSize + k_ScoreBarSize, k_CellSize * boardSize + k_ScoreBarSize);
        }

        private void buildBoard(int i_BoardSize)
        {
            m_BoardButtons = new Button[i_BoardSize, i_BoardSize];

            for (int row = 0; row < i_BoardSize; row++)
            {
                for (int col = 0; col < i_BoardSize; col++)
                {
                    addButtonToGrid(row, col);
                }
            }
        }

        private void buildScoreLabels(int i_BoardSize)
        {
            m_LabelScorePlayer1 = new Label();
            m_LabelScorePlayer2 = new Label();

            m_LabelScorePlayer1.Location = new Point(i_BoardSize * k_CellSize, i_BoardSize * k_CellSize + k_Margin);
            m_LabelScorePlayer2.Location = new Point(i_BoardSize * k_CellSize + k_Margin, i_BoardSize * k_CellSize + k_Margin);

        }

        private void addButtonToGrid(int row, int col)
        {
            m_BoardButtons[col, row] = new Button();

            m_BoardButtons[col, row].Size = new Size(k_CellSize, k_CellSize);
            m_BoardButtons[col, row].Location = new Point(col * k_CellSize, row * k_CellSize);
            m_BoardButtons[col, row].Text = string.Empty;
            m_BoardButtons[col, row].Tag = new Point(col, row);
            m_BoardButtons[col, row].Click += cellButton_Click;

            Controls.Add(m_BoardButtons[col, row]);
        }

        private void refreshBoard(int i_BoardSize)
        {

            for (int row = 0; row < i_BoardSize; row++)
            {
                for (int col = 0; col < i_BoardSize; col++)
                {
                    ePlayerSymbol symbol = m_Game.Board.GetCell(row, col);
                    Button button = m_BoardButtons[col, row];

                    button.Text = playerSymbolToString(symbol);
                    button.Enabled = (symbol == ePlayerSymbol.None);
                }
            }

        }

        private void cellButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            Point cell = (Point)clickedButton.Tag;

            int col = cell.X;
            int row = cell.Y;

            playMove(row, col);

            if (m_Game.GameState != eGameState.InProgress)
            {
                handleEndOfRound();
            }
        }

        private void handleEndOfRound()
        {
            StringBuilder endMessage = new StringBuilder();

            switch (m_Game.GameState)
            {
                case eGameState.Draw:
                    endMessage.Append("Tie!");
                    break;
                case eGameState.Winner:
                    endMessage.Append("The winner is ");
                    endMessage.Append(m_Game.CurrentPlayer.Name);
                    break;
            }
            endMessage.Append(Environment.NewLine);
            endMessage.Append("Would you like to play another round?");

            MessageBox.Show(endMessage.ToString(), "...", MessageBoxButtons.YesNo);
        }

        private void playMove(int i_Row, int i_Col)
        {
            m_Game.MakeMoveAndUpdateResult(i_Row, i_Col);
            refreshBoard(m_Game.Board.BoardSize);
        }

        private void enableCPU()
        {
            while (m_Game.GameState == eGameState.InProgress && m_Game.CurrentPlayer.IsCPU && m_CPU != null)
            {
                m_CPU.GetMove(m_Game.Board, out int cpuRow, out int cpuCol);
                playMove(cpuRow, cpuCol);
            }
        }

        private string playerSymbolToString(ePlayerSymbol symbol)
        {
            string result = string.Empty;

            switch (symbol)
            {
                case ePlayerSymbol.None:
                    break;

                case ePlayerSymbol.X:
                    result = "X";
                    break;

                case ePlayerSymbol.O:
                    result = "O";
                    break;
            }

            return result;
        }
    }
}