using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulateResource_Effect : Effect{

    public string targetResource;
    public int value;
    public bool targetSelf;


    public override void Use(Being target)
    {

        Resource actualTargetResource;

        if (targetSelf)
        {
            actualTargetResource = parentBeing.GetResource(targetResource);
            ResourceModifier rm = new ResourceModifier(value);
            actualTargetResource.Modify(rm);
        }

        if (!targetSelf)
        {
           actualTargetResource = target.GetResource(targetResource);
           ResourceModifier rm = new ResourceModifier(value);
           actualTargetResource.Modify(rm);
        }

        

    }



    public ModulateResource_Effect(BattleManager battleManager, Being parentBeing, Ability parentAbility, string effectName, string targetStat, int value, bool targetSelf) : base(battleManager, parentBeing, parentAbility, effectName)
    {
        this.targetResource = targetStat;
        this.value = value;
        this.targetSelf = targetSelf;
    }
}
