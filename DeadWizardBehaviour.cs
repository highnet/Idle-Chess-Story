using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadWizardBehaviour : NPC
{

  

    /*
    IEnumerator Live()
    {
        for (; ; )
        {

            if (boardController.gameStatus.Equals("Fight") && occupyingTile.GetComponent<TileBehaviour>().i != 8)
            {

                float distance = 0;
                float minDistance = 9000f;

                if (!isEnemy)
                {
                    foreach (NPC npc in npcController.enemyList)
                    {
                        distance = Vector3.Distance(this.transform.position, npc.transform.position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            target = npc;
                        }
                    }
                }
                else
                {
                    foreach (NPC npc in npcController.allyList)
                    {
                        distance = Vector3.Distance(this.transform.position, npc.transform.position);
                        if (distance < minDistance && npc.occupyingTile.GetComponent<TileBehaviour>().i != 8)
                        {
                            minDistance = distance;
                            target = npc;
                        }
                    }
                }

                if (target != null)
                {


                    int deltaI = 0;
                    int deltaJ = 0;

                    deltaI = Random.Range(-2, 2);
                    deltaJ = Random.Range(-2, 2);



                    moveToEmptyTile(target.occupyingTile.GetComponent<TileBehaviour>().i + deltaI, target.occupyingTile.GetComponent<TileBehaviour>().j + deltaJ);
                    transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

                    distance = Vector3.Distance(this.transform.position, target.transform.position);

                    if (distance < 4)
                    {
                        if (animator != null)
                        {
                            animator.Play("Attack");
                        }
                        target.RecieveHitHP(ATTACKPOWER, this.GetComponent<NPC>(),DamageSource.MagicDamage);
                    }
                }
            }
            yield return new WaitForSeconds(3f);
        }
    }

    */
}
