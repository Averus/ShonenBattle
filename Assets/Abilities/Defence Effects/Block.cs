using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Effect {

    public override void Use(Being target)
    {
        Debug.Log(target.beingName + " crosses their arms and blocks the hit.");

    }






    public Block(BattleManager battleManager, Being parentBeing, Ability parentAbility, string effectName) : base(battleManager, parentBeing, parentAbility, effectName)
    {

    }
}

