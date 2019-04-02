using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DisplayMode {RegularDamage,Retaliation,Lifesteal,Heal,AbilityDamage}
public class FloatingCombatText : MonoBehaviour
{


    public DamageReport dmgReport;
    public string ExtraDisplayString;
    public DisplayMode displayMode;
    // Start is called before the first frame update
    void Start()
    {
        if (dmgReport.primaryDamageDealt < 0)
        {
            dmgReport.primaryDamageDealt = 0;
        }

        dmgReport.primaryDamageDealt = (float) System.Math.Round(dmgReport.primaryDamageDealt,0,System.MidpointRounding.AwayFromZero);
        dmgReport.lifeStealHeal = (float) System.Math.Round(dmgReport.lifeStealHeal,0,System.MidpointRounding.AwayFromZero);
        dmgReport.retaliationDamageRecieved = (float) System.Math.Round(dmgReport.retaliationDamageRecieved,0,System.MidpointRounding.AwayFromZero);

        TextMesh textmesh = this.GetComponent<TextMesh>();

        if (displayMode == DisplayMode.Retaliation)
        {
            textmesh.text = "" + dmgReport.retaliationDamageRecieved;
        }
        else if (displayMode == DisplayMode.Heal)
        {
            textmesh.text = "" + dmgReport.primaryDamageDealt;
        }
        else if (displayMode == DisplayMode.Lifesteal)
        {
            textmesh.text = "" + dmgReport.lifeStealHeal;
        }
        else if (displayMode == DisplayMode.RegularDamage)
        {
            textmesh.text = "" + dmgReport.primaryDamageDealt;
        }
        else if (displayMode == DisplayMode.AbilityDamage)
        {
            textmesh.text = "" + dmgReport.primaryDamageDealt;
        }

        if (dmgReport.wasCriticalStrike  && displayMode != DisplayMode.Retaliation)
        {
            ExtraDisplayString = "CRIT";
        }
        else if (dmgReport.wasMiss && displayMode != DisplayMode.Retaliation)
        {
            ExtraDisplayString = "MISS";
        }
        else if (dmgReport.wasDampenedMiss && displayMode != DisplayMode.Retaliation)
        {
            ExtraDisplayString = "DAMPENED";
        }
        if (ExtraDisplayString != "")
        {
            textmesh.text += " (" + ExtraDisplayString + ")";
        }

        if (displayMode == DisplayMode.RegularDamage || displayMode == DisplayMode.AbilityDamage || displayMode == DisplayMode.Retaliation)
        {
            //adapt floating combat text colors
            if (dmgReport.damageSourceNPC.isEnemy == true || displayMode == DisplayMode.Retaliation) // enemy attacks are always red
            {
                textmesh.color = Color.red;
            }
            else if (dmgReport.damageSourceNPC.isEnemy == false) // human attack
            {
                if (displayMode == DisplayMode.RegularDamage)  // auto attacks
                {
                    textmesh.color = Color.white; // auto attacks are white
                }
                  
                    else if (displayMode == DisplayMode.AbilityDamage)
                {
                    textmesh.color = Color.yellow;
                }
            }
        
        }

        else if (displayMode == DisplayMode.Lifesteal || displayMode == DisplayMode.Heal)
        {
              
                textmesh.color = Color.green;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation; // "billboard"
    }
}
