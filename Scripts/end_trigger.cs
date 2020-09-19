using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end_trigger : MonoBehaviour
{
	public game_manager gm;
    // Start is called before the first frame update
    void OnTriggerEnter()
    {
    	gm.LevelComplete();
    }
}
