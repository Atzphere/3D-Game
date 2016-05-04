using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public GameObject camera;
    public float lookSpeed;

    private Rigidbody rb;
    private float spin;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //spin = transform.localRotation;
    }

    void LateUpdate()
    {
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        Vector3 offset = new Vector3(moveHoriz, 0, moveVert);
        //transform.position = transform.position + offset;
        rb.AddForce(offset * speed);
        //Quaternion newRotation = camera.transform.rotation;
        //newRotation.x = 0;
       //newRotation.z = 0;
        float step = lookSpeed * Time.deltaTime;
        //rb.transform.rotation = Quaternion.RotateTowards(transform.rotation, camera.transform.rotation, step);
        camera.transform.rotation = rb.transform.rotation;
        //rb.transform.Rotate(camera.transform.eulerAngles, Space.Self);
        Debug.Log("Player Rotation: " + rb.transform.rotation.ToString());
        Debug.Log("Camera Rotation: " + camera.transform.rotation.ToString());
    }
}