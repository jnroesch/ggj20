using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Console
{
    public class GameConsole : MonoBehaviour
    {
        public static GameConsole instance;

        public GameObject logEntryPrefab;
        public Transform logEntryParent;

        private List<LogEntry> logEntries = new List<LogEntry>();
        private int currentLogEntryInt;
        private LogEntry logEntry;
        private TMP_InputField inputField;

        public delegate void GameConsoleEvent();
        public GameConsoleEvent OnNewSubmission;

        private void Awake()
        {
            instance = this;

            CreateNewInputEntry();
        }

        private void Update()
        {
            ForceInputFieldFocus();

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SelectLastCommand();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SlelectNextCommand();
            }
        }

        private void SelectLastCommand()
        {
            if (currentLogEntryInt - 1 >= 0)
            {
                currentLogEntryInt--;
                inputField.text = logEntries[currentLogEntryInt].GetText();
                inputField.caretPosition = inputField.text.Length;
            }
        }

        private void SlelectNextCommand()
        {
            if (currentLogEntryInt + 1 < logEntries.Count)
            {
                currentLogEntryInt++;
                inputField.text = logEntries[currentLogEntryInt].GetText();
                inputField.caretPosition = inputField.text.Length;
            }
        }

        private void ForceInputFieldFocus()
        {
            if (!inputField.isFocused)
            {
                inputField.ActivateInputField();
                inputField.caretPosition = inputField.text.Length;
            }
        }

        private void CreateNewInputEntry()
        {
            logEntry = Instantiate(logEntryPrefab, logEntryParent).GetComponent<LogEntry>();
            inputField = logEntry.GetComponentInChildren<TMP_InputField>();
            inputField.onSubmit.AddListener(delegate { Submit(); });
            inputField.ActivateInputField();
        }

        private void Submit()
        {
            CreateNewLogEntry();
            OnNewSubmission?.Invoke();
        }

        public void CreateNewLogEntry()
        {
            logEntries.Add(logEntry);
            currentLogEntryInt = logEntries.Count;
            print(currentLogEntryInt);
            inputField.onValueChanged.RemoveAllListeners();
            inputField.interactable = false;

            CreateNewInputEntry();
        }

        public void Log(string text)
        {
            LogEntry autoLogEntry = Instantiate(logEntryPrefab, logEntryParent).GetComponent<LogEntry>();
            autoLogEntry.GetComponentInChildren<TMP_InputField>().interactable = false;
            autoLogEntry.SetText(text);
            autoLogEntry.SetDirectoryText("");

            logEntry.transform.SetAsLastSibling();
        }

        public string GetLastLogEntryString()
        {
            if (logEntries.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                return logEntries[logEntries.Count - 1].GetText();
            }
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus) inputField.ActivateInputField();
        }
    }
}
