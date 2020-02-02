using Game.Tasks.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Game
{
	public class TaskGenerator
	{
		private System.Random _random;

		public TaskGenerator()
		{
			_random = new System.Random();
		}

		public GameTask CreateRandomTask()
		{
			var randomValue = _random.Next(100);

			if (randomValue < 50)
			{
				return CreateRandomConsoleTask();
			}
			else if (randomValue >= 50 && randomValue < 90)
			{
				return CreateRandomFileTask();
			}
			else
			{
				return CreateRandomNeedyTask();
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
					where type.IsClass && type.Namespace == nameSpace && !type.IsAbstract
					select type;
			return types;
		}

		private GameTask GetRandomTask(string nameSpace)
		{
			//lol random hack for the moveFileTast that has a ghost item instantiated sometimes for whatever reason
			var possibleTasks = GetTypes(nameSpace).Where(entry => !entry.FullName.Split(new char[]{ '.', '/'}, '\\').Last().Contains("<")).ToList();
			var index = _random.Next(possibleTasks.Count());		

			return (GameTask)Activator.CreateInstance(possibleTasks[index]);
		}
	}
}