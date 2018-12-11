using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SendMsgs : MonoBehaviour {

    public static SendMsgs instance;

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
    public IEnumerator SendMsg(string _msg)
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

}
