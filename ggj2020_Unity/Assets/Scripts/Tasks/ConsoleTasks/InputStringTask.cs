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
			MonobehaviorUtil.Instance.Write("That was the wrong command you idiot");
		}

		public override bool IsCompleted()
		{
			var lastInput = GameManager.Instance.GetLastConsoleInput();
			return string.Equals(lastInput, _textToInput, System.StringComparison.OrdinalIgnoreCase);
		}

		public override void StartTask()
		{
			MonobehaviorUtil.Instance.Write("Please type in the following command: " + _textToInput);
		}

		public override void WinTask()
		{
			MonobehaviorUtil.Instance.Write("well done");
			GameManager.Instance.FinishCurrentTask();
		}
	}
}