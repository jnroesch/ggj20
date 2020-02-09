using Game.Console;
using Game.Tasks.Abstract;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.ConsoleTasks
{
	public class InputStringTask : ConsoleTask
	{
		private List<string> options;

		private string _textToInput;

		public InputStringTask()
		{
			options = new List<string>()
			{
				"passwrod",
				"gamejam",
				"ggj20",
				"metasys",
				"rm -rf",
				"drop table"
			};

			var random = new System.Random();
			int randomInt = random.Next(options.Count);
			_textToInput = options[randomInt];
		}

		~InputStringTask()
		{
			GameConsole.instance.OnNewSubmission -= OnConsoleInput;
		}

		public override void FailTask()
		{
			GameManager.Instance.LogToConsole("Incorrect.");
		}

		public override bool IsCompleted()
		{
			var lastInput = GameManager.Instance.GetLastConsoleInput();
			return string.Equals(lastInput, _textToInput, System.StringComparison.OrdinalIgnoreCase);
		}

		public override void StartTask()
		{
			GameConsole.instance.OnNewSubmission += OnConsoleInput;
			GameManager.Instance.LogToConsole(@"
Database corrupted... Please spell recovery command: " + _textToInput + @"

	TYPE COMMAND.		

");
		}
		
		public override void WinTask()
		{
			SFX.Instance.PlayOneShot(SFX.Instance.GetRandomWinBeep());
			GameConsole.instance.OnNewSubmission -= OnConsoleInput;
			GameManager.Instance.LogToConsole("Command accepted.");
			GameManager.Instance.FinishCurrentTask();
		}

		private void OnConsoleInput()
		{
			if (IsCompleted())
			{
				WinTask();
			}
		}
	}
}