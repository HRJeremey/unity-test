using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class HTTPRequest : MonoBehaviour
{
    public TMP_Text aLabel;
    public int DoseRate;
    public int CurrentDistance;
    public int NewDistance;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HandleRequest("http://api.hyperionar.stroetenga.nl/Calculations/DoseRateAtNewDistance?DoseRate=" + DoseRate.ToString() + "&CurrentDistance=" + CurrentDistance.ToString() + "&NewDistance=" + NewDistance.ToString()));
    }

    IEnumerator HandleRequest(string URL)
    {
        Debug.Log("Requesting: " + URL);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(URL))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    aLabel.text = webRequest.downloadHandler.text;
                    break;
            }
        }
    }
}
