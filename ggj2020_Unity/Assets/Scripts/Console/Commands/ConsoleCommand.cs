using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Console.Commands
{
	public class ConsoleCommand : MonoBehaviour
	{
		public string[] Variants;

		public UnityEvent action;

		public void OpenPersistentDataPath()
		{
			Process.Start(new System.Diagnostics.ProcessStartInfo()
			{
				FileName = Application.persistentDataPath,
				UseShellExecute = true,
				Verb = "open"
			});
		}

		public void OpenDataPath()
		{
			Process.Start(new System.Diagnostics.ProcessStartInfo()
			{
				FileName = Application.dataPath,
				UseShellExecute = true,
				Verb = "open"
			});
		}

		public void Help()
		{
			GameConsole.instance.Log("Please contact METASYS HR.");
		}

		public void CloseApp()
		{
			Application.Quit();
		}
	}
}