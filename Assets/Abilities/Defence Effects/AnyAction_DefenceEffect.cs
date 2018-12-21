using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyAction_DefenceEffect : Effect {

    public override void Use(Being target)
    {
        Debug.Log(target.beingName + ": Too slow!");

    }






    public AnyAction_DefenceEffect(BattleManager battleManager, Being parentBeing, Ability parentAbility, string effectName) : base(battleManager, parentBeing, parentAbility, effectName)
    {

    }
}
