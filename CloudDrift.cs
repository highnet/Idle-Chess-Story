using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDrift : MonoBehaviour
{

    void FixedUpdate ()
    {
        transform.position += transform.forward * Time.deltaTime * 0.2f;
    }
}
