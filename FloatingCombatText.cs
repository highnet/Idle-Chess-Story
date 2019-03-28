using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCombatText : MonoBehaviour
{

    public float NumberToDisplay;
    public DamageSource DamageSourceType;
    public HealSource HealSourceType;
    public bool SourceNPC_IsEnemy;
    public string ExtraDisplayString;
    // Start is called before the first frame update
    void Start()
    {
        if (NumberToDisplay < 0)
        {
            NumberToDisplay = 0;
        }

        NumberToDisplay = Mathf.Round(NumberToDisplay);

        TextMesh textmesh = this.GetComponent<TextMesh>();
        textmesh.text = "" + NumberToDisplay;


        if (ExtraDisplayString != "")
        {
            textmesh.text += " (" + ExtraDisplayString + ")";
        }
        if (System.Math.Abs(NumberToDisplay) < 0.001f && ExtraDisplayString != "")
        {
            textmesh.text = " (" + ExtraDisplayString + ")";
        }

        if (DamageSourceType != DamageSource.NOTHING)
        {
        //adapt floating combat text colors
        if (SourceNPC_IsEnemy == true) // enemy attacks are always red
        { 
            textmesh.color = Color.red;
        }
        else if (SourceNPC_IsEnemy == false) // human attack
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
            textmesh.fontStyle = FontStyle.Normal;
        }
        else if (DamageSourceType == DamageSource.Magical_Ability || DamageSourceType == DamageSource.Physical_Ability)
        {
            textmesh.fontStyle = FontStyle.BoldAndItalic;
            textmesh.characterSize *= 1.5f;
        }

        }
        else if (HealSourceType != HealSource.NOTHING)
        {
            textmesh.text = "" + NumberToDisplay;
            textmesh.color = Color.green;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation; // "billboard"
    }
}
