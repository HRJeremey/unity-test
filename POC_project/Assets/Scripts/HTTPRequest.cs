using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HTTPRequest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HandleRequest("https://www.google.nl"));
    }

    IEnumerator HandleRequest(string URL)
    {
        Debug.Log("Requesting: " + URL);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(URL))
        {
            yield return webRequest.SendWebRequest();

            Debug.Log(webRequest.result);
        }
    }
}
