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
			string helpText =@"
[external] -> show external files
[local] -> show local files
[sysinfo] -> show system information
[quit] -> close app
[credits] -> show credits
[easy, medium, hard] -> difficulty
";
			GameConsole.instance.Log(helpText);

		}

		public void CloseApp()
		{
			Application.Quit();
		}

		public void ChangeDifficulty(int newValue)
		{
			GameConsole.instance.Log("SYSTEM COMPLEXITY set to "+(Difficulty)newValue + " on next startup.");
			GameManager.Instance.difficulty = (Difficulty)newValue;
		}

		public void ShowCredits()
		{
			GameConsole.instance.Log(@"
METASYS ENGINEERING REPAIR CONSOLE

EMPLOYEE RECORD
5117¥	Jan Niklas Roesch
483¥	Ludwig Seibt ludwigseibt.com

SYSTEM ORIGIN
Global Game Jam 2020
");
		}

		public void ShowSysInfo()
		{
			GameConsole.instance.Log(@"
There are currently 8295004 METASYS employees connected.
This filesystem consists of 1205183 chunks.
Corruption: 97%.

METASYS ENGINEERING. Your productivity is appreciated.
");
		}

		public void ShowGitStatus()
		{
			GameConsole.instance.Log(@"
# On branch metasys-dev
# Your branch and 'origin/metasys-dev' have diverged,
# You have unmerged paths.
#  (fix conflicts and run git commit)

Conflicts detected in 2739 files:

		both modified: config.json
		both modifies: MainScene.unity
		both modified: ProjectSettings.asset
		failed to load: ...
");

		}
	}
}