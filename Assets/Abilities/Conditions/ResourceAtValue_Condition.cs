using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAtValue_Condition : Condition
{

    String resourceName;
    string modifier;
    float value;

    public override bool CanThisBeUsed()
    {
        Resource r = parentBeing.GetResource(resourceName);

        if (modifier == "<")
        {
            if (r.GetCurrent() < value)
            {
                return true;
            }
        }

        if (modifier == ">")
        {
            if (r.GetCurrent() > value)
            {
                return true;
            }
        }

        return false;
    }


    public ResourceAtValue_Condition(BattleManager battleManager, Being parentBeing, string conditionName, string resourceName, string modifier, float value) : base(battleManager, parentBeing, conditionName)
    {
        this.resourceName = resourceName;
        this.modifier = modifier;
        this.value = value;
    }
}
