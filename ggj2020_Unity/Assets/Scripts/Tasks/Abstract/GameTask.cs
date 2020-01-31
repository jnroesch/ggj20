using Game.Requirements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.Abstract
{
	public abstract class GameTask
	{
		public List<TaskRequirement> Requirements;

		public bool Completed
		{
			get
			{
				if (!_completed)
				{
					_completed = IsCompleted();
				}

				return _completed;
			}
		}

		private bool _completed;

		public bool IsCompleted()
		{
			foreach (var requirement in Requirements)
			{
				if (!requirement.Completed)
				{
					return false;
				}
			}

			//no requirement failed therefore everything must be completed
			return true;
		}

		public abstract void StartTask();

		public abstract void WinTask();

		public abstract void FailTask();

		public GameTask(List<TaskRequirement> requirements)
		{
			Requirements = requirements;
		}
	}
}