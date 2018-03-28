using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
	StateType GetStateType();
	void Enter();
	void Action();
	void Exit();
    bool CanShot();
    InputStatus GetInputStatus();
}
