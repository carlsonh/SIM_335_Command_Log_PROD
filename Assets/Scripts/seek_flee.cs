using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seek_flee : MonoBehaviour
{
	[Header("States")]
	public bool bIsFleeing = true;
	public bool bCanWander = true;

	[Header("General references")]
    public Transform player_Transform;
    public Rigidbody rb;

	[Header("General variables")]
    public float watch_radius = 7f;
    public float arrive_radius = 2f;
    public float approach_speed = 5f;
    public float rotate_multiplier = 2f;

    //Seek Only: Arrive functionality
    [Header("Arrive variables")]
    public float _seek_desired_stop_distance = 2f;//Not quite what it does, but close enough
    public float _seek_speed_deceleration_strength = 3f;//Higher values result in smoother movement. Finicky


    [Header("Wander variables")]
    public float wander_walk_speed = 1f;
    public float wander_deviation_degrees = 5f;
    // Update is called once per frame

	private TrailRenderer tr;

    void Start()
    {
    	tr = GetComponent<TrailRenderer>();
    	tr.startColor = Random.ColorHSV(0,1,0,1,0,1,1,1);
    	tr.endColor = Random.ColorHSV(0,1,0,1,0,1,1,1);

    }


    void Update()
    {
        if((Vector3.Distance(transform.position, player_Transform.position) <= watch_radius))
        {//Player is inside watch radius
        	//Look at the player ///This doesn't fully work on flee bots for some reason
        	transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation( player_Transform.position - transform.position ), Time.deltaTime * rotate_multiplier );


        	//Calculate the vector directly away from the player
        	Vector3 _direction = transform.position - player_Transform.position;
        	_direction.Normalize();
        	_direction *= approach_speed;
        	

        	if(bIsFleeing) 
        	{
        		//Bot in flee mode, move backwards.

        		if(rb.isKinematic)
        		{
        			transform.position += _direction;
        		}
                else
                {
                    rb.AddForce(_direction, ForceMode.VelocityChange);
                }
        	}
        	else if ((Vector3.Distance(transform.position, player_Transform.position) >= arrive_radius)) //If the seeker isn't close enought to the player, approach them
        	{
        		//Bot in seek mode, approach player
        		
        		if(rb.isKinematic)
        		{
        			//ARRIVE
        			///Smooths as the enemy approaches the player based on designer-chosen weights
        			Vector3 _scaling_direction = (_direction*((transform.position - player_Transform.position).magnitude - _seek_desired_stop_distance)/_seek_speed_deceleration_strength);
        			transform.position -= _scaling_direction;
        			Debug.Log("Speed: "+ _scaling_direction.magnitude);//_scaling_direction.ToString("G4"));
        		}
                else
                {
                    rb.AddForce(-_direction, ForceMode.VelocityChange);
                }
        	}

       }
       else if (bCanWander)
       {

       	if(rb.isKinematic)
       	{
       		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + randomCentered() * wander_deviation_degrees, 0);
       		transform.position += transform.forward.normalized * wander_walk_speed/100f;//Scale walk speed to reasonable numbers
       	}

       }
    }
    private float randomCentered()
    {
    	return (Random.value - Random.value);
    }
}
