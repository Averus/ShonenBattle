using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class Being : MonoBehaviour{

    //is this Being player controlled? ------ later need to add a different AI's to equip
    public bool playerControlled = false;

    //beings name
    public string beingName = "Unknown";

    //all stats to go here
    public List<Stat> stats = new List<Stat>();
    public List<Resource> resources = new List<Resource>();
    //public List<StatModulation> statModulations = new List<StatModulation>(); //the list of EffectTokens is now global, it contains all effects currnetly in play and is in the battlemanager

    //normal, dazed, staggared, unconcious, dead etc
    public enum Condition { normal, dazed, staggered, unconcious };
    public Condition condition = Condition.normal;

    //Behaviours will go here, behaviours dictate AI behaviour
    public List<Behaviour> behaviours = new List<Behaviour>();

    //abilities is the total list of abilities for the Being, useable abilities changes each turn based on the gamestate and is populated via a method in battlemanager
    public List<Ability> abilities = new List<Ability>();
    public List<Ability> useableAbilities = new List<Ability>();
    public Ability selectedAbility; //the ability selected for use
    public List<Being> selectedTargets = new List<Being>();

    //The being defences go here
    public List<Ability> defences = new List<Ability>();
    public List<Ability> useableDefences = new List<Ability>();
    public List<Being> selectedDefenceTargets = new List<Being>();

    //sorts defences by defenceSpeed lowest to highest (the order an incoming attack will run through them)
    public void SortDefences()
    {
        defences = defences.OrderBy(Ability => Ability.defenceSpeed).ToList();

    }

    //honestly code for this project is getting sloppier and sloppier, just look at the state of all this messy reliance on strings
    /// <summary>
    /// Returns the value of a substat, 0 = Max, 1 = Base, 2 = current 
    /// </summary>
    /// <param name="stats">The name of the Stat</param>
    /// <param name="value">0 = Max, 1 = Base, 2 = current</param>
    /// <returns></returns>
    public float GetStatValue (string stat, int subStat){

        for (int i = 0; i < stats.Count; i++)
        {
            if (stats[i].statName == stat )
            {
                if (subStat == 0)
                {
                    return stats[i].max;
                }
                if (subStat == 1)
                {
                    return stats[i].baseValue;
                }
                if (subStat == 2)
                {
                    return stats[i].current;
                }
                if (subStat > 2)
                {
                    Debug.Log("ERROR: stat value requested not max, base, or current (subStat parameter > 2) ");
                    return 0;
                }
            }

        }
        Debug.Log("ERROR: " + beingName + " does not contain stat " + stat);
        return 0;

    }
    public Stat GetStat (string statName)
    {

        if (statName == null)
        {
            Debug.Log("ERROR: stat name is null");
            return null;
        }

        for (int i = 0; i < stats.Count; i++)
        {
            if (stats[i].statName == statName)
            {
                return stats[i];
            }

        }

        Debug.Log("ERROR: " + beingName + " does not contain a stat named " + statName);
        return null;
    }
    /// <summary>
    /// Returns the value of a substat, 0 = Max, 1 = Current
    /// </summary>
    /// <param name="resourceName">The name of the resource</param>
    /// <param name="value">0 = Max, 1 = Base, 2 = current</param>
    /// <returns></returns>
    public float GetResourceValue(string resourceName, int subStat)
    {

        for (int i = 0; i < resources.Count; i++)
        {
            if (resources[i].resourceName == resourceName)
            {
                if (subStat == 0)
                {
                    return resources[i].GetMax();
                }
                if (subStat == 1)
                {
                    return resources[i].GetCurrent();
                }
                if (subStat > 1)
                {
                    Debug.Log("ERROR: stat value requested not max(0) or current(1) (subStat parameter > 1) ");
                    return 0;
                }
            }

        }
        Debug.Log("ERROR: " + beingName + " does not contain resource " + resourceName);
        return 0;

    }
    public Resource GetResource(string resourceName)
    {

        if (resourceName == null)
        {
            Debug.Log("ERROR: resource name is null");
            return null;
        }

        for (int i = 0; i < resources.Count; i++)
        {
            if (resources[i].resourceName == resourceName)
            {
                return resources[i];
            }

        }

        Debug.Log("ERROR: " + beingName + " does not contain a resource named " + resourceName);
        return null;
    }
   

    //GetUsableAbilities should be called once per turn. Filters abilities by which ones can be performed, checks for valid targets for each ability and populated their valid targets lists
    public void GetUseableActiveAbilities()
    {
        //Debug.Log("getting useable abilities");
        useableAbilities.Clear();

        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].isPassive == false)
            {
                if (abilities[i].CanThisBeUsed())
                {
                    useableAbilities.Add(abilities[i]);
                    //Debug.Log(abilities[i].abilityName + " added to useableAbilities list");
                }

            }
        }
    }

    public void GetUseablePassiveAbilities()
    {
        //Debug.Log("getting useable abilities");
        useableAbilities.Clear();

        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].isPassive == true) 
            {
                if (abilities[i].CanThisBeUsed())
                {
                    useableAbilities.Add(abilities[i]);
                    //Debug.Log(abilities[i].abilityName + " added to useableAbilities list");
                }

            }
        }
    }

    public void GetUseableDefences()
    {
        //Debug.Log("getting useable defences");
        useableDefences.Clear();

        for (int i = 0; i < defences.Count; i++)
        {
            if (defences[i].CanThisBeUsed())
            {
                useableDefences.Add(defences[i]);
                //Debug.Log(defences[i].abilityName + " added to useableDefences list");
            }
        }
    }

    //Compares behaviours to the abilities that can be used and sets selectedAbility equal to an ability from useableAbilities
    public void SelectAnAbility()
    {

        Debug.Log(beingName + " is selecting and ability to use...");

        List<Ability> selectedAbilities = new List<Ability>();

        for (int i = 0; i < behaviours.Count; i++)
        {
            if (behaviours[i].CanThisBeDone())
            {

                selectedAbilities.AddRange(behaviours[i].GetAppropriateAbilities(useableAbilities));

                //Debug.Log(" selected abilities is at length " + selectedAbilities.Count);

                if (selectedAbilities.Count == 1)
                {
                    selectedAbility = selectedAbilities[0]; 
                    break;
                }

                if (selectedAbilities.Count > 1)
                {
                    int r = Random.Range(0, (selectedAbilities.Count));
                    //Debug.Log("random value is " + r);
                    selectedAbility = selectedAbilities[r];
                    break;
                }

            }
        }


    } 

    public void SelectTargets(Ability ability)
    {
        selectedTargets.Clear();//get rid of any targets from a previous turn

        if (selectedAbility == null)
        {
            Debug.Log("ERROR: " + beingName + " has no selected target for " + ability.abilityName);
            return;
        }

        for (int i = 0; i < ability.numberOfTargets; i++) //fire the number of times you can...
        {
            int r = Random.Range(0, ability.validTargets.Count); //pick a random target from those that are valid

            selectedTargets.Add(ability.validTargets[r]); //add them to the selectedTargets list. This function does not yet handle cases where an ability may only affect a target once. It also has no methods for chosing other than randomly.

        }

    }

    public void SelectDefenceTargets(Ability ability)
    {
        for (int i = 0; i < ability.numberOfTargets; i++) //fire the number of times you can...
        {
            int r = Random.Range(0, ability.validTargets.Count); //pick a random target from those that are valid

            selectedDefenceTargets.Add(ability.validTargets[r]); //add them to the selectedDefenceTargets list. This function does not yet handle cases where an ability may only affect a target once. It also has no methods for chosing other than randomly.

        }

    }

}
