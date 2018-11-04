using UnityEngine;
using System.Collections;

public abstract class Effect{

    public BattleManager battleManager;
    public Being parentBeing;
    public Ability parentAbility;

    public string effectName;

    BattleManager.CombatStates UsedInState;

    public abstract void Use(Being target);


    public Effect(BattleManager battleManager, Being parentBeing, Ability parentAbility, string effectName)
    {
        this.battleManager = battleManager;
        this.parentBeing = parentBeing;
        this.parentAbility = parentAbility;
        this.effectName = effectName;
    }
}
