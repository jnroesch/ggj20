using Game.Console;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Virus
{
	public class MinimizeVirus : VirusAction
	{
		public override void Execute()
		{
			GameConsole.instance.Log($"{Environment.UserName.ToLower()}, i am in your system.");

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