using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Console
{
    public class Console : MonoBehaviour
    {
        public GameObject logEntryPrefab;
        public Transform logEntryParent;

        private TMP_InputField inputField;

        private void Awake()
        {
            SetUpLogEntry();
        }

        private void SetUpLogEntry()
        {
            inputField = Instantiate(logEntryPrefab, logEntryParent).GetComponentInChildren<TMP_InputField>();
            inputField.onSubmit.AddListener(delegate { Submit(); });
            inputField.ActivateInputField();
        }

        private void Submit()
        {
            CreateLogEntry();
        }

        public void CreateLogEntry()
        {
            inputField.onValueChanged.RemoveAllListeners();
            inputField.interactable = false;

            SetUpLogEntry();
        }

        public void Log(string text)
        {
            LogEntry logEntry = Instantiate(logEntryPrefab, logEntryParent).GetComponent<LogEntry>();
            logEntry.GetComponentInChildren<TMP_InputField>().interactable = false;
            logEntry.SetText(text);
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus) inputField.ActivateInputField();
        }
    }
}
