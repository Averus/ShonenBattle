using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour {

    BattleManager battleManager;
    public Text text;

    // Use this for initialization
    void Start () {

        battleManager = GetComponent<BattleManager>();
		
	}
	
	// Update is called once per frame
	void Update () {

        string info = "";




        
        

        for (int i = 0; i < battleManager.combatants.Count; i++)
        {
            if (battleManager.currentElement == i)
            {
                info += "[TURN]";
            }

            info += battleManager.combatants[i].beingName;
            info += "\n";

            for (int ii = 0; ii < battleManager.combatants[i].resources.Count; ii++)
            {
                info += battleManager.combatants[i].resources[ii].resourceName + "\n";
                info += battleManager.combatants[i].resources[ii].GetCurrent() + "/" + battleManager.combatants[i].resources[ii].GetMax() + "\n";
                info += "[";

                for (int iii = 0; iii < battleManager.combatants[i].resources[ii].GetCurrent(); iii++)
                {
                    info += "¦";
                }
                for (int iii = 0; iii < battleManager.combatants[i].resources[ii].GetMax() - battleManager.combatants[i].resources[ii].GetCurrent(); iii++)
                {
                    info += " ";
                }
                info += "]";
                info += "\n";
                info += "\n";
            }
            info += "\n";




            for (int ii = 0; ii < battleManager.combatants[i].stats.Count; ii++)
            {
                info += battleManager.combatants[i].stats[ii].statName + "\n";
                info += "[";

                for (int iii = 0; iii < battleManager.combatants[i].stats[ii].current; iii++)
                {
                    info += "¦";
                }
                for (int iii = 0; iii < battleManager.combatants[i].stats[ii].max - battleManager.combatants[i].stats[ii].current; iii++)
                {
                    info += " ";
                }
                info += "]";
                info += "\n";
                info += "\n";
            }
            info += "\n";



        }

        text.text = info;
	}
}
