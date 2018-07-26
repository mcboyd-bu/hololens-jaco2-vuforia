using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsLabel : MonoBehaviour {

    public static ResultsLabel instance;

    public GameObject cursor;

    public Transform labelPrefab;

    [HideInInspector]
    public Transform lastLabelPlaced;

    [HideInInspector]
    public TextMesh lastLabelPlacedText;

    public void Awake()
    {
        instance = this;
    }

    public void CreateLabel()
    {
        lastLabelPlaced = Instantiate(labelPrefab, cursor.transform.position, transform.rotation);

        lastLabelPlacedText = lastLabelPlaced.GetComponent<TextMesh>();

        //var distTest = GetComponent<Camera>().transform.position - cursor.transform.position;
        //var dt2 = Vector3.Distance(GetComponent<Camera>().transform.position, cursor.transform.position);

        //lastLabelPlacedText.text = "distTest: " + distTest.ToString() + "\r\ndt2: " + dt2.ToString();
        //lastLabelPlacedText.text = "Distance(mm): " + dt2.ToString();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (lastLabelPlaced != null)
        {
            lastLabelPlaced.transform.position = cursor.transform.position;
            lastLabelPlaced.transform.rotation = transform.rotation;

            var dt2 = Vector3.Distance(GetComponent<Camera>().transform.position, cursor.transform.position);
            dt2 = dt2 * 1000;
            int dt3 = (int)dt2;

            lastLabelPlacedText.text = "Distance(mm): " + dt3.ToString();

            if (dt3 < 1000) lastLabelPlacedText.color = Color.green;
            else if (dt3 >= 1000 && dt3 <= 3000) lastLabelPlacedText.color = Color.blue;
            else if (dt3 > 3000) lastLabelPlacedText.color = Color.red;
        }
        
    }
}
