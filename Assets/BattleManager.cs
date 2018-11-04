using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {



    public Main mainState;
    public BeingFactory beingFactory;
    

    public List<Being> combatants;
    public int currentElement = 0;

    public float textSpeed = 500f;


    public enum CombatStates
    {
        INITIATIVESORT, STARTTURN, CALCULATETOHIT, CALCULATEDEFENCE, CALCULATEEFFECT
    }
    public CombatStates currentState;


    // Use this for initialization
    void Start () {
        mainState = GetComponent<Main>();
        beingFactory = GetComponent<BeingFactory>();

        combatants = new List<Being>();


    }

    public void StartCombat()
    {
        currentElement = 0;
        StartCoroutine(StartTurn(combatants[currentElement]));
    }

    public void NextTurn()
    {

        if (combatants.Count == 0)
        {
            Debug.Log("ERROR: Cannot move to next turn as there are no combatants");
            return;
        }
        
        if (currentElement <= combatants.Count -1)
        {
            if (combatants[currentElement].condition == Being.Condition.normal) //check if the next Being in the initiative order is not unconcious, asleep etc
            {
                if (combatants[currentElement].playerControlled == true)
                {
                   
                    //Players turn

                }
                else
                {
                    currentElement += 1;

                    if (currentElement > combatants.Count -1)
                    {
                        Debug.Log("End of the round");
                        NewRound();
                        return;
                    }


                    //Debug.Log("moving to next turn from inside NextTurn");
                    StartCoroutine(StartTurn(combatants[currentElement]));
   
                }
            }

        }
        else
        {
            Debug.Log("Everyone has gone");
        }

    }

    void NewRound()
    {
        currentElement = 0;
        StartCoroutine(StartTurn(combatants[currentElement]));
    }

    IEnumerator StartTurn(Being being) //should perhaps be called Select ability
    {
        Debug.Log("starting " + being.beingName + "'s turn");

        //Any looking for passive or some such would go here

        being.GetUseableAbilities();

        if (being.useableAbilities.Count > 0)
        {
            being.SelectAnAbility();
            being.SelectTargets(being.selectedAbility);
            Debug.Log(being.beingName + " chooses " + being.selectedAbility.abilityName);

        }
        else
        {
            Debug.Log(being.beingName + " can't do anything! (is this an error?)");
        }
   
        yield return new WaitForSeconds(textSpeed);
        
        StartCoroutine(CalculateToHit(being, being.selectedAbility, being.selectedTargets)); //right now I'm building the calculateToHit method, so the Use() below is commented out, it should be fired from elsewhere eventually anyway

        NextTurn();

    }

    IEnumerator CalculateToHit(Being attacker, Ability ability, List<Being> defenders)
    {
        Debug.Log(attacker.beingName + " uses " + ability.abilityName);
        //any looking for passives etc would come here. //this would include effects like 'Goblin punch gets +5 to hit against Goblins' <-- that would be a separate passive ability to Goblin Punch

        for (int i = 0; i < defenders.Count; i++)
        {
            //Debug.Log("for target " + (i+1) + "...");
            //check to see if any effect of this ability will change the toHit values (eg it adds +5 to hit or similar)
            for (int ii = 0; ii < ability.effects.Count; ii++)
            {
                if (ability.effects[ii] is ModulateToHitSelf) //here I'm literally only looking for one type of effect, I couldnt think of another way of doing it but it seems a but ugly.
                {
                    ability.effects[ii].Use(attacker); //This should add any relevent statmodulations into the beings statmodulation list
                }
            }

            if (attacker.statModulations.Count > 0)
            {
                List<StatModulation> tempList = new List<StatModulation>(); //create a temporary list of stat modulatons

                for (int iii = 0; iii < attacker.statModulations.Count; iii++)
                {
                    if (attacker.statModulations[iii].targetStat.statName == "TOHIT" && attacker.statModulations[iii].targetStat.parentBeing == attacker)
                    {
                        tempList.Add(attacker.statModulations[iii]); //add relevant stat mods to our temporary list     
                    }
                }

                //now we run through our temporary list of stat modulations and fire them in BODMAS order

                for (int iiii = 0; iiii < tempList.Count; iiii++) //first +
                {
                    if (tempList[iiii].modifier == "+")
                    {
                        tempList[iiii].Use();
                    }
                }
                for (int iiii = 0; iiii < tempList.Count; iiii++) //then for -
                {
                    if (tempList[iiii].modifier == "-")
                    {
                        tempList[iiii].Use();
                    }
                }

                //If we need to remove those used Stat Modulations from the beings statMOdulations list then here is where we would do it.
                //It's not in yet because we might have statmods that last for X turns or count themselves down to death etc.
            }


            //actually calculate the toHit value

            Debug.Log("Calculating to hit...");
            int tohit = attacker.GetStatValue("TOHIT", 2);
            int dex = attacker.GetStatValue("DEXTERITY", 2);
            int random = Random.Range(1, 101);
            int pl = attacker.GetStatValue("POWERLEVEL", 2);
            float final =  pl + ((dex / 100) * ability.ranks) + random + tohit;
            Debug.Log(pl + " + (" + (dex / 100) + " * " + ability.ranks + ") + " + random + " + " + tohit + " = " + final);

            //Debug.Log("To Hit value is " + final);

            CalculateDefence(attacker, ability, final, defenders[i]);

        }


        yield return new WaitForSeconds(textSpeed);
    

    }

    void CalculateDefence(Being attacker, Ability ability, float toHit, Being defender)
    {
        //look through defences and put useable ones in the useableDefences list
        defender.GetUseableDefences();

        //the next part selects a defence to use, this is done automatically by comparing speeds at the moment.
        //compare useable defences to the incoming ability
        if (defender.useableDefences.Count > 0)
        {
            for (int i = 0; i < defender.useableDefences.Count; i++)
            {
                //Debug.Log(ability.abilityName + " " + toHit + " vs " + defender.useableDefences[i].abilityName + " " + defender.useableDefences[i].defenceSpeed);

                if (toHit <= defender.useableDefences[i].defenceSpeed) //choose this defence to use
                {
                    defender.SelectDefenceTargets(defender.useableDefences[i]);
                    defender.useableDefences[i].Use();
                    return;
                }
            }
            Debug.Log(defender.beingName + ": Too fast!");
            return;
        }
        Debug.Log("ERROR: " + defender.beingName + " has no defences, they should at least have a basic reflex");   
    }

    // Update is called once per frame
    void Update () {




    }

    public BattleManager(Main main, BeingFactory bf)
    {
        mainState = main;
        beingFactory = bf;
    }
}
