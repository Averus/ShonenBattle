using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BattleManager : MonoBehaviour {

    public Main mainState;

    public BeingFactory beingFactory;

    public float textSpeed = 500f;
    bool pause = false;

    public List<Being> combatants;

    public int currentElement = 0;
    public int playerSelection = -1;

    public List<EffectToken> effectQueue;

    public enum CombatStates
    {
        OFF, NEWROUND, NEXTTURN, INITIATIVESORT, SELECTABILITY, CALCULATETOHIT, CALCULATEDEFENCE, CALCULATEEFFECT, WAITFORPLAYERINPUT
    }

    public CombatStates currentState;
    public CombatStates savedState;



    public bool newRoundDone = false;
    public bool nextTurnDone = false;
    public bool waitForPlayerInputDone = false;

    // Use this for initialization
    void Start () {
        mainState = GetComponent<Main>();
        beingFactory = GetComponent<BeingFactory>();

        combatants = new List<Being>();
        effectQueue = new List<EffectToken>();



    }

    public void StartCombat()//unused, now using states in Update
    {
        currentState = CombatStates.NEWROUND;
    }

    void NextTurn()
    {

        Debug.Log("starting NEXT TURN");

        if (combatants.Count == 0)
        {
            Debug.Log("ERROR: Cannot move to next turn as there are no combatants");
            return;
        }

        currentElement += 1;

        if (currentElement <= combatants.Count - 1)
        {
            if (combatants[currentElement].condition == Being.Condition.normal) //check if the next Being in the initiative order is not unconcious, asleep etc
            {
                Debug.Log("==========TURN " + currentElement + "==========");
                StartCoroutine(SelectAbility(combatants[currentElement]));
                return;
            }
            else
            {
                nextTurnDone = false;
                return;
            }
        }
        newRoundDone = false;
        currentState = CombatStates.NEWROUND;
    }
    
    public void UseReleventPassiveAbilities() // This literally checks and uses the abilities in the order of the combatants.
    {
        for (int i = 0; i < combatants.Count; i++)
        {
            combatants[i].GetUseablePassiveAbilities();  //useable passive abilities are put in the useable abilities list

            if (combatants[i].useableAbilities.Count == 0)
            {
                Debug.Log(combatants[i].beingName + " has no relevant passive abilities");

            }
            else
            {
                for (int ii = 0; ii < combatants[i].useableAbilities.Count; ii++)
                {
                    combatants[i].useableAbilities[ii].Use(); //all useble abilities are used putting Effect Tokens into the Effect Queue
                }
            }


        }

        ResolveEffectQueue(); //use any tokens in the effect queue

    }

    IEnumerator SelectAbility(Being being) //should perhaps be called Select ability
    {
        Debug.Log("starting " + being.beingName + "'s turn" + " HP:" + being.GetResourceValue("HP", 1) + " STAMINA:" + being.GetResourceValue("STAMINA", 1));

        //Any looking for passive or some such would go here

        being.GetUseableActiveAbilities();

        if (being.useableAbilities.Count > 0)
        {

            if (being.playerControlled == true)
            {
                for (int i = 0; i < being.useableAbilities.Count; i++)
                {
                    Debug.Log(i + ". " + being.useableAbilities[i].abilityName);
                }

                if (playerSelection > -1)
                {
                    being.SelectTargets(being.useableAbilities[playerSelection - 1]);
                }
                if (being.selectedAbility == null)
                {
                    currentState = CombatStates.WAITFORPLAYERINPUT;
                }
                savedState = CombatStates.SELECTABILITY;
                

            }
            if (being.playerControlled == false)
            {
                being.SelectAnAbility();
                being.SelectTargets(being.selectedAbility);
                Debug.Log(being.beingName + " chooses " + being.selectedAbility.abilityName);
                yield return new WaitForSeconds(textSpeed);
                CalculateToHit(being, being.selectedAbility, being.selectedTargets);
                yield return new WaitForSeconds(textSpeed);
                nextTurnDone = false;
            }
            
        }
        else
        {
            Debug.Log(being.beingName + " can't do anything! (is this an error?)");
            nextTurnDone = false;
            yield return null;
        }
   


    }

    void CalculateToHit(Being attacker, Ability ability, List<Being> defenders)
    {
        Debug.Log(attacker.beingName + " uses " + ability.abilityName);
        //any looking for passives etc would come here. //this would include effects like 'Goblin punch gets +5 to hit against Goblins' <-- that would be a separate passive ability to Goblin Punch

        for (int i = 0; i < defenders.Count; i++)
        {
            //Debug.Log("for target " + (i+1) + "...");
            //check to see if any effect of this ability will change the toHit values (eg it adds +5 to hit or similar)
            for (int ii = 0; ii < ability.effects.Count; ii++)
            {
                if (ability.effects[ii] is ModulateToHitSelf_Effect) //here I'm literally only looking for one type of effect, I couldnt think of another way of doing it but it seems a but ugly.
                {
                    ability.effects[ii].Use(attacker); //This should add any relevent statmodulations into the beings statmodulation list
                }
            }


            if (effectQueue.Count > 0) //if we have some effects in the effect queue...
            {
                List<StatModulation> tempList = new List<StatModulation>(); //create a temporary list of stat modulatons

                for (int iii = 0; iii < effectQueue.Count; iii++)
                {
                    if (effectQueue[iii] is StatModulation)
                    {
                        StatModulation sm = effectQueue[iii] as StatModulation;
                        if (sm.targetStat.statName == "TOHIT") //if it's a TOHIT modulator
                        {
                            tempList.Add(effectQueue[iii] as StatModulation); //Add it to our temporary list
                        } 
                        
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

            //Debug.Log("Calculating to hit...");
            float tohit = attacker.GetStatValue("TOHIT", 2);
            float dex = attacker.GetStatValue("DEXTERITY", 2);
            float random = Random.Range(1, 101);
            float pl = attacker.GetResourceValue("POWERLEVEL", 1);
            float final =  pl + ((dex / 100) * ability.ranks) + random + tohit;
            Debug.Log(pl + " + (" + (dex / 100) + " * " + ability.ranks + ") + " + random + " + " + tohit + " = " + final);

            //Debug.Log("To Hit value is " + final);

            CalculateDefence(attacker, ability, final, defenders[i]);

        }


        //yield return new WaitForSeconds(textSpeed);
    

    }

    void CalculateDefence(Being attacker, Ability ability, float toHit, Being defender)
    {
        //TESTReportOnEffectQueue();//test
        //look through defences and put useable ones in the useableDefences list
        defender.GetUseableDefences();

        //the next part selects a defence to use, this is done automatically by comparing speeds at the moment.
        //compare useable defences to the incoming ability
        if (defender.useableDefences.Count > 0)
        {
            for (int i = 0; i < defender.useableDefences.Count; i++)
            {
                Debug.Log(ability.abilityName + " " + toHit + " vs " + defender.useableDefences[i].abilityName + " " + defender.useableDefences[i].defenceSpeed);

                if (toHit <= defender.useableDefences[i].defenceSpeed) //choose this defence to use
                {
                    defender.SelectDefenceTargets(defender.useableDefences[i]);
                    defender.useableDefences[i].Use();
                    return;
                }
            }
            Debug.Log(defender.beingName + ": I can't dodge it!");
            UseAbility(attacker, ability, defender);
            return;
        }
        Debug.Log("ERROR: " + defender.beingName + " has no defences, they should at least have a basic reflex");



        //TODO now
        //1. consider how to implement a general list of effect tokens (stat mods, incoming damage etc)
        //2. That list might need to be overseen by a 'resolve effects' function

        


    }

    void UseAbility(Being attacker, Ability ability, Being target)
    {
        ability.Use(target); //dumps stat modulations into the right place

        ResolveEffectQueue(); //runs through the EffectQueue and uses the tokens

        Debug.Log(target.beingName + " HP:" + target.GetResourceValue("HP", 1));

    }

    void ResolveEffectQueue()
    {
        //just so it does something for now...ALL TEST STUFF HERE just to close the loop
        //TESTReportOnEffectQueue();//test
        if (effectQueue.Count > 0)
        {
            for (int i = 0; i < effectQueue.Count; i++)
            {
                effectQueue[i].Use();
            }

            //This is most certainly a test thing if we are to ever have tokens that persist they shouldnt be cleaned out like this but instead a counter decremented or similar
            effectQueue.Clear(); 
        }
        //TESTReportOnEffectQueue();
    }

    void TESTReportOnEffectQueue()
    {
        if (effectQueue.Count > 0)
        {
            string s = "";

            for (int i = 0; i < effectQueue.Count; i++)
            {
                string e = effectQueue[i].ToString();
                s += e;
                s += " ";
            }

            Debug.Log("Effect Queue: " + s);
            return;
        }

        Debug.Log("Effect Queue: empty");
    }


    // Update is called once per frame
    void Update()
    {

        if (currentState == CombatStates.NEWROUND)
        {
            if (newRoundDone == false)
            {
                Debug.Log("==========NEW ROUND==========");
                currentElement = -1;
                UseReleventPassiveAbilities();
                newRoundDone = true;
                currentState = CombatStates.NEXTTURN;
                nextTurnDone = false;
            }
        }
        if (currentState == CombatStates.NEXTTURN)
        {
            if (nextTurnDone == false)
            {
                nextTurnDone = true;
                NextTurn();
            }
        }
        if (currentState == CombatStates.WAITFORPLAYERINPUT)
        {
            if (waitForPlayerInputDone == false)
            {
                if (Input.anyKey)
                {
                    int i;
                    if (int.TryParse(Input.inputString, out i))
                    {
                        waitForPlayerInputDone = true;
                        playerSelection = i;
                        Debug.Log("PLayer selected " + i);
                    }

                }
            }

        }
    }

    public BattleManager(Main main, BeingFactory bf)
    {
        mainState = main;
        beingFactory = bf;
    }
}
