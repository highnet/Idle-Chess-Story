using UnityEngine;
using System.Collections;

/*  This class controls the Main camera.
*/

public class MainCamera : MonoBehaviour
{
    public GameObject worldController;
    public BoardController boardController;
    public Camera cam;
    public GameObject target;
    public float cameraSpeed;
    private Rigidbody rb;

    private GameObject gameBoard;

    public string cameraMode;

    void Start()
    {
        worldController = GameObject.FindGameObjectWithTag("Controller");
        boardController = worldController.GetComponent<BoardController>();
        rb = GetComponent<Rigidbody>();
        cameraMode = "Normal";

        if (target != null)
        {  // move to position behind target.
            gameBoard = target;
            transform.position = target.transform.position + new Vector3(0, 1, 0);
        }

    }

    void LateUpdate()
    {

        cam.transform.LookAt(rb.transform); // look at target.

        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {  // Rotate camera with mouse.
            this.transform.RotateAround(this.transform.position, Vector3.up, -Input.GetAxis("Mouse X") / 2);
            this.transform.RotateAround(this.transform.position, this.transform.forward, Input.GetAxis("Mouse Y") / 2);
        }

        if (((cam.transform.localPosition.x > 10) && (Input.mouseScrollDelta.y > 0)) || ((cam.transform.localPosition.x < 100) && (Input.mouseScrollDelta.y < 0))) {
            cam.transform.localPosition = cam.transform.localPosition + (0.25f * new Vector3(-Input.mouseScrollDelta.y, 0, 0));
        }

        if (Input.GetKey(KeyCode.LeftArrow) && rb.transform.position.z > -3.5F)
        {
            rb.transform.position = rb.transform.position + Vector3.back / 10;
        }

        if (Input.GetKey(KeyCode.RightArrow) && rb.transform.position.z < 3.5F)
        {
            rb.transform.position = rb.transform.position + Vector3.forward / 10;
        }

        if (Input.GetKey(KeyCode.UpArrow) && rb.transform.position.x > -3.5F)
        {
            rb.transform.position = rb.transform.position + Vector3.left / 10;
        }

        if (Input.GetKey(KeyCode.DownArrow) && rb.transform.position.x < 3.5F)
        {
            rb.transform.position = rb.transform.position + Vector3.right / 10;
        }

        if (Input.GetKey(KeyCode.C))
        {
            rb.transform.position =  target.transform.position + new Vector3(0, 1, 0);
        }


        if (transform.rotation.eulerAngles.z > 80 && transform.rotation.eulerAngles.z < 180)
        {

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 80);
        }
        if (transform.rotation.eulerAngles.z < 5 || (transform.rotation.eulerAngles.z > 80 && transform.rotation.eulerAngles.z > 180))
        {

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 5);
        }

    
        if (boardController.gameStatus.Equals(GameStatus.Fight) && cameraMode.Equals("Follow") && boardController.selectedObject != null)
        {
            target = boardController.selectedObject;
            transform.position = target.transform.position + new Vector3(0, 1, 0);
        }
        else if (cameraMode.Equals("Follow"))
        {
            target = gameBoard;
            transform.position = target.transform.position + new Vector3(0, 1, 0);
        }
    }
}
