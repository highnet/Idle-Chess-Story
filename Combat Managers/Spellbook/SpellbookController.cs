using System.Collections;
using UnityEngine;

public class SpellbookController : MonoBehaviour
{
    public GameObject worldController;
    public UiController uiController;
    public NpcController npcController;

    private void Start() // todo: pool me
    {
        worldController = GameObject.Find("World Controller");
        npcController = worldController.GetComponent<NpcController>();
        uiController = worldController.GetComponent<UiController>();
    }

    public void CastSpell(NPC casterNPC, NPC targetNPC, Ability abilityToCast, float CasterSpellPower,float CasterAttackPower)
    {

        if (abilityToCast == Ability.NOTHING || casterNPC == null || targetNPC == null)
        {
            return;
        } else if (casterNPC != null)
        {
           casterNPC.TryPlayAttackAnimation();
           casterNPC.CONCENTRATION = 0;
        }

        if (abilityToCast == Ability.Fireball)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("Fireball"), this.transform.position, Quaternion.identity);
            DamageReport dmgReport = targetNPC.CalculateDamageTaken(2 * CasterSpellPower, casterNPC, DamageSource.Magical_Ability);
            go.GetComponentInChildren<AbilityFireball>().dmgReport = dmgReport;
            go.GetComponentInChildren<AbilityFireball>().destination = dmgReport.damageReceiverNPC.transform.position;
        //    uiController.combatLogger.combatReport.Add(dmgReport.damageSourceNPC.name, dmgReport.primaryDamageDealt);

        }

        else if (abilityToCast == Ability.AP_UP_Self)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("AP_PowerUp"), this.transform.position, Quaternion.identity);
            go.transform.SetParent(casterNPC.transform);
            float boostValue = CasterAttackPower * (0.1f + (casterNPC.TIER * 0.05f));
            float boostDuration = 5 + (1 * casterNPC.TIER);
            casterNPC.ATTACKPOWER += boostValue;
            if (casterNPC.PowerChangeParticleSystem != null)
            {
                casterNPC.PowerChangeParticleSystem.PowerUpObject.SetActive(true);
            }
            StartCoroutine(AP_SubstractBonusAfterSeconds(casterNPC, boostDuration, boostValue, go));
        }

        else if (abilityToCast == Ability.Armor_UP_Self)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("ARMOR_PowerUp"), this.transform.position, Quaternion.identity);
            go.transform.SetParent(casterNPC.transform);
            float boostValue = 7;
            float boostDuration = 5 + (1 * casterNPC.TIER);
            casterNPC.ARMOR += boostValue;
            if (casterNPC.PowerChangeParticleSystem != null)
            {
                casterNPC.PowerChangeParticleSystem.PowerUpObject.SetActive(true);
            }
            StartCoroutine(ARMOR_SubstractBonusAfterSeconds(casterNPC, boostDuration, boostValue, go));
        }
        else if (abilityToCast == Ability.MaxHP_Up_Self)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("MAXHP_PowerUp"), this.transform.position, Quaternion.identity);
            go.transform.SetParent(casterNPC.transform);
            float boostValue = casterNPC.BASE_MAXHP * 0.25f;
            float boostDuration = 5 + (1 * casterNPC.TIER);
            casterNPC.MAXHP += boostValue;
            casterNPC.HP += boostValue;
            if (casterNPC.PowerChangeParticleSystem != null)
            {
                casterNPC.PowerChangeParticleSystem.PowerUpObject.SetActive(true);
            }
            StartCoroutine(MAXHP_SubstractBonusAfterSeconds(casterNPC, boostDuration, boostValue, go));
        }
        else if (abilityToCast == Ability.Retaliation_UP_Self)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("RETALIATION_PowerUp"), this.transform.position, Quaternion.identity);
            go.transform.SetParent(casterNPC.transform);
            float boostValue = 7;
            float boostDuration = 5 + (1 * casterNPC.TIER);
            casterNPC.RETALIATION += boostValue;
            if (casterNPC.PowerChangeParticleSystem != null)
            {
                casterNPC.PowerChangeParticleSystem.PowerUpObject.SetActive(true);
            }
            StartCoroutine(RETALIATION_SubstractBonusAfterSeconds(casterNPC, boostDuration, boostValue, go));
        }
        else if (abilityToCast == Ability.HeroicStrike)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("HeroicStrike Animation"), this.transform.position, Quaternion.identity);
            go.transform.LookAt(targetNPC.transform);
            go.transform.SetParent(casterNPC.transform);
            DamageReport dmgReport = targetNPC.CalculateDamageTaken(1.9f * CasterAttackPower, casterNPC, DamageSource.Physical_Ability);
            uiController.SpawnFloatingCombatText(dmgReport.damageReceiverNPC, dmgReport, DisplayMode.AbilityDamage);
            targetNPC.TakePureDamage(dmgReport);
        //    uiController.combatLogger.combatReport.Add(dmgReport.damageSourceNPC.name, dmgReport.primaryDamageDealt);
            GameObject.Destroy(go, 4);

        }
        else if (abilityToCast == Ability.Stab)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("Stab Animation"), this.transform.position, Quaternion.identity);
            go.transform.LookAt(targetNPC.transform);
            go.transform.SetParent(casterNPC.transform);
            DamageReport dmgReport = targetNPC.CalculateDamageTaken(1.4f * CasterAttackPower, casterNPC, DamageSource.Physical_Ability);
            uiController.SpawnFloatingCombatText(dmgReport.damageReceiverNPC, dmgReport, DisplayMode.AbilityDamage);
            targetNPC.TakePureDamage(dmgReport);
       //     uiController.combatLogger.combatReport.Add(dmgReport.damageSourceNPC.name, dmgReport.primaryDamageDealt);
            GameObject.Destroy(go, 3);

        }
        else if (abilityToCast == Ability.AP_DOWN_OTHER)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("AP_PowerDown"), targetNPC.transform.position, Quaternion.identity);
            go.transform.LookAt(targetNPC.transform);
            go.transform.SetParent(targetNPC.transform);
            float boostValue = targetNPC.BASE_ATTACKPOWER * (0.1f + (casterNPC.TIER * 0.05f));
            float boostDuration = 6;
            targetNPC.ReduceAttackPower(boostValue);
            if (targetNPC.PowerChangeParticleSystem != null)
            {
                targetNPC.PowerChangeParticleSystem.PowerDownObject.SetActive(true);
            }
            StartCoroutine(AP_AddBonusAfterSeconds(targetNPC, boostDuration, boostValue, go));

        }

        else if (abilityToCast == Ability.ARMOR_DOWN_OTHER)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("ARMOR_PowerDown"), targetNPC.transform.position, Quaternion.identity);
            go.transform.SetParent(targetNPC.transform);
            float boostValue = 7;
            float boostDuration = 6;
            targetNPC.ReduceArmor(boostValue);
            if (targetNPC.PowerChangeParticleSystem != null)
            {
                targetNPC.PowerChangeParticleSystem.PowerDownObject.SetActive(true);
            }
            StartCoroutine(ARMOR_AddBonusAfterSeconds(targetNPC, boostDuration, boostValue, go));
        }
        else if (abilityToCast == Ability.FrostBall)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("FrostBall"), this.transform.position, Quaternion.identity);
            DamageReport dmgReport = targetNPC.CalculateDamageTaken(2 * CasterSpellPower, casterNPC, DamageSource.Magical_Ability);
            go.GetComponentInChildren<AbilityFrostBall>().dmgReport = dmgReport;
            go.GetComponentInChildren<AbilityFrostBall>().destination = dmgReport.damageReceiverNPC.transform.position;
        //    uiController.combatLogger.combatReport.Add(dmgReport.damageSourceNPC.name, dmgReport.primaryDamageDealt);
        }
        else if (abilityToCast == Ability.Stun)
        {
            float stunDuration = 3;
            DoStunCycle(stunDuration,targetNPC,casterNPC);
        }
        else if (abilityToCast == Ability.HealFriend)
        {
            float amountToHeal = CasterSpellPower;
            NPC[] casters_AllyList = null;

            if (casterNPC.isEnemy == false)
            {
                 casters_AllyList = npcController.deployedAllyList.ToArray();
            }
            else
            {
                casters_AllyList = npcController.GetComponent<NpcController>().enemyList.ToArray();
            }
            targetNPC = casters_AllyList[0];
            float maximumHPDeficit = 0;

                foreach(NPC nPC in casters_AllyList)
                {

                float HPDeficit = nPC.MAXHP - nPC.HP;

                   if (HPDeficit >= maximumHPDeficit)
                    {
                        maximumHPDeficit = nPC.MAXHP - nPC.HP;
                        targetNPC = nPC;
                    }
                }

            DamageReport dmgReport = ScriptableObject.CreateInstance<DamageReport>();
            dmgReport.damageReceiverNPC = targetNPC;
            dmgReport.damageSourceNPC = casterNPC;
            dmgReport.primaryDamageDealt = amountToHeal;
            dmgReport.wasCriticalStrike = false;
            dmgReport.wasDampenedMiss = false;
            dmgReport.wasMiss = false;
            uiController.SpawnFloatingCombatText(targetNPC, dmgReport,DisplayMode.Heal);
            GameObject anim = (GameObject)Instantiate(Resources.Load("Heal Animation"), targetNPC.transform.position, Quaternion.identity);
            anim.transform.SetParent(targetNPC.transform);
            targetNPC.GainHP(amountToHeal, HealSource.Heal);
            Object.Destroy(anim, 1);

        }

    }
    public void DoStunCycle(float secondsToBeStunned,NPC tobeAffected,NPC casterNPC)
    {
        if (tobeAffected.isStunned == false)
        {
            GameObject anim = (GameObject)Instantiate(Resources.Load("Stun Animation"), tobeAffected.transform.position, Quaternion.identity);
            anim.transform.SetParent(tobeAffected.transform);
            tobeAffected.GetComponent<NPC>().isStunned = true;
            StartCoroutine(WakeUpAfterSeconds(secondsToBeStunned, anim, tobeAffected));
        }
        else
        {
           
            if (casterNPC != null)
            {
                casterNPC.CONCENTRATION = casterNPC.MAXCONCENTRATION;
            }
        }

    }

    public IEnumerator WakeUpAfterSeconds(float secondsToBeStunned, GameObject anim,NPC toBeAffected)
    {
        yield return new WaitForSeconds(secondsToBeStunned);

            if (toBeAffected!= null)
            {
               toBeAffected.isStunned = false;
            }

            if (anim != null)
        {
            Object.Destroy(anim);
        }
   
    }

    IEnumerator ARMOR_AddBonusAfterSeconds(NPC npcToAffect, float seconds, float bonusToIncrease, GameObject animation)
    {
        yield return new WaitForSeconds(seconds);
        npcToAffect.ARMOR += bonusToIncrease;
        if (npcToAffect.PowerChangeParticleSystem != null)
        {
            npcToAffect.PowerChangeParticleSystem.PowerDownObject.SetActive(false);
        }
        Object.Destroy(animation);
    }


    IEnumerator AP_AddBonusAfterSeconds(NPC npcToAffect, float seconds, float bonusToIncrease, GameObject animation)
    {
        yield return new WaitForSeconds(seconds);
        npcToAffect.ATTACKPOWER += bonusToIncrease;
        if (npcToAffect.PowerChangeParticleSystem != null)
        {
            npcToAffect.PowerChangeParticleSystem.PowerDownObject.SetActive(false);
        }
        Object.Destroy(animation);
    }


    IEnumerator AP_SubstractBonusAfterSeconds(NPC npcToAffect, float seconds, float bonusToReduce,GameObject animation)
    {
        yield return new WaitForSeconds(seconds);
        npcToAffect.ATTACKPOWER -= bonusToReduce;
        if (npcToAffect.PowerChangeParticleSystem != null)
        {
            npcToAffect.PowerChangeParticleSystem.PowerUpObject.SetActive(false);
        }
        Object.Destroy(animation);
    }
    IEnumerator RETALIATION_SubstractBonusAfterSeconds(NPC npcToAffect, float seconds, float bonusToReduce,Object animation)
    {
        yield return new WaitForSeconds(seconds);
        npcToAffect.RETALIATION -= bonusToReduce;
        if (npcToAffect.PowerChangeParticleSystem != null)
        {
            npcToAffect.PowerChangeParticleSystem.PowerUpObject.SetActive(false);
        }
        Object.Destroy(animation);
    }
    IEnumerator ARMOR_SubstractBonusAfterSeconds(NPC npcToAffect, float seconds, float bonusToReduce,Object animation)
    {
        yield return new WaitForSeconds(seconds);
        npcToAffect.ARMOR -= bonusToReduce;
        if (npcToAffect.PowerChangeParticleSystem != null)
        {
            npcToAffect.PowerChangeParticleSystem.PowerUpObject.SetActive(false);
        }
        Object.Destroy(animation);
    }
    IEnumerator MAXHP_SubstractBonusAfterSeconds(NPC npcToAffect, float seconds, float bonusToReduce,Object animation)
    {
        yield return new WaitForSeconds(seconds);
        npcToAffect.MAXHP -= bonusToReduce;
        npcToAffect.HP -= bonusToReduce;
        if (npcToAffect.PowerChangeParticleSystem != null)
        {
            npcToAffect.PowerChangeParticleSystem.PowerUpObject.SetActive(false);
        }
        Object.Destroy(animation);
    }
}
