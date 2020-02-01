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
			MonobehaviorUtil.Instance.Write("failed task");
		}

		public override bool IsCompleted()
		{
			throw new System.NotImplementedException();
		}

		public override void StartTask()
		{
			MonobehaviorUtil.Instance.Write("Please regain focus in console");
		}

		public override void WinTask()
		{
			MonobehaviorUtil.Instance.Write("needy task completed");
			GameManager.Instance.FinishCurrentTask();
		}
	}
}