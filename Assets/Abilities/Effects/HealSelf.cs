using UnityEngine;
using System.Collections;
using System;

public class HealSelf : Effect {

    public int healByValue;

    public override void Use(Being target)
    {
        Stat hp = parentBeing.GetStat("HP");
        hp.current += healByValue;
        Debug.Log(parentBeing.beingName + " heals itself for " + healByValue + " hp.");
        Debug.Log(parentBeing.beingName + " HP at " + hp.current);
    }


    public HealSelf(BattleManager battleManager, Being parentBeing, Ability parentAbility, string effectName, int healAmount) : base (battleManager, parentBeing, parentAbility, effectName)
    {
        healByValue = healAmount;
    }

}
