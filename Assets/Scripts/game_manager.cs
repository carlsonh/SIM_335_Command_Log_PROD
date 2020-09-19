
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class game_manager : MonoBehaviour
{
	private bool bGameHasEnded = false;

	private bool bIsReplaying = false;
	public float gameEndRestartWait = 1f;

	public GameObject completeLevelUI;

	public Rigidbody  rbPlayer; 

	

	private void Start() 
	{
		player_movement playerMovement = FindObjectOfType<player_movement>();
		rbPlayer = playerMovement.gameObject.GetComponent<Rigidbody>();

		//Check if there are commands in the log
		if(CommandLog.commands.Count >= 1)
		{
			bIsReplaying = true;

		}
	}

	private void Update() 
	{
		if (bIsReplaying)
		{
			ReplayPlayerMovements();
		}	
	}

	


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
			Invoke("RestartWithReplay", gameEndRestartWait);
		}
	}

	private void ReplayPlayerMovements()
	{

		Command command = CommandLog.commands.Peek();
		//Debug.Log("Peeked command " + command.GetType().Name);
		
		if(command.timestamp <= Time.timeSinceLevelLoad)
		{//The event had already happened at this point, run it

			command = CommandLog.commands.Dequeue();
			command._rbPlayer = rbPlayer;

			Invoker invoker = new Invoker();
			invoker.bIsLogging = false;//This is a replay, don't re-add it
			invoker.bIsLoggingToUI = false;

			invoker.SetCommand(command);
			invoker.ExecuteCommand();

		}
	
	}

	private void RestartWithReplay()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		ReplayPlayerMovements();
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


	
}
