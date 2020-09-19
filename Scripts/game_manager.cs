
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class game_manager : MonoBehaviour
{
	private bool bGameHasEnded = false;
	public float gameEndRestartWait = 1f;

	public GameObject completeLevelUI;


	public void LevelComplete()
	{
		completeLevelUI.SetActive(true);
		level_complete.instance.LoadNextLevel();

	}
	public void GameEnd ()
	{
		if(!bGameHasEnded)
		{
			bGameHasEnded = true;
			Debug.Log("Game End");
			Invoke("Restart", gameEndRestartWait);
		}
	}

	void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


	
}
