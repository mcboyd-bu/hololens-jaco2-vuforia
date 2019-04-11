/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using HoloToolkit.Unity.InputModule;
using UnityEngine;
using Vuforia;

/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    private bool _tracked;

    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        //Debug.Log("1. virtual void start");
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        //Debug.Log("2. virtual void ondestroy");
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        //Debug.Log("3. public void OnTrackableStateChanged");
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            //Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound(mTrackableBehaviour.TrackableName);
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
           // Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost, heading to OnTrackingLost");
           OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound(string foundObj="unk")
    {
        //Debug.Log("4. virtual void OnTrackingFound");
        //var rendererComponents = GetComponentsInChildren<Renderer>(true);
        //var colliderComponents = GetComponentsInChildren<Collider>(true);
        //var canvasComponents = GetComponentsInChildren<Canvas>(true);

        //// Enable rendering:
        //foreach (var component in rendererComponents)
        //    component.enabled = true;

        //// Enable colliders:
        //foreach (var component in colliderComponents)
        //    component.enabled = true;

        //// Enable canvas':
        //foreach (var component in canvasComponents)
        //    component.enabled = true;

        if (!_tracked)
        {
            _tracked = true;
            StartCoroutine(SendMsgs.instance.SendMsg("In !tracked based on object: " + foundObj));
                      
            // Now swap data sets
            //SwapDatasets();

            //Debug.Log("4. virtual void OnTrackingFound");
            var rendererComponents = GetComponentsInChildren<Renderer>(true);
            var colliderComponents = GetComponentsInChildren<Collider>(true);
            var canvasComponents = GetComponentsInChildren<Canvas>(true);

            // Enable rendering:
            foreach (var component in rendererComponents)
                component.enabled = true;

            // Enable colliders:
            foreach (var component in colliderComponents)
                component.enabled = true;

            // Enable canvas':
            foreach (var component in canvasComponents)
                component.enabled = true;

            if (foundObj == "Box-MarysCrackers")
            {
                StartCoroutine(SendMsgs.instance.SendMsg("In !tracked, in first if;"));
                int prob = GetComponentInChildren<CubeProps>().Probability;
                StartCoroutine(SendMsgs.instance.SendMsg("In !tracked, in first if; prob = " + prob));
                if (prob < 0)
                {
                    StartCoroutine(SendMsgs.instance.SendMsg("In !tracked, in second if"));
                    StartCoroutine(SendProbability.instance.GetProbability("cube", returnValue =>
                    {
                        prob = returnValue;
                        StartCoroutine(SendMsgs.instance.SendMsg("In !tracked, in second if; returned prob = " + returnValue));
                        GetComponentInChildren<CubeProps>().Probability = returnValue;
                    }));
                }

            }

        }
        else
        {
            
        }
        
    }


    protected virtual void OnTrackingLost()
    {
        //Debug.Log("5. virtual void OnTrackingLost");
        //var rendererComponents = GetComponentsInChildren<Renderer>(true);
        //var colliderComponents = GetComponentsInChildren<Collider>(true);
        //var canvasComponents = GetComponentsInChildren<Canvas>(true);

        //// Disable rendering:
        //foreach (var component in rendererComponents)
        //    component.enabled = false;

        //// Disable colliders:
        //foreach (var component in colliderComponents)
        //    component.enabled = false;

        //// Disable canvas':
        //foreach (var component in canvasComponents)
        //    component.enabled = false;

    }

    #endregion // PROTECTED_METHODS

}
