using Game.Console;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Virus
{
	public class MinimizeVirus : VirusAction
	{
		public override void Execute()
		{
			GameConsole.instance.Log("muahahaah");
			if(Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen)
			{
				Screen.fullScreenMode = FullScreenMode.Windowed;
				Screen.SetResolution(640, 400, false);
			}
			else
			{
				Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
			}
		}
	}
}