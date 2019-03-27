using System.Collections;
using UnityEngine;

public class SpellbookController : MonoBehaviour
{
    public void CastSpell(NPC casterNPC, NPC targetNPC, Ability abilityToCast, DamageSource damageTypeDealt, float CasterSpellPower,float CasterAttackPower)
    {

        if (abilityToCast == Ability.NOTHING || casterNPC == null || targetNPC == null)
        {
            return;
        } else
        {
           casterNPC.CONCENTRATION = 0;
        }

        if (abilityToCast == Ability.Fireball)
        {

            GameObject go = (GameObject)Instantiate(Resources.Load("Fireball"), this.transform.position, Quaternion.identity);
            go.GetComponentInChildren<AbilityFireball>().SetCaster(casterNPC);
            go.GetComponentInChildren<AbilityFireball>().SetTarget(targetNPC);
            go.GetComponentInChildren<AbilityFireball>().SetDamageToDeal(targetNPC.CalculateDamageTaken(2 * CasterSpellPower, casterNPC, damageTypeDealt));

        }

        else if (abilityToCast == Ability.AP_UP_Self)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("AP_PowerUp"), this.transform.position, Quaternion.identity);
            go.transform.SetParent(casterNPC.transform);
            float boostValue = 100;
            float boostDuration = 6;
            casterNPC.ATTACKPOWER += 100;
            StartCoroutine(AP_SubstractBonusAfterSeconds(casterNPC, boostDuration, boostValue, go));

        }

        else if (abilityToCast == Ability.Armor_UP_Self)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("ARMOR_PowerUp"), this.transform.position, Quaternion.identity);
            go.transform.SetParent(casterNPC.transform);
            float boostValue = 35;
            float boostDuration = 6;
            casterNPC.ARMOR += 35;
            StartCoroutine(ARMOR_SubstractBonusAfterSeconds(casterNPC, boostDuration, boostValue, go));
        }
        else if (abilityToCast == Ability.MaxHP_Up_Self)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("MAXHP_PowerUp"), this.transform.position, Quaternion.identity);
            go.transform.SetParent(casterNPC.transform);
            float boostValue = 100;
            float boostDuration = 6;
            casterNPC.MAXHP += 100;
            casterNPC.HP += 100;
            StartCoroutine(MAXHP_SubstractBonusAfterSeconds(casterNPC, boostDuration, boostValue, go));
        }
        else if (abilityToCast == Ability.Retaliation_UP_Self)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("RETALIATION_PowerUp"), this.transform.position, Quaternion.identity);
            go.transform.SetParent(casterNPC.transform);
            float boostValue = 35;
            float boostDuration = 6;
            casterNPC.RETALIATION += boostValue;
            StartCoroutine(RETALIATION_SubstractBonusAfterSeconds(casterNPC, boostDuration, boostValue, go));
        }
        else if (abilityToCast == Ability.HeroicStrike)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("HeroicStrike Animation"), this.transform.position, Quaternion.identity);
            go.transform.LookAt(targetNPC.transform);
            go.transform.SetParent(casterNPC.transform);
            targetNPC.TakePureDamage(targetNPC.CalculateDamageTaken(3 * CasterAttackPower, casterNPC, damageTypeDealt));
            GameObject.Destroy(go, 4);

        }
        else if (abilityToCast == Ability.Stab)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("Stab Animation"), this.transform.position, Quaternion.identity);
            go.transform.LookAt(targetNPC.transform);
            go.transform.SetParent(casterNPC.transform);
            targetNPC.TakePureDamage(targetNPC.CalculateDamageTaken(1.5f * CasterAttackPower, casterNPC, damageTypeDealt));
            GameObject.Destroy(go, 3);

        }
        else if (abilityToCast == Ability.AP_DOWN_OTHER)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("AP_PowerDown"), targetNPC.transform.position, Quaternion.identity);
            go.transform.LookAt(targetNPC.transform);
            go.transform.SetParent(targetNPC.transform);
            float boostValue = 35;
            float boostDuration = 6;
            targetNPC.ReduceAttackPower(boostValue);
            StartCoroutine(AP_AddBonusAfterSeconds(targetNPC, boostDuration, boostValue, go));

        }


        else if (abilityToCast == Ability.ARMOR_DOWN_OTHER)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("ARMOR_PowerDown"), targetNPC.transform.position, Quaternion.identity);
            go.transform.SetParent(targetNPC.transform);
            float boostValue = 35;
            float boostDuration = 6;
            targetNPC.ReduceArmor(boostValue);
            StartCoroutine(ARMOR_AddBonusAfterSeconds(targetNPC, boostDuration, boostValue, go));
        }
        else if (abilityToCast == Ability.FrostBall)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("FrostBall"), this.transform.position, Quaternion.identity);
            go.GetComponentInChildren<AbilityFrostBall>().SetCaster(casterNPC);
            go.GetComponentInChildren<AbilityFrostBall>().SetTarget(targetNPC);
            go.GetComponentInChildren<AbilityFrostBall>().SetDamageToDeal(targetNPC.CalculateDamageTaken(2 * CasterSpellPower, casterNPC, damageTypeDealt));
        } else if (abilityToCast == Ability.Stun)
        {
            float stunDuration = 3;
            GameObject anim = (GameObject)Instantiate(Resources.Load("Stun Animation"), targetNPC.transform.position, Quaternion.identity);
            anim.transform.SetParent(targetNPC.transform);
            DoStunCycle(stunDuration,targetNPC,anim);
        }
        else if (abilityToCast == Ability.HealFriend)
        {
            float amountToHeal = CasterSpellPower;

            NPC[] casters_AllyList = null;

            if (casterNPC.isEnemy == false)
            {
                 casters_AllyList = GameObject.Find("World Controller").GetComponent<NpcController>().deployedAllyList.ToArray();
            }
            else
            {
                casters_AllyList = GameObject.Find("World Controller").GetComponent<NpcController>().enemyList.ToArray();
            }
            targetNPC = casters_AllyList[0];
            float minimumHP = 9999999999f;

                foreach(NPC nPC in casters_AllyList)
                {
                   if (nPC.HP <= minimumHP)
                    {
                        minimumHP = nPC.HP;
                        targetNPC = nPC;
                    }
                }
                targetNPC.GainHP(amountToHeal, HealSource.Heal);
        }

    }
    public void DoStunCycle(float secondsToBeStunned,NPC tobeAffected,GameObject anim)
    {
        if (tobeAffected.liveRoutine != null)
        {
            tobeAffected.StopCoroutine(tobeAffected.liveRoutine);
            tobeAffected.liveRoutine = null;
        }

        StartCoroutine(WakeUpAfterSeconds(secondsToBeStunned, anim, tobeAffected));
    }

    public IEnumerator WakeUpAfterSeconds(float secondsToBeStunned, GameObject anim,NPC toBeAffected)
    {
        yield return new WaitForSeconds(secondsToBeStunned);

            if (toBeAffected!= null && toBeAffected.liveRoutine == null)
            {
                toBeAffected.StartLiveRoutine(); 
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
        Object.Destroy(animation);
    }


    IEnumerator AP_AddBonusAfterSeconds(NPC npcToAffect, float seconds, float bonusToIncrease, GameObject animation)
    {
        yield return new WaitForSeconds(seconds);
        npcToAffect.ATTACKPOWER += bonusToIncrease;
        Object.Destroy(animation);
    }


    IEnumerator AP_SubstractBonusAfterSeconds(NPC npcToAffect, float seconds, float bonusToReduce,GameObject animation)
    {
        yield return new WaitForSeconds(seconds);
        npcToAffect.ATTACKPOWER -= bonusToReduce;
        Object.Destroy(animation);
    }
    IEnumerator RETALIATION_SubstractBonusAfterSeconds(NPC npcToAffect, float seconds, float bonusToReduce,Object animation)
    {
        yield return new WaitForSeconds(seconds);
        npcToAffect.RETALIATION -= bonusToReduce;
        Object.Destroy(animation);
    }
    IEnumerator ARMOR_SubstractBonusAfterSeconds(NPC npcToAffect, float seconds, float bonusToReduce,Object animation)
    {
        yield return new WaitForSeconds(seconds);
        npcToAffect.ARMOR -= bonusToReduce;
        Object.Destroy(animation);
    }
    IEnumerator MAXHP_SubstractBonusAfterSeconds(NPC npcToAffect, float seconds, float bonusToReduce,Object animation)
    {
        yield return new WaitForSeconds(seconds);
        npcToAffect.MAXHP -= bonusToReduce;
        npcToAffect.HP -= bonusToReduce;
        Object.Destroy(animation);
    }
}
