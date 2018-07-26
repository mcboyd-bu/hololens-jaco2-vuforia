using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SendData : MonoBehaviour {

    public static SendData instance;

    public string endPoint;

    [System.Serializable]
    public class V3
    {
        public float X;
        public float Y;
        public float Z;
    }

    [System.Serializable]
    public class ApiResp
    {
        public string apiResp;
    }

    private void Awake()
    {
        // allows this instance to behave like a singleton
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        // StartCoroutine(SendDataToAPI());
        
        //Below for testing
        //StartCoroutine(Post());
        //StartCoroutine(GetText());
    }

    // Basically just used for testing to ensure the API is working and the REST call can be completed successfully
    IEnumerator GetText()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(endPoint))
        {
            Debug.Log("in get text now");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(String.Format("error results: {0}", www.error));
            }
            else
            {
                // Show results as text
                Debug.Log(String.Format("get results: {0}", www.downloadHandler.text));

                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Post()
    {
        var request = new UnityWebRequest(endPoint, "POST");
        V3 v = new V3 { X = 0.2f, Y = 0.2f, Z = 0.1f };
        // Debug.Log(String.Format("endpoint: {0}", endPoint));
        string postData = JsonUtility.ToJson(v);
        // Debug.Log(String.Format("json: {0}", postData));
        byte[] bodyRaw = Encoding.UTF8.GetBytes(postData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.uploadHandler.contentType = "application/json";
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.chunkedTransfer = false;

        yield return request.SendWebRequest();

        //Debug.Log("Status Code: " + request.responseCode);
    }

    public IEnumerator SendDataToAPI(float _x, float _y, float _z)
    {
        WWWForm webForm = new WWWForm();
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(endPoint, webForm))
        {
            V3 v = new V3 { X = _x, Y = _y, Z = _z };
            //Debug.Log(String.Format("v3: {0}", v.ToString()));
            string postData = JsonUtility.ToJson(v);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(postData);
            unityWebRequest.SetRequestHeader("Content-Type", "application/json");
            //unityWebRequest.SetRequestHeader(ocpApimSubscriptionKeyHeader, authorizationKey);

            // the download handler will help receiving the response from API endpoint
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();

            // the upload handler will help uploading the byte array with the request
            unityWebRequest.uploadHandler = new UploadHandlerRaw(bytes)
            {
                contentType = "application/json"
            };
            
            yield return unityWebRequest.SendWebRequest();
           
            long responseCode = unityWebRequest.responseCode;
            if (responseCode == 200)
            {
                // Debug.Log(String.Format("sdta, responsecode (should be 200): {0}", responseCode.ToString()));
                // update indicated sphere color or something
            }
            else
            {
                // Debug.Log("not 200....");
            }

            //try
            //{
            //    string jsonResponse = null;
            //    jsonResponse = unityWebRequest.downloadHandler.text;
            //    ApiResp apiResp = new ApiResp();
            //    apiResp = JsonUtility.FromJson<ApiResp>(jsonResponse);

            //    if (apiResp.apiResp == null)
            //    {
            //        Debug.Log("sdta 10: apiResp.apiResp is null");
            //    }
            //    else
            //    {
            //        //ResultsLabel.instance.SetTagsToLastLabel(tagsDictionary);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    Debug.Log("Json exception.Message: " + exception.Message);
            //}

            yield return null;
        }
    }

    
}
