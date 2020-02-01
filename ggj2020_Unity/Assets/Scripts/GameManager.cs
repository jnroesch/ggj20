using Game.Tasks.Abstract;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Game.Console.Commands;

namespace Game
{
	public enum Difficulty
	{
		Easy,
		Medium,
		Hard
	}

	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;

		//reference to console object

		private Stack<GameTask> _gameTasks;
		private Difficulty _difficulty;

		private GameTask _currentTask;

		private void Awake()
		{
			Instance = this;
		}

		void Start()
		{
			//TODO: print stuff to console to welcome player

			//TODO: select difficulty at start of the game
			_difficulty = Difficulty.Medium;

			GenerateGameTasks((int)_difficulty + 1);


			//TODO: remove, only for testing
			string currentCommand = "help";
			var commands = GetComponents<ConsoleCommand>();
			foreach(var command in commands)
			{
				if(string.Equals(command.Command, currentCommand, System.StringComparison.OrdinalIgnoreCase))
				{
					command.action.Invoke();
				}
			}
		}

		private void Update()
		{
			if(_currentTask == null)
			{
				return;
			}

			if (_currentTask.IsCompleted())
			{

				_currentTask.WinTask();
			}
		}

		/// <summary>
		/// Gets called when the user has read the intro dialogue and accepted the start of the game
		/// </summary>
		public void StartGame()
		{
			StartNewTask();
		}

		private void StartNewTask()
		{
			_currentTask = _gameTasks.Pop();
			_currentTask.StartTask();
		}

		public void FinishCurrentTask()
		{
			if(_gameTasks.Count == 0)
			{
				Debug.Log("A WINNER IS YOU");
				_currentTask = null;
				return;
			}

			Debug.Log("... but wait, there is more");

			StartNewTask();
		}

		/// <summary>
		/// Gets called when the time limit is reached or maybe some number of tasks have failed
		/// </summary>
		public void GameOver()
		{
			//print to console that game is over

			//reset or use existing folder states?

			//press enter to start again
		}

		private void GenerateGameTasks(int amountOfTasksToCreate)
		{
			_gameTasks = new Stack<GameTask>();

			var taskGenerator = new TaskGenerator();
			for(int i=0; i< amountOfTasksToCreate; i++)
			{
				var newTask = taskGenerator.CreateRandomTask();
				_gameTasks.Push(newTask);
			}
		}

		public string GetLastConsoleInput()
		{
			return "temp";
		}
	}
}