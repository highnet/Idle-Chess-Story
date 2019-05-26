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
    public GameObject tooltipPanel;
    public Text AbilityNameTooltipText;
    public Text AbilityDescriptionTooltipText;

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
            AbilityNameTooltipText.text = "Fireball";
            AbilityDescriptionTooltipText.text = "Deal damage equal to 200% of your spell power";
        }
        else if(abilitytoSet == Ability.AP_UP_Self)
        {
            AP_UP_SelfIcon.enabled = true;
            AbilityNameTooltipText.text = "AttackPower UP Self";
            AbilityDescriptionTooltipText.text = "Increase your attack power for a duration of time;";
        }
        else if (abilitytoSet == Ability.Armor_UP_Self)
        {
            Armor_UP_SelfIcon.enabled = true;
            AbilityNameTooltipText.text = "Armor UP Self";
            AbilityDescriptionTooltipText.text = "Increase your armor for a duration of time";
        }
        else if (abilitytoSet == Ability.MaxHP_Up_Self)
        {
            MaxHP_Up_SelfIcon.enabled = true;
            AbilityNameTooltipText.text = "HP UP Self";
            AbilityDescriptionTooltipText.text = "Increases your HP for a duration of time";
        }
        else if (abilitytoSet == Ability.Retaliation_UP_Self)
        {
            Retaliation_UP_SelfIcon.enabled = true;
            AbilityNameTooltipText.text = "Retalation UP Self";
            AbilityDescriptionTooltipText.text = "Increases your retaliation for a duration of time";
        }
        else if (abilitytoSet == Ability.HeroicStrike)
        {
            heroicStrikeIcon.enabled = true;
            AbilityNameTooltipText.text = "Heroic Strike";
            AbilityDescriptionTooltipText.text = "Deal Damage equal to 190% of your Attack Power";
        }
        else if (abilitytoSet == Ability.AP_DOWN_OTHER)
        {
            apPowerDownIcon.enabled = true;
            AbilityNameTooltipText.text = "AttackPower DOWN Other";
            AbilityDescriptionTooltipText.text = "Reduce your target's Attackpower for a duration of time";
        }
        else if (abilitytoSet == Ability.ARMOR_DOWN_OTHER)
        {
            armorPowerDownIcon.enabled = true;
            AbilityNameTooltipText.text = "Armor DOWN Other";
            AbilityDescriptionTooltipText.text = "Deal damage equal to 200% of your spell power";
        }
        else if (abilitytoSet == Ability.FrostBall)
        {
            frostballIcon.enabled = true;
            AbilityNameTooltipText.text = "Frostball";
            AbilityDescriptionTooltipText.text = "Deal damage equal to 150% of your spell power";
        }
        else if (abilitytoSet == Ability.Stab)
        {
            stabIcon.enabled = true;
            AbilityNameTooltipText.text = "Stab";
            AbilityDescriptionTooltipText.text = "Deal damage equal to 140% of your AttackPower";
        }
        else if (abilitytoSet == Ability.Stun)
        {
            stunIcon.enabled = true;
            AbilityNameTooltipText.text = "Stun";
            AbilityDescriptionTooltipText.text = "Stun your target for a duration of time";
        }
        else if (abilitytoSet == Ability.HealFriend)
        {
            healIcon.enabled = true;
            AbilityNameTooltipText.text = "Heal Friend";
            AbilityDescriptionTooltipText.text = "Heal a friend for 100% of your spell power";
        } else
        {
            AbilityNameTooltipText.text = "";
            AbilityDescriptionTooltipText.text = "";
        }
    }

    public void DisableTooltipPanel()
    {
        tooltipPanel.gameObject.SetActive(false);
    }
    public void EnableTooltipPanel()
    {
        if (AbilityNameTooltipText.text != "")
        {
            tooltipPanel.gameObject.SetActive(true);
        }
    }
}
