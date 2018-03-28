using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : IState
{
    protected Player _Player;

    public abstract StateType GetStateType();

	public virtual void Enter() 
	{
		_Player.ChangeSpeed(0.3f);
	}

	public virtual void Exit() 
	{
	}

	public virtual void Action()
	{
	}

    public StateBase(Player player)
    {
        _Player = player;
    }

    public abstract bool CanShot();

    public virtual InputStatus GetInputStatus()
    {
        InputStatus inputStatus = new InputStatus();
        if (Input.GetMouseButtonDown(0))
        {
            inputStatus.IsShot = true;

        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            inputStatus.IsRun = true;

        }
        return inputStatus;
    }
}
