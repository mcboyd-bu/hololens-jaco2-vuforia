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
            if (pCount == 0)
            {
                TextChanges();
                pCount += 1;

                //ArmPlane.instance.CreatePlanePR(pos2, rot3);
                TargetSphere.instance.CreateSphere();
            }

        }
    }

    public void TextChanges()
    {
        GameObject _LookingArm = GameObject.Find("LookingArm");
        GameObject _HHPlane = GameObject.Find("HHPlane");
        _HelpHolder = GameObject.Find("HelpHolder");
        Material m_Material = _HHPlane.GetComponent<Renderer>().material;
        m_Material.color = new Color(0.0118f, 0.788f, 0.114f, 1);
        TextMesh _tm = _LookingArm.GetComponent<TextMesh>();
        _tm.text = "Arm Found!";
        Invoke("CloseText", 5);
    }

    void CloseText()
    {
        _HelpHolder.SetActive(false);
    }

}
