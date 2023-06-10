using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.Mime.MediaTypeNames;

public class DevicePos : MonoBehaviour
{
    public Transform obj_transform;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("test");
    }

    IEnumerator postRequest()
    {
        string ip = $"192.168.18.4";
        string port = $"32775";
        string url = $"http://{ip}:{port}/log?x={obj_transform.position}&y={obj_transform.rotation}&z=1";
        WWWForm form = new WWWForm();

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(postRequest());
    }
}
