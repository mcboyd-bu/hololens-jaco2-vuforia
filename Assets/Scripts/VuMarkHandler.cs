﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuMarkHandler : MonoBehaviour {

    private VuMarkManager vuMarkManager;
    private TextMesh _sphereLabel, _sphereLabel2;
    private int pCount = 0;

    // Use this for initialization
    private void Awake() {
        vuMarkManager = TrackerManager.Instance.GetStateManager().GetVuMarkManager();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var bhvr in vuMarkManager.GetActiveBehaviours())
        {
            //Vector3 position = bhvr.transform.position;
            //position.y += 0.07f;
            //position.z += 0.07f;
            //var rotation = bhvr.transform.rotation;
            //_sphereLabel = GameObject.Find("SphereLabel2").GetComponent<TextMesh>();
            //_sphereLabel.text = "Pos: " + position.ToString() + "\r\nRot: " + rotation.ToString();
            //_sphereLabel2 = GameObject.Find("SphereLabel3").GetComponent<TextMesh>();
            //_sphereLabel2.text += "\r\n" + bhvr.name;
            if (pCount == 0)
            {
                //GameObject _textLoading = GameObject.Find("LoadingArm");
                //_textLoading.SetActive(false);
                //GameObject _textLoadingSub = GameObject.Find("LoadingArmSubtext");
                //TextMesh _subMesh = _textLoadingSub.GetComponent<TextMesh>();
                //_subMesh.text = bhvr.transform.position.ToString() + "\r\n" + bhvr.transform.rotation.ToString();
                //_textLoadingSub.SetActive(false);
                //GameObject _textFound = GameObject.Find("ArmFound");
                //_textFound.SetActive(true);
                //GameObject _markText = GameObject.Find("MarkText");
                //Vector3 pos2 = _markText.transform.position;
                //pos2.y += 0.07f;
                //pos2.z += 0.07f;
                //var rot2 = _markText.transform.rotation;
                //Vector3 relativePos = _markText.transform.forward;
                //Quaternion rot3 = Quaternion.LookRotation(relativePos);

                //ArmPlane.instance.CreatePlanePR(pos2, rot3);
                TargetSphere.instance.CreateSphere();
                pCount += 1;
                //_sphereLabel = GameObject.Find("SphereLabel1").GetComponent<TextMesh>();
                //_sphereLabel.text += "\r\nMT pos: " + _markText.transform.position.ToString() + "\r\nMT forw: " + relativePos.ToString();
                //_sphereLabel2 = GameObject.Find("SphereLabel2").GetComponent<TextMesh>();
                //_sphereLabel2.text += "\r\nBHVR pos: " + bhvr.transform.position.ToString() + "\r\nBHVR rot (E): " + bhvr.transform.rotation.eulerAngles.ToString();
            }

        }
    }
}
