using UnityEngine;
using System.Collections;
using System;

public class CostsStat_Effect : Effect {

    int cost;
    String statName;

    public override void Use(Being target)
    {
        //Debug.Log("using stamina cost effect");

        Stat s = parentBeing.GetStat(statName);

        if (s!= null)
        {

            StatModulation sm = new StatModulation(s, "-", cost);
            battleManager.effectQueue.Add(sm);
            //Debug.Log( parentBeing.beingName + "gets a " + statName + " -" + cost + " token");
            return;
        }

        Debug.Log("ERROR: " + parentBeing.beingName + " does not have a " + statName + " stat.");
        



    }

    public CostsStat_Effect(BattleManager battleManager, Being parentBeing, Ability parentAbility, String effectName, String statName, int cost) : base(battleManager, parentBeing, parentAbility, effectName)
    {
        this.battleManager = battleManager;
        this.parentBeing = parentBeing;
        this.parentAbility = parentAbility;
        this.statName = statName;
        this.cost = cost;
    }
}
