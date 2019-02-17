using UnityEngine;
using System.Collections;
using System;

public class Damage_Effect : Effect
{

    int damage;



    public override void Use(Being target)
    {

        StatModulation sm = new StatModulation(target.GetStat("HP"), "-", damage);
        battleManager.effectQueue.Add(sm);
        //Debug.Log("HP minus damage StatModulation put in " + target.beingName + "'s statModulation list.");


    }


    public Damage_Effect(BattleManager battleManager, Being parentBeing, Ability parentAbility, string effectName, int damage) : base(battleManager, parentBeing, parentAbility, effectName)
    {
        this.damage = damage;
    } 


}
