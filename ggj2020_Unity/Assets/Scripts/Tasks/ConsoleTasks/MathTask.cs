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
			Substraction,
			Multiplication
		}

		private int _number1;
		private int _number2;

		private Operation _operation;

		public MathTask()
		{
			var random = new System.Random();
			int value = random.Next(3);

			if (value == 0) _operation = Operation.Addition;
			else if (value == 1) _operation = Operation.Substraction;
			else _operation = Operation.Multiplication;


			_number1 = random.Next(maxValue);
			_number2 = random.Next(maxValue);
		}

		~MathTask()
		{
			GameConsole.instance.OnNewSubmission -= OnConsoleInput;
		}

		public override void FailTask()
		{
			GameManager.Instance.LogToConsole(@"
Difference to correct answer > 0.
");
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
				else if (_operation == Operation.Substraction)
				{
					return number == _number1 - _number2;
				}
				else
				{
					return number == _number1 * _number2;
				}
			}
			catch
			{
				return false;
			}
		}

		public override void StartTask()
		{
			GameConsole.instance.OnNewSubmission += OnConsoleInput;

			string symbol;
			if (_operation == Operation.Addition) symbol = "+";
			else if (_operation == Operation.Substraction) symbol = "-";
			else symbol = "*";

			GameConsole.instance.Log(@"
Orbit recalculation required. What is: " + $"{_number1} {symbol} {_number2}" + @"

	CALCULATE.

");
		}

		public override void WinTask()
		{
			SFX.Instance.PlayOneShot(SFX.Instance.GetRandomWinBeep());

			GameConsole.instance.OnNewSubmission -= OnConsoleInput;
			GameManager.Instance.LogToConsole("Calculation complete.");
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
