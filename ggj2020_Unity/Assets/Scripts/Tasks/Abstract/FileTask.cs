using Game.Requirements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.Abstract
{
	public class FileTask : GameTask
	{
		public FileTask(List<TaskRequirement> requirements) : base(requirements)
		{

		}

		public override void FailTask()
		{
			throw new System.NotImplementedException();
		}

		public override void StartTask()
		{
			throw new System.NotImplementedException();
		}

		public override void WinTask()
		{
			throw new System.NotImplementedException();
		}
	}
}