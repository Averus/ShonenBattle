using UnityEngine;
using System.Collections;
using System;

public class Self_TargetingCriteria : TargetingCriteria {



    public override bool CanThisBeTargeted(Being potentialTarget)
    {
        if (potentialTarget == parentBeing)
        {
            return true;
        }

        return false;
    }




    public Self_TargetingCriteria(BattleManager battleManager, Being parentBeing, Ability parentAbility) : base(battleManager, parentBeing, parentAbility)
    {
        this.battleManager = battleManager;
        this.parentBeing = parentBeing;
        this.parentAbility = parentAbility;

    }


}
