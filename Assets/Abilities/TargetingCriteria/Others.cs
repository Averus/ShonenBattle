﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Others : TargetingCriteria {


    public override bool CanThisBeTargeted(Being potentialTarget)
    {
        if (potentialTarget == parentBeing)
        {
            return false;
        }

        return true;
    }


    public Others(BattleManager battleManager, Being parentBeing, Ability parentAbility) : base(battleManager, parentBeing, parentAbility)
    {
        this.battleManager = battleManager;
        this.parentBeing = parentBeing;
        this.parentAbility = parentAbility;

    }

}
