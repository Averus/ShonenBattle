using UnityEngine;
using System.Collections;

public abstract class SelectionCriteria {


    public BattleManager battleManager;
    public Being parentBeing;
    public Ability parentAbility;
    public string selectionCriteriaName;


    public abstract bool Check(Ability abil);


    public SelectionCriteria(BattleManager battleManager, Being parentBeing, string selectionCriteriaName)
    {
        this.battleManager = battleManager;
        this.parentBeing = parentBeing;
        this.selectionCriteriaName = selectionCriteriaName;    
    }

}
