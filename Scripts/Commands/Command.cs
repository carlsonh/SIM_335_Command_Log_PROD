using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{//Commandbase: self-descriptive
    public Rigidbody _rbPlayer;
    public float timestamp;
    public abstract void Execute();
}

class MoveLeft : Command
{
    private float _moveForce;

    public MoveLeft(Rigidbody rbPlayer, float moveForce)
    {
        _rbPlayer = rbPlayer;
        _moveForce = moveForce;
    }

    public override void Execute()
    {
        timestamp = Time.timeSinceLevelLoad;
        _rbPlayer.AddForce(new Vector3(-_moveForce, 0, 0) * Time.deltaTime, ForceMode.VelocityChange);//Negative for left-wards
    }
}

class MoveRight : Command
{
    private float _moveForce;

    public MoveRight(Rigidbody rbPlayer, float moveForce)
    {
        _rbPlayer = rbPlayer;
        _moveForce = moveForce;
    }

    public override void Execute()
    {
        timestamp = Time.timeSinceLevelLoad;
        _rbPlayer.AddForce(new Vector3(_moveForce, 0, 0) * Time.deltaTime, ForceMode.VelocityChange);//Positive for Right-wards
    }
}

class MoveForward : Command
{
    private float _moveForce;

    public MoveForward(Rigidbody rbPlayer, float moveForce)
    {
        _rbPlayer = rbPlayer;
        _moveForce = moveForce;
    }

    public override void Execute()
    {
        timestamp = Time.timeSinceLevelLoad;
        _rbPlayer.AddForce(new Vector3(0, 0, _moveForce) * Time.deltaTime, ForceMode.VelocityChange);//Positive for Right-wards
    }
}
class MoveBack : Command
{
    private float _moveForce;

    public MoveBack(Rigidbody rbPlayer, float moveForce)
    {
        _rbPlayer = rbPlayer;
        _moveForce = moveForce;
    }

    public override void Execute()
    {
        timestamp = Time.timeSinceLevelLoad;
        _rbPlayer.AddForce(new Vector3(0, 0, -_moveForce) * Time.deltaTime, ForceMode.VelocityChange);//Positive for Right-wards
    }
}