using UnityEngine;
using System.Collections;

public abstract class Condition {

    //this is an abstract class beacuse they can use constructors and interfaces cannot. The constructor is used to pass the BattleManager reference

    public BattleManager battleManager;
    public Being parentBeing;
    public Ability parentAbility;

    string conditionName;

    public string GetConditionName()
    {
        return conditionName;
    }


    public abstract bool CanThisBeUsed();

 

    public Condition(BattleManager battleManager, Being parentBeing, string conditionName)
    {
        this.battleManager = battleManager;
        this.parentBeing = parentBeing;
        this.conditionName = conditionName;

    }

}

