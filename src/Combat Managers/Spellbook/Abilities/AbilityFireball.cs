using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFireball : MonoBehaviour
{

    public Vector3 destination;
    public DamageReport dmgReport;
    public AudioClip abilitySound;

    private void Start()
    {
        AudioSource.PlayClipAtPoint(abilitySound, this.transform.position);
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 5f); // move towards the target destination
        transform.LookAt(destination);
        if (dmgReport.damageReceiverNPC != null)
        {
            destination = dmgReport.damageReceiverNPC.transform.position;  // refresh the target destination, in case the target npc moves
        }

        if (Vector3.Distance(transform.position, destination) < 0.1f)  // if reached the target
        {

            if (dmgReport.damageReceiverNPC != null)
            {
                UiController uic = GameObject.Find("World Controller").GetComponent<UiController>(); // fetch the ui controller once
                uic.SpawnFloatingCombatText(dmgReport.damageReceiverNPC, dmgReport, DisplayMode.AbilityDamage); // spawn floating combat text
                dmgReport.damageReceiverNPC.TakePureDamage(dmgReport); // deal damage
            }
            Object.Destroy(this.transform.parent.gameObject); // destroy this projectile

        }
    }
}
