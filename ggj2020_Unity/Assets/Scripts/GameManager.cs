using Game.Tasks.Abstract;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Game.Console.Commands;
using Game.Console;
using Game.Virus;
using Game.Tasks.FileTasks;
using Game.Tasks.ConsoleTasks;
using UnityEngine.SceneManagement;

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
		public Difficulty difficulty;

		private GameTask _currentTask;

		public bool IsInFocus;

		private bool gameHasEnded;

		private bool playedFirstRound;

		public bool IsConsoleBlocked => _currentTask is NeedyTask;

		private void Awake()
		{
			Instance = this;

			IsInFocus = true;
			gameHasEnded = true;

			Screen.fullScreenMode = FullScreenMode.Windowed;
			Screen.SetResolution(800, 500, false);
		}

		private void OnEnable()
		{
			console.OnNewSubmission += CheckForCommands;
		}

		private void OnDisable()
		{
			console.OnNewSubmission -= CheckForCommands;
		}

		void Start()
		{
			console.Log(@"
███╗   ███╗███████╗████████╗ █████╗ ███████╗██╗   ██╗███████╗
████╗ ████║██╔════╝╚══██╔══╝██╔══██╗██╔════╝╚██╗ ██╔╝██╔════╝
██╔████╔██║█████╗     ██║   ███████║███████╗ ╚████╔╝ ███████╗
██║╚██╔╝██║██╔══╝     ██║   ██╔══██║╚════██║  ╚██╔╝  ╚════██║
██║ ╚═╝ ██║███████╗   ██║   ██║  ██║███████║   ██║   ███████║
╚═╝     ╚═╝╚══════╝   ╚═╝   ╚═╝  ╚═╝╚══════╝   ╚═╝   ╚══════╝
WELCOME ENGINEER. Your productivity is appreciated.


Your energy budget for today is 755981 kWh == 20 SECONDS SYSTEMTIME.
Recommended commands: [help]

	PRESS <RETURN> TO START REPAIR SYSTEM.

");

			difficulty = Difficulty.Medium;	
		}

		private void Update()
		{
			if(_currentTask == null || gameHasEnded)
			{
				return;
			}

			switch (_currentTask)
			{
				case FileTask task:
					if (task.IsCompleted())
					{

						task.WinTask();
					}
					break;

				case ConsoleTask task:
					//handled in the task itself
					break;

				case NeedyTask task:
					if (task.IsCompleted())
					{

						task.WinTask();
					}
					break;
			}
			
		}

		/// <summary>
		/// Gets called when the user has read the intro dialogue and accepted the start of the game
		/// </summary>
		public void StartGame()
		{
			SFX.Instance.StartBackgroundMusic();
			GenerateGameTasks((int)difficulty + 1);
			gameHasEnded = false;
			timer.StartTimer();
			StartNewTask();
			playedFirstRound = true;
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
				SFX.Instance.StopBackgroundMusic();
				console.Log($@"

Filesystem chunk {Mathf.RoundToInt(Random.Range(10, 500))}/1205183 has been restored.
Your termination will be reconsidered.

        _  _ ___ ________   ____   
  _ _ _| || |__ \____  \ \ / /\ \  
 (_|_|_) || |_ ) |  / /_\ V /_ \ \ 
       |__   _/ /  / /|___ ___| > >
  _ _ _   | |/ /_ / / |___ ___|/ / 
 (_|_|_)  |_|____/_/     |_|  /_/  
");
				console.Log(@"
	PRESS <RETURN> to continue your METASYS employment.
");
				timer.StopTimer();
				_currentTask = null;
				return;
			}

			console.Log(@"
	NEXT task awaits.
");

			StartNewTask();
		}

		/// <summary>
		/// Gets called when the time limit is reached or maybe some number of tasks have failed
		/// </summary>
		public void GameOver()
		{
			SFX.Instance.StopBackgroundMusic();
			SFX.Instance.PlayGameOverMusic();
			gameHasEnded = true;
			console.Log(@"
METASYS ARK chunksystem integrity is in CRITICAL CONDITION.
Your productivity score has been noted. Goodbye.

███╗   ███╗███████╗████████╗ █████╗ ███████╗██   ██╗███████╗
████████╗   ██║  ╗ ████║██╔════╝╚══██╔══╝██╔══██╗██╔════╝╚██╗ ██╔╝██╔═══
██╔████╔██║█████║███████╗ ╚████╔╝ ███████╗
  ╚██╔╝██║██╔══╝     ██║   ██╔══██║╚════██║  ╚██╔╝  ╚════██║
██║ ╚═╝ ██║██████╗   ██║  █████╗   ██║   ██║  ██║███████║   ██║   ███████║
╚═╝     ╚═╝╚══════╝   ╚═╝   ╚═╝████╗   ██║    ╚═╝╚══════╝   ╚═╝   ╚══════╝████╗   ██║  
");
			console.Log(@"
	PRESS <RETURN> to continue your METASYS employment.
");
			_currentTask = null;
			_gameTasks.Clear();
			timer.StopTimer();

			//reset or use existing folder states?

		}

		public void ExecuteVirusAction()
		{
			var virusGenerator = new VirusGenerator();
			var virus = virusGenerator.CreateRandomVirusAction();
			virus.Execute();
		}

		public void CheckForCommands()
		{
			string currentCommand = GetLastConsoleInput();

			//ignore empty commands after game has started
			if (_currentTask != null && string.IsNullOrWhiteSpace(currentCommand))
			{
				return;
			}

			var commands = GetComponents<ConsoleCommand>();

			foreach (var command in commands)
			{
				foreach(var variant in command.Variants)
				{
					if (string.Equals(variant, currentCommand, System.StringComparison.OrdinalIgnoreCase))
					{
						command.action.Invoke();
						return;
					}
				}
			}

			//game did not yet start
			if (_currentTask == null)
			{
				if (Input.GetKeyDown(KeyCode.Return))
				{
					if (playedFirstRound)
					{
						SceneManager.LoadScene(SceneManager.GetActiveScene().name);
					}
					else
					{
						StartGame();
					}
				}
				return;
			}

			if(_currentTask is ConsoleTask)
			{
				//do not harm the player on wrong input
				return;
			}

			ExecuteVirusAction();
		}

		private void GenerateGameTasks(int amountOfTasksToCreate)
		{
			_gameTasks = new Stack<GameTask>();

			var taskGenerator = new TaskGenerator();
			for(int i=0; i< amountOfTasksToCreate; i++)
			{
				GenerateGameTask(taskGenerator);
			}
		}

		private void GenerateGameTask(TaskGenerator taskGenerator)
		{
			var newTask = taskGenerator.CreateRandomTask();
			foreach (var task in _gameTasks)
			{
				if (task.GetType() == newTask.GetType())
				{
					//if same type already exists, try again and return
					GenerateGameTask(taskGenerator);
					return;
				}
			}
			_gameTasks.Push(newTask);
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