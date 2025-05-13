﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/*  This class controls the Main camera.
*/

public class MainCamera : MonoBehaviour
{
    public GameObject worldController;
    public BoardController boardController;
    public Camera cam;
    public GameObject target;
    private Rigidbody rb;
    private Vector3 nextPosition;

    public float panningSpeed;
    public float cameraSpeed;

    private bool moveFlag;
    public float scrollSpeed;
    public float lookAroundSpeed;


    private GameObject gameBoard;

    public string cameraMode;

    void Start()
    {
        worldController = GameObject.FindGameObjectWithTag("Controller");
        boardController = worldController.GetComponent<BoardController>();
        rb = GetComponent<Rigidbody>();
        moveFlag = false;

        if (target != null)
        {  // move to position behind target.
            gameBoard = target;
            transform.position = target.transform.position + new Vector3(0, 1, 0);
            nextPosition = target.transform.position + new Vector3(0, 1, 0);
        }

    }

    private void FixedUpdate()
    {

        Vector3 direction = rb.transform.position - cam.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction, cam.transform.up);
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, toRotation, panningSpeed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, nextPosition, cameraSpeed * Time.deltaTime);

        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {  // Rotate camera with mouse.
            this.transform.RotateAround(this.transform.position, Vector3.up, -Input.GetAxis("Mouse X") * lookAroundSpeed);
            this.transform.RotateAround(this.transform.position, this.transform.forward, Input.GetAxis("Mouse Y") * lookAroundSpeed);
        }

        if (!EventSystem.current.IsPointerOverGameObject(-1) && (((cam.transform.localPosition.x > 40) && (Input.mouseScrollDelta.y > 0)) || ((cam.transform.localPosition.x < 65) && (Input.mouseScrollDelta.y < 0))))
        {
            cam.transform.localPosition = cam.transform.localPosition + (scrollSpeed * new Vector3(-Input.mouseScrollDelta.y, 0, 0));
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            nextPosition = rb.transform.position - transform.forward * cameraSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            nextPosition = rb.transform.position + transform.forward * cameraSpeed;
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            nextPosition = rb.transform.position + (Quaternion.Euler(0, -90, 0) * transform.forward * cameraSpeed);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            nextPosition = rb.transform.position + (Quaternion.Euler(0, 90, 0) * transform.forward * cameraSpeed);
        }

        if (rb.transform.position.x > 3.5f)
        {
            rb.transform.position = new Vector3(3.5f, rb.transform.position.y, rb.transform.position.z);
        }

        if (rb.transform.position.x < -3.5f)
        {
            rb.transform.position = new Vector3(-3.5f, rb.transform.position.y, rb.transform.position.z);
        }

        if (rb.transform.position.z > 3.5f)
        {
            rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, 3.5f);
        }

        if (rb.transform.position.z < -3.5f)
        {
            rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, -3.5f);
        }

        if (Input.GetKey(KeyCode.C))
        {
            nextPosition = target.transform.position + new Vector3(0, 1, 0);
        }

        if (transform.rotation.eulerAngles.z > 80 && transform.rotation.eulerAngles.z < 180)
        {

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 80);
        }
        if (transform.rotation.eulerAngles.z < 5 || (transform.rotation.eulerAngles.z > 80 && transform.rotation.eulerAngles.z > 180))
        {

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 5);
        }

        if (boardController.gameStatus.Equals(GameStatus.Fight) && cameraMode.Equals("Follow") && boardController.selectedNPC != null)
        {
            target = boardController.selectedNPC;
            nextPosition = target.transform.position + new Vector3(0, 1, 0);
        }
        else if (cameraMode.Equals("Follow"))
        {
            target = gameBoard;
            nextPosition = target.transform.position + new Vector3(0, 1, 0);
        }
    }
}
