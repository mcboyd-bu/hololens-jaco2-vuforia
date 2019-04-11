using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendProbability : MonoBehaviour {

    public static SendProbability instance;

    public string endPoint;

    private void Awake()
    {
        // allows this instance to behave like a singleton
        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Used to actually send data from the arm to the API endpoint on the PC
    public IEnumerator SendSuccess(string _msg)
    {
        WWWForm form = new WWWForm();
        form.AddField("data", _msg);

        using (UnityWebRequest www = UnityWebRequest.Post(endPoint, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }

            yield return null;
        }
    }

    // Used to actually get the probability data for the seen object from the API endpoint on the PC
    public IEnumerator GetProbability(string _obj, System.Action<int> callback = null)
    {
        WWWForm form = new WWWForm();
        //form.AddField("data", _msg);
        string endPointObj = endPoint + "/" + _obj;

        using (UnityWebRequest www = UnityWebRequest.Get(endPointObj))
        {
            yield return www.SendWebRequest();

            string[] pages = endPointObj.Split('/');
            int page = pages.Length - 1;

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //StartCoroutine(SendMsgs.instance.SendMsg("Form GET request complete!"));
                //string returnText = pages[page] + ":\nReceived: " + www.downloadHandler.text;
                //StartCoroutine(SendMsgs.instance.SendMsg(returnText));
                int prob = Convert.ToInt32(www.downloadHandler.text);
                StartCoroutine(SendMsgs.instance.SendMsg("returned prob in sp.cs: " + prob));
                callback(prob);
            }

            yield return null;
        }
    }
}
