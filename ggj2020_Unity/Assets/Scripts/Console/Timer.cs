using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Game.Console
{
    public class Timer : MonoBehaviour
    {
		private const int totalTime = 20;
        private int timeLeft;
        private GameConsole console;

		private IEnumerator timerCoroutine;

        private void Awake()
        {
            console = FindObjectOfType<GameConsole>();
        }

        public void StartTimer()
        {
			timeLeft = totalTime;
			timerCoroutine = TimerRoutine();
			StartCoroutine(timerCoroutine);
        }

		public void StopTimer()
		{
			StopCoroutine(timerCoroutine);
		}

        private IEnumerator TimerRoutine()
        {
            while (timeLeft > 0)
            {
                if (timeLeft == 20)
                {
                    console.Log(timeLeft.ToString() + " SECONDS. STARTING NOW.");
                    SFX.Instance.PlayOneShot(SFX.Instance.TimerBeep);
                }
                if (timeLeft == 15)
                {
                    console.Log(timeLeft.ToString() + " SECONDS LEFT. Please remain productive.");
                    SFX.Instance.PlayOneShot(SFX.Instance.TimerBeep);
                }
                if (timeLeft == 10)
                {
                    console.Log(timeLeft.ToString() + " SECONDS. Eliminate all distractions.");
                    SFX.Instance.PlayOneShot(SFX.Instance.TimerBeep);
                }
                if (timeLeft <= 5)
                {
                    console.Log(timeLeft.ToString() + " SECONDS. Concentrate.");
                    SFX.Instance.PlayOneShot(SFX.Instance.TimerBeep);
                }

                int step = timeLeft > 5 ? 5 : 1;
                yield return new WaitForSeconds(step);
                timeLeft -= step;
            }
            GameManager.Instance.GameOver();
        }
    }
}
