using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public GameObject camera;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        Vector3 offset = new Vector3(moveHoriz, 0, moveVert);
        //transform.position = transform.position + offset;
        rb.AddForce(offset * speed);
        Quaternion newRotation = camera.transform.rotation;
        newRotation.x = 0;
        newRotation.z = 0;
        rb.transform.rotation = newRotation;
        //rb.transform.rotation = camera.transform.rotation;
        //rb.transform.Rotate(camera.transform.eulerAngles, Space.Self);
        Debug.Log("Player Rotation: " + rb.transform.rotation.ToString());
        Debug.Log("Camera Rotation: " + camera.transform.rotation.ToString());
    }
}