using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffToHit_Effect : Effect {

    int buff;
    string modulator;



    public override void Use(Being target)
    {
        //

    }


    public BuffToHit_Effect(BattleManager battleManager, Being parentBeing, Ability parentAbility, string effectName, string modulator, int buff) : base(battleManager, parentBeing, parentAbility, effectName)
    {
        this.buff = buff;
        this.modulator = modulator;
    }
}
