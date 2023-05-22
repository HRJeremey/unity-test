using System.Runtime.InteropServices;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceText : MonoBehaviour
{
    public Transform target;
    public GameObject other;
    private TextMeshProUGUI textComponent;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {

        float distance = Vector3.Distance(target.position, other.transform.position);
        textComponent.text = string.Format("Distance: {0:0.00} m", distance);
        if(distance > 15) textComponent.color = Color.green;
        else if(distance > 10 || distance > 5) textComponent.color = Color.yellow;
        if(distance < 5) textComponent.color = Color.red;

        // face the text to the target
        transform.LookAt(other.transform);
    }

}
