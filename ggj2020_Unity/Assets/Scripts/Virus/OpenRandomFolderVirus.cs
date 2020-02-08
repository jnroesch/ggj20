using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Game.Console;

namespace Game.Virus
{
	public class OpenRandomFolderVirus : VirusAction
	{
		private List<string> options = new List<string>()
		{
			$"c:/users/{Environment.UserName}/Downloads",
			$"c:/users/{Environment.UserName}/Desktop"
		};

		public override void Execute()
		{
			GameConsole.instance.Log($"muahahaah. private data of {Environment.UserName.ToLower()} aquired.");

			var random = new System.Random();

			var index = random.Next(options.Count);
			var result = options[index];

			Process.Start(new System.Diagnostics.ProcessStartInfo()
			{
				FileName = result,
				UseShellExecute = true,
				Verb = "open"
			});
		}
	}
}