using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex05
{
    public partial class GameForm : Form
    {
        private const int k_CellSize = 60;
        private const int k_ScoreBarSize = 40;
        private const int k_Margin = 6;

        private Game m_Game;
        private CPU m_CPU;
        private Button[,] m_BoardButtons;
        private Label m_LabelScore;

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

            this.ClientSize = new Size(k_CellSize * boardSize, k_CellSize * boardSize + k_ScoreBarSize);

            refreshBoard(boardSize);
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
            m_LabelScore = new Label();

            m_LabelScore.AutoSize = true;
            Controls.Add(m_LabelScore);
        }

        private void addButtonToGrid(int i_Row, int i_Col)
        {
            m_BoardButtons[i_Col, i_Row] = new Button();

            m_BoardButtons[i_Col, i_Row].Size = new Size(k_CellSize, k_CellSize);
            m_BoardButtons[i_Col, i_Row].Location = new Point(i_Col * k_CellSize, i_Row * k_CellSize);
            m_BoardButtons[i_Col, i_Row].Text = string.Empty;
            m_BoardButtons[i_Col, i_Row].Tag = new Point(i_Col, i_Row);
            m_BoardButtons[i_Col, i_Row].Click += cellButton_Click;
            m_BoardButtons[i_Col, i_Row].TabStop = false;

            Controls.Add(m_BoardButtons[i_Col, i_Row]);
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

            m_LabelScore.Text = m_Game.Players[0].Name + ": " + m_Game.Players[0].Score
                                + "   " + m_Game.Players[1].Name + ": " + m_Game.Players[1].Score;

            m_LabelScore.Location = new Point((ClientSize.Width - m_LabelScore.Width) / 2,
                i_BoardSize * k_CellSize + k_Margin);
        }

        private void cellButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            Point cell = (Point)clickedButton.Tag;

            int col = cell.X;
            int row = cell.Y;

            playMove(row, col);
            playCpuTurns();

            if (m_Game.GameState != eGameState.InProgress)
            {
                handleEndOfRound();
            }
        }

        private void handleEndOfRound()
        {
            StringBuilder endMessage = new StringBuilder();
            string endMessageTitle = string.Empty;
            switch (m_Game.GameState)
            {
                case eGameState.Draw:
                    endMessage.Append("Tie!");
                    endMessageTitle = "A Tie!";
                    break;
                case eGameState.Winner:
                    endMessage.Append("The winner is ");
                    endMessage.Append(m_Game.Winner.Name);
                    endMessage.Append("!");
                    endMessageTitle = "A Win!";
                    break;
            }

            endMessage.Append(Environment.NewLine);
            endMessage.Append("Would you like to play another round?");

            DialogResult userChoice = MessageBox.Show(endMessage.ToString(), endMessageTitle, MessageBoxButtons.YesNo);

            if (userChoice == DialogResult.Yes)
            {
                m_Game.ResetBoard();
                refreshBoard(m_Game.Board.BoardSize);
                playCpuTurns();
            }
            else
            {
                Application.Exit();
            }
        }

        private void playMove(int i_Row, int i_Col)
        {
            m_Game.MakeMoveAndUpdateResult(i_Row, i_Col);
            refreshBoard(m_Game.Board.BoardSize);
        }

        private void playCpuTurns()
        {
            while (m_Game.GameState == eGameState.InProgress && m_Game.CurrentPlayer.IsCPU && m_CPU != null)
            {
                m_CPU.GetMove(m_Game.Board, out int cpuRow, out int cpuCol);
                playMove(cpuRow, cpuCol);
            }
        }

        private string playerSymbolToString(ePlayerSymbol i_Symbol)
        {
            string result = string.Empty;

            switch (i_Symbol)
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