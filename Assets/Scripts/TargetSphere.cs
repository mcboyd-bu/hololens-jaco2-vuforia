using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSphere : MonoBehaviour {

    public static TargetSphere instance;

    public Transform spherePrefab;

    public Transform labelPrefab;

    public GameObject cursor;

    private Transform _arm;

    private Vector3 _relativePos;

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
        //_arm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ArmPlane>().lastPlanePlaced;
        _arm = GameObject.Find("ArmPlane").transform;
        Vector3 armV = _arm.position;
        Quaternion sphereRotation2 = _arm.rotation;
        // string displayText;
        for (int i = 0; i < 6; i++)
        {
            lastSpherePlaced = Instantiate(spherePrefab, armV + spherePositions[i], sphereRotation2);
            //lastSpherePlaced = Instantiate(spherePrefab, spherePositions[i], Quaternion.identity);
            //lastSpherePlaced.transform.SetParent(_arm, false);

            //GameObject lSP = Instantiate(spherePrefab);
            //lSP.transform.parent = _arm;
            //lSP.transform.localPosition = spherePositions[i];

            //lastSpherePlaced = lSP.transform;

            _relativePos = _arm.InverseTransformPoint(lastSpherePlaced.position);
            _relativePos = _relativePos * 0.007f;

            string tagS = "Sphere" + i.ToString();
            string tagSL = "SphereLabel" + i.ToString();
            lastSpherePlaced.name = tagS;
            lastSphereLabelPlaced = Instantiate(labelPrefab, lastSpherePlaced.transform.position + (new Vector3(0.01f, 0.01f, 0.01f)), transform.rotation);
            lastSphereLabelPlaced.name = tagSL;
            lastSphereLabelPlacedText = lastSphereLabelPlaced.GetComponent<TextMesh>();
            vectPtoS = lastSpherePlaced.transform.position - armV;
            lastSpherePlaced.GetComponent<SphereXYZ>().X = _relativePos.x; // vectPtoS.x;
            lastSpherePlaced.GetComponent<SphereXYZ>().Y = _relativePos.y; // vectPtoS.y;
            lastSpherePlaced.GetComponent<SphereXYZ>().Z = _relativePos.z; // vectPtoS.z;
            // displayText = "Arm Pos: " + armV.ToString() + "\r\nSphere Pos: " + lastSpherePlaced.transform.position.ToString() + "\n\r" + vectPtoS.ToString();
            // lastSphereLabelPlacedText.text = displayText;
            //Renderer rend = lastSpherePlaced.GetComponent<Renderer>();
            //string test = rend.material.color.ToString();
            //string test2 = "\n\rColor: " + test;
            lastSphereLabelPlacedText.text = vectPtoS.ToString() + "\r\nRel: " + _relativePos.ToString(); // + test2; // + "\n\r" + lastSpherePlaced.name.ToString() + "\n\r" + lastSphereLabelPlaced.name.ToString();
            
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
