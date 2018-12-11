using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuMarkHandler : MonoBehaviour {

    private VuMarkManager vuMarkManager;
    private int pCount = 0;
    private GameObject _HelpHolder;

    // Use this for initialization
    private void Awake() {
        vuMarkManager = TrackerManager.Instance.GetStateManager().GetVuMarkManager();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var bhvr in vuMarkManager.GetActiveBehaviours())
        {
            // When VuMark is seen for the first time, the below gets triggered
            if (pCount == 0)
            {
                // First, update the text displayed to the headset wearer
                TextChanges("vumark-found");

                // Then increment the counter so none of this stuff happens again
                pCount += 1;

                // Then, either create the virtual spheres (for testing / demo)
                TargetSphere.instance.CreateSphere();

                // Or, enable the Vuforia image database
                //SwapDatasets();
                
            }

        }
    }

    public void TextChanges(string changeType)
    {
        GameObject _LookingArm = GameObject.Find("LookingArm");
        GameObject _HHPlane = GameObject.Find("HHPlane");
        _HelpHolder = GameObject.Find("HelpHolder");

        if (changeType == "vumark-found")
        {
            StartCoroutine(SendMsgs.instance.SendMsg("vumark found"));
            //GameObject _LookingArm = GameObject.Find("LookingArm");
            //GameObject _HHPlane = GameObject.Find("HHPlane");
            //_HelpHolder = GameObject.Find("HelpHolder");
            Material m_Material = _HHPlane.GetComponent<Renderer>().material;
            m_Material.color = new Color(0.0118f, 0.788f, 0.114f, 1);
            TextMesh _tm = _LookingArm.GetComponent<TextMesh>();
            _tm.text = "Arm Found!";
            //Invoke("CloseText", 5);
        } else if (changeType == "imagedb-loaded")
        {
            Material m_Material = _HHPlane.GetComponent<Renderer>().material;
            m_Material.color = new Color(0.0118f, 0.788f, 0.114f, 1);
            TextMesh _tm = _LookingArm.GetComponent<TextMesh>();
            _tm.text = "Image DB loaded!";
            //Invoke("CloseText", 5);
            //_HelpHolder.SetActive(true);
        }
        
    }

    void CloseText()
    {
        _HelpHolder.SetActive(false);
    }

    public void SwapDatasets()
    {
        string vuMarkDataset = "HoloJacoVuMarkDB";
        string imageDataset = "HoloJacoDeviceImages";
        TrackerManager trackerManager = (TrackerManager)TrackerManager.Instance;
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        //Stop the tracker.
        objectTracker.Stop();

        //Create a new dataset object.
        DataSet dataset = objectTracker.CreateDataSet();
        //Load and deactivate the dataset if it exists.
        if (DataSet.Exists(vuMarkDataset))
        {
            dataset.Load(vuMarkDataset);
            objectTracker.DeactivateDataSet(dataset);
        }

        if (DataSet.Exists(imageDataset))
        {
            dataset.Load(imageDataset);
            objectTracker.ActivateDataSet(dataset);
        }

        //Start the object tracker.
        objectTracker.Start();
        TextChanges("imagedb-loaded");
    }

}
