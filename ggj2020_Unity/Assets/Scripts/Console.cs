using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
	public Text text;

		private void Start()
		{
			StartCoroutine(Tick());
		}
		
		private IEnumerator Tick()
		{
			while(true)
			{
						text.text += ".";
			yield return new WaitForSeconds(1);
			}

		}
}
