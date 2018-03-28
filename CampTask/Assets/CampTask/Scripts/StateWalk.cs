using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWalk : StateBase
{
    public StateWalk(Player player) : base(player) { }

	public override StateType GetStateType ()
	{
		return StateType.Walk;
	}

    public override void Enter ()
	{
		base.Enter();
		_Player.ChangeSpeed(0.3f);
	}

	public override void Action ()
	{
		base.Action ();
		// 歩く.
		var inputStatus = GetInputStatus();
		if(inputStatus.IsMoving)
		{
			if(inputStatus.IsRun)
	        {
				_Player.ChangeState(StateType.Run);
	        }
		}
		else
		{
			_Player.ChangeState(StateType.Idle);
		}
        if(inputStatus.IsProstrate)
        {
			_Player.ChangeState(StateType.Prostrate);
        }
        if(inputStatus.IsSit)
        {
			_Player.ChangeState(StateType.Sit);
		}
	}

    public override bool CanShot()
    {
        return true;
    }

    public override InputStatus GetInputStatus ()
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
