using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepBehaviour : NPC
{

    void Start()
    {
        StartLiveRoutine();

        var currentGameRound = boardController.currentGameRound;
 

        BASE_MAXHP  = 333 * Mathf.Pow((1 + 0.20f), currentGameRound);
        BASE_ARMOR = 5 * Mathf.Pow((1 + 0.05f), currentGameRound);
        if (BASE_ARMOR >= 100)
        {
            BASE_ARMOR = 99f;
        }
        BASE_ATTACKPOWER = 45 * Mathf.Pow( (1 + 0.20f), currentGameRound);
        BASE_SPELLPOWER = 0;
        BASE_RETALIATION = 0;

        if (uiController.wizard_difficultyPicker.value == 0)
        {
            BASE_MAXHP *= 0.9f;
            BASE_ARMOR *= 0.9f;
            BASE_ATTACKPOWER *= 0.9f;
        }
        else if (uiController.wizard_difficultyPicker.value == 2)
        {
            BASE_MAXHP *= 1.1f;
            BASE_ARMOR *= 1.1f;
            BASE_ATTACKPOWER *= 1.1f;
        }

        HP = BASE_MAXHP;
        MAXHP = BASE_MAXHP;
        ARMOR = BASE_ARMOR;
        ATTACKPOWER = BASE_ATTACKPOWER;
        SPELLPOWER = BASE_SPELLPOWER;
        RETALIATION = BASE_RETALIATION;
    }
}
