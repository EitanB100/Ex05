using System;
using System.Windows.Forms;

namespace Ex05
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GameSettingsForm gameSettings = new GameSettingsForm();

            DialogResult settingsFormResult = gameSettings.ShowDialog();

            if (settingsFormResult == DialogResult.OK)
            {
                Application.Run(new GameForm());
            }
        }
    }
}
