using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{

   public List<GameObject> floatingCombatTextPool;

    // Start is called before the first frame update
    void Start()
    {
        floatingCombatTextPool = new List<GameObject>();
    }



    public GameObject InstantiateFloatingCombatText( NPC targetDestination)
    {
        if (floatingCombatTextPool.Count == 0)
        {
           return (GameObject)Instantiate(Resources.Load("Floating Combat Text"), targetDestination.transform.position, Quaternion.identity);
        } else
        {
            GameObject gameObject = floatingCombatTextPool[0];
            floatingCombatTextPool.Remove(gameObject);
            gameObject.transform.position = targetDestination.transform.position;
            gameObject.GetComponentInChildren<Animator>().Play("Float");
            return gameObject; 
        }

    }

    public void DestroyFloatingCombatTextAfterSeconds(float delayInSeconds, GameObject gameObject)
    {
        StartCoroutine(DestroyFloatingCombatTextCoRoutine(delayInSeconds, gameObject));
    }


    IEnumerator DestroyFloatingCombatTextCoRoutine(float delayInSeconds, GameObject gameObject)
    {
        yield return new WaitForSeconds(delayInSeconds);
        gameObject.transform.position = Vector3.up * 1000;
        floatingCombatTextPool.Add(gameObject);
        
    }

}
