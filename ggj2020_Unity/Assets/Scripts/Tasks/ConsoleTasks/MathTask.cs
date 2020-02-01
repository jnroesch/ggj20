using Game.Console;
using Game.Tasks.Abstract;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.ConsoleTasks
{
	public class MathTask : ConsoleTask
	{
		private const int maxValue = 50;

		private enum Operation
		{
			Addition,
			Substraction
		}

		private int _number1;
		private int _number2;

		private Operation _operation;

		public MathTask()
		{
			GameConsole.instance.OnNewSubmission += OnConsoleInput;

			var random = new System.Random();
			int value = random.Next(100);
			_operation = value < 50 ? Operation.Addition : Operation.Substraction;

			_number1 = random.Next(maxValue);
			_number2 = random.Next(maxValue);
		}

		~MathTask()
		{
			GameConsole.instance.OnNewSubmission -= OnConsoleInput;
		}

		public override void FailTask()
		{
			GameManager.Instance.LogToConsole("WRONG!");
		}

		public override bool IsCompleted()
		{
			var lastInput = GameManager.Instance.GetLastConsoleInput();
			try
			{
				var number = int.Parse(lastInput);
				if(_operation == Operation.Addition)
				{
					return number == _number1 + _number2;
				}
				else
				{
					return number == _number1 - _number2;
				}
			}
			catch
			{
				return false;
			}
		}

		public override void StartTask()
		{
			string symbol = _operation == Operation.Addition ? "+" : "-";
			GameConsole.instance.Log($"Time for some math: What is {_number1} {symbol} {_number2}");
		}

		public override void WinTask()
		{
			GameManager.Instance.LogToConsole("well done");
			GameManager.Instance.FinishCurrentTask();
		}

		private void OnConsoleInput()
		{
			if (IsCompleted())
			{
				WinTask();
			}
			else
			{
				FailTask();
			}
		}
	}
}
