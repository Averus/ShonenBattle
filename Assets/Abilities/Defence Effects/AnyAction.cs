using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyAction : Effect {

    public override void Use(Being target)
    {
        Debug.Log(target.beingName + ": Too slow!");

    }






    public AnyAction(BattleManager battleManager, Being parentBeing, Ability parentAbility, string effectName) : base(battleManager, parentBeing, parentAbility, effectName)
    {

    }
}
