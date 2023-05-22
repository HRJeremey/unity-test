using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BoxDistance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the box");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            float distance = Vector3.Distance(other.transform.position, transform.position);
            Debug.Log($"distance: {distance}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited the box");
        }
    }
}
