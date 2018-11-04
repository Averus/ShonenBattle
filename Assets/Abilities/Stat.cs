using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat {

    public Being parentBeing;
    public string statName = "NO NAME";

    public int max = 0;
    public int baseValue = 0;
    public int current = 0;

    

    public Stat(string name, Being parentBeing)
    {
        this.parentBeing = parentBeing;
        this.statName = name;
    }



}
