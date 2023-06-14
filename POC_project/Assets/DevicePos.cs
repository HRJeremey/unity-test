using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.Mime.MediaTypeNames;

public class DevicePos : MonoBehaviour
{
    public Transform obj_transform;
    public string ip = $"192.168.18.4";
    public string port = $"32773";
    private string url => $"http://{ip}:{port}/log?x={obj_transform.position}&y={obj_transform.rotation}&z=1";
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("test");
    }

    IEnumerator UnityGetRequest()
    {
        //WWWForm form = new WWWForm();

        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();
    }

    IEnumerator NetGetRequest()
    {
        HttpClient httpClient = new HttpClient();
        yield return httpClient.GetAsync(url);
    }

    // Update is called once per frame
    // use for translate/rotation/animations/input
    //void Update()
    //{

    //}

    // use for Forces/input
    //void FixedUpdate()
    //{

    //}

    // use for camera translation/transform manipulation post animation
    void LateUpdate()
    {
        StartCoroutine(UnityGetRequest());
        //StartCoroutine(NetGetRequest());
    }
}
