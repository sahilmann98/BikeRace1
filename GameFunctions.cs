using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BikeRace
{
    public abstract class GameFunctions
    {
        public abstract void RestartGame();
        public abstract void ExitGame();
    }

    public class BikeRaceFunctions : GameFunctions
    {
        public override void RestartGame()
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath); // to start new instance of application
            BikeGame.ActiveForm.Close();
        }

        public override void ExitGame()
        {
            BikeGame.ActiveForm.Close();
        }
    }


}
