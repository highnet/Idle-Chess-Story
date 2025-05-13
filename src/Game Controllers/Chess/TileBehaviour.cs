using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour


{
    public GameObject worldControl;
    public BoardController boardControl;
    public int i;
    public int j;
    public GameObject occupyingUnit;

    // Start is called before the first frame update
    void Start()
    {
        findController();
    }

    public void findController()
    {
        worldControl = GameObject.FindGameObjectWithTag("Controller");
        boardControl = worldControl.GetComponent<BoardController>();
    }





}
