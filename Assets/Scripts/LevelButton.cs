using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {

	public static LevelButton instance;

	private void Awake() 
	{ 
		instance = this; 
	}

	public string levelID;
	public string levelToLoad;

	public void LoadLevel()
	{
        SceneManager.LoadScene(levelToLoad);
    }
}
