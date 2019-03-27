using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBehaviour : NPC
{

    public TextMesh textMesh;
    public float highestDamageTaken = 0;
    public float lastDamageTaken = 0;
    public float hpLastFrame = 0;
    public float hpLastSecond = 0;
    public float currentDamagePerSecond = 0;
    public float maxDamagePerSecond = 0;
    public float damagePerSecondAdder = 0;


    void Start()
    {
        StartCoroutine(Live(1, this.autoattack_DamageType, this.ABILITY,this.actionTime));
        textMesh = GetComponentInChildren<TextMesh>();
    }


    private void Update()
    {

        if (HP < hpLastFrame)
        {
            lastDamageTaken = -(HP - hpLastFrame);

            damagePerSecondAdder += lastDamageTaken;

            if (lastDamageTaken > highestDamageTaken)
            {
                highestDamageTaken = lastDamageTaken;
            }
        }

        textMesh.text = "Last Damage: " + lastDamageTaken + " Top Damage: " + highestDamageTaken + " DPS: " + currentDamagePerSecond + " TOP DPS: " + maxDamagePerSecond;

        if (HP < 500)
        {
            HP = MAXHP;
        }
        hpLastFrame = HP;
    }

    public new IEnumerator Live(int attackDistance, DamageSource autoAttackType, Ability abilityToUse,float actionTime)
    {
        for (; ; )
        {
            if (currentDamagePerSecond > maxDamagePerSecond)
            {
                maxDamagePerSecond = currentDamagePerSecond;
            }
            currentDamagePerSecond = damagePerSecondAdder;


            hpLastSecond = HP;
            damagePerSecondAdder = 0;
            yield return new WaitForSeconds(1f);
        }
    }
}
