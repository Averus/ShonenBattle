using UnityEngine;
using System.Collections;
using System;

public class CostsMP_Effect : Effect {

    int cost;

    public override void Use(Being target)
    {
  
        StatModulation sm = new StatModulation(target.GetStat("MP"), "-", cost);
        target.statModulations.Add(sm);
        Debug.Log("MP minus cost StatModulation put in " + target.beingName + "'s statModulation list.");


    
}

    public CostsMP_Effect(BattleManager battleManager, Being parentBeing, Ability parentAbility, String effectName, int cost) : base(battleManager, parentBeing, parentAbility, effectName)
    {
        this.battleManager = battleManager;
        this.parentBeing = parentBeing;
        this.parentAbility = parentAbility;
        this.cost = cost;
    }
}
