using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicTribeIconVisualizer : MonoBehaviour
{
    public Image wizardIcon;
    public Image warriorIcon;
    public Image undeadIcon;
    public Image structureIcon;
    public Image elementalIcon;
    public Image beastIcon;
    public Image assassinIcon;
    public Image guardianIcon;

      

    public void SetImage(Tribe tribeToSet, bool isVisible)
    {
        wizardIcon.enabled = false;
        warriorIcon.enabled = false;
        undeadIcon.enabled = false;
        structureIcon.enabled = false;
        elementalIcon.enabled = false;
        beastIcon.enabled = false;
        assassinIcon.enabled = false;
        guardianIcon.enabled = false;

        if (isVisible)
        {
        if (tribeToSet == Tribe.Wizard)
        {
            wizardIcon.enabled = true;
        }
        else if (tribeToSet == Tribe.Warrior)
        {
            warriorIcon.enabled = true;
        }
        else if (tribeToSet == Tribe.Undead)
        {
            undeadIcon.enabled = true;
        }
        else if (tribeToSet == Tribe.Structure)
        {
            structureIcon.enabled = true;
        }
        else if (tribeToSet == Tribe.Elemental)
        {
            elementalIcon.enabled = true;
        }
        else if (tribeToSet == Tribe.Beast)
        {
            beastIcon.enabled = true;
        }
        else if (tribeToSet == Tribe.Assassin)
        {
            assassinIcon.enabled = true;
        }
        else if (tribeToSet == Tribe.Guardian)
        {
            guardianIcon.enabled = true;
        }

        }
    }
}
