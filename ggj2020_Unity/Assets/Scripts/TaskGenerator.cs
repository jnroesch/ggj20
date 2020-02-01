using Game.Tasks.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Game
{
	public class TaskGenerator
	{
		public GameTask CreateRandomTask()
		{
			var random = new System.Random();

			//update number depending on possible types
			var randomValue = random.Next(3);

			switch (randomValue)
			{
				case 0:
					return CreateRandomConsoleTask();

				case 1:
					return CreateRandomFileTask();

				case 2:
					return CreateRandomNeedyTask();

				default:
					return CreateRandomConsoleTask();
			}
		}

		private FileTask CreateRandomFileTask()
		{
			return (FileTask)GetRandomTask("Game.Tasks.FileTasks");
		}

		private ConsoleTask CreateRandomConsoleTask()
		{
			return (ConsoleTask)GetRandomTask("Game.Tasks.ConsoleTasks");
		}

		private NeedyTask CreateRandomNeedyTask()
		{
			return (NeedyTask)GetRandomTask("Game.Tasks.NeedyTasks");
		}

		private IEnumerable<Type> GetTypes(string nameSpace)
		{
			var types = from type in Assembly.GetExecutingAssembly().GetTypes()
					where type.IsClass && type.Namespace == nameSpace
					select type;
			return types;
		}

		private GameTask GetRandomTask(string nameSpace)
		{
			var possibleTasks = GetTypes(nameSpace).ToList();

			var random = new System.Random();
			var index = random.Next(possibleTasks.Count());

			return (GameTask)Activator.CreateInstance(possibleTasks[index]);
		}
	}
}