using Game.Requirements;
using Game.Tasks.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Game
{
	public class TaskGenerator : MonoBehaviour
	{
		void Start()
		{
			var task = CreateRandomTask();

			Debug.Log(task.Completed);
		}

		public GameTask CreateRandomTask()
		{
			return UnityEngine.Random.value < 0.5f ? (GameTask)CreateRandomConsoleTask() : (GameTask)CreateRandomFileTask();
		}

		private FileTask CreateRandomFileTask()
		{
			var requirement = GetRandomRequirement("Game.Requirements.FileRequirements");
			var requirements = new List<TaskRequirement>(){ requirement };
			return new FileTask(requirements);
		}

		private ConsoleTask CreateRandomConsoleTask()
		{
			var requirement = GetRandomRequirement("Game.Requirements.ConsoleRequirement");
			var requirements = new List<TaskRequirement>() { requirement };

			return new ConsoleTask(requirements);
		}

		private IEnumerable<Type> GetTypes(string nameSpace)
		{
			var types = from type in Assembly.GetExecutingAssembly().GetTypes()
					where type.IsClass && type.Namespace == nameSpace
					select type;
			return types;
		}

		private TaskRequirement GetRandomRequirement(string nameSpace)
		{
			var possibleReqs = GetTypes(nameSpace).ToList();

			var random = new System.Random();
			var index = random.Next(possibleReqs.Count());

			var requirement = (TaskRequirement)Activator.CreateInstance(possibleReqs[index]);

			return requirement;
		}
	}
}