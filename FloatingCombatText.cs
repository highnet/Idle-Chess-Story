using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCombatText : MonoBehaviour
{
    public DamageSource DamageSourceType;
    public HealSource HealSourceType;
    public DamageReport dmgReport;
    public string ExtraDisplayString;
    // Start is called before the first frame update
    void Start()
    {
        if (dmgReport.damageToTakeOrDisplay  < 0)
        {
            dmgReport.damageToTakeOrDisplay = 0;
        }


        dmgReport.damageToTakeOrDisplay = Mathf.Round(dmgReport.damageToTakeOrDisplay);

        TextMesh textmesh = this.GetComponent<TextMesh>();
        textmesh.text = "" + dmgReport.damageToTakeOrDisplay;


        if (dmgReport.damageToTakeOrDisplay != 0 && dmgReport.wasCriticalStrike)
        {
            textmesh.characterSize *= 2;
            ExtraDisplayString = "CRIT";
        }
        else if (dmgReport.wasMiss)
        {
            ExtraDisplayString = "MISS";
        } else if (dmgReport.wasDampenedMiss)
        {
            ExtraDisplayString = "DAMPENED";
        }

        if (ExtraDisplayString != "")
        {
            textmesh.text += " (" + ExtraDisplayString + ")";
        }
        if (dmgReport.damageToTakeOrDisplay == 0 && ExtraDisplayString != "")
        {
            textmesh.text = " (" + ExtraDisplayString + ")";
        }

        if (DamageSourceType != DamageSource.NOTHING)
        {
        //adapt floating combat text colors
        if (dmgReport.damageSourceNPC.isEnemy == true) // enemy attacks are always red
        { 
            textmesh.color = Color.red;
        }
        else if (dmgReport.damageSourceNPC.isEnemy == false) // human attack
        {
            if (DamageSourceType == DamageSource.MagicalDamage_AutoAttack || DamageSourceType == DamageSource.PhysicalDamage_AutoAttack) // auto attacks
            {
                textmesh.color = Color.white; // auto attacks are white
            }
            else if (DamageSourceType == DamageSource.Magical_Ability || DamageSourceType == DamageSource.Physical_Ability)
            {
                textmesh.color = Color.yellow;
            }
        }

        //adapt floating combat text styles
        if (DamageSourceType == DamageSource.MagicalDamage_AutoAttack || DamageSourceType == DamageSource.PhysicalDamage_AutoAttack)
        {
      
        }
        else if (DamageSourceType == DamageSource.Magical_Ability || DamageSourceType == DamageSource.Physical_Ability)
        {
            textmesh.characterSize *= 1.3f;
        }

        }
        else if (HealSourceType != HealSource.NOTHING)
        {
            textmesh.text = "" + dmgReport.damageToTakeOrDisplay;
            textmesh.color = Color.green;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation; // "billboard"
    }
}
