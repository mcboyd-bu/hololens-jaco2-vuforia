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
        lastPlanePlaced = Instantiate(planePrefab, cursor.transform.position, Quaternion.Euler(0,0,0));
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
