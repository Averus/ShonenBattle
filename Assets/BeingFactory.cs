using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeingFactory : MonoBehaviour {

    public BattleManager battleManager;
    public GameObject beingPrefab;


    // Use this for initialization
    void Start()
    {

        battleManager = GetComponent<BattleManager>();

        if (beingPrefab == null)
        {
            Debug.Log("ERROR: Beingfactory requires beingPrefab reference");
        }



    }

    //Resources
    void BasicResources(Being b)
    {
        Resource hp = new Resource("HP", 100, 100);
        Resource stamina = new Resource("STAMINA", 100, 100);
        Resource powerLevel = new Resource("POWERLEVEL", 10);

        b.resources.Add(powerLevel);
        b.resources.Add(hp);
        b.resources.Add(stamina);
    }

    //stat blocks
    void BasicStats(Being b)
    {
      

        Stat dex = new Stat("DEXTERITY", b, 100, 100);
        dex.current = 10;

        Stat toHit = new Stat("TOHIT", b, 100, 100);
        toHit.current = 0;


        b.stats.Add(dex);
        b.stats.Add(toHit);
        


    }

    //defence blocks
    void BasicDefences(Being b)
    {
        Ability def1 = new Ability(battleManager, "Block",100, 1, true);
        def1.isDefence = true;
        def1.defenceSpeed = 50;
        ResourceAtValue_Condition reqStam = new ResourceAtValue_Condition(battleManager, b, "Stamina above 0", "STAMINA", ">", 0);
        ModulateResource_Effect costsStamina = new ModulateResource_Effect(battleManager, b, def1, "CostsStamina", "STAMINA", -10, true);
        Block_DefenceEffect block = new Block_DefenceEffect(battleManager, b, def1, "Block");
        Self_TargetingCriteria self = new Self_TargetingCriteria(battleManager, b, def1);

        def1.effects.Add(block);
        def1.effects.Add(costsStamina);
        def1.conditions.Add(reqStam);
        def1.targetingCriteria.Add(self);
        b.defences.Add(def1);

        //Create a generic 'dodge' defence
        Ability def2 = new Ability(battleManager, "Dodge", 100, 1, true);
        def2.isDefence = true;
        def2.defenceSpeed = 40;
        ResourceAtValue_Condition reqStam2 = new ResourceAtValue_Condition(battleManager, b, "Stamina above 0", "STAMINA", ">", 0);
        ModulateResource_Effect costsStamina2 = new ModulateResource_Effect(battleManager, b, def2, "CostsStamina", "STAMINA", -20, true);
        Dodge_DefenceEffect dodge = new Dodge_DefenceEffect(battleManager, b, def2, "Dodge");
        Self_TargetingCriteria self2 = new Self_TargetingCriteria(battleManager, b, def2);

        def2.effects.Add(dodge);
        def2.effects.Add(costsStamina2);
        def2.conditions.Add(reqStam2);
        def2.targetingCriteria.Add(self2);
        b.defences.Add(def2);

        //Create a generic 'free action' defence
        Ability def3 = new Ability(battleManager, "Any Action", 100, 1, true);
        def3.isDefence = true;
        def3.defenceSpeed = 5;
        NoCondition_Condition noCondition6 = new NoCondition_Condition(battleManager, b, "NoCondition");
        Self_TargetingCriteria self3 = new Self_TargetingCriteria(battleManager, b, def3);
        AnyAction_DefenceEffect anyAction = new AnyAction_DefenceEffect(battleManager, b, def3, "AnyAction");

        def3.effects.Add(anyAction);
        def3.conditions.Add(noCondition6);
        def3.targetingCriteria.Add(self3);
        b.defences.Add(def3);
    }

    //behaviours
    void AttackBehaviour(Being b)
    {
        //Create a new behaviour called 'just attack'
        Behaviour justAttack = new Behaviour(battleManager, b, "Just attack");
        NoCondition_Condition noCondition = new NoCondition_Condition(battleManager, b, "NoCondition");
        IncludesEffect_SelectionCriteria includesDamage = new IncludesEffect_SelectionCriteria(battleManager, b, "IncludesDamage", "Damage");

        justAttack.conditions.Add(noCondition); ;
        justAttack.selectionCriteria.Add(includesDamage);
        b.behaviours.Add(justAttack);
    }
    void HealSelfBehaviour(Being b)
    {
        //Create a new behaviour called 'heal self'
        Behaviour healSelf = new Behaviour(battleManager, b, "Heal self");
        //Create a condition that hp must be below 20 
        ResourceAtValue_Condition hpLessThanTwenty = new ResourceAtValue_Condition(battleManager, b, "HP less that 20", "HP", "<", 20);
        //Create a selectionCriteria for appropriate abilities that the must have a 'healself' effect
        IncludesEffect_SelectionCriteria includesHealSelf = new IncludesEffect_SelectionCriteria(battleManager, b, "IncludesHealSelf", "HealSelf");

        healSelf.conditions.Add(hpLessThanTwenty); ;
        healSelf.selectionCriteria.Add(includesHealSelf);
        b.behaviours.Add(healSelf);
    }

    //ability packs
    void BasicAttackAbilities(Being b)
    {

        Ability ab3 = new Ability(battleManager, "Poor punch", 100, 1, false);
        NoCondition_Condition noCondition2 = new NoCondition_Condition(battleManager, b, "NoCondition");
        ModulateResource_Effect damage = new ModulateResource_Effect(battleManager, b, ab3, "Damage", "HP", -3, false);
        Others_TargetingCriteria o = new Others_TargetingCriteria(battleManager, b, ab3);

        ab3.conditions.Add(noCondition2);
        ab3.effects.Add(damage);
        ab3.targetingCriteria.Add(o);
        ab3.numberOfTargets = 1;
        // b.abilities.Add(ab3);


        Ability ab4 = new Ability(battleManager, "Punch", 100,1, false);
        ResourceAtValue_Condition reqStam = new ResourceAtValue_Condition(battleManager, b, "Stamina above 0", "STAMINA", ">", 0);
        ModulateResource_Effect costsStamina = new ModulateResource_Effect(battleManager, b, ab3, "CostsStamina", "STAMINA", -20, true);
        ModulateResource_Effect damage2 = new ModulateResource_Effect(battleManager, b, ab4, "Damage", "HP", -10, false);
        Others_TargetingCriteria o2 = new Others_TargetingCriteria(battleManager, b, ab4);

        ab4.conditions.Add(reqStam);
        ab4.effects.Add(costsStamina);
        ab4.effects.Add(damage2);
        ab4.targetingCriteria.Add(o2);
        ab4.numberOfTargets = 1;
        b.abilities.Add(ab4);
    }
    void BasicSelfHealingAbility(Being b)
    {
        //Create a new ability called Healself
        Ability ab = new Ability(battleManager, "Heal", 100, 1, false);
        //create a condition that MP must not be greater the 5 (the cost)
        ResourceAtValue_Condition gt = new ResourceAtValue_Condition(battleManager, b, "MP greater than 5", "MP", ">", 5);

        HealSelf_Effect hs = new HealSelf_Effect(battleManager, b, ab, "HealSelf", 10);
        Self_TargetingCriteria s = new Self_TargetingCriteria(battleManager, b, ab);

        ab.conditions.Add(gt);
        ab.effects.Add(hs);
        ab.targetingCriteria.Add(s);
        b.abilities.Add(ab);


    }
    
    //passive ability packs
    void BasicPassiveAbilities(Being b)
    {
        Ability reg = new Ability(battleManager, "Stamina regen", 100, 1, false);
        reg.isDefence = false;

        NewRound_Condition nrc = new NewRound_Condition(battleManager, b, "New round condition");
        ModulateResource_Effect staminaRegen = new ModulateResource_Effect(battleManager, b, reg, "Stamina regen", "STAMINA", +50, true);
        Self_TargetingCriteria self = new Self_TargetingCriteria(battleManager, b, reg);

        reg.effects.Add(staminaRegen);
        reg.conditions.Add(nrc);
        reg.targetingCriteria.Add(self);
        b.abilities.Add(reg);
    }


    public Being CreateBeing(string name)
    {

        GameObject g = Instantiate(beingPrefab);
        Being p;

        if(g.GetComponent<Being>() == false)
        {
            Debug.Log("ERROR: beingfactory's references beingPrefab has no Being component");
            return null;
        }

        p = g.GetComponent<Being>();

        //Name being
        p.beingName = name;


        //Bestow Resources
        BasicResources(p);

        //Bestow stats
        BasicStats(p);

        //Bestow behaviours
        AttackBehaviour(p);

        //Bestow abilities
        BasicAttackAbilities(p);

        //Bestow passive abilities
        BasicPassiveAbilities(p);

        //bestow defences
        BasicDefences(p);



        //puts the beings defences in the right order (sorted by defenceSpeed)
        p.SortDefences(); 

        Debug.Log("Being created");

        return p;

       


    }

    public BeingFactory(BattleManager bm)
    {
        battleManager = bm;
    }


}
