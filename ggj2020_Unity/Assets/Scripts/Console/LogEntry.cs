using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Console
{
    public class LogEntry : MonoBehaviour
    {
        public TMP_InputField inputField;
        public TMP_Text directoryText;
        
        public void SetText(string text)
        {
            inputField.text = text;
        }

        public string GetText()
        {
            return inputField.text;
        }

        public void SetDirectoryText(string text)
        {
            directoryText.text = text;
        }

        public string GetDirectoryText()
        {
            return directoryText.text;
        }
    }
}
