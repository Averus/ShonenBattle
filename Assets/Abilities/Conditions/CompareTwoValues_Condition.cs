using UnityEngine;
using System.Collections;

public class CompareTwoValues_Condition : Condition
{

    //this whole class is ridiculous. It's to stop me having to create >9000 individual classes for 'less that MP'. 'greater than mp', 'less than hp' etc but it's nto exactly good programming

    Being p;

    string value;

    string mode;

    int target;



    public override bool CanThisBeUsed()
    {

        if (mode == "<")
        {
            if (value == "HP")
            {
                //Debug.Log( p.beingName + " HP is " + p.GetStatValue("HP", 2) + " but the target is "+ target);
                if (p.GetStatValue("HP", 2) <= target)
                {
                    //Debug.Log(p.beingName + " hp is at " + p.GetStatValue("HP", 2));
                    //Debug.Log("target is at " + target);
                    return true;
                }
                return false;

            }

            if (value == "MP")
            {
                if (p.GetStatValue("MP", 2) <= target)
                {
                    //Debug.Log(p.beingName + " mp is at " + p.GetStatValue("MP", 2));
                    //Debug.Log("target is at " + target);
                    return true;
                }
                return false;
            }


        }

        if (mode == ">")
        {
            if (value == "HP")
            {
                if (p.GetStatValue("HP", 2) >= target)
                {
                    //Debug.Log(p.beingName + " hp is at " + p.GetStatValue("HP", 2));
                    //Debug.Log("target is at " + target);
                    return true;
                }
                return false;
            }

            if (value == "MP")
            {
                if (p.GetStatValue("MP", 2) >= target)
                {
                    //Debug.Log(p.beingName + " mp is at " + p.GetStatValue("MP", 2));
                    //Debug.Log("target is at " + target);
                    return true;
                }
                return false;
            }

        }


        Debug.Log("ERROR: CompareTwoValues did not run, were the constructor perameters correct?");
        return false;

    }


    

    public CompareTwoValues_Condition(BattleManager battleManager, Being parentBeing, string conditionName, Being p, string value, string mode, int target) : base(battleManager, parentBeing, conditionName) //This should grab the battleManager from the base constructor (Condition)
    {
        this.p = p;

        this.value = value;

        this.mode = mode;

        this.target = target;

    }


}

