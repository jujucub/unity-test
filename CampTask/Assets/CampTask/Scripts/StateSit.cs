using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSit : StateBase
{
	public override StateType GetStateType ()
	{
		return StateType.Sit;
	}

	public StateSit(Player player) : base(player)
    {
    }

    public override void Enter ()
	{
		base.Enter ();
		_Player.ChangeSpeed(0.2f);
	}

	public override void Action ()
	{
		base.Action ();
		var inputStatus = GetInputStatus();
        if(inputStatus.IsProstrate)
        {
			_Player.ChangeState(StateType.Prostrate);
        }
        if(inputStatus.IsSit)
        {
			_Player.ChangeState(StateType.Idle);
		}
	}

    public override bool CanShot()
    {
        return true;
    }

    public override InputStatus GetInputStatus()
    {
        InputStatus inputStatus = new InputStatus();
        if (Input.GetMouseButtonDown(0))
        {
            inputStatus.IsShot = true;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            inputStatus.IsRun = true;
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
			inputStatus.IsSit = true;
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
        	inputStatus.IsProstrate = true;
        }
		if (Input.GetKey(KeyCode.W))
        {
			inputStatus.IsMoveForward = true;
        }
		if (Input.GetKey(KeyCode.S))
        {
			inputStatus.IsMoveBackward = true;
        }
		if (Input.GetKey(KeyCode.A))
        {
			inputStatus.IsMoveLeft = true;
        }
		if (Input.GetKey(KeyCode.D))
        {
			inputStatus.IsMoveRight = true;
        }
        return inputStatus;
    }

}
