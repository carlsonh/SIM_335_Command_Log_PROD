
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

	

	/// <summary>
	/// Find the player's rigidBody for replay purposes, and check if we should run a replay
	/// </summary>
	private void Start() 
	{
		player_movement playerMovement = FindObjectOfType<player_movement>();
		rbPlayer = playerMovement.gameObject.GetComponent<Rigidbody>();

		//Check if there are commands in the log
		if(CommandLog.commands.Count >= 1)
		{//Commands in the log, run the replay
			bIsReplaying = true;
			playerMovement.bIsReplaying = true; //Prevent the player from issuing new commands during replay
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
	{//Level win, load up the next one
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

	/// <summary>
	/// Replay player movements from the command log at the time they would've happened
	/// </summary>
	private void ReplayPlayerMovements()
	{

		Command command = CommandLog.commands.Peek();
		
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

	private void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


	
}
