using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ImageTargetScript : MonoBehaviour,ITrackableEventHandler {

    TrackableBehaviour trackableBehaviour;

	// Use this for initialization
	void Start () {
        trackableBehaviour = GetComponent<TrackableBehaviour>();
        if(trackableBehaviour)
        {
            trackableBehaviour.RegisterTrackableEventHandler(this);
        }
	}

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            StartCoroutine(SendMsgs.instance.SendMsg("Trackable " + trackableBehaviour.TrackableName + " found and status = " + newStatus + ", oldstatus = " + previousStatus));
        }
        else
        {
            StartCoroutine(SendMsgs.instance.SendMsg("Trackable " + trackableBehaviour.TrackableName + " found and newstatus = " + newStatus + ", oldstatus = " + previousStatus + " (else)"));
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
