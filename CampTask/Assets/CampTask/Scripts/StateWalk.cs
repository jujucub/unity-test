using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWalk : StateBase
{
    public StateWalk(Player player) : base(player) { }

    public override bool CanShot()
    {
        return true;
    }

}
