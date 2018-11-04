using UnityEngine;
using System.Collections;

public abstract class TargetingCriteria{

    //this is an abstract class beacuse they can use constructors and interfaces cannot. The constructor is used to pass the BattleManager reference

    public BattleManager battleManager;
    public Being parentBeing;
    public Ability parentAbility;


    public abstract bool CanThisBeTargeted(Being potentialTarget);



    public TargetingCriteria(BattleManager battleManager, Being parentBeing, Ability parentAbility)
    {
        this.battleManager = battleManager;
        this.parentBeing = parentBeing;
        this.parentAbility = parentAbility;

    }
}
