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
                console.Log(timeLeft.ToString());
                yield return new WaitForSeconds(1);
                timeLeft--;

				if(timeLeft == 10)
				{
					GameManager.Instance.ExecuteVirusAction();
				}
            }

            GameManager.Instance.GameOver();
        }
    }
}
