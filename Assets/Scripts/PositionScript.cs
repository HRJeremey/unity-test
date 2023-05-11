using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionScript : MonoBehaviour
{
    public GameObject myCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("X position: " + myCamera.transform.position.x);
        Debug.Log("Y position: " + myCamera.transform.position.y);
        Debug.Log("Z position: " + myCamera.transform.position.z);
    }
}
