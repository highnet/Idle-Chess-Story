using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSoldierBehaviour : NPC
{
    private void Start()
    {
        StartLiveRoutine();
        if (uiController.wizard_difficultyPicker.value == 1)
        {
            BASE_MAXHP *= 1.3f;
            BASE_ARMOR *= 1.3f;
            BASE_ATTACKPOWER *= 1.3f;
            BASE_SPELLPOWER *= 1.3f;
        }
        else if (uiController.wizard_difficultyPicker.value == 2)
        {
            BASE_MAXHP *= 1.4f;
            BASE_ARMOR *= 1.4f;
            BASE_ATTACKPOWER *= 1.4f;
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
