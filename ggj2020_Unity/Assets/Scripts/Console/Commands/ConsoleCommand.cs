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
[quit] -> close app
[easy, medium, hard]
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
			GameConsole.instance.Log("game crated at ggj20");
		}

		public void ShowGitStatus()
		{
			GameConsole.instance.Log(@"
# On branch metasys-dev
# # Your branch and 'origin/metasys-dev' have diverged,
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