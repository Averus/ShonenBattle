using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_DefenceEffect : Effect {

    public override void Use(Being target)
    {
        Debug.Log(target.beingName + " crosses their arms and blocks the hit.");

    }






    public Block_DefenceEffect(BattleManager battleManager, Being parentBeing, Ability parentAbility, string effectName) : base(battleManager, parentBeing, parentAbility, effectName)
    {

    }
}

