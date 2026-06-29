using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05
{
    public class Program
    {
        public static void Main()
        {

            GameSettingsForm gameSettings = new GameSettingsForm();

            DialogResult settingsFormResult = gameSettings.ShowDialog();

        }
    }
}
