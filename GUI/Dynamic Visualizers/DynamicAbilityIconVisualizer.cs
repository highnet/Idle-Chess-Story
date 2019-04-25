using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicAbilityIconVisualizer : MonoBehaviour
{
    public Image fireballIcon;
    public Image AP_UP_SelfIcon;
    public Image Armor_UP_SelfIcon;
    public Image MaxHP_Up_SelfIcon;
    public Image Retaliation_UP_SelfIcon;
    public Image heroicStrikeIcon;
    public Image apPowerDownIcon;
    public Image armorPowerDownIcon;
    public Image frostballIcon;
    public Image stabIcon;
    public Image stunIcon;
    public Image healIcon;

    public void SetImage(Ability abilitytoSet)
    {
        fireballIcon.enabled = false;
        AP_UP_SelfIcon.enabled = false;
        Armor_UP_SelfIcon.enabled = false;
        MaxHP_Up_SelfIcon.enabled = false;
        Retaliation_UP_SelfIcon.enabled = false;
        heroicStrikeIcon.enabled = false;
        apPowerDownIcon.enabled = false;
        armorPowerDownIcon.enabled = false;
        frostballIcon.enabled = false;
        stabIcon.enabled = false;
        stunIcon.enabled = false;
        healIcon.enabled = false;

        if (abilitytoSet == Ability.Fireball)
        {
            fireballIcon.enabled = true;
        }
        else if(abilitytoSet == Ability.AP_UP_Self)
        {
            AP_UP_SelfIcon.enabled = true;
        }
        else if (abilitytoSet == Ability.Armor_UP_Self)
        {
            Armor_UP_SelfIcon.enabled = true;
        }
        else if (abilitytoSet == Ability.MaxHP_Up_Self)
        {
            MaxHP_Up_SelfIcon.enabled = true;
        }
        else if (abilitytoSet == Ability.Retaliation_UP_Self)
        {
            Retaliation_UP_SelfIcon.enabled = true;
        }
        else if (abilitytoSet == Ability.HeroicStrike)
        {
            heroicStrikeIcon.enabled = true;
        }
        else if (abilitytoSet == Ability.AP_DOWN_OTHER)
        {
            apPowerDownIcon.enabled = true;
        }
        else if (abilitytoSet == Ability.ARMOR_DOWN_OTHER)
        {
            armorPowerDownIcon.enabled = true;
        }
        else if (abilitytoSet == Ability.FrostBall)
        {
            frostballIcon.enabled = true;
        }
        else if (abilitytoSet == Ability.Stab)
        {
            stabIcon.enabled = true;
        }
        else if (abilitytoSet == Ability.Stun)
        {
            stunIcon.enabled = true;
        }
        else if (abilitytoSet == Ability.HealFriend)
        {
            healIcon.enabled = true;
        }
    }
}
