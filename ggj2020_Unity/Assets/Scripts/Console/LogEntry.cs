﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Console
{
    public class LogEntry : MonoBehaviour
    {
        public Text LogEntryText;
        public Text LogEntryDirectory;

        private string[] lines;
        private float delay = .03f;

        public void SetText(string text)
        {
            LogEntryText.text = text;
        }

        public string GetText()
        {
            return LogEntryText.text;
        }

        public void SetDirectoryText(string text)
        {
            LogEntryDirectory.text = text;
        }

        public string GetDirectoryText()
        {
            return LogEntryDirectory.text;
        }

        public void SetTextGameBoy(string text)
        {
            StartCoroutine(GameBoyText(text));
        }

        private IEnumerator GameBoyText(string text)
        {
            yield return new WaitForSeconds(delay);

            for (int i = 0; i < text.Length; i++)
            {
                LogEntryText.text += text[i];
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
