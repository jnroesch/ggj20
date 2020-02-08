using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Console;

namespace Game.Virus
{
    public class TinyWindowVirus : VirusAction
    {
        public override void Execute()
        {
            GameConsole.instance.Log("ntwrklrd sends his regards. this console has been h4kkd.");
            Screen.SetResolution(64, 64, false);
        }
    }
}
