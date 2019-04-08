using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxRotation : MonoBehaviour
{
    private void FixedUpdate()
    {
        this.transform.Rotate(new Vector3(0f, 0f, 0.5f * Time.deltaTime), Space.Self);
    }
}
