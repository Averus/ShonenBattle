using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {

    public Being parentBeing;
    public string statName = "NO NAME";
    private bool hasChanged; //if it hasnt changed then there's no need to CalculateFinalValue again

    public float max = 0;
    public float baseValue = 0;
    public float current;
    private float lastBaseValue = float.MinValue;

    readonly List<StatModifier> statModifiers;

    /*
    public float current
    {
        get
        {
            if (hasChanged || lastBaseValue != baseValue)
            {
                lastBaseValue = baseValue;
                _current = CalculateFinalValue();
                hasChanged = false;
            }
            return _current;
        }
    }
    */


    private int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.order < b.order)
            return -1;
        else if (a.order > b.order)
            return 1;
        return 0; // if (a.Order == b.Order)
    }




    public void AddModifier(StatModifier mod)
    {
        hasChanged = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
        CalculateFinalValue(); // I added this here - Tim

    }

    public bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            hasChanged = true;
            CalculateFinalValue();// I added this - Tim
            return true;
        }
        return false;
    }

    private float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdd = 0; // This will hold the sum of our "PercentAdd" modifiers

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.type == StatModType.Flat)
            {
                finalValue += mod.value;
            }
            else if (mod.type == StatModType.PercentAdd) // When we encounter a "PercentAdd" modifier
            {
                sumPercentAdd += mod.value; // Start adding together all modifiers of this type

                // If we're at the end of the list OR the next modifer isn't of this type
                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd; // Multiply the sum with the "finalValue", like we do for "PercentMult" modifiers
                    sumPercentAdd = 0; // Reset the sum back to 0
                }
            }
            else if (mod.type == StatModType.PercentMult) // Percent renamed to PercentMult
            {
                finalValue *= 1 + mod.value;
            }
        }

        return (float)System.Math.Round(finalValue, 4);
    }

    public bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].source == source)
            {
                hasChanged = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        if (didRemove)
        {
            CalculateFinalValue();// I added this - Tim
        }
        return didRemove;
    }



    public Stat(string name, Being parentBeing, float max, float baseValue)
    {
        this.parentBeing = parentBeing;
        this.statName = name;
        this.max = max;
        this.baseValue = baseValue;

        statModifiers = new List<StatModifier>();
    }



}
