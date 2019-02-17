using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource{


    public readonly string resourceName = "NO NAME";
    float current;
    float max;

    //readonly List<ResourceModifier> resourceModifiers;

    public float GetCurrent()
    {
        return current;
    }

    public float GetMax()
    {
        return max;
    }

    public void Modify(ResourceModifier r) //This just adds stright away for now. Add more complex behaviour as it's required.
    {
        current += r.GetValue();

        if (max > 0 && current > max)
        {
            current = max;
        }
    }

    /*
    public void UpdateCurrent()
    {

        if (resourceModifiers.Count > 0)
        {
            for (int i = resourceModifiers.Count; i >= 0 ; i--)
            {
                current += resourceModifiers[i].GetValue();
                resourceModifiers.RemoveAt(i);

            }
        }


    }
    */

    //main constructor requires all arguments
    public Resource(string resourceName, float current, float max)
    {
        this.resourceName = resourceName;
        this.current = current;
        this.max = max;

        //resourceModifiers = new List<ResourceModifier>();
    }

    //second constructor does nto require Max, which is set to 0 instead (floats cannot be nulled)
    public Resource(string resourceName, float current) : this(resourceName, current, 0) { }

}
