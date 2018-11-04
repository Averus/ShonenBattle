using UnityEngine;
using System.Collections;
using System;

public class NoCondition : Condition
{
    public override bool CanThisBeUsed()
    {
        return true;
    }

    public NoCondition(BattleManager battleManager, Being parentBeing, string conditionName) : base(battleManager, parentBeing, conditionName) //This should grab the battleManager from the base constructor (Condition)
    {
       
    }
}
