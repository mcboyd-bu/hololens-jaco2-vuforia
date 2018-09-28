using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPlane : MonoBehaviour {

    public static ArmPlane instance;

    public Transform planePrefab;

    public GameObject cursor;

    [HideInInspector]
    public Transform lastPlanePlaced;

    public void Awake()
    {
        instance = this;
    }

    public void CreatePlane()
    {
        // Not using this...
        lastPlanePlaced = Instantiate(planePrefab, cursor.transform.position, Quaternion.Euler(0,0,0));
    }

    public void CreatePlanePR(Vector3 position, Quaternion rotation)
    {
        // Or this...
        lastPlanePlaced = Instantiate(planePrefab, position, rotation);
        GameObject mrc = GameObject.Find("MixedRealityCameraParent");
        lastPlanePlaced.transform.SetParent(mrc.transform);
        lastPlanePlaced.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
