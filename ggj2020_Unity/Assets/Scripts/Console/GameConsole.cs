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
        private LogEntry logEntry;
        private TMP_InputField inputField;

        public delegate void GameConsoleEvent();
        public GameConsoleEvent OnNewSubmission;

        private void Awake()
        {
            instance = this;

            SetUpLogEntry();
        }

        private void SetUpLogEntry()
        {
            logEntry = Instantiate(logEntryPrefab, logEntryParent).GetComponent<LogEntry>();
            inputField = logEntry.GetComponentInChildren<TMP_InputField>();
            inputField.onSubmit.AddListener(delegate { Submit(); });
            inputField.ActivateInputField();
        }

        private void Submit()
        {
            CreateLogEntry();
            OnNewSubmission?.Invoke();
        }

        public void CreateLogEntry()
        {
            logEntries.Add(logEntry);
            inputField.onValueChanged.RemoveAllListeners();
            inputField.interactable = false;

            SetUpLogEntry();
        }

        public void Log(string text)
        {
            LogEntry autoLogEntry = Instantiate(logEntryPrefab, logEntryParent).GetComponent<LogEntry>();
            autoLogEntry.GetComponentInChildren<TMP_InputField>().interactable = false;
            autoLogEntry.SetText(text);

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
