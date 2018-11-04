using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulateToHitSelf : Effect
{



    string modulator;
    int value;

    public override void Use(Being target)
    {
        if (target != parentBeing)
        {
            Debug.Log("ERROR: target " + target.beingName + " does not match parent being " + parentBeing.beingName + " for Effect ModulateToHitSelf");
            return;
        }

        StatModulation sm = new StatModulation(target.GetStat("TOHIT"), modulator, value);
        target.statModulations.Add(sm);
    }





    public ModulateToHitSelf(BattleManager battleManager, Being parentBeing, Ability parentAbility, string effectName, string modulator, int value) : base(battleManager, parentBeing, parentAbility, effectName)
    {
        this.modulator = modulator;
        this.value = value;
    }
}
