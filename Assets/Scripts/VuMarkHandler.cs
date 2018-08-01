using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuMarkHandler : MonoBehaviour {

    private VuMarkManager vuMarkManager;
    List<VuMarkBehaviour> registeredBehaviours = new List<VuMarkBehaviour>();
    private TextMesh _sphereLabel, _sphereLabel2;
    //private bool _placed;
    private int pCount = 0;

    // Use this for initialization
    private void Awake() {
        vuMarkManager = TrackerManager.Instance.GetStateManager().GetVuMarkManager();
        //vuMarkManager.RegisterVuMarkBehaviourDetectedCallback(onVuMarkBehaviourFound);
    }

    //private void onVuMarkBehaviourFound(VuMarkBehaviour pVuMarkBehaviour)
    //{
    //    //check if we have already registered for the target assigned callbacks
    //    if (registeredBehaviours.Contains(pVuMarkBehaviour))
    //    {
    //        Debug.Log("Previously tracked VumarkBehaviour found (" + pVuMarkBehaviour.name + ")");
    //    }
    //    else
    //    {
    //        Debug.Log("Newly tracked VumarkBehaviour found (" + pVuMarkBehaviour.name + ")");
    //        Debug.Log("Registering for VuMarkTargetAssignedCallbacks from " + pVuMarkBehaviour.name);

    //        //if we hadn't registered yet, we do so now
    //        registeredBehaviours.Add(pVuMarkBehaviour);
    //        pVuMarkBehaviour.RegisterVuMarkTargetAssignedCallback(
    //            () => vumarkTargetAssigned(pVuMarkBehaviour)
    //        );
    //    }
    //}

    //private void vumarkTargetAssigned(VuMarkBehaviour pVuMarkBehaviour)
    //{
    //    Debug.Log("VuMarkTarget assigned to " + pVuMarkBehaviour.name + " with ID:" + pVuMarkBehaviour.VuMarkTarget.InstanceId.ToString());

    //    //string myID = GetID(pVuMarkBehaviour.VuMarkTarget.InstanceId);

    //    //Debug.Log("Enabling object with ID:" + myID + " ....");

    //    //var position = pVuMarkBehaviour.transform.position;
    //    //var rotation = pVuMarkBehaviour.transform.rotation;
    //    //_sphereLabel = GameObject.Find("SphereLabel2").GetComponent<TextMesh>();
    //    //_sphereLabel.text = "\r\nPos: " + position.ToString() + "\r\nRot: " + rotation.ToString();
    //    //ArmPlane.instance.CreatePlanePR(position, rotation);

    //    foreach (var bhvr in vuMarkManager.GetActiveBehaviours())
    //    {
    //        var position = bhvr.transform.position;
    //        var rotation = bhvr.transform.rotation;
    //        //hard work goes here
    //        _sphereLabel = GameObject.Find("SphereLabel2").GetComponent<TextMesh>();
    //        _sphereLabel.text = "\r\nPos: " + position.ToString() + "\r\nRot: " + rotation.ToString();
    //        ArmPlane.instance.CreatePlanePR(position, rotation);
    //    }

    //    //foreach (Transform child in pVuMarkBehaviour.transform)
    //    //{
    //    //    Debug.Log("Matching gameObject " + child.name + " with ID " + myID + " SetActive (" + (myID == child.name) + ")");
    //    //    child.gameObject.SetActive(myID == child.name);
    //    //}
    //}

    // Update is called once per frame
    void Update()
    {
        foreach (var bhvr in vuMarkManager.GetActiveBehaviours())
        {
            Vector3 position = bhvr.transform.position;
            position.y += 0.07f;
            position.z += 0.07f;
            //position = position + new Vector3(0f, 0.7f, 0.7f);
            var rotation = bhvr.transform.rotation;
            //hard work goes here
            //_sphereLabel = GameObject.Find("SphereLabel2").GetComponent<TextMesh>();
            //_sphereLabel.text = "Pos: " + position.ToString() + "\r\nRot: " + rotation.ToString();
            //_sphereLabel2 = GameObject.Find("SphereLabel3").GetComponent<TextMesh>();
            //_sphereLabel2.text += "\r\n" + bhvr.name;
            if (pCount == 0)
            {
                ArmPlane.instance.CreatePlanePR(position, rotation);
                TargetSphere.instance.CreateSphere();
                pCount += 1;
            }
            
        }
        //if (pCount == 0)
        //{

        //}

        //pCount += 1;
    }
}
