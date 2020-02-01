using Game.Tasks.Abstract;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.NeedyTasks
{
	public class RegainFocusTask : NeedyTask
	{
		private bool hasBeenUnfocused = false;

		public override void FailTask()
		{
			GameManager.Instance.LogToConsole("failed task");
		}

		public override bool IsCompleted()
		{
			if(hasBeenUnfocused == false && GameManager.Instance.IsInFocus == false)
			{
				hasBeenUnfocused = true;
			}
			return GameManager.Instance.IsInFocus && hasBeenUnfocused;
		}

		public override void StartTask()
		{
			GameManager.Instance.LogToConsole("Please regain focus in console");
		}

		public override void WinTask()
		{
			GameManager.Instance.LogToConsole("needy task completed");
			GameManager.Instance.FinishCurrentTask();
		}
	}
}