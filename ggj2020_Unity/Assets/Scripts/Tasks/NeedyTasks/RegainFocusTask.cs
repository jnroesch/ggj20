using Game.Tasks.Abstract;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.NeedyTasks
{
	public class RegainFocusTask : NeedyTask
	{
		public override void FailTask()
		{
			GameManager.Instance.LogToConsole("failed task");
		}

		public override bool IsCompleted()
		{
			return GameManager.Instance.IsInFocus;
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