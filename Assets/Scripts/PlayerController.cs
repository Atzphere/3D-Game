using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{

    public float speed;
    public GameObject camera;
    public GameObject frontdoor;
    public float lookSpeed;

    private static string INTERACTABLE_TAG = "Interactable";
    private Rigidbody rb;
    private float spin;

    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //spin = transform.localRotation;
    }
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject rayobject = hit.collider.gameObject;
            print(hit.collider.name);
            if (rayobject.CompareTag(INTERACTABLE_TAG))
            {

                print("Is Interactable");
                switch (rayobject.name)
                {
                    case "test1":
                        {
                            print("Is Test1");
                            break;
                        }

                    case "test2":
                        {
                            print("Is Test2");
                            break;
                        }
                    case "FrontDoor":
                        {

                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                DoorRotate rotator = (DoorRotate) rayobject.GetComponent<DoorRotate>();
                                if (rotator == null)
                                {
                                    rotator = (DoorRotate) rayobject.AddComponent<DoorRotate>();
                                    rotator.finalDegree = 90;
                                }

                                else 
                                {
                                    rotator.ChangeDirection();

                                }

                            }
                        break;
                        }

                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    print("e");
                }
            }
        }
    }
    void LateUpdate()
    {
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        Vector3 offset = new Vector3(moveHoriz, 0, moveVert);
        rb.AddForce(offset * speed);
        float step = lookSpeed * Time.deltaTime;
        camera.transform.rotation = rb.transform.rotation;
        //Debug.Log("Player Rotation: " + rb.transform.rotation.ToString());
        //Debug.Log("Camera Rotation: " + camera.transform.rotation.ToString());
    }

    


}

// http://docs.unity3d.com/ScriptReference/Physics.Raycast.html
// http://docs.unity3d.com/ScriptReference/Camera.ScreenToWorldPoint.html
