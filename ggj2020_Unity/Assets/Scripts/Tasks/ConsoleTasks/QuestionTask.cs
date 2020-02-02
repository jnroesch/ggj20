using Game.Console;
using Game.Tasks.Abstract;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.ConsoleTasks
{
	public class QuestionTask : ConsoleTask
	{
		private List<Dictionary<string, string>> options;

		private Dictionary<string, string> option;

		public QuestionTask()
		{
			options = new List<Dictionary<string, string>>();

			#region options

			options.Add(new Dictionary<string, string>()
			{
				{"question", "ATTENTION: Execute Virus?" },
				{"keyAccept", "no" },
				{"keyDecline", "yes" }
			});

			#endregion

			var random = new System.Random();
			int randomInt = random.Next(options.Count);
			option = options[randomInt];
		}

		~QuestionTask()
		{
			GameConsole.instance.OnNewSubmission -= OnConsoleInput;
		}

		public override void FailTask()
		{
			
		}

		public override bool IsCompleted()
		{
			var lastInput = GameManager.Instance.GetLastConsoleInput();
			if(string.Equals(lastInput, option["keyAccept"], System.StringComparison.OrdinalIgnoreCase))
			{
				GameConsole.instance.Log("Virus executed!");
				GameManager.Instance.ExecuteVirusAction();
				return true;
			}
			else if(string.Equals(lastInput, option["keyDecline"], System.StringComparison.OrdinalIgnoreCase))
			{
				GameConsole.instance.Log("Virus did not execute!");
				return true;
			}
			else
			{
				GameConsole.instance.Log("WRONG COMMAND");
				GameConsole.instance.Log($"Type [{option["keyAccept"]}] to accept or [{option["keyDecline"]}] to decline");
				return false;
			}
		}

		public override void StartTask()
		{
			GameConsole.instance.OnNewSubmission += OnConsoleInput;
			GameConsole.instance.Log(option["question"]);
			GameConsole.instance.Log($"Type [{option["keyAccept"]}] to accept or [{option["keyDecline"]}] to decline");
		}

		public override void WinTask()
		{
			GameConsole.instance.OnNewSubmission -= OnConsoleInput;
			GameManager.Instance.LogToConsole("well done");
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