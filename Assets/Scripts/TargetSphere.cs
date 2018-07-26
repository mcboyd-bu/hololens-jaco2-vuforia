using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSphere : MonoBehaviour {

    public static TargetSphere instance;

    public Transform spherePrefab;

    public Transform labelPrefab;

    public GameObject cursor;

    private Transform _arm;

    [HideInInspector]
    public Transform lastSpherePlaced;

    [HideInInspector]
    public Transform lastSphereLabelPlaced;

    [HideInInspector]
    public TextMesh lastSphereLabelPlacedText;

    public void Awake()
    {
        instance = this;
    }

    public void CreateSphere()
    {
        Vector3[] spherePositions = new[] { new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0.2f, 0.3f, 0.1f), new Vector3(0.1f, 0.2f, 0.6f), new Vector3(-0.2f, 0.5f, 0.2f), new Vector3(0.4f, 0.2f, 0.0f), new Vector3(0.1f, 0.6f, -0.2f) };
        Quaternion sphereRotation = Quaternion.identity;
        Vector3 vectPtoS;
        _arm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ArmPlane>().lastPlanePlaced;
        Vector3 armV = _arm.position;
        // string displayText;
        for (int i = 0; i < 6; i++)
        {
            lastSpherePlaced = Instantiate(spherePrefab, armV + spherePositions[i], sphereRotation);
            string tagS = "Sphere" + i.ToString();
            string tagSL = "SphereLabel" + i.ToString();
            lastSpherePlaced.name = tagS;
            lastSphereLabelPlaced = Instantiate(labelPrefab, lastSpherePlaced.transform.position + (new Vector3(0.01f, 0.01f, 0.01f)), transform.rotation);
            lastSphereLabelPlaced.name = tagSL;
            lastSphereLabelPlacedText = lastSphereLabelPlaced.GetComponent<TextMesh>();
            vectPtoS = lastSpherePlaced.transform.position - armV;
            lastSpherePlaced.GetComponent<SphereXYZ>().X = vectPtoS.x;
            lastSpherePlaced.GetComponent<SphereXYZ>().Y = vectPtoS.y;
            lastSpherePlaced.GetComponent<SphereXYZ>().Z = vectPtoS.z;
            // displayText = "Arm Pos: " + armV.ToString() + "\r\nSphere Pos: " + lastSpherePlaced.transform.position.ToString() + "\n\r" + vectPtoS.ToString();
            // lastSphereLabelPlacedText.text = displayText;
            //Renderer rend = lastSpherePlaced.GetComponent<Renderer>();
            //string test = rend.material.color.ToString();
            //string test2 = "\n\rColor: " + test;
            lastSphereLabelPlacedText.text = vectPtoS.ToString(); // + test2; // + "\n\r" + lastSpherePlaced.name.ToString() + "\n\r" + lastSphereLabelPlaced.name.ToString();
            
        }



        //lastSpherePlaced = Instantiate(spherePrefab, cursor.transform.position + sPos, Quaternion.Euler(new Vector3(0, 0, 0)));
        //lastSphereLabelPlaced = Instantiate(labelPrefab, lastSpherePlaced.transform.position + (new Vector3(0.01f, 0.01f, 0.01f)), transform.rotation);
        //lastSphereLabelPlacedText = lastSphereLabelPlaced.GetComponent<TextMesh>();
        //_arm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ArmPlane>().lastPlanePlaced;
        //Vector3 vectPtoS = _arm.position + lastSpherePlaced.transform.position;
        //string displayText = "Arm Pos: " + _arm.position.ToString() + "\r\nSphere Pos: " + lastSpherePlaced.transform.position.ToString() + "\n\r" + vectPtoS.ToString();
        //lastSphereLabelPlacedText.text = displayText; // "Pos: " + lastSphereLabelPlaced.transform.position.ToString(); // (new Vector3(-0.5f, 0.5f, 0.5f)).ToString();
        //lastSphereLabelPlacedText.text = vectPtoS.ToString();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
