using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepBehaviour : NPC
{

    void Start()
    {
        StartLiveRoutine();

        var currentGameRound = boardController.currentGameRound;
        Debug.Log(currentGameRound);

        BASE_MAXHP *= 10 * currentGameRound; // try to scale the unit
        BASE_ARMOR *= currentGameRound / 100;
        BASE_ATTACKPOWER = 50 +  ((currentGameRound));
        BASE_SPELLPOWER = 50;
        RETALIATION *= currentGameRound / 150;

        HP = BASE_MAXHP;
        MAXHP = BASE_MAXHP;
        ARMOR = BASE_ARMOR;
        ATTACKPOWER = BASE_ATTACKPOWER;
        SPELLPOWER = BASE_SPELLPOWER;
        RETALIATION = BASE_RETALIATION;
    }
}
