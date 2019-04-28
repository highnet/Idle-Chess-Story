using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerBehaviour : NPC
{
    private void Start()
    {
        StartLiveRoutine();
        if (uiController.wizard_difficultyPicker.value == 2)
        {
            BASE_MAXHP *= 1.1f;
            BASE_ARMOR *= 1.1f;
            BASE_ATTACKPOWER *= 1.1f;
        }
        else if (uiController.wizard_difficultyPicker.value == 3)
        {
            BASE_MAXHP *= 1.2f;
            BASE_ARMOR *= 1.2f;
            BASE_ATTACKPOWER *= 1.2f;
        }

        HP = BASE_MAXHP;
        MAXHP = BASE_MAXHP;
        ARMOR = BASE_ARMOR;
        ATTACKPOWER = BASE_ATTACKPOWER;
        SPELLPOWER = BASE_SPELLPOWER;
        RETALIATION = BASE_RETALIATION;

        RecalculateInventoryItemValues();
    }
}
