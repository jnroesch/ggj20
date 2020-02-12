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
        public TMP_InputField inputField;

        private List<LogEntry> logEntries = new List<LogEntry>();
        private int currentLogEntryInt;
        private LogEntry logEntry;
        private Text textField;

        private int _currentCaretPosition;
        private string _caret = "_";

        public delegate void GameConsoleEvent();
        public GameConsoleEvent OnNewSubmission;

        private void OnEnable()
        {
            inputField.onSubmit.AddListener(delegate { Submit(); });
            inputField.onValueChanged.AddListener(delegate { UpdateTextFieldText(); });
        }

        private void OnDisable()
        {
            inputField.onSubmit.RemoveAllListeners();
        }

        private void Awake()
        {
            instance = this;

            CreateNewInputEntry();
            UpdateTextFieldCaret();
        }

        private void Update()
        {
            ForceInputFieldFocus();

			if (GameManager.Instance.IsConsoleBlocked)
			{
				return;
			}

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SelectLastCommand();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SlelectNextCommand();
            }

            CheckForCaretMovement();
        }

        private void CheckForCaretMovement()
        {
            if (_currentCaretPosition != inputField.caretPosition)
            {
                _currentCaretPosition = inputField.caretPosition;
                UpdateTextFieldCaret();
            }
        }

        //called from Timer.cs
        private IEnumerator BlinkCaret()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                if (_caret == "_")
                {
                    _caret = " "; //this " " is a special whitespace without linebreak. created with Alt+255.
                }
                else
                {
                    _caret = "_";
                }
                UpdateTextFieldCaret();
            }
        }

        private void UpdateTextFieldCaret()
        {
            string text = inputField.text;
            if (text.ToCharArray().Length > inputField.caretPosition)
            {
                text = text.Insert(inputField.caretPosition, _caret);
            }
            else
            {
                text += _caret;
            }
            textField.text = text;
        }

        private void UpdateTextFieldText()
        {
			if (GameManager.Instance.IsConsoleBlocked)
			{
				return;
			}

            SFX.Instance.Keyboard();
            string text = inputField.text;
            textField.text = text;
        }

        private void SelectLastCommand()
        {
            if (currentLogEntryInt - 1 >= 0)
            {
                currentLogEntryInt--;
                inputField.text = logEntries[currentLogEntryInt].GetText();
                UpdateTextFieldCaret();
            }
        }

        private void SlelectNextCommand()
        {
            if (currentLogEntryInt + 1 < logEntries.Count)
            {
                currentLogEntryInt++;
                inputField.text = logEntries[currentLogEntryInt].GetText();
                UpdateTextFieldCaret();
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
            textField = logEntry.GetComponentInChildren<Text>();
            inputField.text = "";
        }

        private void Submit()
        {
			if (GameManager.Instance.IsConsoleBlocked)
			{
				return;
			}

            textField.text = inputField.text;

            SFX.Instance.PlayOneShot(SFX.Instance.SubmitSound);
            CreateNewLogEntry();
            OnNewSubmission?.Invoke();
        }

        public void CreateNewLogEntry()
        {
            logEntries.Add(logEntry);
            currentLogEntryInt = logEntries.Count;

            CreateNewInputEntry();
        }

        public void Log(string text)
        {
			LogEntry autoLogEntry = Instantiate(logEntryPrefab, logEntryParent).GetComponent<LogEntry>();
            autoLogEntry.SetTextGameBoy(text);
            autoLogEntry.SetDirectoryText("");
            logEntry.transform.SetAsLastSibling();
        }

        public void LogImage(string text)
        {
			LogEntry autoLogEntry = Instantiate(logEntryPrefab, logEntryParent).GetComponent<LogEntry>();
            autoLogEntry.SetFastTextGameBoy(text);
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
