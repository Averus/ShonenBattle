using UnityEngine;
using System.Collections;
using System;

public class IncludesEffect : SelectionCriteria
{

    String effectName;


    public override bool Check(Ability abil)
    {
       //Debug.Log("Looking for abilities in " + abil.abilityName + " that contain an effect called " + effectName);
        for (int i = 0; i < abil.effects.Count; i++)
        {
            if (abil.effects[i].effectName == effectName)
            {
                //Debug.Log(abil.abilityName + " contains an effect called " + effectName);
                return true;
            }
        }
        //Debug.Log(abil.abilityName + " does not contain an effect called " + effectName);
        return false;

    }


    public IncludesEffect(BattleManager battleManager, Being parentBeing, string selectionCriteriaName, string effectName) : base(battleManager, parentBeing, selectionCriteriaName)
    {
        this.battleManager = battleManager;
        this.parentBeing = parentBeing;
        this.effectName = effectName;

    }



}
