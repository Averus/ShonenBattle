using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300,

}


public class StatModifier{

    public readonly float value;
    public readonly StatModType type;
    public readonly int order; //Order of resolution
    public readonly object source;


    // "Main" constructor. Requires all variables.
    public StatModifier(float value, StatModType type, int order, object source) // Added "source" input parameter
    {
        this.value = value;
        this.type = type;
        this.order = order;
        this.source = source; // Assign Source to our new input parameter
    }

    // Requires Value and Type. Calls the "Main" constructor and sets Order and Source to their default values: (int)type and null, respectively.
    public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

    // Requires Value, Type and Order. Sets Source to its default value: null
    public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

    // Requires Value, Type and Source. Sets Order to its default value: (int)Type
    public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}
