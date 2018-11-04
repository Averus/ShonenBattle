using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingFactory : MonoBehaviour {

    public BattleManager battleManager;



    // Use this for initialization
    void Start()
    {

        battleManager = GetComponent<BattleManager>();



    }

    public Being CreateBeing(string name)
    {
        Being p = new Being();


        

        p.beingName = name;

        Stat hp = new Stat("HP", p);
        hp.max = 100;
        hp.current = 100; //Beings are created with full hp I guess.

        Stat mp = new Stat("MP", p);
        mp.max = 100;
        mp.current = 100;

        Stat dex = new Stat("DEXTERITY", p);
        dex.current = 10;

        Stat toHit = new Stat("TOHIT", p);
        toHit.current = 0;

        Stat powerLevel = new Stat("POWERLEVEL", p);
        powerLevel.current = 10;

        //do the rest later

        p.stats.Add(hp);
        p.stats.Add(mp);
        p.stats.Add(dex);
        p.stats.Add(toHit);
        p.stats.Add(powerLevel);



        //Create a new behaviour called 'heal self'
        Behaviour healSelf = new Behaviour(battleManager, p, "Heal self");
        //Create a condition that hp must be below 20 
        CompareTwoValues hpLessThanTwenty = new CompareTwoValues(battleManager, p, "HP less than 20", p, "HP", "<", 20); //this is set to a stupidly high figure for test purposes
        //Create a selectionCriteria for appropriate abilities that the must have a 'healself' effect
        IncludesEffect includesHealSelf = new IncludesEffect(battleManager, p, "IncludesHealSelf", "HealSelf");

        healSelf.conditions.Add(hpLessThanTwenty); ;
        healSelf.selectionCriteria.Add(includesHealSelf);
        p.behaviours.Add(healSelf);

        //Create a new ability called Healself
        Ability ab = new Ability(battleManager, "Heal", true);
        //create a condition that MP must not be greater the 5 (the cost)
        CompareTwoValues gt = new CompareTwoValues(battleManager, p, "MP greater than 5", p, "MP", ">", 5);
        CostsMP cm = new CostsMP(battleManager, p, ab, "CostsMP", 5);
        HealSelf hs = new HealSelf(battleManager, p, ab, "HealSelf", 10);
        Self s = new Self(battleManager, p, ab);

        ab.conditions.Add(gt);
        ab.effects.Add(cm);
        ab.effects.Add(hs);
        ab.targetingCriteria.Add(s);
        p.abilities.Add(ab);

        //Create a new ability called Healall
        Ability ab2 = new Ability(battleManager, "Leech Life", true);
        //create a condition that MP must not be greater the 5 (the cost)
        CompareTwoValues gt2 = new CompareTwoValues(battleManager, p, "MP greater than 5", p, "MP", ">", 5);
        CostsMP cm2 = new CostsMP(battleManager, p, ab2, "CostsMP", 5);
        HealSelf hs2 = new HealSelf(battleManager, p, ab2, "HealSelf", 10);
        Damage d2 = new Damage(battleManager, p, ab2, "Damage", 4);

        ab2.conditions.Add(gt2);
        ab2.effects.Add(cm2);
        ab2.effects.Add(hs2);
        ab2.effects.Add(d2);
        //p.abilities.Add(ab2);

        //Create a new behaviour called 'just attack'
        Behaviour justAttack = new Behaviour(battleManager, p, "Just attack");
        NoCondition noCondition = new NoCondition(battleManager, p, "NoCondition");
        IncludesEffect includesDamage = new IncludesEffect(battleManager, p, "IncludesDamage", "Damage");

        justAttack.conditions.Add(noCondition); ;
        justAttack.selectionCriteria.Add(includesDamage);
        p.behaviours.Add(justAttack);

        //Create a new ability called Damage
        Ability ab3 = new Ability(battleManager, "Quick Punch", true);
        NoCondition noCondition2 = new NoCondition(battleManager, p, "NoCondition");
        Damage damage = new Damage(battleManager, p, ab3, "Damage", 7);
        ModulateToHitSelf modSelfToHit = new ModulateToHitSelf(battleManager, p, ab3, "Modulate ToHit Self", "+", 1);
        Others o = new Others(battleManager, p, ab3);

        ab3.conditions.Add(noCondition2);
        ab3.effects.Add(damage);
        ab3.effects.Add(modSelfToHit);
        ab3.targetingCriteria.Add(o);
        ab3.numberOfTargets = 1;
        p.abilities.Add(ab3);


        //CREATE DEFENCES  
        //Create a generic 'block' defence
        Ability def1 = new Ability(battleManager, "Block", true);
        def1.isDefence = true;
        def1.defenceSpeed = 100;
        Block block = new Block(battleManager, p, def1, "Block");
        NoCondition noCondition3 = new NoCondition(battleManager, p, "NoCondition");//should be a stamian cost probably
        Self self = new Self(battleManager, p, def1);

        def1.effects.Add(block);
        def1.conditions.Add(noCondition3);
        def1.targetingCriteria.Add(self);
        p.defences.Add(def1);

        //Create a generic 'dodge' defence
        Ability def2 = new Ability(battleManager, "Dodge", true);
        def2.isDefence = true;
        def2.defenceSpeed = 80;
        Dodge dodge = new Dodge(battleManager, p, def2, "Dodge");
        NoCondition noCondition4 = new NoCondition(battleManager, p, "NoCondition");
        Self self2 = new Self(battleManager, p, def2);

        def2.effects.Add(dodge);
        def2.conditions.Add(noCondition4);
        def2.targetingCriteria.Add(self2);
        p.defences.Add(def2);

        //Create a generic 'free action' defence
        Ability def3 = new Ability(battleManager, "Any Action", true);
        def3.isDefence = true;
        def3.defenceSpeed = 50;
        NoCondition noCondition5 = new NoCondition(battleManager, p, "NoCondition");
        Self self3 = new Self(battleManager, p, def3);
        AnyAction anyAction = new AnyAction(battleManager, p, def3, "AnyAction");

        def3.effects.Add(anyAction);
        def3.conditions.Add(noCondition5);
        def3.targetingCriteria.Add(self3);
        p.defences.Add(def3);









        p.SortDefences(); //puts the beings defences in the right order (sorted by defenceSpeed)
        Debug.Log("Being created");
        return p;


    }

    public BeingFactory(BattleManager bm)
    {
        battleManager = bm;
    }


}
