using System;
using System.Windows.Forms;

namespace Ex05
{
    public partial class GameSettingsForm : Form
    {
        private GameSettings m_GameSettings;

        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlayer2.Enabled = checkBoxPlayer2.Checked;

            if (checkBoxPlayer2.Checked)
            {
                textBoxPlayer2.Clear();
            }
            else
            {
                textBoxPlayer2.Text = "CPU";
            }
        }

        private void numericUpDownRows_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownRows.Value != numericUpDownCols.Value)
            {
                numericUpDownCols.Value = numericUpDownRows.Value;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            eGameMode chosenGameMode = (checkBoxPlayer2.Checked ? eGameMode.TwoPlayers : eGameMode.PlayerVsCPU);

            string player1Name = textBoxPlayer1.Text;
            string player2Name = textBoxPlayer2.Text;

            if (player1Name == string.Empty)
            {
                player1Name = "Player 1";
            }

            if (player2Name == string.Empty)
            {
                player2Name = "Player 2";
            }

            m_GameSettings = new GameSettings(chosenGameMode, player1Name, player2Name, (int)numericUpDownRows.Value);
            DialogResult = DialogResult.OK;
        }

        private void numericUpDownCols_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownCols.Value != numericUpDownRows.Value)
            {
                numericUpDownRows.Value = numericUpDownCols.Value;
            }
        }

        public GameSettings GameSettings
        {
            get
            {
                return m_GameSettings;
            }
        }

    }
}