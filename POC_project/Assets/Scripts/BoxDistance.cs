using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using Debug = UnityEngine.Debug;

public class BoxDistance : MonoBehaviour
{
    private float lastRefresh;
    private float currentDistance;
    private float lastSentDistance = 0.0f;
    private GameObject other;

    public float RequestRate = 2.0f;
    public int DoseRate = 7;
    public float MinDistance = 0.5f;
    public float MaxDistance = 4.5f;

    public GameObject Hand;
    public GameObject TextObject;
    private TextMeshProUGUI textComponent;

    // Start is called before the first frame update
    void Start()
    {
        other = GameObject.Find("Camera");
        lastRefresh = RequestRate;

        if(TextObject != null)
        {
            textComponent = TextObject.GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = Vector3.Distance(transform.position, other.transform.position);
        lastRefresh -= Time.deltaTime;
        if (lastRefresh <= 0.0f)
        {
            lastRefresh = RequestRate;

            if (currentDistance <= MaxDistance && currentDistance > MinDistance)// && (currentDistance - lastSentDistance) > MinDistance)
            {
                string URL = "http://api.hyperionar.stroetenga.nl/Calculations/DoseRateAtNewDistance?DoseRate=" + DoseRate.ToString() + "&CurrentDistance=1&NewDistance=" + currentDistance.ToString().Replace(",", ".");
                lastSentDistance = currentDistance;
                StartCoroutine(HandleRequest(URL));
            }
            else
            {
                if (textComponent != null)
                {
                    textComponent.text = "0 μSv/h";
                }
            }
        }
        if (TextObject != null)
        {
            if (Hand != null)
            {
                TextObject.transform.position = Hand.transform.position;
            }

            TextObject.transform.LookAt(other.transform);
        }
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
            currentDistance = Vector3.Distance(other.transform.position, transform.position);
            Debug.Log($"distance: {currentDistance}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited the box");
        }
    }

    IEnumerator HandleRequest(string URL)
    {
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
                    if (textComponent != null)
                    {
                        textComponent.text = webRequest.downloadHandler.text.Substring(29, 4) + " μSv/h";
                    }
                    break;
            }
        }
    }
}
