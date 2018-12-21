using UnityEngine;
using System.Collections;
using System;

public class LessThan_Condition : Condition
{

    //this whole class is ridiculous. It's to stop me having to create >9000 individual classes for 'less that MP'. 'greater than mp', 'less than hp' etc but it's nto exactly good programming
    // EDIT I think this class is obsolete and replaced by CompareTwoValues, which is an even worse offender (lol)
    string mode;

    Being p;
    int target;



    public override bool CanThisBeUsed()
    {

        if (mode == "HP")
        {
            /*
            if (p.hp <= target)
            {
                Debug.Log(p.beingName + " hp is at " + p.hp);
                Debug.Log("target is at " + target);
                return true;
            }
            */
    
        }

        return false;

    }


    public LessThan_Condition(BattleManager battleManager, Being parentBeing, string conditionName, string mode, Being p, int target) : base(battleManager, parentBeing, conditionName) //This should grab the battleManager from the base constructor (Condition)
    {
        this.mode = mode;
        this.p = p;
    }


}
