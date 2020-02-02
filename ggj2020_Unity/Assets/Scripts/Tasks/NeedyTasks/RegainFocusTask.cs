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
			GameManager.Instance.LogToConsole("Failure.");
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
			GameManager.Instance.LogToConsole(@"
Remote console connectivity overload. Please refocus console.

	REFOCUS CONSOLE.

");
		}

		public override void WinTask()
		{
			SFX.Instance.PlayOneShot(SFX.Instance.GetRandomWinBeep());

			GameManager.Instance.LogToConsole("Focus regained.");
			GameManager.Instance.FinishCurrentTask();
		}
	}
}