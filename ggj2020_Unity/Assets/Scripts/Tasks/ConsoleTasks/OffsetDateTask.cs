using Game.Console;
using Game.Tasks.Abstract;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

namespace Game.Tasks.ConsoleTasks
{
	public class OffsetDateTask : ConsoleTask
	{
		private DateTime _dateTime;

		~OffsetDateTask()
		{
			GameConsole.instance.OnNewSubmission -= OnConsoleInput;
		}

		public override void FailTask()
		{
			GameManager.Instance.LogToConsole("Inconsistency detected. Please check calendar.");
		}

		public override bool IsCompleted()
		{
			var lastInput = GameManager.Instance.GetLastConsoleInput();
			return string.Equals(lastInput, _dateTime.ToString("dddd", new CultureInfo("")), StringComparison.OrdinalIgnoreCase);
		}

		public override void StartTask()
		{
			var random = new System.Random();
			int dayOffset = random.Next(2, 31);
			int yearOffset = 2035 - DateTime.Today.Year;

			_dateTime = DateTime.Today.AddYears(yearOffset).AddDays(dayOffset);

			GameConsole.instance.OnNewSubmission += OnConsoleInput;
			GameManager.Instance.LogToConsole($@"
User request overloaded {Environment.ProcessorCount} processors. What day of the week is in {dayOffset} days ({_dateTime.ToString("d")})?.

	ENTER WEEKDAY.		
");
		}
		
		public override void WinTask()
		{
			SFX.Instance.PlayOneShot(SFX.Instance.GetRandomWinBeep());
			GameConsole.instance.OnNewSubmission -= OnConsoleInput;
			GameManager.Instance.LogToConsole("User satisfied.");
			GameManager.Instance.FinishCurrentTask();
		}

		private void OnConsoleInput()
		{
			if (IsCompleted())
			{
				WinTask();
			}
			else
			{
				FailTask();
			}
		}
	}
}