using UnityEngine;
using System.Collections;
using System;

public class GreaterThan : Condition
{
    string mode;

    Being p;
    int target;


    public override bool CanThisBeUsed()
    {

        return true;
    }

    public GreaterThan(BattleManager battleManager, Being parentBeing, string conditionName, string mode, Being p, int target) : base(battleManager, parentBeing, conditionName) //This should grab the battleManager from the base constructor (Condition)
    {
        this.mode = mode;
        this.p = p;
        this.target = target;
    }

}



