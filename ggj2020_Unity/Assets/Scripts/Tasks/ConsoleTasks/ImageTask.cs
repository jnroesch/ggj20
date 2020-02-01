using Game.Console;
using Game.Tasks.Abstract;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.ConsoleTasks
{
	public class ImageTask : ConsoleTask
	{
		private string _textToInput;
		private ImageTaskSO[] imageTasks;
		private ImageTaskSO myImageTask;

		public ImageTask()
		{
			GameConsole.instance.OnNewSubmission += OnConsoleInput;
		}

		~ImageTask()
		{
			GameConsole.instance.OnNewSubmission -= OnConsoleInput;
		}

		public override void FailTask()
		{
			GameConsole.instance.Log(myImageTask.failText);
		}

		public override bool IsCompleted()
		{
			var lastInput = GameManager.Instance.GetLastConsoleInput();
			return string.Equals(lastInput, _textToInput, System.StringComparison.OrdinalIgnoreCase);
		}

		public override void StartTask()
		{
			imageTasks = Resources.LoadAll<ImageTaskSO>("");

			int randomInt = Random.Range(0, imageTasks.Length + 1);
			myImageTask = imageTasks[randomInt];

			GameConsole.instance.Log(myImageTask.startText);
			GameConsole.instance.Log(myImageTask.image);
		}

		public override void WinTask()
		{
			GameConsole.instance.Log(myImageTask.winText);
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