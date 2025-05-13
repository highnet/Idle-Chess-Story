using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRendererBillboard : MonoBehaviour
{
    public GameObject cameraTarget;

    void LateUpdate()
    {
        this.transform.eulerAngles = new Vector3(90, cameraTarget.transform.eulerAngles.y - 270, 0);
    }
}
