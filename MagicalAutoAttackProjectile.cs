using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalAutoAttackProjectile : MonoBehaviour
{
    public NPC OwnerCaster;
    public NPC Target;
    public Vector3 destination;
    public float damageToDeal;
    private bool Local_OwnerCaster_IsEnemy;
   

    private void Start()
    {
        Local_OwnerCaster_IsEnemy = OwnerCaster.isEnemy; // store a copy of wether the caster is enemy or not.
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 5f); // move towards the target destination
        transform.LookAt(destination);
        if (Target != null)
        {
            destination = Target.transform.position;  // refresh the target destination, in case the target npc moves
        }

        if (Vector3.Distance(transform.position, destination) < 0.1f)  // if reached the target
        {
           
            if (Target != null)
            {
                UiController uic = GameObject.Find("World Controller").GetComponent<UiController>(); // fetch the ui controller once
                uic.SpawnFloatingCombatText(Target, damageToDeal,DamageSource.MagicalDamage_AutoAttack,Local_OwnerCaster_IsEnemy,HealSource.NOTHING); // spawn floating combat text
                Target.TakePureDamage(damageToDeal); // deal damage

            }
            Object.Destroy(this.transform.parent.gameObject); // destroy this projectile

        }

    }
    public void SetDamageToDeal(float dmgToDeal)
    {
        this.damageToDeal = dmgToDeal;
    }


    public void SetCaster(NPC casterNPC)
    {
        this.OwnerCaster = casterNPC;
    }
    public void SetTarget(NPC targetNPC)
    {
        this.Target = targetNPC;
        this.destination = targetNPC.transform.position;
    }
}
