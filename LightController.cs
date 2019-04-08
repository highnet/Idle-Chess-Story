using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public float dayAndNightCycleSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(dayAndNightCycleSpeed * Vector3.up * Time.deltaTime, Space.Self);
    }
}
