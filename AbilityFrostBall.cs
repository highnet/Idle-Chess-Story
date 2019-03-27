using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFrostBall : MonoBehaviour
{
    public NPC OwnerCaster;
    public NPC Target;
    public Vector3 destination;
    public float damageToDeal;

    // Update is called once per frame
    void Update()
    {
        //    Debug.Log(destination.ToString());
        transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 3f);
        if (Target != null)
        {
            destination = Target.transform.position;
        }

        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            Object.Destroy(this.transform.parent.gameObject);
            if (Target != null)
            {
                Target.TakePureDamage(damageToDeal);
            }

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
