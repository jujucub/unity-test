using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : StateBase
{

    public StateIdle(Player player) : base(player) { }

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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            inputStatus.IsRun = false;

        }
        return inputStatus;
    }

}
