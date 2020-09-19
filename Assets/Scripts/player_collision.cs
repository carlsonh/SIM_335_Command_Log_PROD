using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_collision : MonoBehaviour
{
	public player_movement movement;
    // Start is called before the first frame update
    void OnCollisionEnter (Collision collisionInfo)
    {
    	if(collisionInfo.collider.tag == "Obstacle")
    	{
    		movement.enabled = false;
    		FindObjectOfType<game_manager>().GameEnd();
			Debug.Log(collisionInfo.gameObject.name);
    	}
    	else
    	{

    	}
    }
}
