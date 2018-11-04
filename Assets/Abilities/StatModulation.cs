using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModulation  {


    //This class will be created by 'damage' effects, 'buff to hit' effects etc, anything that modulates a stat. They will get put in a modulation array in each being and resolved in BODMAS order.


    public Stat targetStat;
    public string modifier;
    public int value;

    public void Use()
    {

        if (modifier == "+")
        {
            targetStat.current += value;
            return;
        }

        if (modifier == "-")
        {
            targetStat.current -= value;
            return;
        }

        else
        {
            Debug.Log("ERROR: modifier requested was not recognised");
        }


       

    }

    public StatModulation(Stat targetStat, string modifier, int value)
    {
        this.targetStat = targetStat;
        this.modifier = modifier;
        this.value = value;
    }
}
