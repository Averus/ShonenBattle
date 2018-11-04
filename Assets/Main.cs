using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

public enum GameStates { BATTLE };
public GameStates gameState = new GameStates();


    public BattleManager battleManager;
    public BeingFactory beingFactory;
    

    void Start()
    {

        battleManager = GetComponent<BattleManager>();
        beingFactory = GetComponent<BeingFactory>();




        StartCoroutine(waitToStart());

      
    }

    IEnumerator waitToStart()
    {
        yield return new WaitForSeconds(2);

        //test stuff below


        Being b = beingFactory.CreateBeing("Jeff");
        Being c = beingFactory.CreateBeing("AshogiJizoh");


        battleManager.combatants.Add(b);

        battleManager.combatants.Add(c);



        gameState = GameStates.BATTLE;
        battleManager.StartCombat();

       

    }

}
