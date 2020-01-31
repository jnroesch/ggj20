using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Requirements.ConsoleRequirement
{
	public class ConsoleTestRequirement : TaskRequirement
	{
		public ConsoleTestRequirement()
		{
			//empty constructor required for generic instantiation
		}

		public override bool IsCompleted()
		{
			return true;
		}
	}
}