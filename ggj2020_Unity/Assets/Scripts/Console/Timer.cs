using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Game.Console
{
    public class Timer : MonoBehaviour
    {
        private int timeLeft = 20;
        private GameConsole console;

		private IEnumerator timerCoroutine;

        private void Awake()
        {
            console = FindObjectOfType<GameConsole>();
        }

        public void StartTimer()
        {
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
                if (timeLeft == 20) console.Log("Your debugging budget for today is: " + timeLeft.ToString() + " SECONDS. STARTING NOW.");
                if (timeLeft == 15) console.Log(timeLeft.ToString() + " SECONDS LEFT. Please remain productive.");
                if (timeLeft == 10) console.Log(timeLeft.ToString() + " SECONDS. Eliminate all distractions.");
                if (timeLeft <= 5) console.Log(timeLeft.ToString() + " SECONDS. Concentrate.");

                int step = timeLeft > 5 ? 5 : 1;
                yield return new WaitForSeconds(step);
                timeLeft -= step;
            }
            GameManager.Instance.GameOver();
        }
    }
}
