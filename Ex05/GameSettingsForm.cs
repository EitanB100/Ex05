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
    public partial class GameSettingsForm : Form
    {
        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private GameSettings m_GameSettings;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void GameSettingsForm_Load(object sender, EventArgs e)
        {

        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlayer2.Enabled = checkBoxPlayer2.Checked;
        }

        private void numericUpDownRows_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownRows.Value != numericUpDownCols.Value)
            {
                numericUpDownCols.Value = numericUpDownRows.Value;
            }
        }

        private void numericUpDownCols_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownCols.Value != numericUpDownRows.Value)
            {
                numericUpDownRows.Value = numericUpDownCols.Value;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            eGameMode chosenGameMode = (checkBoxPlayer2.Checked ? eGameMode.TwoPlayers : eGameMode.PlayerVsCPU);

            m_GameSettings = new GameSettings(chosenGameMode, textBoxPlayer1.Text, textBoxPlayer2.Text, (int)numericUpDownRows.Value);

        }
    }
}

