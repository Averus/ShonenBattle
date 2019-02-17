using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulateToHitSelf_Effect : Effect
{



    string modulator;
    int value;

    public override void Use(Being target)
    {
        //this effect ignores the target and uses the paarent being instead. Feels not modular enough if I'm honest, we need a dashboard for effects to refer to.

        StatModulation sm = new StatModulation(parentBeing.GetStat("TOHIT"), modulator, value);
        battleManager.effectQueue.Add(sm);
        //Debug.Log("TOHIT token generated for " + parentBeing.beingName);
    }





    public ModulateToHitSelf_Effect(BattleManager battleManager, Being parentBeing, Ability parentAbility, string effectName, string modulator, int value) : base(battleManager, parentBeing, parentAbility, effectName)
    {
        this.modulator = modulator;
        this.value = value;
    }
}
