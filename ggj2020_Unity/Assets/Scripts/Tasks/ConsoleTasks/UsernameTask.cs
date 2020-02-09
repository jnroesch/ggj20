using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Tasks.Abstract;
using Game.Console;

namespace Game.Tasks.ConsoleTasks
{
    public class UsernameTask : ConsoleTask
    {
        //void Start()
        //{
        //    Debug.Log(Environment.UserDomainName);
        //    Debug.Log(Environment.UserName);
        //    Debug.Log(Environment.CommandLine);
        //    Debug.Log(Environment.OSVersion);
        //    Debug.Log(Environment.ProcessorCount);
        //    Debug.Log(Environment.GetLogicalDrives().ToString());
        //}

        ~UsernameTask()
        {
            GameConsole.instance.OnNewSubmission -= OnConsoleInput;
        }

        public override void FailTask()
        {
            var lastInput = GameManager.Instance.GetLastConsoleInput();
            GameConsole.instance.Log($"Unauthorized identity \"{lastInput}\" has been logged. Please reauthorize.");
        }

        public override bool IsCompleted()
        {
            var lastInput = GameManager.Instance.GetLastConsoleInput();
            return string.Equals(lastInput, Environment.UserName, StringComparison.OrdinalIgnoreCase);
        }

        public override void StartTask()
        {
            GameConsole.instance.OnNewSubmission += OnConsoleInput;
            GameConsole.instance.Log($@"
Legacy system {Environment.OSVersion} credentials manager requests user reauthorization. Please enter your username.

	ENTER USERNAME.
");
        }

        public override void WinTask()
        {
            GameConsole.instance.OnNewSubmission -= OnConsoleInput;
            GameConsole.instance.Log("Credentials accepted.");
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
