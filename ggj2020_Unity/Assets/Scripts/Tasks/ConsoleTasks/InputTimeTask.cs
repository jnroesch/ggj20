using Game.Console;
using Game.Tasks.Abstract;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.Tasks.ConsoleTasks
{
	public class InputTimeTask : ConsoleTask
	{
		~InputTimeTask()
		{
			GameConsole.instance.OnNewSubmission -= OnConsoleInput;
		}

		public override void FailTask()
		{
			GameManager.Instance.LogToConsole("System synchronization failed.");
		}

		public override bool IsCompleted()
		{
			var lastInput = GameManager.Instance.GetLastConsoleInput();
			return string.Equals(lastInput, DateTime.Now.ToString("mm-HH"));
		}

		public override void StartTask()
		{
			GameConsole.instance.OnNewSubmission += OnConsoleInput;
			GameManager.Instance.LogToConsole(@"
Atomic clock has reached half-life. Please set current time (mm-HH).

	SET TIME.		
");
		}
		
		public override void WinTask()
		{
			SFX.Instance.PlayOneShot(SFX.Instance.GetRandomWinBeep());
			GameConsole.instance.OnNewSubmission -= OnConsoleInput;
			GameManager.Instance.LogToConsole("Spacetime adjusted.");
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