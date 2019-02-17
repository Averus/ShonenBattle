using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRound_Condition : Condition
{
    public override bool CanThisBeUsed()
    {
        if (battleManager.currentElement == -1)
        {
            return true;
        }

        return false;
    }


    public NewRound_Condition(BattleManager battleManager, Being parentBeing, string conditionName) : base(battleManager, parentBeing, conditionName)
    {

    }
}
