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
    private Rigidbody rb;
    private Vector3 nextPosition;

    public float lookSpeed;
    public float cameraSpeed;

    private bool moveFlag;


    private GameObject gameBoard;

    public string cameraMode;

    void Start()
    {
        worldController = GameObject.FindGameObjectWithTag("Controller");
        boardController = worldController.GetComponent<BoardController>();
        rb = GetComponent<Rigidbody>();
        cameraMode = "Normal";
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
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, toRotation, lookSpeed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, nextPosition, cameraSpeed * Time.deltaTime);




        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {  // Rotate camera with mouse.
            this.transform.RotateAround(this.transform.position, Vector3.up, -Input.GetAxis("Mouse X") / 2);
            this.transform.RotateAround(this.transform.position, this.transform.forward, Input.GetAxis("Mouse Y") / 2);
        }

        if (((cam.transform.localPosition.x > 10) && (Input.mouseScrollDelta.y > 0)) || ((cam.transform.localPosition.x < 100) && (Input.mouseScrollDelta.y < 0)))
        {
            cam.transform.localPosition = cam.transform.localPosition + (0.4f * new Vector3(-Input.mouseScrollDelta.y, 0, 0));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            nextPosition = rb.transform.position - transform.forward * cameraSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            nextPosition = rb.transform.position + transform.forward * cameraSpeed;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            nextPosition = rb.transform.position + (Quaternion.Euler(0, -90, 0) * transform.forward * cameraSpeed);
        }


        if (Input.GetKey(KeyCode.DownArrow))
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




        if (boardController.gameStatus.Equals(GameStatus.Fight) && cameraMode.Equals("Follow") && boardController.selectedObject != null)
        {
            target = boardController.selectedObject;
            nextPosition = target.transform.position + new Vector3(0, 1, 0);
        }
        else if (cameraMode.Equals("Follow"))
        {
            target = gameBoard;
            nextPosition = target.transform.position + new Vector3(0, 1, 0);
        }
    }
}
