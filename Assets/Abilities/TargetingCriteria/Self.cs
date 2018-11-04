using UnityEngine;
using System.Collections;
using System;

public class Self : TargetingCriteria {



    public override bool CanThisBeTargeted(Being potentialTarget)
    {
        if (potentialTarget == parentBeing)
        {
            return true;
        }

        return false;
    }




    public Self(BattleManager battleManager, Being parentBeing, Ability parentAbility) : base(battleManager, parentBeing, parentAbility)
    {
        this.battleManager = battleManager;
        this.parentBeing = parentBeing;
        this.parentAbility = parentAbility;

    }


}
