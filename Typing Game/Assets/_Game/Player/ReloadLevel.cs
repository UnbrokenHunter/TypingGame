using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
	
	public void ReloadLevelNow()
	{

	}

	public void ResetPlayer()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

		objs[0].GetComponent<TypingController>().ResetValues();
	}

}
