using Game.Tasks.Abstract;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Game.Console.Commands;
using Game.Console;

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

		[SerializeField]
		private GameConsole console;

		[SerializeField]
		private Timer timer;

		private Stack<GameTask> _gameTasks;
		private Difficulty _difficulty;

		private GameTask _currentTask;

		public bool IsInFocus;

		private void Awake()
		{
			Instance = this;

			IsInFocus = true;
		}

		void Start()
		{
			console.Log("hello player, press space to start");

			//TODO: select difficulty at start of the game
			_difficulty = Difficulty.Medium;

			GenerateGameTasks((int)_difficulty + 1);
		}

		private void Update()
		{
			if(_currentTask == null)
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					StartGame();
				}
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
			timer.StartTimer();
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
				console.Log("A WINNER IS YOU");
				timer.StopTimer();
				_currentTask = null;
				return;
			}

			console.Log("... but wait, there is more");

			StartNewTask();
		}

		/// <summary>
		/// Gets called when the time limit is reached or maybe some number of tasks have failed
		/// </summary>
		public void GameOver()
		{
			console.Log("GAME OVER");
			timer.StopTimer();
			//print to console that game is over

			//reset or use existing folder states?

			//press enter to start again
		}

		public void CheckForCommands()
		{
			string currentCommand = GetLastConsoleInput();

			var commands = GetComponents<ConsoleCommand>();

			foreach (var command in commands)
			{
				if (string.Equals(command.Command, currentCommand, System.StringComparison.OrdinalIgnoreCase))
				{
					command.action.Invoke();
				}
			}
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
			return console.GetLastLogEntryString();
		}

		private void OnApplicationFocus(bool focus)
		{
			IsInFocus = focus;
		}

		public void LogToConsole(string text)
		{
			console.Log(text);
		}
	}
}