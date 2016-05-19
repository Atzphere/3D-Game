using UnityEngine;
using System.Collections;
public class DoorRotate : MonoBehaviour {


    public float finalDegree;

    private float currentDegree;
    private float increment;
    private Vector3 rotationAxis;
    private Vector3 rotationPoint;
    private static float DOOR_HALFWIDTH = 1.25f;

    // Use this for initialization
    void Start () {
        increment = finalDegree * Time.deltaTime;
        rotationAxis = Vector3.down;
        currentDegree = 0;
        rotationPoint = transform.position;
        rotationPoint.z = transform.position.z - DOOR_HALFWIDTH;
    }
	
	// Update is called once per frame
	void Update () {
        if (currentDegree <= finalDegree)
        {
            transform.RotateAround(rotationPoint, rotationAxis, increment);
            currentDegree = currentDegree + increment;

        }



    }

    public void ChangeDirection ()
    {
    	if (rotationAxis == Vector3.down)
    	{
    		rotationAxis = Vector3.up;
    	}
    	else
    	{
    		rotationAxis = Vector3.down;
    	}
    	currentDegree = finalDegree - currentDegree;
    }
}
