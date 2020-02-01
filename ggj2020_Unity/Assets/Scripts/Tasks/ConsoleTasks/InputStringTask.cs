using Game.Tasks.Abstract;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.ConsoleTasks
{
	public class InputStringTask : ConsoleTask
	{
		private string _textToInput = "passwrod";

		public override void FailTask()
		{
			GameManager.Instance.LogToConsole("That was the wrong command you idiot");
		}

		public override bool IsCompleted()
		{
			var lastInput = GameManager.Instance.GetLastConsoleInput();
			return string.Equals(lastInput, _textToInput, System.StringComparison.OrdinalIgnoreCase);
		}

		public override void StartTask()
		{
			GameManager.Instance.LogToConsole("Please type in the following command: " + _textToInput);
		}

		public override void WinTask()
		{
			GameManager.Instance.LogToConsole("well done");
			GameManager.Instance.FinishCurrentTask();
		}
	}
}