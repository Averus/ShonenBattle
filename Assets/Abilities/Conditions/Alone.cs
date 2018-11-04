using UnityEngine;
using System.Collections;

public class Alone : Condition{


    public override bool CanThisBeUsed()
    {
        if (battleManager.combatants.Count == 1)
        {
            Debug.Log("no other beings present");
            return true;

        }

        Debug.Log("other beings present");
        return false;

    }



    public Alone(BattleManager battleManager, Being parentBeing, string conditionName) : base(battleManager, parentBeing, conditionName) //This should grab the battleManager from the base constructor (Condition)
    {
        this.battleManager = battleManager;
    }
}
