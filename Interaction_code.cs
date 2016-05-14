// On each item using OnMouseOver():

using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour
{
    void OnMouseOver()
    {
        print (gameObject.name);
    }
} 

// http://docs.unity3d.com/ScriptReference/MonoBehaviour.OnMouseOver.html


// On the item doing the pointing using RayCast():

using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            print (hit.collider.name);
        }
    }
}

// http://docs.unity3d.com/ScriptReference/Physics.Raycast.html
// http://docs.unity3d.com/ScriptReference/Camera.ScreenToWorldPoint.html
